using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpEdif.User;

namespace SharpEdif
{
	public enum PropType : int
	{
		Static = 1, // Simple static text
		Folder, // Folder
		FolderEnd, // Folder End
		Editbutton, // Edit button, param1 = button text, or NULL if Edit
		EditString, // Edit box for strings, parameter = max length
		EditNumber, // Edit box for numbers, parameters = min value, max value, options (signed, float, spin)
		Combobox, // Combo box, parameters = list of strings, options (sorted, etc)
		Size, // Size
		Color, // Color
		Leftcheckbox, // Checkbox
		Slideredit, // Edit + Slider
		Spinedit, // Edit + Spin
		Dirctrl, // Direction Selector
		Group, // Group
		Listbtn, // Internal, do not use
		Filename, // Edit box + browse file button, parameter = FilenameCreateParam
		Font, // Font dialog box
		Custom, // Custom property
		Picturefilename, // Edit box + browse image file button
		Comboboxbtn, // Combo box, parameters = list of strings, options (sorted, etc)
		EditFloat, // Edit box for floating point numbers, parameters = min value, max value, options (signed, float, spin)
		EditMultiline, // Edit box for multiline texts, no parameter
		Imagelist, // Image list
		Iconcombobox, // Combo box with icons
		Urlbutton, // URL button
	}

	public enum ExpParamType : short
	{
		Int=1,
		Float=2,
		String=3,
		AltValue=4,
		Flag=5
	}
	public enum ExpReturnType : short
	{
		Int=0,
		String=0x0001,
		Double=0x0002
	}
	public enum ParamType : short
	{
		Object = 1,
		Time = 2,
		Border = 3,
		Direction = 4,
		Integer = 5,
		Sample = 6,
		Music = 7,
		Create = 9,
		Animation = 10,
		Nop = 11,
		Player = 12,
		Every = 13,
		Key = 14,
		Speed = 15,
		Position = 16,
		JoyDirection = 17,
		Shoot = 18,
		Zone = 19,
		SysCreate = 21,
		Expression = 22,
		Comparsion = 23,
		Color = 24,
		Buffer4 = 25,
		Frame = 26,
		SampleLoops = 27,
		MusicLoops = 28,
		NewDirection = 29,
		TextNumber = 31,
		ClickDefinition = 32,
		Program = 33,
		ConditionSample = 35,
		ConditionMusic = 36,
		Remark = 37,
		Group = 38,
		GroupPointer = 39,
		Filename = 40,
		String = 41,
		CompareTime = 42,
		PasteSprite = 43,
		VirtualMouseKey = 44,
		ExpString = 45,
		CmpString = 46,
		InkEffect = 47,
		Menu = 48,

	}

