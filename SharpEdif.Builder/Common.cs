namespace SharpEdif.Builder;

public class ACEInfo
{
    public int Code;
    public string MenuName;
    public string EditorName;

}
public class ConditionInfo:ACEInfo
{
}
public class ActionInfo:ACEInfo
{
}
public class ExpressionInfo:ACEInfo
{
}

public class ParamInfo
{
    public string Text;
}
