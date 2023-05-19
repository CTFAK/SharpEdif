using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpEdif.User;

namespace SharpEdif
{
    public unsafe class API
    {
        [DllExport("Alignment1",CallingConvention.StdCall)]
        public static void Alignment1()
        {
        }

        [DllExport("GetInfos",CallingConvention.StdCall)]
        public static int GetInfos(int info)
        {
            Utils.Log("GetInfos got called");
            switch (info)
            {
                case 0:
                    return 0x300;				// Do not change
                case 2:
                    return 0x100;			// Do not change
                case 6:

                    return 2;	// Works with MMF Standard or above
                case 7:
                    return 243;					// Works with build MINBUILD or above
                default:
                    return 0;
            }
            
        }

        [DllExport("Alignment3",CallingConvention.StdCall)]
        public static void Alignment3()
        {
        }

        [DllExport("Alignment4",CallingConvention.StdCall)]
        public static void Alignment4()
        {
        }

        [DllExport("Alignment5",CallingConvention.StdCall)]
        public static void Alignment5()
        {
        }

        [DllExport("LoadObject",CallingConvention.StdCall)]
        public static int LoadObject(mv* mV, byte* fileName, LPEDATA* edPtr, int reserved)
        {
            Utils.Log("LoadObject got called");
            return 0;
        }

        [DllExport("UnloadObject",CallingConvention.StdCall)]
        public static void UnloadObject(mv* mV, LPEDATA* edPtr, int reserved)
        {
            Utils.Log("UnloadObject got called");
        }

        [DllExport("PutObject",CallingConvention.StdCall)]
        public static void PutObject(mv* mV, fpLevObj* loPtr, LPEDATA* edPtr, ushort cpt)
        {
            Utils.Log("PutObject got called");
        }

        [DllExport("RemoveObject",CallingConvention.StdCall)]
        public static void RemoveObject(mv* mV, fpLevObj* loPtr, LPEDATA* edPtr, ushort cpt)
        {
            Utils.Log("RemoveObject got called");
        }

        [DllExport("Alignment10",CallingConvention.StdCall)]
        public static void Alignment10()
        {
        }

        [DllExport("Alignment11",CallingConvention.StdCall)]
        //public static void MakeIconEx()
        public static void NotMakeIconEx()
        {
            Utils.Log("MakeIconEx got called");
        }

        [DllExport("Alignment12",CallingConvention.StdCall)]
        public static void Alignment12()
        {
        }

        [DllExport("Alignment13",CallingConvention.StdCall)]
        public static void Alignment13()
        {
        }

        [DllExport("CreateObject",CallingConvention.StdCall)]
        public static int CreateObject(mv* mV, fpLevObj* loPtr, LPEDATA* edPtr)
        {
            edPtr->_userData = GCHandle.ToIntPtr(GCHandle.Alloc(new EditData()));
            edPtr->editData.props = new FusionProperties();
            Utils.Log("CreateObject got called");

            return 0;
        }

        [DllExport("Alignment15",CallingConvention.StdCall)]
        public static void Alignment15()
        {
        }

        [DllExport("GetHelpFileName",CallingConvention.StdCall)]
        public static byte* GetHelpFileName() //No Parameters
        {
            
            Utils.Log("GetHelpFileName got called");
            return (byte*)0;
        }

        [DllExport("Alignment17",CallingConvention.StdCall)]
        public static void Alignment17()
        {
        }

        [DllExport("EditObject",CallingConvention.StdCall)]
        public static int EditObject(mv* mV, fpObjInfo* oiPtr, fpLevObj* loPtr, LPEDATA* edPtr)
        {
            Utils.Log("EditObject got called");
            return 0;
        }

        [DllExport("GetObjectRect",CallingConvention.StdCall)]
        public static void GetObjectRect(mv* mV, RECT* rc, fpLevObj* loPtr, LPEDATA* edPtr)
        {
            //Utils.Log("GetObjectRect got called");
            rc->right = rc->left + 32;
            rc->bottom = rc->top + 32;
        }

