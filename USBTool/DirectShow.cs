#if MEDIA_DSHOW
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;
using System.Runtime.CompilerServices;

namespace USBTool.DShow
{
    [ComImport,ComVisible(true),Guid("56A868B1-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IMediaControl
    {
        [DispId(1610743808)]
        void Run();
        [DispId(1610743809)]
        void Pause();
        [DispId(1610743810)]
        void Stop();
        [DispId(1610743811)]
        void GetState(int msTimeout, out int pfs);
        [DispId(1610743812)]
        void RenderFile(string strFilename);
        [DispId(1610743813)]
        void AddSourceFilter(string strFilename, out object ppUnk);
        [DispId(1610743816)]
        void StopWhenReady();

        [DispId(1610743814)]
        object FilterCollection { get; }
        [DispId(1610743815)]
        object RegFilterCollection { get; }
    }
    [ComImport, ComVisible(true), Guid("56A868B3-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IBasicAudio
    {
        [DispId(1610743808)]
        int Volume { get; set; }
        [DispId(1610743810)]
        int Balance { get; set; }
    }
    [ComImport, ComVisible(true), Guid("56A868B4-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IVideoWindow
    {
        [DispId(1610743838)]
        void SetWindowForeground(int Focus);
        [DispId(1610743839)]
        void NotifyOwnerMessage([ComAliasName("DirectShow.LONG_PTR")] int hwnd, int uMsg, [ComAliasName("DirectShow.LONG_PTR")] int wParam, [ComAliasName("DirectShow.LONG_PTR")] int lParam);
        [DispId(1610743840)]
        void SetWindowPosition(int Left, int Top, int Width, int Height);
        [DispId(1610743841)]
        void GetWindowPosition(out int pLeft, out int pTop, out int pWidth, out int pHeight);
        [DispId(1610743842)]
        void GetMinIdealImageSize(out int pWidth, out int pHeight);
        [DispId(1610743843)]
        void GetMaxIdealImageSize(out int pWidth, out int pHeight);
        [DispId(1610743844)]
        void GetRestorePosition(out int pLeft, out int pTop, out int pWidth, out int pHeight);
        [DispId(1610743845)]
        void HideCursor(int HideCursor);
        [DispId(1610743846)]
        void IsCursorHidden(out int CursorHidden);

        [DispId(1610743808)]
        string Caption { get; set; }
        [DispId(1610743810)]
        int WindowStyle { get; set; }
        [DispId(1610743812)]
        int WindowStyleEx { get; set; }
        [DispId(1610743814)]
        int AutoShow { get; set; }
        [DispId(1610743816)]
        int WindowState { get; set; }
        [DispId(1610743818)]
        int BackgroundPalette { get; set; }
        [DispId(1610743820)]
        int Visible { get; set; }
        [DispId(1610743822)]
        int Left { get; set; }
        [DispId(1610743824)]
        int Width { get; set; }
        [DispId(1610743826)]
        int Top { get; set; }
        [DispId(1610743828)]
        int Height { get; set; }
        [ComAliasName("DirectShow.LONG_PTR")]
        [DispId(1610743830)]
        int Owner { get; set; }
        [ComAliasName("DirectShow.LONG_PTR")]
        [DispId(1610743832)]
        int MessageDrain { get; set; }
        [DispId(1610743834)]
        int BorderColor { get; set; }
        [DispId(1610743836)]
        int FullScreenMode { get; set; }
    }
    [ComImport, ComVisible(true), Guid("56A868B2-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IMediaPosition
    {
        [DispId(1610743817)]
        int CanSeekForward();
        [DispId(1610743818)]
        int CanSeekBackward();

        [DispId(1610743808)]
        double Duration { get; }
        [DispId(1610743809)]
        double CurrentPosition { get; set; }
        [DispId(1610743811)]
        double StopTime { get; set; }
        [DispId(1610743813)]
        double PrerollTime { get; set; }
        [DispId(1610743815)]
        double Rate { get; set; }
    }
    [ComImport, ComVisible(true), Guid("56A868B6-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IMediaEvent
    {
        [DispId(1610743808)]
        void GetEventHandle([ComAliasName("DirectShow.LONG_PTR")] out int hEvent);
        [DispId(1610743809)]
        void GetEvent(out int lEventCode, [ComAliasName("DirectShow.LONG_PTR")] out int lParam1, [ComAliasName("DirectShow.LONG_PTR")] out int lParam2, int msTimeout);
        [DispId(1610743810)]
        void WaitForCompletion(int msTimeout, out int pEvCode);
        [DispId(1610743811)]
        void CancelDefaultHandling(int lEvCode);
        [DispId(1610743812)]
        void RestoreDefaultHandling(int lEvCode);
        [DispId(1610743813)]
        void FreeEventParams(int lEvCode, [ComAliasName("DirectShow.LONG_PTR")] int lParam1, [ComAliasName("DirectShow.LONG_PTR")] int lParam2);
    }
    [ComImport, ComVisible(true), Guid("e436ebb3-524f-11ce-9f53-0020af0ba770")]
    public class FilterGraph
    {

    }
    /*
    [ComImport, ComVisible(true), Guid("E436EBB3-524F-11CE-9F53-0020AF0BA770")]
    [TypeLibType(TypeLibTypeFlags.FCanCreate)]
    [ClassInterface(ClassInterfaceType.None)]
    public class  FilterGraph : IFilterGraph , IMediaControl ,IBasicAudio , IBasicVideo
    {
        [DispId(1610743808)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void Run();
        [DispId(1610743809)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void Pause();
        [DispId(1610743810)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void Stop();
        [DispId(1610743811)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void GetState(int msTimeout, out int pfs);
        [DispId(1610743812)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void RenderFile(string strFilename);
        [DispId(1610743813)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void AddSourceFilter(string strFilename, out object ppUnk);
        [DispId(1610743816)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void StopWhenReady();
        [DispId(1610743814)]
        public extern virtual object FilterCollection { 
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get; }
        [DispId(1610743815)]
        public extern virtual object RegFilterCollection {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get; }
        [DispId(1610743829)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void SetSourcePosition(int Left, int Top, int Width, int Height);
        [DispId(1610743830)]
        [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void GetSourcePosition(out int pLeft, out int pTop, out int pWidth, out int pHeight);
        [DispId(1610743831)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void SetDefaultSourcePosition();
        [DispId(1610743832)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void SetDestinationPosition(int Left, int Top, int Width, int Height);
        [DispId(1610743833)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void GetDestinationPosition(out int pLeft, out int pTop, out int pWidth, out int pHeight);
        [DispId(1610743834)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void SetDefaultDestinationPosition();
        [DispId(1610743835)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void GetVideoSize(out int pWidth, out int pHeight);
        [DispId(1610743836)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void GetVideoPaletteEntries(int StartIndex, int Entries, out int pRetrieved, out int pPalette);
        [DispId(1610743837)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void GetCurrentImage(ref int pBufferSize, out int pDIBImage);
        [DispId(1610743838)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void IsUsingDefaultSource();
        [DispId(1610743839)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void IsUsingDefaultDestination();
        [DispId(1610743808)]
        public extern virtual double AvgTimePerFrame {
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; }
        [DispId(1610743809)]
        public extern virtual int BitRate { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; }
        [DispId(1610743810)]
        public extern virtual int BitErrorRate { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; }
        [DispId(1610743811)]
        public extern virtual int VideoWidth { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; }
        [DispId(1610743812)]
        public extern virtual int VideoHeight { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; }
        [DispId(1610743813)]
        public extern virtual int SourceLeft { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)
                ]get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743815)]
        public extern virtual int SourceWidth { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743817)]
        public extern virtual int SourceTop { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743819)]
        public extern virtual int SourceHeight {
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743821)]
        public extern virtual int DestinationLeft {
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743823)]
        public extern virtual int DestinationWidth {
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743825)]
        public extern virtual int DestinationTop { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743827)]
        public extern virtual int DestinationHeight { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743808)]
        public extern virtual int Volume { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743810)]
        public extern virtual int Balance { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743817)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual int CanSeekForward();
        [DispId(1610743818)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual int CanSeekBackward();
        [DispId(1610743808)]
        public extern virtual double Duration { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; }
        [DispId(1610743809)]
        public extern virtual double CurrentPosition { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743811)]
        public extern virtual double StopTime { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743813)]
        public extern virtual double PrerollTime { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743815)]
        public extern virtual double Rate { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743838)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void SetWindowForeground(int Focus);
        [DispId(1610743839)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void NotifyOwnerMessage([ComAliasName("DirectShow.LONG_PTR")] int hwnd, int uMsg, [ComAliasName("DirectShow.LONG_PTR")] int wParam, [ComAliasName("DirectShow.LONG_PTR")] int lParam);
        [DispId(1610743840)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void SetWindowPosition(int Left, int Top, int Width, int Height);
        [DispId(1610743841)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void GetWindowPosition(out int pLeft, out int pTop, out int pWidth, out int pHeight);
        [DispId(1610743842)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void GetMinIdealImageSize(out int pWidth, out int pHeight);
        [DispId(1610743843)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void GetMaxIdealImageSize(out int pWidth, out int pHeight);
        [DispId(1610743844)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void GetRestorePosition(out int pLeft, out int pTop, out int pWidth, out int pHeight);
        [DispId(1610743845)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void HideCursor(int HideCursor);
        [DispId(1610743846)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern virtual void IsCursorHidden(out int CursorHidden);
        [DispId(1610743808)]
        public extern virtual string Caption { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743810)]
        public extern virtual int WindowStyle { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743812)]
        public extern virtual int WindowStyleEx { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743814)]
        public extern virtual int AutoShow { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743816)]
        public extern virtual int WindowState { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743818)]
        public extern virtual int BackgroundPalette { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743820)]
        public extern virtual int Visible { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743822)]
        public extern virtual int Left { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get;
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743824)]
        public extern virtual int Width { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743826)]
        public extern virtual int Top { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743828)]
        public extern virtual int Height { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [ComAliasName("DirectShow.LONG_PTR")]
        [DispId(1610743830)]
        public extern virtual int Owner { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
             get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [ComAliasName("DirectShow.LONG_PTR")]
        [DispId(1610743832)]
        public extern virtual int MessageDrain { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743834)]
        public extern virtual int BorderColor { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
        [DispId(1610743836)]
        public extern virtual int FullScreenMode { 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            get; 
            [MethodImpl(MethodImplOptions.InternalCall,MethodCodeType = MethodCodeType.Runtime)]
            set; }
    } */
}
#endif