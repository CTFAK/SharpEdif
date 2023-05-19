using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace SharpEdif.Builder
{
    
    public unsafe class ACEInfo
    {
        public int Code;
        public string MenuName;
        public string _editorName;
        public ParamType[] Parameters = new ParamType[] { };

        public string EditorName
        {
            set => _editorName = value;
            get
            {
                if (string.IsNullOrEmpty(_editorName))
                {
                    return MenuName;
                }
                else return _editorName;
            }
        }

        public infosEventsV2* EventInfo;

        public virtual void Build()
        {
            int size = 6 /*code, flags and number of parameters*/+Parameters.Length*4/*2 for type, 2 for name i think*/;
            EventInfo = (infosEventsV2*)Marshal.AllocHGlobal(size).ToPointer();
            var shortPtr = (short*)EventInfo;
            EventInfo->code = (short)Code;
            EventInfo->flags = 0x20;
            EventInfo->nParams = (short)Parameters.Length;
            
            for (int i = 0; i < Parameters.Length; i++)
            {
                shortPtr[3 + i] = (short)Parameters[i]; 
                shortPtr[Parameters.Length+ 3 + i] = 0; 
            }

        }


    }
    public class ConditionInfo:ACEInfo
    {
        public ConditionCallback Callback;
    }
    public class ActionInfo:ACEInfo
    {
        public ActionCallback Callback;

    }
    public class ExpressionInfo:ACEInfo
    {
        public ExpressionCallback Callback;
        public ExpParamType[] ExpParameters;
        public ExpReturnType ReturnType;
        public override unsafe void Build()
        {
            int size = 6 /*code, flags and number of parameters*/+Parameters.Length*4/*2 for type, 2 for name i think*/;
            EventInfo = (infosEventsV2*)Marshal.AllocHGlobal(size).ToPointer();
            var shortPtr = (short*)EventInfo;
            EventInfo->code = (short)Code;
            //EXTFLAG_LONG - 0
            //EXTFLAG_STRING - 1
            //EXTFLAG_DOUBLE - 2
            EventInfo->flags =(short)ReturnType;
            EventInfo->nParams = (short)ExpParameters.Length;
            
            for (int i = 0; i < ExpParameters.Length; i++)
            {
                shortPtr[3 + i] = (short)ExpParameters[i]; 
                shortPtr[ExpParameters.Length+ 3 + i] = 0; 
            }
        }
    }

    

}

