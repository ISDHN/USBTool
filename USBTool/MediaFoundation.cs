using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

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
	[ComVisible(true), ComImport, Guid("90377834-21D0-4dee-8214-BA2E3E6C1127")]
	public interface IMFMediaSession
	{
		//IMFMediaEventGenerator
		[PreserveSig]
		int GetEvent(uint dwFlags, out IMFMediaEvent ppEvent);
		int BeginGetEvent(IUnknown pCallback, IUnknown punkState);
		int EndGetEvent(IUnknown pResult, out IMFMediaEvent ppEvent);
		int QueueEvent(uint met, ref Guid guidExtendedType, int hrStatus, ref PropVariant pvValue);
		//IMFMediaSession
		int SetTopology(uint dwSetTopologyFlags, IMFTopology pTopology);
		int ClearTopologies();
		int Start(ref Guid pguidTimeFormat, ref PropVariant pvarStartPosition);
		int Pause();
		int Stop();
		int Close();
		int Shutdown();
		int GetClock(out IUnknown ppClock);
		int GetSessionCapabilities(out uint pdwCaps);
		int GetFullTopology(uint dwGetFullTopologyFlags, ulong TopoId, IMFTopology ppFullTopology);
	}

	[ComVisible(true), ComImport, Guid("FBE5A32D-A497-4b61-BB85-97B1A848A6E3")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IMFSourceResolver
	{
		int CreateObjectFromURL(string pwszURL, uint dwFlags, IUnknown pProps, out uint pObjectType, out IUnknown ppObject);
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
		int GetNode(ushort wIndex, out IMFTopologyNode ppNode);
		int Clear();
		int CloneFrom(IMFTopology pTopology);
		int GetNodeByID(ulong qwTopoNodeID, out IMFTopologyNode ppNode);
		int GetSourceNodeCollection(out IUnknown ppCollection);
		int GetOutputNodeCollection(out IUnknown ppCollection);
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("279a808d-aec7-40c8-9c6b-a6b492c78a66")]
	public interface IMFMediaSource
	{
		//IMFMediaEventGenerator
		int GetEvent(uint dwFlags, out IMFMediaEvent ppEvent);
		int BeginGetEvent(IUnknown pCallback, IUnknown punkState);
		int EndGetEvent(IUnknown pResult, out IMFMediaEvent ppEvent);
		int QueueEvent(uint met, ref Guid guidExtendedType, int hrStatus, ref PropVariant pvValue);
		//IMFMediaSource
		int GetCharacteristics(out uint pdwCharacteristics);
		int CreatePresentationDescriptor(out IMFPresentationDescriptor ppPresentationDescriptor);
		int Start(IMFPresentationDescriptor pPresentationDescriptor, IUnknown pguidTimeFormat, ref PropVariant pvarStartPosition);
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
		int GetStreamDescriptorByIndex(uint dwIndex, out bool pfSelected, out IMFStreamDescriptor ppDescriptor);
		int SelectStream(uint dwDescriptorIndex);
		int DeselectStream(uint dwDescriptorIndex);
		int Clone(out IMFPresentationDescriptor ppPresentationDescriptor);
	}

	

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("2cd2d921-c447-44a7-a13c-4adabfc247e3")]
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
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("56c03d9c-9dbb-45f5-ab4b-d80f47c05938")]
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
		int ConnectOutput(uint dwOutputIndex, IMFTopologyNode pDownstreamNode, uint dwInputIndexOnDownstreamNode);
		int DisconnectOutput(uint dwOutputIndex);
		int GetInput(uint dwInputIndex, out IMFTopologyNode ppUpstreamNode, out uint pdwOutputIndexOnUpstreamNode);
		int GetOutput(uint dwOutputIndex, out IMFTopologyNode ppDownstreamNode, out uint pdwInputIndexOnDownstreamNode);
		int SetOutputPrefType(uint dwOutputIndex, IUnknown pType);
		int GetOutputPrefType(uint dwOutputIndex, out IUnknown ppType);
		int SetInputPrefType(uint dwInputIndex, IUnknown pType);
		int GetInputPrefType(uint dwInputIndex, out IUnknown ppType);
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
		int ActivateObject(ref Guid riid, out IUnknown ppv);
		int ShutdownObject();
		int DetachObject();
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("e93dcf6c-4b07-4e1e-8123-aa16ed6eadf5")]
	public interface IMFMediaTypeHandler
	{
		int IsMediaTypeSupported(IUnknown pMediaType, out IUnknown ppMediaType);
		int GetMediaTypeCount(out uint pdwTypeCount);
		int GetMediaTypeByIndex(uint dwIndex, out IUnknown ppType);

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
	[ComVisible(true), ComImport, Guid("88ddcd21-03c3-4275-91ed-55ee3929328f")]
	public interface IMFRateControl
	{
		int SetRate(bool fThin, float flRate);
		int GetRate(ref bool pfThin, ref float pflRate);
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("a490b1e4-ab84-4d31-a1b2-181e03b1077a")]
	public interface IMFVideoDisplayControl
	{
		int GetNativeVideoSize(ref Size pszVideo, ref Size pszARVideo);
		int GetIdealVideoSize(ref Size pszMin, ref Size pszMax);
		int SetVideoPosition(IUnknown pnrcSource, ref Rectangle prcDest);
		int GetVideoPosition(out RectangleF pnrcSource, out Rectangle prcDest);
		int SetAspectRatioMode(uint dwAspectRatioMode);
		int GetAspectRatioMode(out uint pdwAspectRatioMode);
		int SetVideoWindow(IntPtr hwndVideo);
		int GetVideoWindow(out IntPtr phwndVideo);
		int RepaintVideo();
		int GetCurrentImage(ref BITMAPINFOHEADER pBih, out Bitmap pDib, out uint pcbDib, ref long pTimeStamp);
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
}