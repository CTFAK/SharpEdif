using dnlib.DotNet;
using dnlib.DotNet.Emit;
using static SharpEdif.Builder.ILStuff.TypesAndMethods;
namespace SharpEdif.Builder.ILStuff;

public static class ExpressionGenerator
{
    public static MethodDef MakeExpression(MethodDef met,ModuleDef mod)
    {
        Console.WriteLine($"Generating wrapper for expression \"{met.Name}\"");

        var wrapper = new MethodDefUser($"{met.Name}_wrapper",
            MethodSig.CreateStatic(intType, lprdata, intType),
            MethodImplAttributes.IL | MethodImplAttributes.Managed,
            MethodAttributes.Public | MethodAttributes.Static);
        wrapper.DeclaringType = met.DeclaringType;
        wrapper.Body = new CilBody();
        wrapper.Body.KeepOldMaxStack = true;
        var insts = wrapper.Body.Instructions;
        
        int currentIndex = 0;
        Local[] paramLocals = new Local[met.Parameters.Count];
        bool first = true;
        foreach (var param in met.Parameters)
        {
            if (param.Type == intType)
            {
                var local = new Local(intType);
                insts.Add(new Instruction(OpCodes.Ldarg_0));
                insts.Add(new Instruction(OpCodes.Ldarg_1));
                if (first)
                    insts.Add(new Instruction(OpCodes.Call, cncGetExprFirstInt));
                else 
                    insts.Add(new Instruction(OpCodes.Call, cncGetExprNextInt));

                insts.Add(new Instruction(OpCodes.Stloc, local));
                paramLocals[currentIndex] = local;
                first = false;
            }

            if (param.Type == floatType)
            {
                var local = new Local(floatType);
                insts.Add(new Instruction(OpCodes.Ldarg_0));
                insts.Add(new Instruction(OpCodes.Ldarg_1));
                if (first)
                    insts.Add(new Instruction(OpCodes.Call, cncGetExprFirstFloat));
                else 
                    insts.Add(new Instruction(OpCodes.Call, cncGetExprNextFloat));
                insts.Add(new Instruction(OpCodes.Stloc, local));
                paramLocals[currentIndex] = local;
                first = false;
            }

            if (param.Type == stringType)
            {
                var local = new Local(stringType);
                insts.Add(new Instruction(OpCodes.Ldarg_0));
                insts.Add(new Instruction(OpCodes.Ldarg_1));
                if (first)
                    insts.Add(new Instruction(OpCodes.Call, cncGetExprFirstString));
                else 
                    insts.Add(new Instruction(OpCodes.Call, cncGetExprNextString));
                insts.Add(new Instruction(OpCodes.Stloc, local));
                paramLocals[currentIndex] = local;
                first = false;
            }

            
            currentIndex++;
        }

        if (met.ReturnType == floatType)
        {
            insts.Add(new Instruction(OpCodes.Ldarg_0));
            insts.Add(new Instruction(OpCodes.Call, returnFloat));

        }
        else if (met.ReturnType == stringType)
        {
            insts.Add(new Instruction(OpCodes.Ldarg_0));
            insts.Add(new Instruction(OpCodes.Call, returnString));

        }
        
        var index2 = 0;
        for (int j = 0; j < currentIndex; j++)
        {

            if (index2 == 0 && paramLocals[0] == null)
            {
                insts.Add(new Instruction(OpCodes.Ldarg_0));

            }
            else
            {
                var local = paramLocals[index2];
                wrapper.Body.Variables.Add(local);
                insts.Add(new Instruction(OpCodes.Ldloc, local));
            }

            index2++;
        }

        

        
        insts.Add(new Instruction(OpCodes.Call, met));
        if (met.ReturnType == floatType)
        {
            insts.Add(new Instruction(OpCodes.Call, toInt));

        }
        else if (met.ReturnType == stringType)
        {
            insts.Add(new Instruction(OpCodes.Call, getStringPtr));

        }
        //insts.Add(new Instruction(OpCodes.Ldc_I4_0));
        insts.Add(new Instruction(OpCodes.Ret));
        return wrapper;
    }
    public static MethodDef MakeExpressionGetter(MethodDef met,ModuleDef mod)
    {
        var infosField = new FieldDefUser($"{met.Name}_infos",new FieldSig(new PtrSig(shortType)),FieldAttributes.Public| FieldAttributes.Static);
        infosField.IsStatic = true;
        infosField.DeclaringType = met.DeclaringType;
        var wrapper = new MethodDefUser($"{met.Name}_makeInfos",
            MethodSig.CreateStatic(voidType),
            MethodImplAttributes.IL | MethodImplAttributes.Managed,
            MethodAttributes.Public | MethodAttributes.Static);
        wrapper.DeclaringType = met.DeclaringType;
        wrapper.Body = new CilBody();
        wrapper.Body.KeepOldMaxStack = true;
        var insts = wrapper.Body.Instructions;
        int sizeToAllocate = 6;
        List<string> parameterTypes = new List<string>();
        foreach (var param in met.Parameters)
        {
            if (param.Type.FullName != lprdata.FullName)
            {
                sizeToAllocate += 4;
                parameterTypes.Add(param.Type.FullName);
            }
        }
        
        var allochGlobal = sdk.FindMethod("AllocBytes");
        var firstPointer = new Local(new PtrSig(shortType));
        wrapper.Body.Variables.Add(firstPointer);
        
        insts.Add(new Instruction(OpCodes.Ldc_I4,sizeToAllocate));
        insts.Add(new Instruction(OpCodes.Call,allochGlobal));
        insts.Add(new Instruction(OpCodes.Stsfld,infosField));
        insts.Add(new Instruction(OpCodes.Ldsfld,infosField));

        insts.putElementInArrayShort(0,Program.currentExpressionCode);
        insts.putElementInArrayShort(1,Utils.GetExpressionReturnFromType(met.ReturnType.FullName));
        insts.putElementInArrayShort(2,parameterTypes.Count,parameterTypes.Count==0);
        for (int i = 0; i < parameterTypes.Count; i++)
        {
            insts.putElementInArrayShort(3+i,Utils.GetExpressionParamFromType(parameterTypes[i]));
            insts.putElementInArrayShort(parameterTypes.Count+3+i,0,i==parameterTypes.Count-1);
        }
        
        insts.Add(new Instruction(OpCodes.Ret));
        return wrapper;
    }
}