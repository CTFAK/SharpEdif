using System.Globalization;
using System.IO.Enumeration;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using CTFAK.Memory;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Writer;
using dnlib.PE;
using SharpEdif.Builder;
using Ressy;
using SharpEdif.Builder.ILStuff;
using FieldAttributes = dnlib.DotNet.FieldAttributes;
using MethodAttributes = dnlib.DotNet.MethodAttributes;
using MethodImplAttributes = dnlib.DotNet.MethodImplAttributes;
using OpCodes = dnlib.DotNet.Emit.OpCodes;
using ResourceType = Ressy.ResourceType;
using static SharpEdif.Builder.ILStuff.TypesAndMethods;
using CallingConvention = dnlib.DotNet.CallingConvention;


namespace SharpEdif.Builder;

public static class Program
{

    public static TypeDef FindType(ModuleDefMD mod, string fullName)
    {

        foreach (var type in mod.Types)
        {
            if (type.FullName == fullName) return type;
        }

        return null;
    }

    public static MethodDef FindMethod(TypeDef type, string fullName)
    {
        foreach (var met in type.Methods)
        {
            if (met.Name == fullName) return met;
        }

        return null;
    }

    
    public static void putElementInArrayShort(this IList<Instruction>? insts,int index, int value,bool last=false)
    {
        if (index == 0)
        {
            if(!last)
                insts.Add(new Instruction(OpCodes.Dup));
            insts.Add(new Instruction(OpCodes.Ldc_I4,value));
            insts.Add(new Instruction(OpCodes.Stind_I2));
        }
        else if (index == 1)
        {
            if(!last)
                insts.Add(new Instruction(OpCodes.Dup));
            insts.Add(new Instruction(OpCodes.Ldc_I4,2));
            insts.Add(new Instruction(OpCodes.Add));

            insts.Add(new Instruction(OpCodes.Ldc_I4,value));
            insts.Add(new Instruction(OpCodes.Stind_I2));
        }
        else
        {
            if(!last)
                insts.Add(new Instruction(OpCodes.Dup));
            insts.Add(new Instruction(OpCodes.Ldc_I4,index));
            insts.Add(new Instruction(OpCodes.Conv_I));
            insts.Add(new Instruction(OpCodes.Ldc_I4_2));
            insts.Add(new Instruction(OpCodes.Mul));
            insts.Add(new Instruction(OpCodes.Add));
            insts.Add(new Instruction(OpCodes.Ldc_I4,value));
            insts.Add(new Instruction(OpCodes.Stind_I2));
        }
        
    }


    
    

    public static int currentConditionCode;
    public static int currentActioncode;
    public static int currentExpressionCode;