        //[DllExport("EditorDisplay",CallingConvention.StdCall)]
        [DllExport("DSBL_EditorDisplay",CallingConvention.StdCall)]
        //public static void EditorDisplay()
        public static void DSBL_EditorDisplay(mv* mV, fpObjInfo* oiPtr, fpLevObj* loPtr, 
                                              LPEDATA* edPtr, RECT* rc)
        {
            Utils.Log("EditorDisplay got called");
        }

        [DllExport("IsTransparent",CallingConvention.StdCall)]
        public static int IsTransparent(mv* mV, fpLevObj* loPtr, LPEDATA* edPtr, int dx, int dy)
        {
            Utils.Log("IsTransparent got called");
            return 0;
        }

        [DllExport("PrepareToWriteObject",CallingConvention.StdCall)]
        public static void PrepareToWriteObject(mv* mV, LPEDATA* edPtr, fpObjInfo adoi)
        {
            Utils.Log("PrepareToWriteObject got called");
        }

        [DllExport("UpdateFileNames",CallingConvention.StdCall)]
        public static void UpdateFileNames(mv* mV, byte* appName, LPEDATA* edPtr, void* lpfnUpdate)
        {
            Utils.Log("UpdateFileNames got called");
        }

        //[DllExport("EnumElts",CallingConvention.StdCall)]
        [DllExport("NoEnumEltsForYou",CallingConvention.StdCall)]
        public static void NoEnumEltsForYou(mv* mV, LPEDATA* edPtr, ENUMELTPROC* enumProc, 
                                            ENUMELTPROC* undoProc, LPARAM* lp1, LPARAM* lp2)
        {
            Utils.Log("EnumElts got called");
        }

        [DllExport("Initialize",CallingConvention.StdCall)]
        public static int Initialize(mv* mV, int quiet)
        {
            //SharpEdif.AllocConsole();
            //Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            SharpEdif.LoadACEs();

            return 0;
        }

        [DllExport("Free",CallingConvention.StdCall)]
        public static void Free(mv* mV)
        {
            Utils.Log("Free got called");
        }

        [DllExport("ExportTexts",CallingConvention.StdCall)]
        public static void ExportTexts()
        {
            Utils.Log("ExportTexts got called");
        }

        [DllExport("ImportTexts",CallingConvention.StdCall)]
        public static void ImportTexts()
        {
            Utils.Log("ImportTexts got called");
        }

        [DllExport("GetSubType",CallingConvention.StdCall)]
        public static void GetSubType()
        {
            Utils.Log("GetSubType got called");
        }

        [DllExport("UsesFile",CallingConvention.StdCall)]
        public static void UsesFile(mv* mV, byte* fileName)
        {
            Utils.Log("UsesFile got called");
        }

        [DllExport("CreateFromFile",CallingConvention.StdCall)]
        public static void CreateFromFile(mv* mV, byte* fileName, LPEDATA* edPtr)
        {
            Utils.Log("CreateFromFile got called");
        }

        [DllExport("DuplicateObject",CallingConvention.StdCall)]
        public static void DuplicateObject(mv* mV, fpObjInfo* loPtr, LPEDATA* edPtr)
        {
            Utils.Log("DuplicateObject got called");
        }

        [DllExport("Alignment33",CallingConvention.StdCall)]
        public static void Alignment33()
        {
        }

        [DllExport("Alignment34",CallingConvention.StdCall)]
        public static void Alignment34()
        {
        }

        [DllExport("Alignment35",CallingConvention.StdCall)]
        public static void Alignment35()
        {
        }

        [DllExport("GetObjInfos",CallingConvention.StdCall)]
        public static void GetObjInfos(mv* mV, LPEDATA* edPtr, byte* ObjName, byte* ObjAuthor, byte* ObjCopyright, byte* ObjComment, byte* ObjHttp)
        {
            Utils.Log("GetObjInfos got called");
            //TODO We might need this one in future, idk
#if EDITTIME
            Utils.CopyStringToMemoryA(SDK.extName,ObjName,255);
            Utils.CopyStringToMemoryA(SDK.extAuthor,ObjAuthor,255);
            Utils.CopyStringToMemoryA(SDK.extCopyright,ObjCopyright,255);
            Utils.CopyStringToMemoryA(SDK.extComment,ObjComment,1024);
            Utils.CopyStringToMemoryA(SDK.extHttp,ObjHttp,255);
#endif
            
            
        }

