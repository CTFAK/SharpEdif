using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpEdif.User;

namespace SharpEdif
{
    public static unsafe class SDK
    {
        public const string extName = Extension.ExtensionName;
        public const string extAuthor = Extension.ExtensionAuthor;
        public const string extCopyright = Extension.ExtensionCopyright;
        public const string extComment = Extension.ExtensionComment;
        public const string extHttp = Extension.ExtensionHttp;
        
        public static short*[] conditionInfos;
        public static short*[] actionInfos;
        public static short*[] expressionInfos;
        
        public static string[] conditionNames;
        public static string[] actionNames;
        public static string[] expressionNames;
        
        public static string[] conditionEditorNames;
        public static string[] actionEditorNames;
        public static string[] expressionEditorNames;

        public static int* conditionCallbacks;
        public static int* actionCallbacks;
        public static int* expressionCallbacks;

        
        public static float ToFloat(this int i)
        {
            return *(float*)&i;
        }

        public static int ToInt(this float i)
        {
            return *(int*)&i;
        }

        public static short* AllocBytes(int count)
        {
            return (short*)Marshal.AllocHGlobal(count).ToPointer();
        }


        public static int CreateConditionDelegate(ConditionCallback met)
        {
            return (int)Marshal.GetFunctionPointerForDelegate(met).ToPointer();
        }
        public static int CreateActionDelegate(ActionCallback met)
        {
            return (int)Marshal.GetFunctionPointerForDelegate(met).ToPointer();
        }
        public static int CreateExpressionDelegate(ExpressionCallback met)
        {
            return (int)Marshal.GetFunctionPointerForDelegate(met).ToPointer();
        }
        public static int GetStringPtr(this string str)
        {
            return (int)Marshal.StringToHGlobalAnsi(str).ToPointer();
        }

        public static void ReturnString(LPRDATA* rdPtr)
        {
            rdPtr->rHo.hoFlags |= 0x8000; // we return a string, let fusion know
        }
        public static void ReturnFloat(LPRDATA* rdPtr)
        {
            rdPtr->rHo.hoFlags |= 0x4000; // we return a float, let fusion know
        }
        public static float CNC_GetFloatParameter(LPRDATA* rdPtr)
        {
            return CallRuntimeFunction(rdPtr, 17, 2, 0).ToFloat();
        }
        public static string CNC_GetStringParameter(LPRDATA* rdPtr)
        {
            return Marshal.PtrToStringAnsi(new IntPtr(CallRuntimeFunction(rdPtr, 17, 0xFFFFFFFF, 0)));
        }
        public static byte* CNC_GetStringParameterPtr(LPRDATA* rdPtr)
        {
            return (byte*)CallRuntimeFunction(rdPtr, 17, 0xFFFFFFFF, 0);
        }
        public static int CNC_GetIntParameter(LPRDATA* rdPtr)
        {
            return CallRuntimeFunction(rdPtr, 17, 0, 0);
        }
        public static int CNC_GetParameter(LPRDATA* rdPtr)
        {
            return CallRuntimeFunction(rdPtr, 17, 0xFFFFFFFF, 0);
        }


        public static int CNC_GetFirstExpressionParameterInt(LPRDATA* rdPtr, int first) => CNC_GetFirstExpressionParameter(rdPtr, first, 0);
        public static int CNC_GetNextExpressionParameterInt(LPRDATA* rdPtr, int first) => CNC_GetNextExpressionParameter(rdPtr, first, 0);
        
        public static float CNC_GetFirstExpressionParameterFloat(LPRDATA* rdPtr, int first) => CNC_GetFirstExpressionParameter(rdPtr, first, 2).ToFloat();
        public static float CNC_GetNextExpressionParameterFloat(LPRDATA* rdPtr, int first) => CNC_GetNextExpressionParameter(rdPtr, first, 2).ToFloat();

        public static string CNC_GetFirstExpressionParameterString(LPRDATA* rdPtr, int first) => Marshal.PtrToStringAnsi(new IntPtr(CNC_GetFirstExpressionParameter(rdPtr, first, 1)));
        public static string CNC_GetNextExpressionParameterString(LPRDATA* rdPtr, int first) => Marshal.PtrToStringAnsi(new IntPtr(CNC_GetNextExpressionParameter(rdPtr, first, 1)));

        public static byte* CNC_GetFirstExpressionParameterStringPtr(LPRDATA* rdPtr, int first) => (byte*)CNC_GetFirstExpressionParameter(rdPtr, first, 1);
        public static byte* CNC_GetNextExpressionParameterStringPtr(LPRDATA* rdPtr, int first) => (byte*)CNC_GetNextExpressionParameter(rdPtr, first, 1);

        
        public static int CNC_GetFirstExpressionParameter(LPRDATA* rdPtr, int first, uint type)
        {
            return CallRuntimeFunction(rdPtr, 4, type, first);
        }
        public static int CNC_GetNextExpressionParameter(LPRDATA* rdPtr, int first, uint type)
        {
            return CallRuntimeFunction(rdPtr, 5, type, first);
        }
        public static int CallRuntimeFunction(LPRDATA* rdPtr, int functionIndex, uint wParam, int lParam)
        {

            int* funcAddr = (int*)rdPtr->rHo.hoAdRunHeader->rh4.rh4KpxFunctions[functionIndex];
            var kpxRoutine = (delegate* unmanaged[Stdcall]<headerObject*, uint, int, int>)funcAddr;

            return kpxRoutine(&rdPtr->rHo, wParam, lParam);
        }
        public static void mvRemoveProps(mv* mv,LPEDATA* edPtr, int* pProperties)
        {
            MvCallFunction(mv, edPtr, 3, (int)pProperties, 0, 0);
        }
        public static void mvInsertProps(mv* mv,LPEDATA* edPtr, int* pProperties,int nInsertPropID,int bAfter)
        {
            MvCallFunction(mv, edPtr, 1, (int)pProperties, nInsertPropID, bAfter);
        }
        public static int MvCallFunction(mv* mv,LPEDATA* edPtr, int functionIndex, int param1,int param2,int param3)
        {
            int funcAddr = (int)mv->mvCallFunction;
            var mvFunction = (delegate* unmanaged[Stdcall]<LPEDATA*,int,int,int,int,int>)funcAddr;
            Console.WriteLine(((int)funcAddr).ToString("X4"));
            return mvFunction(edPtr,functionIndex,param1,param2,param3);
        }
    }
}