#if MEDIA_FOUNDATION
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace USBTool.MediaFoundation
{
	[StructLayout(LayoutKind.Sequential)]
	public struct BITMAPINFOHEADER
	{
		public uint biSize;
		public int biWidth;
		public int biHeight;
		public ushort biPlanes;
		public ushort biBitCount;
		public uint biCompression;
		public uint biSizeImage;
		public int biXPelsPerMeter;
		public int biYPelsPerMeter;
		public uint biClrUsed;
		public uint biClrImportant;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct PropVariant
	{
		public ushort vt;
		public ushort wReserved1;
		public ushort wReserved2;
		public ushort wReserved3;
		public long unionmember;
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true),ComImport,Guid("90377834-21D0-4dee-8214-BA2E3E6C1127")]
	public interface IMFMediaSession 
	{
		//IMFMediaEventGenerator
		int GetEvent(uint dwFlags, out IMFMediaEvent ppEvent);
		[PreserveSig]
		int BeginGetEvent(IMFAsyncCallback pCallback, IUnknown punkState);
		int EndGetEvent(IUnknown pResult, out IMFMediaEvent ppEvent);
		int QueueEvent(uint met, ref Guid guidExtendedType, int hrStatus, ref PropVariant pvValue);
		//IMFMediaSession
		int SetTopology(uint dwSetTopologyFlags,IMFTopology pTopology);
		int ClearTopologies();
		int Start(ref Guid pguidTimeFormat, ref PropVariant pvarStartPosition);
		int Pause();	
		int Stop();
		int Close();
		int Shutdown();
		int GetClock(out IUnknown ppClock);
		int GetSessionCapabilities(out uint pdwCaps);
		int GetFullTopology(uint dwGetFullTopologyFlags,ulong TopoId,IMFTopology ppFullTopology);
	}
	[ComVisible(true), ComImport, Guid("FBE5A32D-A497-4b61-BB85-97B1A848A6E3")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IMFSourceResolver
	{
		int CreateObjectFromURL(string pwszURL,uint dwFlags,IUnknown pProps,out uint pObjectType, out IUnknown ppObject);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("83CF873A-F6DA-4bc8-823F-BACFD55DC433")]
	public interface IMFTopology
	{
		//IMFAttributes
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
		//IMFTopology
		int GetTopologyID(out ulong pID);      
		int AddNode(IMFTopologyNode pNode);
		int RemoveNode(IMFTopologyNode pNode);        
		int GetNodeCount(out ushort pwNodes);  
		int GetNode(ushort wIndex,out IMFTopologyNode ppNode);      
		int Clear();    
		int CloneFrom(IMFTopology pTopology);
		int GetNodeByID(ulong qwTopoNodeID,out IMFTopologyNode ppNode);
		int GetSourceNodeCollection(out IUnknown ppCollection);
		int GetOutputNodeCollection(out IUnknown ppCollection);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("279a808d-aec7-40c8-9c6b-a6b492c78a66")]
	public interface IMFMediaSource 
	{
		//IMFMediaEventGenerator
		int GetEvent(uint dwFlags,out IMFMediaEvent ppEvent) ;   
		int BeginGetEvent(IMFAsyncCallback pCallback,IUnknown punkState);     
		int EndGetEvent(IUnknown pResult,out IMFMediaEvent ppEvent) ; 
		int QueueEvent(uint met,ref Guid guidExtendedType,int hrStatus,ref PropVariant pvValue) ;
		//IMFMediaSource
		int GetCharacteristics(out uint pdwCharacteristics);
		int CreatePresentationDescriptor(out IMFPresentationDescriptor ppPresentationDescriptor);	
		int Start(IMFPresentationDescriptor pPresentationDescriptor,IUnknown pguidTimeFormat,ref PropVariant pvarStartPosition);
		int Stop();
		int Pause();
		int Shutdown();
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("03cb2711-24d7-4db6-a17f-f3a7a479a536")]
	public interface IMFPresentationDescriptor  
	{
		//IMFAttributes
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
		//IMFPresentationDescriptor
		int GetStreamDescriptorCount(out uint pdwDescriptorCount);
		int GetStreamDescriptorByIndex(uint dwIndex,out bool pfSelected,out IMFStreamDescriptor ppDescriptor);
		int SelectStream(uint dwDescriptorIndex);
		int DeselectStream(uint dwDescriptorIndex);
		int Clone(out IMFPresentationDescriptor ppPresentationDescriptor);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true),ComImport, Guid("00000000-0000-0000-C000-000000000046")]
	public interface IUnknown
	{
		int QueryInterface(ref Guid iid,  out IUnknown ppvObj);
		int AddRef();
		int Release();
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true),ComImport,  Guid("2cd2d921-c447-44a7-a13c-4adabfc247e3")]
	public interface IMFAttributes
	{
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In,Out]ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In,Out]ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In,Out]ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true),ComImport,Guid("56c03d9c-9dbb-45f5-ab4b-d80f47c05938")]
	public interface IMFStreamDescriptor 
	{
		//IMFAttributes
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
		//IMFStreamDescriptor
		int GetStreamIdentifier(out uint pdwStreamIdentifier);
		int GetMediaTypeHandler(out IMFMediaTypeHandler ppMediaTypeHandler);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("83CF873A-F6DA-4bc8-823F-BACFD55DC430")]
	public interface IMFTopologyNode
	{
		//IMFAttributes
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
		//IMFTopologyNode
		int SetObject(IUnknown pObject);   
		int GetObject(out IUnknown ppObject);      
		int GetNodeType(out uint pType);       
		int GetTopoNodeID(out ulong pID);        
		int SetTopoNodeID(ulong ullTopoID);      
		int GetInputCount(out uint pcInputs);        
		int GetOutputCount(out uint pcOutputs);       
		int ConnectOutput(uint dwOutputIndex,IMFTopologyNode pDownstreamNode,uint dwInputIndexOnDownstreamNode);       
		int DisconnectOutput(uint dwOutputIndex);      
		int GetInput(uint dwInputIndex,out IMFTopologyNode ppUpstreamNode,out uint pdwOutputIndexOnUpstreamNode);       
		int GetOutput(uint dwOutputIndex,out IMFTopologyNode ppDownstreamNode,out uint pdwInputIndexOnDownstreamNode);       
		int SetOutputPrefType(uint dwOutputIndex,IUnknown pType);       
		int GetOutputPrefType(uint dwOutputIndex,out IUnknown ppType);     
		int SetInputPrefType(uint dwInputIndex,IUnknown pType);       
		int GetInputPrefType(uint dwInputIndex,out IUnknown ppType);
		int CloneFrom(IMFTopologyNode pNode);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("7FEE9E9A-4A89-47a6-899C-B6A53A70FB67")]
	public interface IMFActivate 
	{
		//IMFAttributes
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
		//IMFActivate
		int ActivateObject(ref Guid riid,out IUnknown ppv);  
		int ShutdownObject();  
		int DetachObject();
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("e93dcf6c-4b07-4e1e-8123-aa16ed6eadf5")]
	public interface IMFMediaTypeHandler
	{
		int IsMediaTypeSupported(IUnknown pMediaType,out IUnknown ppMediaType);    
		int GetMediaTypeCount(out uint pdwTypeCount);    
		int GetMediaTypeByIndex(uint dwIndex,out IUnknown ppType);
		
		int SetCurrentMediaType(IUnknown pMediaType);
		
		int GetCurrentMediaType(out IUnknown ppMediaType);
		
		int GetMajorType(out Guid pguidMajorType);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("DF598932-F10C-4E39-BBA2-C308F101DAA3")]
	public interface IMFMediaEvent
	{
		//IMFAttributes
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
		//IMFMediaEvent
		int GetType(out uint pmet);     
		int GetExtendedType(out Guid pguidExtendedType);
		int GetStatus(out int phrStatus);
		int GetValue(out PropVariant pvValue);		
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("0a9ccdbc-d797-4563-9667-94ec5d79292d")]
	public interface IMFRateSupport
	{
		int GetSlowestRate(uint eDirection,bool fThin, out float pflRate);
		int GetFastestRate(uint eDirection,bool fThin,out float pflRate);	
		int IsRateSupported(bool fThin, float flRate,out float pflNearestSupportedRate);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("88ddcd21-03c3-4275-91ed-55ee3929328f")]
	public interface IMFRateControl 
	{
		int SetRate(bool fThin,float flRate);
		int GetRate(ref bool pfThin,ref float pflRate);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("76B1BBDB-4EC8-4f36-B106-70A9316DF593")]
	public interface IMFStreamAudioVolume 
	{
		int GetChannelCount(out uint pdwCount);
		int SetChannelVolume(uint dwIndex, float fLevel);
		int GetChannelVolume(uint dwIndex,out float pfLevel);
		int SetAllVolumes(uint dwCount,ref float[] pfVolumes);
		int GetAllVolumes(uint dwCount,out float[] pfVolumes);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("a490b1e4-ab84-4d31-a1b2-181e03b1077a")]
	public interface IMFVideoDisplayControl 
	{
		int GetNativeVideoSize(ref Size pszVideo,ref Size pszARVideo);        
		int GetIdealVideoSize(ref Size pszMin,ref Size pszMax);       
		int SetVideoPosition(IUnknown pnrcSource,ref Rectangle prcDest);
		int GetVideoPosition( out RectangleF pnrcSource, out Rectangle prcDest);      
		int SetAspectRatioMode(uint dwAspectRatioMode);
		int GetAspectRatioMode(out uint pdwAspectRatioMode);
		int SetVideoWindow(IntPtr hwndVideo);
		int GetVideoWindow(out IntPtr phwndVideo);
		int RepaintVideo();
		int GetCurrentImage(ref BITMAPINFOHEADER pBih, out Bitmap pDib,out uint pcbDib,ref long pTimeStamp);		
		int SetBorderColor(uint Clr);
		int GetBorderColor(out uint pClr);
		int SetRenderingPrefs(uint dwRenderFlags);
		int GetRenderingPrefs(out uint pdwRenderFlags);
		int SetFullscreen(bool fFullscreen);
		int GetFullscreen(out bool pfFullscreen);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("a0638c2b-6465-4395-9ae7-a321a9fd2856")]
	public interface IMFAudioPolicy 
	{
		int SetGroupingParam(ref Guid rguidClass);
		int GetGroupingParam(out Guid pguidClass);      
		int SetDisplayName(string pszName);
		int GetDisplayName(out string pszName);
		int SetIconPath(string pszPath);
		int GetIconPath(out string pszPath);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("a27003cf-2354-4f2a-8d6a-ab7cff15437e")]
	public interface  IMFAsyncCallback{ 
		uint GetParameters(ref uint pdwFlags,ref uint pdwQueue);
		uint Invoke(IUnknown pAsyncResult);
	}
	public enum MFP_MEDIAPLAYER_STATE
	{
		EMPTY = 0,
		STOPPED = 0x1,
		PLAYING = 0x2,
		PAUSED = 0x3,
		SHUTDOWN = 0x4
	}
	public struct MFP_EVENT_HEADER
	{
		public uint eEventType;
		public int hrEvent;
		public IMFPMediaPlayer pMediaPlayer;
		public MFP_MEDIAPLAYER_STATE eState;
		public IUnknown pPropertyStore;
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("A714590A-58AF-430a-85BF-44F5EC838D85")]
	public interface IMFPMediaPlayer :  IUnknown
	{
		int Play();      
		int Pause();        
		int Stop();       
		int FrameStep();      
		int SetPosition(ref Guid guidPositionType,ref PropVariant pvPositionValue);    
		int GetPosition(ref Guid guidPositionType,out PropVariant pvPositionValue);       
		int GetDuration(ref Guid guidPositionType,out PropVariant pvDurationValue);     
		int SetRate(float flRate);     
		int GetRate(out  float pflRate);    
		int GetSupportedRates(bool fForwardDirection,out  float pflSlowestRate,out  float pflFastestRate);       
		int GetState(out MFP_MEDIAPLAYER_STATE peState);       
		int CreateMediaItemFromURL(string pwszURL,bool fSync,int dwUserData, out IMFPMediaItem ppMediaItem);     
		int CreateMediaItemFromObject(IUnknown pIUnknownObj, bool fSync,ulong dwUserData,out IMFPMediaItem ppMediaItem);    
		int SetMediaItem(IMFPMediaItem pIMFPMediaItem);      
		int ClearMediaItem();  
		int GetMediaItem(out IMFPMediaItem ppIMFPMediaItem);   
		int GetVolume(out  float pflVolume);      
		int SetVolume(float flVolume);    
		int GetBalance(out float pflBalance);
		int SetBalance(float flBalance);
		int GetMute(out bool pfMute);    
		int SetMute(bool fMute);      
		int GetNativeVideoSize(out Size pszVideo,out Size pszARVideo);      
		int GetIdealVideoSize(out Size pszMin,out Size pszMax);
		int SetVideoSourceRect(ref RectangleF pnrcSource);      
		int GetVideoSourceRect(out RectangleF pnrcSource);       
		int SetAspectRatioMode( uint dwAspectRatioMode);     
		int GetAspectRatioMode(out uint pdwAspectRatioMode);   
		int GetVideoWindow(out IntPtr phwndVideo);      
		int UpdateVideo();   
		int SetBorderColor(uint Clr);   
		int GetBorderColor(out uint pClr);       
		int InsertEffect(IUnknown pEffect,bool fOptional);      
		int RemoveEffect(IUnknown pEffect);
		int RemoveAllEffects();
		int Shutdown();
	};
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("90EB3E6B-ECBF-45cc-B1DA-C6FE3EA70D57")]
	public interface IMFPMediaItem :  IUnknown
	{
		int GetMediaPlayer(out IMFPMediaPlayer ppMediaPlayer);     
		int GetURL(out string ppwszURL);   
		int GetObject(out IUnknown ppIUnknown);      
		int GetUserData(out uint pdwUserData);
		int SetUserData(ulong dwUserData);       
		int GetStartStopPosition(out Guid pguidStartPositionType,out PropVariant pvStartValue,out Guid pguidStopPositionType,out PropVariant pvStopValue); 
		int SetStartStopPosition(ref Guid pguidStartPositionType,ref PropVariant pvStartValue,ref Guid pguidStopPositionType,ref PropVariant pvStopValue);       
		int HasVideo(out bool pfHasVideo,out bool pfSelected);
		int HasAudio(out bool pfHasAudio,out bool pfSelected);   
		int IsProtected(out bool pfProtected);
		int GetDuration(ref Guid guidPositionType,out PropVariant pvDurationValue);     
		int GetNumberOfStreams(out uint pdwStreamCount);      
		int GetStreamSelection( uint dwStreamIndex,out bool pfEnabled);       
		int SetStreamSelection( uint dwStreamIndex, bool fEnabled);  
		int GetStreamAttribute( uint dwStreamIndex,ref Guid guidMFAttribute,out PropVariant pvValue);     
		int GetPresentationAttribute(ref Guid guidMFAttribute,out PropVariant pvValue);
		int GetCharacteristics(out uint pCharacteristics);       
		int SetStreamSink(uint dwStreamIndex,IUnknown pMediaSink);
		int GetMetadata(out IUnknown ppMetadataStore);
		
	};
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("766C8FFB-5FDB-4fea-A28D-B912996F51BD")]
	public interface IMFPMediaPlayerCallback
	{
		void OnMediaPlayerEvent(ref MFP_EVENT_HEADER pEventHeader);       
	};
	public class MFPMediaPlayerCallback : IMFPMediaPlayerCallback
	{
		public void OnMediaPlayerEvent(ref MFP_EVENT_HEADER pEventHeader)
		{

		}
	}
}
#endif