        [DllExport("GetTextCaps",CallingConvention.StdCall)]
        public static int GetTextCaps(mv* mV, LPEDATA* edPtr)
        {
            //Utils.Log("GetTextCaps got called");
            return 0;
            
        }

        [DllExport("GetTextAlignment",CallingConvention.StdCall)]
        public static int GetTextAlignment(mv* mV, LPEDATA* edPtr)
        {
            Utils.Log("GetTextAlignment got called");
            return 0;
        }

        [DllExport("SetTextAlignment",CallingConvention.StdCall)]
        public static void SetTextAlignment(mv* mV, LPEDATA* edPtr, uint dwAlignFlags)
        {
            Utils.Log("SetTextAlignment got called");
        }

        [DllExport("GetTextFont",CallingConvention.StdCall)]
        public static void GetTextFont(mv* mV, LPEDATA* edPtr, LPLOGFONT* plf, byte* pStyle, uint cbSize)
        {
            Utils.Log("GetTextFont got called");
        }

        [DllExport("SetTextFont",CallingConvention.StdCall)]
        public static void SetTextFont(mv* mV, LPEDATA* edPtr, LPLOGFONT* plf, byte* pStyle)
        {
            Utils.Log("SetTextFont got called");
        }

        [DllExport("GetTextClr",CallingConvention.StdCall)]
        public static void GetTextClr(mv* mV, LPEDATA* edPtr)
        {
            Utils.Log("GetTextClr got called");
        }

        [DllExport("SetTextClr",CallingConvention.StdCall)]
        public static void SetTextClr(mv* mV, LPEDATA* edPtr, int color)
        {
            Utils.Log("SetTextClr got called");
        }

        [DllExport("GetDependencies",CallingConvention.StdCall)]
        public static byte** GetDependencies() // No Parameters
        {
            Utils.Log("GetDependencies got called");
            return (byte**)0;
        }

        [DllExport("GetFilters",CallingConvention.StdCall)]
        public static void GetFilters(mv* mV, LPEDATA* edPtr, uint dwFlags, void* pReserved)
        {
            Utils.Log("GetFilters got called");
        }

        [DllExport("Alignment46",CallingConvention.StdCall)]
        public static void Alignment46()
        {
        }

        [DllExport("suka1",CallingConvention.StdCall)]
        public static void suka1()
        {
            Utils.Log("conditionsTable got called");
        }

        [DllExport("suka2",CallingConvention.StdCall)]
        public static void suka2()
        {
            Utils.Log("actionsTable got called");
        }

        [DllExport("suka3",CallingConvention.StdCall)]
        public static void suka3()
        {
            Utils.Log("expressionsTable got called");
        }

        [DllExport("CreateRunObject",CallingConvention.StdCall)]
        public static short CreateRunObject(LPRDATA* rdPtr, LPEDATA* edPtr, fpcob* cobPtr)
        {
            rdPtr->_userData = GCHandle.ToIntPtr(GCHandle.Alloc(new RunData()));
            
            Utils.Log("CreateRunObject got called");
            return UserMethods.CreateRunObject(rdPtr,edPtr,cobPtr);
        }

        [DllExport("DestroyRunObject",CallingConvention.StdCall)]
        public static void DestroyRunObject(LPRDATA* rdPtr, int fast)
        {
           GCHandle.FromIntPtr( rdPtr->_userData).Free();
           Utils.Log("DestroyRunObject got called");
        }

        [DllExport("HandleRunObject",CallingConvention.StdCall)]
        public static short HandleRunObject(LPRDATA* rdPtr)
        {
            Utils.Log("HandleRunObject got called");
            return 0;
        }

        [DllExport("DisplayRunObject",CallingConvention.StdCall)]
        public static void DisplayRunObject(LPRDATA* rdPtr)
        {
            Utils.Log("DisplayRunObject got called");
        }

