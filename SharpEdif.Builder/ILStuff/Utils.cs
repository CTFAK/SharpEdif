using dnlib.DotNet;

namespace SharpEdif.Builder.ILStuff;

public class Utils
{
    public static int GetExpressionReturnFromType(string type)
    {
        switch (type)
        {
            case "System.Int32":
                return 0; 
            case "System.String":
                return 1;
            case "System.Single":
                return 2;
            default:
                return 0;
        }
        
    }
    public static int GetExpressionParamFromType(string type)
    {
        switch (type)
        {
           
            case "System.Int32":
                return 1; 
            case "System.Single":
                return 2;
            case "System.String":
                return 3;
            default:
                return 1;
        }
        
    }
    public static int GetParamFromType(string type)
    {
        switch (type)
        {
            case "SharpEdif.Parameters.Expression":
            case "System.Single":
            case "System.Int32":
                return 22; //Expression
            case "System.String":
                return 45; //ExpString
            default:
                return 22;
        }
        
    }
    public static int GetParamForType(TypeDef type)
    {
        return 0;
    }
}