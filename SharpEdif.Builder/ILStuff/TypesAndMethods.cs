using dnlib.DotNet;

namespace SharpEdif.Builder.ILStuff;

public class TypesAndMethods
{
    public static TypeDef sdk;

    //Conditions and actions
    
    public static MethodDef cncGetParam;
    public static MethodDef cncGetParamInt;
    public static MethodDef cncGetParamFloat;
    public static MethodDef cncGetParamString;
    
    //Expressions
    public static MethodDef returnFloat;
    public static MethodDef returnString;
    
    public static MethodDef cncGetExprFirstInt;
    public static MethodDef cncGetExprNextInt;

    public static MethodDef cncGetExprFirstFloat;
    public static MethodDef cncGetExprNextFloat;

    public static MethodDef cncGetExprFirstString;
    public static MethodDef cncGetExprNextString;
    
    public static MethodDef toInt;
    public static MethodDef getStringPtr;

    public static CorLibTypeSig voidType;
    public static CorLibTypeSig intType;
    public static CorLibTypeSig shortType;
    public static CorLibTypeSig floatType;
    public static CorLibTypeSig stringType;
    public static PtrSig lprdata;
}