        [DllExport("Alignment54",CallingConvention.StdCall)]
        public static void Alignment54()
        {
        }

        [DllExport("Alignment55",CallingConvention.StdCall)]
        public static void Alignment55()
        {
        }

        [DllExport("Alignment56",CallingConvention.StdCall)]
        public static void Alignment56()
        {
        }

        [DllExport("PauseRunObject",CallingConvention.StdCall)]
        public static void PauseRunObject(LPRDATA* rdPtr)
        {
            Utils.Log("PauseRunObject got called");
        }

        [DllExport("ContinueRunObject",CallingConvention.StdCall)]
        public static void ContinueRunObject(LPRDATA* rdPtr)
        {
            Utils.Log("ContinueRunObject got called");
        }

        [DllExport("GetRunObjectInfos",CallingConvention.StdCall)]
        public static short GetRunObjectInfos(mv* mV, kpxRunInfos* infoPtr)
        {
            Utils.Log("GetRunObjectInfos got called");

            var cndBuffer = (int*)SDK.conditionCallbacks;

            var actBuffer = (int*)SDK.actionCallbacks;

            var exprBuffer = (int*)SDK.expressionCallbacks;
            
            infoPtr->conditions = cndBuffer;
            infoPtr->actions = actBuffer;
            infoPtr->expressions = exprBuffer;

            infoPtr->numOfConditions = (short)SDK.conditionInfos.Length;
            infoPtr->numOfActions = (short)SDK.actionInfos.Length;
            infoPtr->numOfExpressions = (short)SDK.expressionInfos.Length;

            infoPtr->editDataSize = (short)sizeof(LPEDATA);
            infoPtr->editFlags = 0;

            infoPtr->windowProcPriority = 100;

            infoPtr->editPrefs = 0;

            infoPtr->identifier = 1347569747;

            infoPtr->version = 1;
            
            return 1;
        }

        [DllExport("GetConditionMenu",CallingConvention.StdCall)]
        public static void GetConditionMenu(mv* mV, fpObjInfo* oiPtr, LPEDATA* edPtr)
        {
            Utils.Log("GetConditionMenu got called");
        }

        [DllExport("GetActionMenu",CallingConvention.StdCall)]
        public static void GetActionMenu(mv* mV, fpObjInfo* oiPtr, LPEDATA* edPtr)
        {
            Utils.Log("GetActionMenu got called");
        }

        [DllExport("GetExpressionMenu",CallingConvention.StdCall)]
        public static void GetExpressionMenu(mv* mV, fpObjInfo* oiPtr, LPEDATA* edPtr)
        {
            Utils.Log("GetExpressionMenu got called");
        }

        [DllExport("GetConditionCodeFromMenu",CallingConvention.StdCall)]
        public static short GetConditionCodeFromMenu(mv* mV, short menuId)
        {
            Utils.Log("GetConditionCodeFromMenu got called");
#if EDITTIME
            return (short)(menuId-26000);
#else
            return 0;
#endif
        }

        [DllExport("GetActionCodeFromMenu",CallingConvention.StdCall)]
        public static short GetActionCodeFromMenu(mv* mV, short menuId)
        {
            Utils.Log("GetActionCodeFromMenu got called");
#if EDITTIME
            return (short)(menuId-25000);
#else 
            return 0;
#endif
        }

        [DllExport("GetExpressionCodeFromMenu",CallingConvention.StdCall)]
        public static short GetExpressionCodeFromMenu(mv* mV, short menuId)
        {
            Utils.Log("GetExpressionCodeFromMenu got called");
#if EDITTIME
            return (short)(menuId-27000);
#else
            return 0;
#endif
        }

        [DllExport("GetConditionString",CallingConvention.StdCall)]
        public static void GetConditionString(mv* mV, short code, byte* strPtr, short maxLen)
        {
#if EDITTIME
            Utils.CopyStringToMemoryA(SDK.conditionEditorNames[code],strPtr,maxLen);
#endif
            Utils.Log("GetConditionString got called with code "+code);
        }

