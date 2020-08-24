#if MEDIA_FOUNDATION
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace USBTool.MediaFoundation
{
	
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true),ComImport,Guid("90377834-21D0-4dee-8214-BA2E3E6C1127")]
	public interface IMFMediaSession : IUnknown
	{
		//IMFMediaEventGenerator
		int GetEvent(uint dwFlags, out IMFMediaEvent ppEvent);
		int BeginGetEvent(IntPtr pCallback, IUnknown punkState);
		int EndGetEvent(IntPtr pResult, out IMFMediaEvent ppEvent);
		int QueueEvent(uint met, ref Guid guidExtendedType, int hrStatus, ref PropVariant pvValue);
		//IMFMediaSession
		int SetTopology(uint dwSetTopologyFlags,IMFTopology pTopology);
		int ClearTopologies();
		int Start(IntPtr pguidTimeFormat, ref PropVariant pvarStartPosition);
		int Pause();	
		int Stop();
		int Close();
		int Shutdown();
		int GetClock(out IntPtr ppClock);
		int GetSessionCapabilities(out uint pdwCaps);
		int GetFullTopology(uint dwGetFullTopologyFlags,ulong TopoId,IMFTopology ppFullTopology);
	}
	[ComVisible(true), ComImport, Guid("FBE5A32D-A497-4b61-BB85-97B1A848A6E3")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IMFSourceResolver
	{
		int CreateObjectFromURL(string pwszURL,uint dwFlags,IntPtr pProps,out uint pObjectType, out IUnknown ppObject);
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
		int GetSourceNodeCollection(out IntPtr ppCollection);
		int GetOutputNodeCollection(out IntPtr ppCollection);
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct PropVariant
	{
		public ushort vt;
		public ushort wReserved1;
		public ushort wReserved2;
		public ushort wReserved3;
		public IntPtr unionmember;
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("279a808d-aec7-40c8-9c6b-a6b492c78a66")]
	public interface IMFMediaSource : IUnknown
	{
		//IMFMediaEventGenerator
		int GetEvent(uint dwFlags,out IMFMediaEvent ppEvent) ;   
		int BeginGetEvent(IntPtr pCallback,IUnknown punkState);     
		int EndGetEvent(IntPtr pResult,out IMFMediaEvent ppEvent) ; 
		int QueueEvent(uint met,ref Guid guidExtendedType,int hrStatus,ref PropVariant pvValue) ;
		//IMFMediaSource
		int GetCharacteristics(out uint pdwCharacteristics);
		int CreatePresentationDescriptor(out IMFPresentationDescriptor ppPresentationDescriptor);	
		int Start(IMFPresentationDescriptor pPresentationDescriptor,IntPtr pguidTimeFormat,ref PropVariant pvarStartPosition);
		int Stop();
		int Pause();
		int Shutdown();
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("03cb2711-24d7-4db6-a17f-f3a7a479a536")]
	public interface IMFPresentationDescriptor : IUnknown 
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
		void QueryInterface( ref Guid iid,  out IntPtr ppvObj);
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
	public interface IMFStreamDescriptor :IUnknown
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
		int SetOutputPrefType(uint dwOutputIndex,IntPtr pType);       
		int GetOutputPrefType(uint dwOutputIndex,out IntPtr ppType);     
		int SetInputPrefType(uint dwInputIndex,IntPtr pType);       
		int GetInputPrefType(uint dwInputIndex,out IntPtr ppType);
		int CloneFrom(IMFTopologyNode pNode);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("7FEE9E9A-4A89-47a6-899C-B6A53A70FB67")]
	public interface IMFActivate :IUnknown
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
		int IsMediaTypeSupported(IntPtr pMediaType,out IntPtr ppMediaType);    
		int GetMediaTypeCount(out uint pdwTypeCount);    
		int GetMediaTypeByIndex(uint dwIndex,out IntPtr ppType);
		
		int SetCurrentMediaType(IntPtr pMediaType);
		
		int GetCurrentMediaType(out IntPtr ppMediaType);
		
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
	[ComVisible(true), ComImport, Guid("089EDF13-CF71-4338-8D13-9E569DBDC319")]
	public interface IMFSimpleAudioVolume : IUnknown
	{
		int SetMasterVolume(float fLevel);
		int GetMasterVolume(out float pfLevel);
		int SetMute(bool bMute);
		int GetMute(out bool pbMute);
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
	public interface IMFStreamAudioVolume : IUnknown{
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
		int SetVideoPosition(IntPtr pnrcSource, ref  Rectangle prcDest);
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
	public struct  BITMAPINFOHEADER
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
}
#endif