	public unsafe struct RECT
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
	}

	public unsafe struct POINT
	{
		public int x;
		public int y;
	}

	

	public unsafe struct infosEventsV2
	{
		public short code;
		public short flags;
		public short nParams;
	}

	public unsafe struct eventInformations2
	{
		public short menu;
		public short str;
		public infosEventsV2 infos;
	}

	public unsafe struct extHeader
	{
		public int extSize;
		public int extMaxSize;
		public int extVersion;
		public int* extID;
		public int* extpublicData;
	}

	public unsafe struct kpxRunInfos
	{
		public int* conditions;
		public int* actions;
		public int* expressions;
		public short numOfConditions;
		public short numOfActions;
		public short numOfExpressions;
		public short editDataSize;
		public int editFlags;
		public byte windowProcPriority;
		public byte free;
		public short editPrefs;
		public int identifier;
		public short version;
	}

	public unsafe struct mv
	{
int*			mvHInst;				// Application HINSTANCE
	void*				mvIdAppli;				// Application object in DLL
	void*				mvIdMainWin;			// Main window object in DLL
	void*				mvIdEditWin;			// Child window object in DLL
	HWND				mvHMainWin;				// Main window handle
	HWND				mvHEditWin;				// Child window handle
	void*			mvHPal256;				// 256 color palette
	short				mvAppMode;				// Screen mode with flags
	short				mvScrMode;				// Screen mode
	int				mvEditDXDocToClient;	// Edit time only: top-left coordinates
	int				mvEditDYDocToClient;
	int*	mvImgFilterMgr;			// Image filter manager
	int*	mvSndFilterMgr;			// Sound filter manager
	int*		mvSndMgr;				// Sound manager

		CRunApp*		mvRunApp;				// Current application, runtime

		CRunFrame*		mvRunFrame;



		void*			mvRunHdr;
	public int				mvPrefs;				// Preferences (sound on/off)
	public byte*				subType;
	public int				mvFullScreen;			// Full screen mode
	public byte*				mvMainAppFileName;		// App filename
	public int					mvAppListCount;
	public int					mvAppListSize;
	public CRunApp**			mvAppList;
	public int					mvExtListCount;
	public int					mvExtListSize;
	public byte* *				mvExtList;
	public int					mvNbDllTrans;
	public void*			mvDllTransList;
	public fixed int				mvJoyCaps[32];
	public void*				mvHMsgHook;
	public int					mvModalLoop;
	public int					mvModalSubAppCount;
	public fixed int				mvFree[5];

	// Functions
	////////////

	// Editor: Open Help file
	public int* mvHelp;

	// Editor: Get default font for object creation
	public int* mvGetDefaultFont;

	// Editor: Edit images and animations
	public int* mvEditSurface;
	public int* mvEditImage;
	public int* mvEditAnimation;

	// Runtime: Extension User data
	public int* mvGetExtUserData;
	public int* mvSetExtUserData;

	// Runtime: Register dialog box
	public int* mvRegisterDialogBox;
	public int* mvUnregisterDialogBox;

	// Runtime: Add surface as backdrop object
	public int* mvAddBackdrop;

	// Runtime: Binary files
	public int* mvGetFile;
	public int* mvReleaseFile;
	public int* mvOpenHFile;
	public int* mvCloseHFile;

	// Plugin: download file
	public int* mvLoadNetFile;

	// Plugin: send command to Vitalize
	public int* mvNetCommand;

	// Editor & Runtime: Returns the version of MMF or of the runtime
	public int* mvGetVersion;

	// Editor & Runtime: callback function for properties or other functions
	public int* mvCallFunction;

	// Place-holder for next versions
	fixed int				mvAdditionalFncs[16];
	}

	public struct fpLevObj
	{

	}

	public struct fpObjInfo
	{

	}

	public struct ENUMELTPROC
	{

	}

	public struct LPARAM
	{

	}

	public struct LPLOGFONT
	{

	}



	public unsafe struct headerObject
	{
		public short hoNumber;
		public short hoNextSelected;
		public int hoSize;
		public LPRH* hoAdRunHeader;
		public headerObject* hoAddress;
		public short hoHFII; // Number of LevObj
		public short hoOi; // Number of OI
		public short hoNumPrev; // Same OI previous object
		public short hoNumNext; // ... next
		public short hoType; // Type of the object
		public short hoCreationId; // Number of creation
		public void* hoOiList; // Pointer to OILIST information
		public int* hoEvents; // Pointer to specific events
		public uint hoFree0; // Free
		public byte* hoPrevNoRepeat; // One-shot event handling
		public byte* hoBaseNoRepeat;

		public int hoMark1; // #of loop marker for the events
		public int hoMark2;
		public byte* hoMT_NodeName; // Name fo the current node for path movements

		public int hoEventNumber; // Number of the event called (for extensions)
		public int hoFree2;
		public OC* hoCommon; // Common structure address

		public int hoCalculX; // Low weight value
		public int hoX; // X coordinate
		public int hoCalculY; // Low weight value
		public int hoY; // Y coordinate};

		public int hoImgXSpot; // Hot spot of the current image
		public int hoImgYSpot;
		public int hoImgWidth; // Width of the current picture
		public int hoImgHeight;
		public RECT hoRect; // Display rectangle

		public int hoOEFlags; // Objects flags
		public ushort hoFlags; // Flags
		public byte hoSelectedInOR; // Selection lors d'un evenement OR
		public byte hoFree; // Alignement 
		public int hoOffsetValue; // Values structure offset
		public byte hoLayer; // Layer

		public void* hoHandleRoutine; // General handle routine
		public void* hoModifRoutine; // Modification routine when coordinates have been modified
		public void* hoDisplayRoutine; // Display routine

		public short hoLimitFlags; // Collision limitation flags
		public short hoNextQuickDisplay; // Quickdraw list
		public saveRect hoBackSave; // Background

		public void* hoCurrentParam; // Address of the current parameter

		public int hoOffsetToWindows; // Offset to windows
		public int hoIdentifier; // ASCII identifier of the object

	}

	public unsafe struct saveRect
	{
		public byte* pData;
		public RECT rc;
	}

	public unsafe struct LPEDATA
	{
		public extHeader eHeader;
		public IntPtr _userData;
		public EditData editData => GCHandle.FromIntPtr(_userData).Target as EditData;
	}
	public struct LPRDATA
	{
		public headerObject rHo;
		public IntPtr _userData;
		public RunData runData => GCHandle.FromIntPtr(_userData).Target as RunData;
	}

	public struct fpcob
	{

	}

	public struct paramExt
	{

	}

	public struct fprh
	{

	}

	public unsafe struct runHeader2
	{
		int rh2OldPlayer; // Previous player entries
		int rh2NewPlayer; // Modified player entries
		int rh2InputMask; // Inhibated players entries
		int rh2InputPlayers; // Valid players entries (mask!)
		byte rh2MouseKeys; // Mousekey entries
		byte rh2ActionLoop; // Actions flag
		byte rh2ActionOn; // Flag are we in actions?
		byte rh2EnablePick; // Are we in pick for actions?

		int rh2EventCount; // Number of the event 
		void* rh2EventQualPos; // ***Position in event objects
		headerObject* rh2EventPos; // ***Position in event objects
		void* rh2EventPosOiList; // ***Position in oilist for TYPE exploration 
		void* rh2EventPrev; // ***Previous object address

		void* rh2PushedEvents; // ***
		byte* rh2PushedEventsTop; // ***
		byte* rh2PushedEventsMax; // ***
		int rh2NewPushedEvents; // 

		public int rh2ActionCount; // Action counter
		public int rh2ActionLoopCount; // Action loops counter
		public void* rh2ActionEndRoutine; // End of action routine
		public short rh2CreationCount; // Number of objects created since beginning of frame
		public short rh2EventType;
		public POINT rh2Mouse; // Mouse coordinate
		public POINT rh2MouseClient; // Mouse coordinates in the window
		public short rh2CurrentClick; // For click events II
		public short rh2Free2;
		public headerObject** rh2ShuffleBuffer; // ***
		public headerObject** rh2ShufflePos; // ***
		public int rh2ShuffleNumber;

		POINT rh2MouseSave; // Mouse saving when pause
		int rh2PauseCompteur;
		int rh2PauseTimer;
		uint rh2PauseVbl;
		void* rh2LoopTraceProc; // Debugging routine
		void* rh2EventTraceProc;
	}

	public unsafe struct runHeader3
	{
		short rh3Graine; // random generator seed
		short rh3Free; // Alignment...

		public int rh3DisplayX; // To scroll
		public int rh3DisplayY;

		int rh3CurrentMenu; // For menu II events

		public int rh3WindowSx; // Window size
		public int rh3WindowSy;

		public short rh3CollisionCount; // Collision counter 
		public byte rh3DoStop; // Force the test of stop actions
		public byte rh3Scrolling; // Flag: we need to scroll

		int rh3Panic;

		int rh3PanicBase;
		int rh3PanicPile;

//	short	  	rh3XBorder_;				// Authorised border
//	short	  	rh3YBorder_;
		public int rh3XMinimum; // Object inactivation coordinates
		public int rh3YMinimum;
		public int rh3XMaximum;
		public int rh3YMaximum;
		public int rh3XMinimumKill; // Object destruction coordinates
		public int rh3YMinimumKill;
		public int rh3XMaximumKill;
		public int rh3YMaximumKill;
	}

	public unsafe struct tagEVT
	{
		short evtSize; // 0 Size of the event

		short evtType; // 2 Type of object
		short evtNum; // 4 Number of action/condition

		short evtOi; // 6 OI if normal object
		short evtOiList; // 8 Pointer
		byte evtFlags; // 10 Flags
		byte evtFlags2; // 11 Flags II
		byte evtNParams; // 12 Number of parameters

		byte evtDefType; // 13 If default, type

// Pour les conditions
		short evtIdentifier; // 14 Event identifier
	}

	public unsafe struct LPFL
	{
		byte* next;
		public fixed byte name[64];
		short flags;
		int index;
	}

	public unsafe struct runHeader4
	{

		public kpj* rh4KpxJumps; // Jump table offset
		public short rh4KpxNumOfWindowProcs; // Number of routines to call
		public short rh4Free;
		public fixed int rh4KpxWindowProc[96]; // Message handle routines
		public fixed int rh4KpxFunctions[32]; // Available internal routines
		public FusionBullshit_FunctionPointer rh4Animations;
		public FusionBullshit_FunctionPointer rh4DirAtStart;
		public FusionBullshit_FunctionPointer rh4MoveIt;
		public FusionBullshit_FunctionPointer rh4ApproachObject;
		public FusionBullshit_FunctionPointer rh4Collisions;
		public FusionBullshit_FunctionPointer rh4TestPosition;
		public FusionBullshit_FunctionPointer rh4GetJoystick;
		public FusionBullshit_FunctionPointer rh4ColMaskTestRect;
		public FusionBullshit_FunctionPointer rh4ColMaskTestPoint;

		public int rh4SaveVersion;
		public tagEVT* rh4ActionStart; // Sauvergarde action courante
		public int rh4PauseKey;
		public byte* rh4CurrentFastLoop;
		public int rh4EndOfPause;
		public int rh4EventCountOR; // Number of the event for OR conditions
		public short rh4ConditionsFalse;
		public short rh4MouseWheelDelta;
		public int rh4OnMouseWheel;
		public byte* rh4PSaveFilename;
		public uint rh4MusicHandle;
		public int rh4MusicFlags;
		public int rh4MusicLoops;
		public int rh4LoadCount;
		public short rh4DemoMode;
		public short rh4Free4;
		public void* rh4Demo;
		public fixed byte rh4QuitString[52]; // FREE!!!! GREAT!

		public int rh4PickFlags0; // 00-31
		public int rh4PickFlags1; // 31-63
		public int rh4PickFlags2; // 64-95
		public int rh4PickFlags3; // 96-127
		public int* rh4TimerEventsBase; // Timer events base

		public short rh4DroppedFlag;
		public short rh4NDroppedFiles;
		public byte* rh4DroppedFiles;
		public LPFL* rh4FastLoops;
		public byte* rh4CreationErrorMessages;
		public CValue rh4ExpValue1; // New V2
		public CValue rh4ExpValue2;

		public int rh4KpxReturn; // WindowProc return 
		public void* rh4ObjectCurCreate;
		public short rh4ObjectAddCreate;
		public short rh4Free10; // For step through : fake key pressed
		public void* rh4Instance; // Application instance
		public HWND rh4HStopWindow; // STOP window handle
		public byte rh4DoUpdate; // Flag for screen update on first loop
		public byte rh4MenuEaten; // Menu handled in an event?
		public short rh4Free2;
		public int rh4OnCloseCount; // For OnClose event
		public short rh4CursorCount; // Mouse counter
		public short rh4ScrMode; // Current screen mode
		public void* rh4HPalette; // Handle current palette
		public int rh4VBLDelta; // Number of VBL
		public int rh4LoopTheoric; // Theorical VBL counter
		public int rh4EventCount;
		public void* rh4FirstBackDrawRoutine; // Backrgound draw routines list
		public void* rh4LastBackDrawRoutine; // Last routine used

		public int rh4ObjectList; // Object list offset
		public short rh4LastQuickDisplay; // Quick - display list
		public byte rh4CheckDoneInstart; // Build92 to correct start of frame with fade in
		public byte rh4Free0;
		public mv* rh4Mv; // Yves's data
		public int rh4Free1; // String buffer position
		public headerObject* rh4_2ndObject; // Collision object address
		public short rh4_2ndObjectNumber; // Number for collisions
		public short rh4FirstQuickDisplay; // Quick-display object list
		public int rh4WindowDeltaX; // For scrolling
		public int rh4WindowDeltaY;
		public int rh4TimeOut; // For time-out!
		public int rh4MouseXCenter; // To correct CROSOFT bugs!
		public int rh4MouseYCenter; // To correct CROSOFT bugs!
		public int rh4TabCounter; // Objects with tabulation

		public int rh4AtomNum; // For child window handling
		public int rh4AtomRd;
		public int rh4AtomProc;
		public short rh4SubProcCounter; // To accelerate the windows
		public short rh4Free3;

		public int rh4PosPile; // Expression evaluation pile position
		public void* rh4ExpToken; // Current position in expressions
		public fixed int rh4Results[256]; // Result pile
		public fixed int rh4Operators[256]; // Operators pile
		public fixed int rh4PTempStrings[256]; // Temporary string calculation positiion
		public int rh4NCurTempString; // Pointer on the current string
		public fixed short rh4FrameRateArray[10]; // Framerate calculation buffer
		public int rh4FrameRatePos; // Position in buffer
		public int rh4FrameRatePrevious; // Previous time 

	}

	public unsafe struct LPRH
	{
		void* rhIdEditWin;
		void* rhIdMainWin;
		void* rhIdAppli;

		HWND rhHEditWin;
		HWND rhHMainWin;
		HWND rhHTopLevelWnd;

		public CRunApp* rhApp; // Application info
		CRunFrame* rhFrame; // Frame info

		int rhJoystickPatch; // To reroute the joystick

		byte rhFree10; // Current movement needs to be stopped
		byte rhFree12; // Event evaluation flag
		byte rhNPlayers; // Number of players
		byte rhMouseUsed; // Players using the mouse

		short rhGameFlags; // Game flags
		short rhFree; // Alignment
		int rhPlayer; // Current players entry

		short rhQuit;
		short rhQuitBis; // Secondary quit (scrollings)
		int rhFree11; // Value to return to the editor
		int rhQuitParam;

// Buffers
		int rhNObjects;
		int rhMaxObjects;

		int rhFree0;
		int rhFree1;
		int rhFree2;
		int rhFree3;

		int rhNumberOi; // Number of OI in the list
		void* rhOiList; // ObjectInfo list

		public fixed int rhEvents[7 + 1]; // Events pointers
		int* rhEventLists; // Pointers on pointers list
		int* rhFree8; // Timer pointers
		int* rhEventAlways; // Pointers on events to see at each loop
		void* rhPrograms; // Program pointers
		void* rhLimitLists; // Movement limitation list
		void* rhQualToOiList; // Conversion qualifier->oilist

		int rhSBuffers; // Buffer size /1024	
		byte* rhBuffer; // Position in current buffer
		byte* rhFBuffer; // End of current buffer
		byte* rhBuffer1; // First buffer
		byte* rhBuffer2; // Second buffer

		int rhLevelSx; // Window size
		int rhLevelSy;
		int rhWindowX; // Start of window in X/Y
		int rhWindowY;

		uint rhVBLDeltaOld; // Number of VBL
		uint rhVBLObjet; // For the objects
		uint rhVBLOld; // For the counter

		int free10;
		short rhMT_VBLStep; // Path movement variables
		short rhMT_VBLCount;
		int rhMT_MoveStep;

		int rhLoopCount; // Number of loops since start of level
		uint rhTimer; // Timer in 1/50 since start of level
		uint rhTimerOld; // For delta calculation
		uint rhTimerDelta; // For delta calculation

		void* rhEventGroup; // Current group
		int rhCurCode; // Current event
		short rhCurOi;
		short rhFree4; // Alignment
		public fixed int rhCurParam[2];
		short rhCurObjectNumber; // Object number
		short rh1stObjectNumber; // Number, for collisions

		int rhOiListPtr; // OI list enumeration
		short rhObListNext; // Branch label

		short rhDestroyPos;
		int rhFree5;
		int rhFree6;

		public runHeader2 rh2; // Sub-structure #1
		public runHeader3 rh3; // Sub-structure #2
		public runHeader4 rh4; // Sub-structure #3

		int* rhDestroyList; // Destroy list address

		int rhDebuggerCommand; // Current debugger command
		public fixed byte rhDebuggerBuffer[256]; // Code transmission buffer
		byte* rhDbOldHO;
		short rhDbOldId;
		short rhFree7;

		void* rhObjectList; // Object list address

	}

	public unsafe struct HWND
	{
		public int* dataInside;
	}

	public struct WPARAM
	{

	}

	public struct cSurface
	{

	}

	public struct CRunFrame
	{

	}

	public struct CDebugger
	{

	}

	public struct CValue
	{
		public int type;
		public int paddle;
		public int val;
	}

	public struct WINDOWPLACEMENT
	{
		int length;
		int flags;
		int showCmd;
		POINT ptMinPosition;
		POINT ptMaxPosition;
		RECT rcNormalPosition;
	}

	public unsafe struct Fusion_ControlBullshit
	{
		public fixed short data[8];
	}

	public unsafe struct fpPlayerCtrls
	{
		public fixed short pcCtrlType[4];
		public fixed short pcCtrlKeys[32];
	}

	public unsafe struct CRunApp
	{
		// Application info
		public AppMiniHeader m_miniHdr; // Version
		public AppHeader m_hdr; // General info
		public byte* m_name; // Name of the application
		public byte* m_appFileName; // filename (temporary file in editor mode)
		public byte* m_editorFileName; // filename of original .mfa file
		public byte* m_copyright; // copyright
		public byte* m_aboutText; // text to display in the About box

		// File infos
		public byte* m_targetFileName; // filename of original CCN/EXE file
		public byte* m_tempPath; // Temporary directory for external files
		public int m_file; // File handle

		uint m_startOffset;

		// Help file
		byte* m_doc; // Help file pathname

		// Icon
		byte* m_icon16x16x8; // = LPBITMAPINFOHEADER
		void* m_iconNew; // Small icon for the main window

		// Menu
		void* m_hRunMenu; // Menu
		byte* m_accels; // Accelerators
		byte* m_pMenuTexts; // Menu texts (for ownerdraw menu)
		byte* m_pMenuImages; // Images index used in the menu
		MenuHdr* m_pMenu;

		// Frame offsets
		int m_frameMaxIndex; // Max. number of frames
		int m_frameMaxHandle; // Max. frame handle
		short* m_frame_handle_to_index; // Handle -> index table
		int* m_frameOffset; // Frame offsets in the file

		// Frame passwords
		byte** m_framePasswords; // Table of frame passwords

		// Extensions
		int m_nbKpx; // Number of extensions

		//fpKpxFunc m_kpxTab;                 // Function table 1
		void* m_kpxTab; // Function table 1
		TABKPT* m_kpxDataTable; // Function table 2

		// Movement Extensions
		int m_nbMvx; // Number of extensions

		//MvxFnc* m_mvxTable;                 // DLL info
		void* m_mvxTable; // DLL info

		// Elements
		fixed int m_eltFileName[4]; // Element banks
		fixed int m_hfElt[4];

		uint m_eltBaseOff;
		fixed short m_nbEltOff[4]; // Sizes of file offset tables
		fixed int m_adTabEltOff[4]; // File offsets of bank elements

		fixed short m_nbEltMemToDisk[4]; // Size of elt cross-ref tables
		fixed int m_EltMemToDisk[4]; // Element memory index -> element disk index
		fixed int m_EltDiskToMem[4]; // Element disk index -> memory index

		fixed short m_tabNbCpt[4]; // Sizes of usage count tables
		fixed int m_tabAdCpt[4]; // Usage count tables of bank elements

		// Binary files

		fixed uint m_binaryFiles[8];

		// Temporary images
		uint m_nImagesTemp; // List of temporary images (used by Replace Color action)
		byte* m_pImagesTemp;

		// Frame objects
		public int m_oiMaxIndex;
		public int m_oiMaxHandle;
		public short* m_oi_handle_to_index;
		public OI** m_ois;
		public int m_oiFranIndex; // for enumerating
		public int m_oiExtFranIndex; // for enumerating

		// Sub-application
		public CRunApp* m_pParentApp; // Parent application
		public void* m_pSubAppObject; // LPRS
		public uint m_dwSubAppOptions; // Sub-app options
		public bool m_bSubAppIsVisible; // Sub-app visibility
		public void* m_hSubAppIcon; // Sub-app icon
		public int m_cx; // Subapp: valid if stretch
		public int m_cy;

		// DLL infos
		void* m_idAppli; // App object in DLL
		int m_nDepth; // Screen depth
		cSurface* m_protoLogScreen; // Surface prototype

		// Edit window
		HWND m_hEditWin; // Edit Window handle
		void* m_idEditWin; // Edit Window identifier

		// Current frame
		public CRunFrame* m_Frame; // Pointer to current frame

		// Run-time status
		public int m_bResizeTimer;
		public int m_refTime;
		public int m_appRunningState;
		public int m_startFrame;
		public int m_nextFrame;
		public int m_nCurrentFrame;
		public bool m_bWakeUp;
		public short m_oldFlags;
		public short m_appRunFlags;
		public bool m_bPlayFromMsgProc;

		// Debugger
		public CDebugger* m_pDebugger;

		// Full Screen
		public int m_depthFullScreen;
		public WINDOWPLACEMENT m_sWndPlacement; // Window position backup
		public int m_oldCxMax; // Window size backup
		public int m_oldCyMax;
		public cSurface* m_pFullScreenSurface;

		// Global data
		public bool m_bSharePlayerCtrls; // Sub-app: TRUE if shares player controls
		public bool m_bShareLives; // Sub-app: TRUE if shares lives
		public bool m_bShareScores; // Sub-app: TRUE if shares scores
		public bool m_bShareGlobalValues; // Sub-app: TRUE if shares global values
		public bool m_bShareGlobalStrings; // Sub-app: TRUE if shares global strings

		// Players
		public fpPlayerCtrls m_pPlayerCtrls;
		public int* m_pLives;
		public int* m_pScores;

		// Global values (warning: not valid if sub-app and global values are shared)
		public byte* m_pGlobalValuesInit;
		public int m_nGlobalValues; // Number of global values
		public CValue* m_pGlobalValues; // Global values
		public byte* m_pFree; // No longer used
		public byte* m_pGlobalValueNames;

		// Global strings (warning: not valid if sub-app and global values are shared)
		public byte* m_pGlobalStringInit; // Default global string values
		public int m_nGlobalStrings; // Number of global strings
		public byte** m_pGlobalString; // Pointers to global strings
		public byte* m_pGlobalStringNames;

		// Global objects
		public byte* m_AdGO; // Global objects data

		// FRANCOIS
		public fixed short m_NConditions[7 + 32 - 1];

		// External sound files
		byte* m_pExtMusicFile;
		public fixed int m_pExtSampleFile[32]; // External sample file per channel

		// New Build 243
		int m_nInModalLoopCount;
		byte* m_pPlayerNames;
		int m_dwColorCache;

		// New Build 245
		byte* m_pVtz4Opt;
		int m_dwVtz4Chk;

		// Application load
		byte* m_pLoadFilename;
		int m_saveVersion;
		bool m_bLoading;
	}

	public struct LOGFONT
	{

	}

	public struct HANDLE
	{

	}

	public unsafe struct OI
	{
		ObjInfoHeader oiHdr; // Header
		byte* oiName; // Name
		OC* oiOC; // ObjectsCommon

		int oiFileOffset;
		int oiLoadFlags;
		short oiLoadCount;
		short oiCount;
	}

	public unsafe struct OC
	{
		int ocDWSize; // Total size of the structures

		short ocMovements; // Offset of the movements
		short ocAnimations; // Offset of the animations
		short ocVersion; // For version versions > MOULI 
		short ocCounter; // Pointer to COUNTER structure
		short ocData; // Pointer to DATA structure
		short ocFree;
		int ocOEFlags; // New flags

		fixed short ocQualifiers[8]; // Qualifier list
		short ocExtension; // Extension structure 
		short ocValues; // Values structure
		short ocStrings; // String structure

		short ocFlags2; // New news flags, before was ocEvents
		short ocOEPrefs; // Automatically modifiable flags
		int ocIdentifier; // Identifier d'objet

		int ocBackColor; // Background color
		int ocFadeIn; // Offset fade in 
		int ocFadeOut; // Offset fade out 
		int ocValueNames; // For the debugger
		int ocStringNames;
	}

	public struct ObjInfoHeader
	{
		short oiHandle;
		short oiType;
		short oiFlags; // Memory flags
		short oiReserved; // No longer used
		int oiInkEffect; // Ink effect
		int oiInkEffectParam; // Ink effect param
	}

	public struct AppMiniHeader
	{
		public unsafe fixed byte gaDWType[4]; // "PAME"
		public short gaVersion; // Version number
		public short gaSubVersion; // Sub-version number
		public int gaPrdVersion; // MMF version
		public int gaPrdBuild; // MMF build number
	}

	public unsafe struct AppHeader
	{
		public int gaSize; // Structure size
		public short gaFlags; // Flags
		public short gaNewFlags; // New flags
		public short gaMode; // graphic mode
		public short gaOtherFlags; // Other Flags
		public short gaCxWin; // Window x-size
		public short gaCyWin; // Window y-size
		public int gaScoreInit; // Initial score
		public int gaLivesInit; // Initial number of lives
		public PlayerCtrls gaPlayerCtrls;
		public int gaBorderColour; // Border colour
		public int gaNbFrames; // Number of frames
		public int gaFrameRate; // Number of frames per second
		public byte gaMDIWindowMenu; // Index of Window menu for MDI applications
		public fixed byte gaFree[3];
	}

	public unsafe struct PlayerCtrls
	{
		public fixed short pcCtrlType[4]; // Control type per player (0=mouse,1=joy1, 2=joy2, 3=keyb)
		public fixed short pcCtrlKeys[32]; // Control keys per player
	}

	public struct MenuHdr
	{
		int mhHdrSize; // == sizeof(MenuHdr)
		int mhMenuOffset; // From start of MenuHdr
		int mhMenuSize;
		int mhAccelOffset; // From start of MenuHdr

		int mhAccelSize;
		// Total data size = mhHdrSize + mhMenuSize + mhAccelSize 
	}

	public unsafe struct FusionBullshit_FunctionPointer
	{
		public void* fPtr;
	}

	public unsafe struct kpj
	{
		FusionBullshit_FunctionPointer CreateRunObject;
		FusionBullshit_FunctionPointer DestroyRunObject;
		FusionBullshit_FunctionPointer HandleRunObject;
		FusionBullshit_FunctionPointer DisplayRunObject;
		FusionBullshit_FunctionPointer GetRunObjectSurface;
		FusionBullshit_FunctionPointer ReInitRunObject;
		FusionBullshit_FunctionPointer PauseRunObject;
		FusionBullshit_FunctionPointer ContinueRunObject;
		FusionBullshit_FunctionPointer PrepareToSave;
		FusionBullshit_FunctionPointer PrepareToSave2;
		FusionBullshit_FunctionPointer GetRunObjectDataSize;
		FusionBullshit_FunctionPointer SaveBackground;
		FusionBullshit_FunctionPointer RestoreBackground;
		FusionBullshit_FunctionPointer ChangeRunData;
		FusionBullshit_FunctionPointer KillBackground;
		FusionBullshit_FunctionPointer GetZoneInfo;
		FusionBullshit_FunctionPointer GetDebugTree;
		FusionBullshit_FunctionPointer GetDebugItem;
		FusionBullshit_FunctionPointer EditDebugItem;
		FusionBullshit_FunctionPointer GetRunObjectFont;
		FusionBullshit_FunctionPointer SetRunObjectFont;
		FusionBullshit_FunctionPointer GetRunObjectTextColor;
		FusionBullshit_FunctionPointer SetRunObjectTextColor;
		FusionBullshit_FunctionPointer GetRunObjectWindow;
		FusionBullshit_FunctionPointer GetRunObjectCollisionMask;
		FusionBullshit_FunctionPointer SaveRunObject;
		FusionBullshit_FunctionPointer LoadRunObject;
		kpxRunInfos infos;
	}

	public unsafe struct TABKPT
	{
		public kpj number1;
		public kpj number2;
	}
}