        [DllExport("GetActionString",CallingConvention.StdCall)]
        public static void GetActionString(mv* mV, short code, byte* strPtr, short maxLen)
        {
#if EDITTIME
            Utils.CopyStringToMemoryA(SDK.actionEditorNames[code],strPtr,maxLen);
#endif
            Utils.Log("GetActionString got called");
        }

        [DllExport("GetExpressionString",CallingConvention.StdCall)]
        public static void GetExpressionString(mv* mV, short code, byte* strPtr, short maxLen)
        {
#if EDITTIME
            Utils.CopyStringToMemoryA(SDK.expressionEditorNames[code],strPtr,maxLen);
#endif

            Utils.Log("GetExpressionString got called");
        }

        [DllExport("InitParameter",CallingConvention.StdCall)]
        public static void InitParameter(mv* mV, short code, paramExt* pExt)
        {
            Utils.Log("InitParameter got called");
        }

        [DllExport("EditParameter",CallingConvention.StdCall)]
        public static void EditParameter(mv* mV, short code, paramExt* pExt)
        {
            Utils.Log("EditParameter got called");
        }

        [DllExport("GetParameterString",CallingConvention.StdCall)]
        public static void GetParameterString(mv* mV, short code, paramExt* pExt, 
                                              byte* pDest, short size)
        {
            Utils.Log("GetParameterString got called");
        }

        [DllExport("GetConditionTitle",CallingConvention.StdCall)]
        public static void GetConditionTitle(mv* mV, short code, short param, byte* strBuf, short maxLen)
        {
            Utils.Log("GetConditionTitle got called");
#if EDITTIME
            Utils.CopyStringToMemoryA(SDK.conditionNames[code],strBuf,maxLen);
#endif
        }

        [DllExport("GetActionTitle",CallingConvention.StdCall)]
        public static void GetActionTitle(mv* mV, short code, short param, byte* strBuf, short maxLen)
        {
            Utils.Log("GetActionTitle got called");
#if EDITTIME
            Utils.CopyStringToMemoryA(SDK.actionNames[code],strBuf,maxLen);
#endif

        }

        [DllExport("GetExpressionTitle",CallingConvention.StdCall)]
        public static void GetExpressionTitle(mv* mV, short code, byte* strBuf, short maxLen)
        {
            Utils.Log("GetExpressionTitle got called");
#if EDITTIME
            Utils.CopyStringToMemoryA(SDK.expressionNames[code],strBuf,maxLen);
#endif
            
        }

        [DllExport("GetConditionInfos",CallingConvention.StdCall)]
        public static infosEventsV2* GetConditionInfos(mv* mV, short code)
        {
            Utils.Log("GetConditionInfos got called with code "+code);
#if EDITTIME
            return (infosEventsV2*)SDK.conditionInfos[code];
#else 
            return (infosEventsV2*)0;
#endif
        }

        [DllExport("GetActionInfos",CallingConvention.StdCall)]
        public static infosEventsV2* GetActionInfos(mv* mV, short code)
        {
            Utils.Log("GetActionInfos got called");
#if EDITTIME
            return (infosEventsV2*)SDK.actionInfos[code];
#else 
            return (infosEventsV2*)0;
#endif
        }

        [DllExport("GetExpressionInfos",CallingConvention.StdCall)]
        public static infosEventsV2* GetExpressionInfos(mv* mV, short code)
        {
            Utils.Log("GetExpressionInfos got called");
#if EDITTIME
            return (infosEventsV2*)SDK.expressionInfos[code];
#else 
            return (infosEventsV2*)0;
#endif
        }

        [DllExport("UpdateEditStructure",CallingConvention.StdCall)]
        public static void UpdateEditStructure(mv* mV, void* OldEdPtr)
        {
            Utils.Log("UpdateEditStructure got called");
        }

        [DllExport("Alignment79",CallingConvention.StdCall)]
        public static void Alignment79()
        {
        }

        [DllExport("GetRunObjectDataSize",CallingConvention.StdCall)]
        public static ushort GetRunObjectDataSize(fprh* rhPtr, LPEDATA* edPtr)
        {
            Utils.Log("GetRunObjectDataSize got called");
            return (ushort)sizeof(LPRDATA);
        }

