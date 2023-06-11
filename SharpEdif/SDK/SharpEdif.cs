/////WARNING: DON'T MODIFY THIS UNLESS YOU KNOW EXACTLY WHAT YOU'RE DOING/////

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using SharpEdif.User;

namespace SharpEdif
{
    public class DllExportAttribute:Attribute
    {
        
        public string ExportName;
        public CallingConvention Convention;

        public DllExportAttribute(string exportName, CallingConvention convention=CallingConvention.StdCall)
        {
            ExportName = exportName;
            Convention = convention;
        }
    }
    public unsafe delegate int ConditionCallback(LPRDATA* rdPtr, int param1, int param2);

    public unsafe delegate short ActionCallback(LPRDATA* rdPtr, int param1, int param2);

    public unsafe delegate int ExpressionCallback(LPRDATA* rdPtr, int param1);

    public class ACEAttribute : Attribute
    {
      
        public ACEAttribute(string menuName, string editorName)
        {
           
        }
        public ACEAttribute(string menuName, string editorName,string[] parameterNames)
        {
            
        }

    }
    public class ConditionAttribute : ACEAttribute
    {
        public ConditionAttribute(string menuName, string editorName) : base(menuName, editorName)
        {
        }
        public ConditionAttribute(string menuName, string editorName,string[] parameterNames) : base(menuName, editorName, parameterNames)
        {
        }
    }
    public class ActionAttribute : ACEAttribute
    {
        public ActionAttribute(string menuName, string editorName) : base(menuName, editorName)
        {
        }
        public ActionAttribute(string menuName, string editorName,string[] parameterNames) : base(menuName, editorName, parameterNames)
        {
        }
    }
    public class ExpressionAttribute : ACEAttribute
    {
        public ExpressionAttribute(string menuName, string editorName) : base(menuName, editorName)
        {
        }
    }

    public class FusionProp
    {
        public int Id;
        public string Name;
        public string Info;
        public PropType Type;
        public int Options;
        public int CreateParam;

        public static FusionProp CreateStatic(string name, string info)
        {
            return new FusionProp()
            {
                Name = name,
                Info = info,
                Type = PropType.Static
            };
        }
        public static FusionProp CreateEditString(string name, string info)
        {
            return new FusionProp()
            {
                Name = name,
                Info = info,
                Type = PropType.EditString
            };
        }
    }

    public unsafe class FusionProperties
    {
        public List<FusionProp> Items = new List<FusionProp>();

        public bool propsFilled;
        public int* data=(int*)0;

        public void InvalidateData()
        {
            data = (int*)0;
        }
        public int* ObtainData()
        {
            if (data == (int*)0)
            {
                data = (int*)Marshal.AllocHGlobal(6*4*(Items.Count+1)).ToPointer();
                
                for (int i = 0; i < Items.Count; i++)
                {
                    var item = Items[i];
                    data[6*i+0] = item.Id= i+1;
                    data[6*i+1] = (int)Marshal.StringToHGlobalAnsi(item.Name).ToPointer();
                    data[6*i+2] = (int)Marshal.StringToHGlobalAnsi(item.Info).ToPointer();
                    data[6*i+3] = (int)item.Type;
                    data[6*i+4] = item.Options;
                    data[6*i+5] = (int)Marshal.StringToHGlobalAnsi("Default value").ToPointer();;
                    
                }
                data[Items.Count*6] = 0;
                data[Items.Count*6+1] = 0;
                
            }

            return data;
        }
    }
    public static class SharpEdif
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();
        public static void LoadACEs()
        {
            Console.WriteLine("Stub");
            
        }
    }
    
}