    public static void PreprocessAPI(ModuleDefMD mod)
    {
        mod.Cor20HeaderFlags &= ~ ComImageFlags.ILOnly;
        ushort currentOrdinal = 2;
        var exportAttr = FindType(mod, "SharpEdif.DllExportAttribute");
        foreach (var type in mod.Types)
        {
            foreach (var met in type.Methods)
            {
                //if(met.Name=="GetConditionTitle")
                if (met.CustomAttributes.Any(a => a.AttributeType == exportAttr))
                {
                    
                    met.CustomAttributes.Clear();

                    Console.WriteLine($"Found exported method: {met.Name} - {(int)met.Signature.GetCallingConvention()}");
                    var typeRef = met.Module.CorLibTypes.GetTypeRef("System.Runtime.CompilerServices", "CallConvStdcall");
                    var callConvStdcall = new CModOptSig(typeRef, met.ReturnType);
                    
                    var parameters = met.GetParams();

                    var methodSig = new MethodSig(CallingConvention.Default,(uint)parameters.Count,callConvStdcall,parameters);
                    met.Signature = methodSig;
                    //met.IsUnmanaged = false;
                    
                    //exportMethod.IsStatic = true;
                   
                    //met.CustomAttributes.Add(new CustomAttribute());
                    met.ExportInfo = new MethodExportInfo(met.Name,currentOrdinal++,MethodExportInfoOptions.FromUnmanagedRetainAppDomain);
                    //met.IsUnmanagedExport = true;
                    //met.RVA = 0;
                    //module.GlobalType.Methods.Add(exportMethod);
                }
            }   
        }
        
    }

    
    public static void Recompile(string path)
    {

        if(File.Exists(path+"temp"))
            File.Delete(path+"temp");
        string outPath = path.Replace(".dll", ".mfxtemp");
        List<ConditionInfo> conditions = new List<ConditionInfo>();
        List<ActionInfo> actions = new List<ActionInfo>();
        List<ExpressionInfo> expressions = new List<ExpressionInfo>();
        List<ParamInfo> parameters = new List<ParamInfo>();
        
        
        var mod = ModuleDefMD.Load(path);
        
        var condAttr = mod.Types.First(a => a.FullName == "SharpEdif.ConditionAttribute");
        var actAttr = mod.Types.First(a => a.FullName == "SharpEdif.ActionAttribute");
        var expAttr = mod.Types.First(a => a.FullName == "SharpEdif.ExpressionAttribute");
        sdk = FindType(mod, "SharpEdif.SDK");

        string extName = sdk.GetField("extName").Constant.Value.ToString();
        string extAuthor = sdk.GetField("extAuthor").Constant.Value.ToString();
        string extCopyright = sdk.GetField("extCopyright").Constant.Value.ToString();
        string extComment = sdk.GetField("extComment").Constant.Value.ToString();
        string extHttp = sdk.GetField("extHttp").Constant.Value.ToString();
        voidType = mod.CorLibTypes.Void;
        intType = mod.CorLibTypes.Int32;
        shortType = mod.CorLibTypes.Int16;
        floatType = mod.CorLibTypes.Single;
        stringType = mod.CorLibTypes.String;
        lprdata = new PtrSig(FindType(mod, "SharpEdif.LPRDATA").ToTypeSig());
        cncGetParam = FindMethod(sdk, "CNC_GetParameter");
        cncGetParamInt = FindMethod(sdk, "CNC_GetIntParameter");
        cncGetParamFloat = FindMethod(sdk, "CNC_GetFloatParameter");
        cncGetParamString = FindMethod(sdk, "CNC_GetStringParameter");
        returnFloat = FindMethod(sdk, "ReturnFloat");
        returnString = FindMethod(sdk, "ReturnString");
        
        cncGetExprFirstInt = FindMethod(sdk, "CNC_GetFirstExpressionParameterInt");
        cncGetExprNextInt = FindMethod(sdk, "CNC_GetNextExpressionParameterInt");
        
        cncGetExprFirstFloat = FindMethod(sdk, "CNC_GetFirstExpressionParameterFloat");
        cncGetExprNextFloat = FindMethod(sdk, "CNC_GetNextExpressionParameterFloat");
        
        cncGetExprFirstString = FindMethod(sdk, "CNC_GetFirstExpressionParameterString");
        cncGetExprNextString = FindMethod(sdk, "CNC_GetNextExpressionParameterString");
        
        toInt = FindMethod(sdk, "ToInt");
        getStringPtr = FindMethod(sdk, "GetStringPtr");
        var loadAces = FindMethod(FindType(mod, "SharpEdif.SharpEdif"),"LoadACEs");
        var allochGlobal = sdk.FindMethod("AllocBytes");

        var conditionInfos = sdk.FindField("conditionInfos");
        var conditionNames = sdk.FindField("conditionNames");
        var conditionEditorNames = sdk.FindField("conditionEditorNames");
        var conditionCallbacks = sdk.FindField("conditionCallbacks");
        
        var actionInfos = sdk.FindField("actionInfos");
        var actionNames = sdk.FindField("actionNames");
        var actionEditorNames = sdk.FindField("actionEditorNames");
        var actionCallbacks = sdk.FindField("actionCallbacks");
        
        var expressionInfos = sdk.FindField("expressionInfos");
        var expressionNames = sdk.FindField("expressionNames");
        var expressionEditorNames = sdk.FindField("expressionEditorNames");
        var expressionCallbacks = sdk.FindField("expressionCallbacks");
        loadAces.Body.KeepOldMaxStack = true;
        PreprocessAPI(mod);

        var insts = loadAces.Body.Instructions;
        insts.Clear();
        var tempInsts = new List<Instruction>();
        foreach (var type in mod.Types)
        {
            for (int i = 0; i < type.Methods.Count; i++)
            {
                var met = type.Methods[i];

                foreach (var attr in met.CustomAttributes)
                {
                    if (attr.AttributeType == condAttr)
                    {
                        var wrapper = ConditionGenerator.MakeCondition(met,mod);
                        var init = ConditionGenerator.MakeConditionGetter(met,mod);
                        tempInsts.Add(new Instruction(OpCodes.Call,init));
                        var condInfo = new ConditionInfo();
                        condInfo.MenuName=attr.ConstructorArguments[0].Value.ToString();
                        condInfo.EditorName=attr.ConstructorArguments[1].Value.ToString();
                        condInfo.Code = currentConditionCode;
                        conditions.Add(condInfo);
                        var nameField = new FieldDefUser($"{met.Name}_menuName",new FieldSig(stringType),FieldAttributes.Public| FieldAttributes.Static| FieldAttributes.Literal);
                        nameField.DeclaringType = met.DeclaringType;
                        nameField.Constant = new ConstantUser(condInfo.MenuName);
                        
                        var editorNameField = new FieldDefUser($"{met.Name}_editorName",new FieldSig(stringType),FieldAttributes.Public| FieldAttributes.Static| FieldAttributes.Literal);
                        editorNameField.DeclaringType = met.DeclaringType;
                        editorNameField.Constant = new ConstantUser(condInfo.EditorName);
                        
                        
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,conditionInfos));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4,currentConditionCode));
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,met.DeclaringType.FindField(met.Name+"_infos")));
                        tempInsts.Add(new Instruction(OpCodes.Stelem_I));
                        
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,conditionNames));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4,currentConditionCode));
                        tempInsts.Add(new Instruction(OpCodes.Ldstr,met.DeclaringType.FindField(met.Name+"_menuName").Constant.Value));
                        tempInsts.Add(new Instruction(OpCodes.Stelem_I));
                        
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,conditionEditorNames));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4,currentConditionCode));
                        tempInsts.Add(new Instruction(OpCodes.Ldstr,met.DeclaringType.FindField(met.Name+"_editorName").Constant.Value));
                        tempInsts.Add(new Instruction(OpCodes.Stelem_I));
                        
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,conditionCallbacks));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4,currentConditionCode));
                        tempInsts.Add(new Instruction(OpCodes.Conv_I));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4_4));
                        tempInsts.Add(new Instruction(OpCodes.Mul));
                        tempInsts.Add(new Instruction(OpCodes.Add));

                        //insts.Add(new Instruction(OpCodes.Ldc_I4,value));
                        tempInsts.Add(new Instruction(OpCodes.Ldnull));
                        tempInsts.Add( new Instruction(OpCodes.Ldftn,wrapper));
                        tempInsts.Add(new Instruction(OpCodes.Newobj,FindType(mod,"SharpEdif.ConditionCallback").FindMethod(".ctor")));
                        tempInsts.Add(new Instruction(OpCodes.Call,sdk.FindMethod("CreateConditionDelegate")));
                        tempInsts.Add(new Instruction(OpCodes.Stind_I4));
                        //insts.Add(new Instruction(OpCodes.Stind_I4));

                        
                        //insts.Add(new Instruction(OpCodes.Ldsfld,met.DeclaringType.FindField(met.Name+"_editorName")));
                        currentConditionCode++;
                    }
                    else if (attr.AttributeType == actAttr)
                    {
                        var wrapper = ActionGenerator.MakeAction(met,mod);
                        var init = ActionGenerator.MakeActionGetter(met, mod);
                        tempInsts.Add(new Instruction(OpCodes.Call,init));
                        var actInfo = new ActionInfo();
                        
                        actInfo.MenuName=attr.ConstructorArguments[0].Value.ToString();
                        actInfo.EditorName=attr.ConstructorArguments[1].Value.ToString();
                        actInfo.Code = currentActioncode;
                        actions.Add(actInfo);
                        var nameField = new FieldDefUser($"{met.Name}_menuName",new FieldSig(stringType),FieldAttributes.Public| FieldAttributes.Static| FieldAttributes.Literal);
                        nameField.DeclaringType = met.DeclaringType;
                        nameField.Constant = new ConstantUser(actInfo.MenuName);
                        
                        var editorNameField = new FieldDefUser($"{met.Name}_editorName",new FieldSig(stringType),FieldAttributes.Public| FieldAttributes.Static| FieldAttributes.Literal);
                        editorNameField.DeclaringType = met.DeclaringType;
                        editorNameField.Constant = new ConstantUser(actInfo.EditorName);
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,actionInfos));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4,currentActioncode));
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,met.DeclaringType.FindField(met.Name+"_infos")));
                        tempInsts.Add(new Instruction(OpCodes.Stelem_I));
                        
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,actionNames));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4,currentActioncode));
                        tempInsts.Add(new Instruction(OpCodes.Ldstr,met.DeclaringType.FindField(met.Name+"_menuName").Constant.Value));
                        tempInsts.Add(new Instruction(OpCodes.Stelem_I));
                        
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,actionEditorNames));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4,currentActioncode));
                        tempInsts.Add(new Instruction(OpCodes.Ldstr,met.DeclaringType.FindField(met.Name+"_editorName").Constant.Value));
                        tempInsts.Add(new Instruction(OpCodes.Stelem_I));
                        
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,actionCallbacks));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4,currentActioncode));
                        tempInsts.Add(new Instruction(OpCodes.Conv_I));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4_4));
                        tempInsts.Add(new Instruction(OpCodes.Mul));
                        tempInsts.Add(new Instruction(OpCodes.Add));
                        tempInsts.Add(new Instruction(OpCodes.Ldnull));
                        tempInsts.Add(new Instruction(OpCodes.Ldftn,wrapper));
                        tempInsts.Add(new Instruction(OpCodes.Newobj,FindType(mod,"SharpEdif.ActionCallback").FindMethod(".ctor")));
                        tempInsts.Add(new Instruction(OpCodes.Call,sdk.FindMethod("CreateActionDelegate")));
                        tempInsts.Add(new Instruction(OpCodes.Stind_I4));
                        currentActioncode++;
                    }
                    else if (attr.AttributeType == expAttr)
                    {
                        var wrapper = ExpressionGenerator.MakeExpression(met,mod);
                        var init = ExpressionGenerator.MakeExpressionGetter(met, mod);
                        tempInsts.Add(new Instruction(OpCodes.Call,init));
                        var expInfo = new ExpressionInfo();
                        expInfo.MenuName=attr.ConstructorArguments[0].Value.ToString();
                        expInfo.EditorName=attr.ConstructorArguments[1].Value.ToString();
                        expInfo.Code = currentExpressionCode;
                        expressions.Add(expInfo);
                        var nameField = new FieldDefUser($"{met.Name}_menuName",new FieldSig(stringType),FieldAttributes.Public| FieldAttributes.Static| FieldAttributes.Literal);
                        nameField.DeclaringType = met.DeclaringType;
                        nameField.Constant = new ConstantUser(expInfo.MenuName);
                        
                        var editorNameField = new FieldDefUser($"{met.Name}_editorName",new FieldSig(stringType),FieldAttributes.Public| FieldAttributes.Static| FieldAttributes.Literal);
                        editorNameField.DeclaringType = met.DeclaringType;
                        editorNameField.Constant = new ConstantUser(expInfo.EditorName);
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,expressionInfos));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4,currentExpressionCode));
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,met.DeclaringType.FindField(met.Name+"_infos")));
                        tempInsts.Add(new Instruction(OpCodes.Stelem_I));
                        
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,expressionNames));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4,currentExpressionCode));
                        tempInsts.Add(new Instruction(OpCodes.Ldstr,met.DeclaringType.FindField(met.Name+"_menuName").Constant.Value));
                        tempInsts.Add(new Instruction(OpCodes.Stelem_I));
                        
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,expressionEditorNames));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4,currentExpressionCode));
                        tempInsts.Add(new Instruction(OpCodes.Ldstr,met.DeclaringType.FindField(met.Name+"_editorName").Constant.Value));
                        tempInsts.Add(new Instruction(OpCodes.Stelem_I));
                        
                        tempInsts.Add(new Instruction(OpCodes.Ldsfld,expressionCallbacks));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4,currentExpressionCode));
                        tempInsts.Add(new Instruction(OpCodes.Conv_I));
                        tempInsts.Add(new Instruction(OpCodes.Ldc_I4_4));
                        tempInsts.Add(new Instruction(OpCodes.Mul));
                        tempInsts.Add(new Instruction(OpCodes.Add));
                        tempInsts.Add(new Instruction(OpCodes.Ldnull));
                        tempInsts.Add( new Instruction(OpCodes.Ldftn,wrapper));
                        tempInsts.Add(new Instruction(OpCodes.Newobj,FindType(mod,"SharpEdif.ExpressionCallback").FindMethod(".ctor")));
                        tempInsts.Add(new Instruction(OpCodes.Call,sdk.FindMethod("CreateExpressionDelegate")));
                        tempInsts.Add(new Instruction(OpCodes.Stind_I4));
                        currentExpressionCode++;
                    }
                }
            }
        }
        insts.Add(new Instruction(OpCodes.Ldc_I4,conditions.Count));
        insts.Add(new Instruction(OpCodes.Newarr,new TypeSpecUser(new PtrSig(shortType))));
        insts.Add(new Instruction(OpCodes.Stsfld,conditionInfos));
        
        insts.Add(new Instruction(OpCodes.Ldc_I4,conditions.Count));
        insts.Add(new Instruction(OpCodes.Newarr,new TypeSpecUser(stringType)));
        insts.Add(new Instruction(OpCodes.Stsfld,conditionNames));
        
        insts.Add(new Instruction(OpCodes.Ldc_I4,conditions.Count));
        insts.Add(new Instruction(OpCodes.Newarr,new TypeSpecUser(stringType)));
        insts.Add(new Instruction(OpCodes.Stsfld,conditionEditorNames));

        
        
        insts.Add(new Instruction(OpCodes.Ldc_I4,actions.Count));
        insts.Add(new Instruction(OpCodes.Newarr,new TypeSpecUser(new PtrSig(shortType))));
        insts.Add(new Instruction(OpCodes.Stsfld,actionInfos));

        insts.Add(new Instruction(OpCodes.Ldc_I4,actions.Count));
        insts.Add(new Instruction(OpCodes.Newarr,new TypeSpecUser(stringType)));
        insts.Add(new Instruction(OpCodes.Stsfld,actionNames));
        
        insts.Add(new Instruction(OpCodes.Ldc_I4,actions.Count));
        insts.Add(new Instruction(OpCodes.Newarr,new TypeSpecUser(stringType)));
        insts.Add(new Instruction(OpCodes.Stsfld,actionEditorNames));
        
        
        
        insts.Add(new Instruction(OpCodes.Ldc_I4,expressions.Count));
        insts.Add(new Instruction(OpCodes.Newarr,new TypeSpecUser(new PtrSig(shortType))));
        insts.Add(new Instruction(OpCodes.Stsfld,expressionInfos));
        
        insts.Add(new Instruction(OpCodes.Ldc_I4,expressions.Count));
        insts.Add(new Instruction(OpCodes.Newarr,new TypeSpecUser(stringType)));
        insts.Add(new Instruction(OpCodes.Stsfld,expressionNames));

        insts.Add(new Instruction(OpCodes.Ldc_I4,expressions.Count));
        insts.Add(new Instruction(OpCodes.Newarr,new TypeSpecUser(stringType)));
        insts.Add(new Instruction(OpCodes.Stsfld,expressionEditorNames));
        
        
        
        
        
        insts.Add(new Instruction(OpCodes.Ldc_I4,conditions.Count*4+4));
        insts.Add(new Instruction(OpCodes.Call,allochGlobal));
        insts.Add(new Instruction(OpCodes.Stsfld,conditionCallbacks));
        insts.Add( new Instruction(OpCodes.Ldsfld,conditionCallbacks));

        insts.Add(new Instruction(OpCodes.Ldc_I4,conditions.Count));
        insts.Add(new Instruction(OpCodes.Conv_I));
        insts.Add(new Instruction(OpCodes.Ldc_I4_4));
        insts.Add(new Instruction(OpCodes.Mul));
        insts.Add(new Instruction(OpCodes.Add));
        insts.Add(new Instruction(OpCodes.Ldc_I4,0));
        insts.Add(new Instruction(OpCodes.Stind_I4));
        
        
        
        insts.Add(new Instruction(OpCodes.Ldc_I4,actions.Count*4+4));
        insts.Add(new Instruction(OpCodes.Call,allochGlobal));
        insts.Add(new Instruction(OpCodes.Stsfld,actionCallbacks));
        insts.Add( new Instruction(OpCodes.Ldsfld,actionCallbacks));

        insts.Add(new Instruction(OpCodes.Ldc_I4,actions.Count));
        insts.Add(new Instruction(OpCodes.Conv_I));
        insts.Add(new Instruction(OpCodes.Ldc_I4_4));
        insts.Add(new Instruction(OpCodes.Mul));
        insts.Add(new Instruction(OpCodes.Add));
        insts.Add(new Instruction(OpCodes.Ldc_I4,0));
        insts.Add(new Instruction(OpCodes.Stind_I4));
        
        
        
        insts.Add(new Instruction(OpCodes.Ldc_I4,expressions.Count*4+4));
        insts.Add(new Instruction(OpCodes.Call,allochGlobal));
        insts.Add(new Instruction(OpCodes.Stsfld,expressionCallbacks));
        insts.Add( new Instruction(OpCodes.Ldsfld,expressionCallbacks));

        insts.Add(new Instruction(OpCodes.Ldc_I4,expressions.Count));
        insts.Add(new Instruction(OpCodes.Conv_I));
        insts.Add(new Instruction(OpCodes.Ldc_I4_4));
        insts.Add(new Instruction(OpCodes.Mul));
        insts.Add(new Instruction(OpCodes.Add));
        insts.Add(new Instruction(OpCodes.Ldc_I4,0));
        insts.Add(new Instruction(OpCodes.Stind_I4));





        foreach (var temp in tempInsts)
        {
            insts.Add(temp);
        }
        

        insts.Add(new Instruction(OpCodes.Ret));


              
        var wopts = new dnlib.DotNet.Writer.ModuleWriterOptions(mod);
        wopts.WritePdb = true;
        //assembly.Write(outPath,wopts);
        mod.Write(outPath);
        mod = ModuleDefMD.Load(outPath);
        outPath=outPath.Replace(".mfxtemp", ".mfx");
        mod.Write(outPath);
        
        var modFile = new PortableExecutable(outPath);
        var lang = Language.FromCultureInfo(new CultureInfo("en-us"));
        //File.WriteAllBytes("ogStrTable.bin",modFile.GetResource(new ResourceIdentifier(ResourceType.String, ResourceName.FromString("#376"))).Data);
        // Fill out basic info
        var iconReader = new ByteReader(File.ReadAllBytes(Path.Join(Path.GetDirectoryName(outPath),"Icon.bmp")));
        iconReader.Skip(14);
        var imgReader = new ByteReader(File.ReadAllBytes(Path.Join(Path.GetDirectoryName(outPath),"Icon.bmp")));
        imgReader.Skip(14);
        modFile.SetResource(new ResourceIdentifier(ResourceType.Bitmap, ResourceName.FromCode(400), lang),
            iconReader.ReadBytes());
        modFile.SetResource(new ResourceIdentifier(ResourceType.Bitmap, ResourceName.FromCode(401), lang),
            imgReader.ReadBytes());
        modFile.SetResource(new ResourceIdentifier(ResourceType.String, ResourceName.FromCode(1), lang),
            ConstructStringTable(new[] { "", "MF2", extName }));
        modFile.SetResource(new ResourceIdentifier(ResourceType.String, ResourceName.FromCode(9), lang),
            ConstructStringTable(new[] { extName, extAuthor, extCopyright, extComment, extHttp }));
        modFile.SetResource(new ResourceIdentifier(ResourceType.RawData, ResourceName.FromCode(200), lang),
            new byte[] { 0x16, 0x25, 0x8, 0x59 });

        // =============
        // ===Actions===
        // =============
        modFile.SetResource(new ResourceIdentifier(ResourceType.Menu, ResourceName.FromCode(20000), lang),
            ConstructMenu(actions.ToArray(), 25000));
        var tempActList = new List<string>();
        foreach (var act in actions)
        {
            tempActList.Add(act.MenuName);
        }

        var actStrTable = ConstructStringTable(tempActList.ToArray());
        // a dirty padding workaround
        var cList = new List<byte>();
        for (int i = 0; i < 16; i++)
        {
            cList.Add(0);
        }

        cList.AddRange(actStrTable);
        modFile.SetResource(new ResourceIdentifier(ResourceType.String, ResourceName.FromCode(313), lang),
            cList.ToArray());

        // ================
        // ===Conditions===
        // ================
        modFile.SetResource(new ResourceIdentifier(ResourceType.Menu, ResourceName.FromCode(20001), lang),
            ConstructMenu(conditions.ToArray(), 26000));
        var tempCndList = new List<string>();
        foreach (var cnd in conditions)
        {
            tempCndList.Add(cnd.MenuName);
        }

        var cndStrTable = ConstructStringTable(tempCndList.ToArray());
        modFile.SetResource(new ResourceIdentifier(ResourceType.String, ResourceName.FromCode(376), lang), cndStrTable);

        // =================
        // ===Expressions===
        // =================
        modFile.SetResource(new ResourceIdentifier(ResourceType.Menu, ResourceName.FromCode(20002), lang),
            ConstructMenu(expressions.ToArray(), 27000));
        var tempExprList = new List<string>();
        foreach (var expr in expressions)
        {
            tempExprList.Add(expr.EditorName);
        }

        var exprStrTable = ConstructStringTable(tempExprList.ToArray());

        // a dirty padding workaround
        var eList = new List<byte>();
        for (int i = 0; i < 16; i++)
        {
            eList.Add(0);
        }

        eList.AddRange(exprStrTable);
        modFile.SetResource(new ResourceIdentifier(ResourceType.String, ResourceName.FromCode(438), lang),
            eList.ToArray());

        // ================
        // ===Parameters===
        // ================
        var tempParamList = new List<string>();
        foreach (var prm in parameters)
        {
            tempParamList.Add(prm.Text);
        }

        var paramStringTable = ConstructStringTable(tempParamList.ToArray());

        // a dirty padding workaround
        var pList = new List<byte>();
        for (int i = 0; i < 10; i++)
        {
            pList.Add(0);
        }

        pList.AddRange(paramStringTable);
        modFile.SetResource(new ResourceIdentifier(ResourceType.String, ResourceName.FromCode(407), lang),
            pList.ToArray());
        
        
    }
    public static byte[] ConstructMenu(ACEInfo[] infos, short baseId)
    {
        Console.WriteLine($"Constructing menu with {infos.Length} entries");
        var writer = new ByteWriter(new MemoryStream());

        writer.WriteInt32(0);
        writer.WriteInt32(144);

        int idx = 0;
        foreach (var info in infos)
        {
            if (idx == infos.Length - 1)
            {
                writer.WriteInt16(128);
            }
            else writer.WriteInt16(0);

            writer.WriteInt16((short)(baseId + idx));
            writer.WriteUnicode(info.MenuName, true);
            idx++;
        }


        var buf = ((MemoryStream)writer.BaseStream).GetBuffer();
        Array.Resize(ref buf, (int)writer.Tell());
        return buf;
    }

    public static byte[] ConstructStringTable(string[] strs)
    {
        var writer = new ByteWriter(new MemoryStream());
        foreach (var str in strs)
        {
            writer.WriteInt16((short)str.Length);
            writer.WriteUnicode(str);
        }

        writer.Skip(26);
        

        var buf = ((MemoryStream)writer.BaseStream).GetBuffer();
        Array.Resize(ref buf, (int)writer.Tell());
        return buf;
    }

    public static string ToHexString(this byte[] data)
    {
        StringBuilder temp = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            temp.Append($"{data[i].ToString("X2")} ");

        }

        return temp.ToString();
    }

    public static void Main(string[] args)
    {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        if (args.Length > 0)
        {
            Recompile(args[0]);
        }

    }
}