        [DllExport("SaveBackground",CallingConvention.StdCall)]
        public static void SaveBackground()
        {
            Utils.Log("SaveBackground got called");
        }

        [DllExport("RestoreBackground",CallingConvention.StdCall)]
        public static void RestoreBackground()
        {
            Utils.Log("RestoreBackground got called");
        }

        [DllExport("KillBackground",CallingConvention.StdCall)]
        public static void KillBackground()
        {
            Utils.Log("KillBackground got called");
        }

        [DllExport("WindowProc",CallingConvention.StdCall)]
        public static void WindowProc(LPRH* rhPtr, HWND* hWnd, uint nMsg,
                                      WPARAM* wParam, LPARAM* lParam)
        {
            Utils.Log("WindowProc got called");
        }

        [DllExport("StartApp",CallingConvention.StdCall)]
        public static void StartApp(mv* mV, CRunApp* pApp)
        {
            Utils.Log("StartApp got called");
        }

        [DllExport("EndApp",CallingConvention.StdCall)]
        public static void EndApp(mv* mV, CRunApp* pApp)
        {
            Utils.Log("EndApp got called");
        }

        [DllExport("StartFrame",CallingConvention.StdCall)]
        public static void StartFrame(mv* mV, uint dwReserved, int nFrameIndex)
        {
            Utils.Log("StartFrame got called");
        }

        [DllExport("EndFrame",CallingConvention.StdCall)]
        public static void EndFrame(mv* mV, uint dwReserved, int nFrameIndex)
        {
            Utils.Log("EndFrame got called");
        }

        [DllExport("GetExpressionParam",CallingConvention.StdCall)]
        public static void GetExpressionParam(mv* mV, short code, short param,
                                              byte* strBuf, short maxLen)
        {
            Utils.Log("GetExpressionParam got called");
        }

        [DllExport("GetRunObjectSurface",CallingConvention.StdCall)]
        public static void GetRunObjectSurface(LPRDATA* rdPtr)
        {
            Utils.Log("GetRunObjectSurface got called");
        }

        [DllExport("GetRunObjectCollisionMask",CallingConvention.StdCall)]
        public static void GetRunObjectCollisionMask(LPRDATA* rdPtr, LPARAM* lParam)
        {
            Utils.Log("GetRunObjectCollisionMask got called");
        }

        [DllExport("GetRunObjectFont",CallingConvention.StdCall)]
        public static void GetRunObjectFont(LPRDATA* rdPtr, LOGFONT* pLf)
        {
            Utils.Log("GetRunObjectFont got called");
        }

        [DllExport("SetRunObjectFont",CallingConvention.StdCall)]
        public static void SetRunObjectFont(LPRDATA* rdPtr, LOGFONT* pLf, RECT* pRc)
        {
            Utils.Log("SetRunObjectFont got called");
        }

        [DllExport("GetRunObjectTextColor",CallingConvention.StdCall)]
        public static void GetRunObjectTextColor(LPRDATA* rdPtr)
        {
            Utils.Log("GetRunObjectTextColor got called");
        }

        [DllExport("SetRunObjectTextColor",CallingConvention.StdCall)]
        public static void SetRunObjectTextColor(LPRDATA* rdPtr, int rgb)
        {
            Utils.Log("SetRunObjectTextColor got called");
        }

        [DllExport("Alignment96",CallingConvention.StdCall)]
        public static void Alignment96()
        {
        }

        [DllExport("Alignment97",CallingConvention.StdCall)]
        public static void Alignment97()
        {
        }

        [DllExport("Alignment98",CallingConvention.StdCall)]
        public static void Alignment98()
        {
        }

        [DllExport("SetEditSize",CallingConvention.StdCall)]
        public static void SetEditSize(mv* mv, LPEDATA* edPtr, int cx, int cy)
        {
            Utils.Log("SetEditSize got called");
        }

        [DllExport("GetProperties",CallingConvention.StdCall)]
        public static int GetProperties(mv* mV, LPEDATA* edPtr, bool bMasterItem)
        {
            Utils.Log("GetProperties got called");
            var props = edPtr->editData.props;
            if (!props.propsFilled)
            {
                UserMethods.FillProperties(edPtr->editData.props);
                edPtr->editData.props.InvalidateData();
                props.propsFilled = true;
            }
            
            SDK.mvInsertProps(mV,edPtr,edPtr->editData.props.ObtainData(),1,1);
            return 1;
        }

        [DllExport("ReleaseProperties",CallingConvention.StdCall)]
        public static void ReleaseProperties(mv* mV, LPEDATA* edPtr, bool bMasterItem)
        {
            Utils.Log("ReleaseProperties got called");
        }

        [DllExport("GetPropValue",CallingConvention.StdCall)]
        public static void GetPropValue(mv* mV, LPEDATA* edPtr, uint nPropID)
        {
            Utils.Log("GetPropValue got called");
        }

        [DllExport("SetPropValue",CallingConvention.StdCall)]
        public static void SetPropValue(mv* mV, LPEDATA* edPtr, uint nPropID, void* lParam)
        {
            Utils.Log("SetPropValue got called");
        }

        [DllExport("IsPropEnabled",CallingConvention.StdCall)]
        public static void IsPropEnabled(mv* mV, LPEDATA* edPtr, uint nPropID)
        {
            Utils.Log("IsPropEnabled got called");
        }

        [DllExport("GetPropCheck",CallingConvention.StdCall)]
        public static void GetPropCheck(mv* mV, LPEDATA* edPtr, uint nPropID)
        {
            Utils.Log("GetPropCheck got called");
        }

        [DllExport("SetPropCheck",CallingConvention.StdCall)]
        public static void SetPropCheck(mv* mV, LPEDATA* edPtr, uint nPropID, bool nCheck)
        {
            Utils.Log("SetPropCheck got called");
        }

        [DllExport("GetPropCreateParam",CallingConvention.StdCall)]
        public static void GetPropCreateParam(mv* mV, LPEDATA* edPtr, uint nPropID)
        {
            Utils.Log("GetPropCreateParam got called");
        }

        [DllExport("ReleasePropCreateParam",CallingConvention.StdCall)]
        public static void ReleasePropCreateParam(mv* mV, LPEDATA* edPtr, uint nPropID, LPARAM lParam)
        {
            Utils.Log("ReleasePropCreateParam got called");
        }

        [DllExport("EditProp",CallingConvention.StdCall)]
        public static void EditProp(mv* mV, LPEDATA* edPtr, uint nPropID)
        {
            Utils.Log("EditProp got called");
        }

        [DllExport("GetDebugTree",CallingConvention.StdCall)]
        public static void GetDebugTree(LPRDATA* rdPtr)
        {
            Utils.Log("GetDebugTree got called");
        }

        [DllExport("GetDebugItem",CallingConvention.StdCall)]
        public static void GetDebugItem(byte* pBuffer, LPRDATA* rdPtr, int id)
        {
            Utils.Log("GetDebugItem got called");
        }

        [DllExport("EditDebugItem",CallingConvention.StdCall)]
        public static void EditDebugItem(LPRDATA* rdPtr, int id)
        {
            Utils.Log("EditDebugItem got called");
        }

        [DllExport("SaveRunObject",CallingConvention.StdCall)]
        public static void SaveRunObject(LPRDATA* rdPtr, HANDLE* hf)
        {
            Utils.Log("SaveRunObject got called");
        }

        [DllExport("LoadRunObject",CallingConvention.StdCall)]
        public static void LoadRunObject(LPRDATA* rdPtr, HANDLE* hf)
        {
            Utils.Log("LoadRunObject got called");
        }

        [DllExport("GetRunObjectMemoryUsage",CallingConvention.StdCall)]
        public static void GetRunObjectMemoryUsage()
        {
            Utils.Log("GetRunObjectMemoryUsage got called");
        }

        [DllExport("ChangeScale",CallingConvention.StdCall)]
        public static void ChangeScale()
        {
            Utils.Log("ChangeScale got called");
        }
    }
}
