#if MEDIA_FOUNDATION
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using USBTool.MediaFoundation;
namespace USBTool
{
	[ComVisible(true),ClassInterface(ClassInterfaceType.AutoDual)]
	class MediaManager : IMFAsyncCallback
	{
		public uint GetParameters(ref uint pdwFlags, ref uint pdwQueue)
		{
			return E_NOTIMPL;
		}
		public uint Invoke(IUnknown pAsyncResult)
		{
			int hr;
			mediaSession.EndGetEvent(pAsyncResult, out IMFMediaEvent pEvent);
			pEvent.GetType(out uint eventtype);
			pEvent = null;
			if (eventtype == MESessionClosed)
			{
				VideoWnd.Hide();
			}
			else
			{
				mediaSession.BeginGetEvent(this, null);
			}
			switch (eventtype)
			{
				case MESessionEnded:
					VideoWnd.Hide();
					state = "Ended";
					break;
				case MESessionTopologyStatus:
					TopologyPrepared = true;
					break;

			}
			return 0;
		}
		public Guid MF_TOPOLOGY_RESOLUTION_STATUS = new Guid(0x494bbcde, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
		public Guid MF_TOPONODE_SOURCE = Guid.Parse("835c58ec-e075-4bc7-bcba-4de000df9ae6");
		public Guid MF_TOPONODE_PRESENTATION_DESCRIPTOR = Guid.Parse("835c58ed-e075-4bc7-bcba-4de000df9ae6");
		public Guid MF_TOPONODE_STREAM_DESCRIPTOR = Guid.Parse("835c58ee-e075-4bc7-bcba-4de000df9ae6");
		public Guid MFMediaType_Audio = Guid.Parse("73647561-0000-0010-8000-00AA00389B71");
		public Guid MFMediaType_Video = Guid.Parse("73646976-0000-0010-8000-00AA00389B71");
		public Guid MF_TOPONODE_NOSHUTDOWN_ON_REMOVE = Guid.Parse("14932f9c-9087-4bb4-8412-5167145cbe04");
		public Guid MF_TOPONODE_STREAMID = Guid.Parse("14932f9b-9087-4bb4-8412-5167145cbe04");
		public Guid MR_POLICY_VOLUME_SERVICE = new Guid(0x1abaa2ac, 0x9d3b, 0x47c6, 0xab, 0x48, 0xc5, 0x95, 0x6, 0xde, 0x78, 0x4d);
		public Guid MR_STREAM_VOLUME_SERVICE = Guid.Parse("f8b5fa2f-32ef-46f5-b172-1321212fb2c4");
		public Guid MR_VIDEO_RENDER_SERVICE = new Guid(0x1092a86c, 0xab1a, 0x459a, 0xa3, 0x36, 0x83, 0x1f, 0xbc, 0x4d, 0x11, 0xff);
		public Guid MF_RATE_CONTROL_SERVICE = Guid.Parse("866fa297-b802-4bf8-9dc9-5e3b6a9f53c9");
		public Guid MR_AUDIO_POLICY_SERVICE = new Guid(0x911fd737, 0x6775, 0x4ab0, 0xa6, 0x14, 0x29, 0x78, 0x62, 0xfd, 0xac, 0x88);
		public const uint MF_SDK_VERSION = 0x0001;
		public const uint MF_API_VERSION = 0x0070;
		public const uint MF_VERSION = (MF_SDK_VERSION << 16) | MF_API_VERSION;
		public const uint MFSTARTUP_NOSOCKET = 0x1;
		public const uint MFSTARTUP_FULL = 0x0;
		public const uint MF_RESOLUTION_MEDIASOURCE = 0x00000001;
		public const uint MF_RESOLUTION_BYTESTREAM = 0x00000002;
		public const uint MF_RESOLUTION_CONTENT_DOES_NOT_HAVE_TO_MATCH_EXTENSION_OR_MIME_TYPE = 0x00000010;
		public const uint MF_TOPOLOGY_OUTPUT_NODE = 0x0;
		public const uint MF_TOPOLOGY_SOURCESTREAM_NODE = 0x1;
		public const uint MFSESSION_SETTOPOLOGY_IMMEDIATE = 0x1;
		public const uint MESessionEnded = 107;
		public const uint MESessionTopologyStatus = 111;
		public const uint MESessionTopologySet = 101;
		public const uint MESessionStopped = 105;
		public const uint MESessionClosed = 106;
		public const uint E_NOTIMPL = 0x80004001;
		public const ushort VT_I8 = 20;

		[DllImport("mfplat.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFStartup(uint Version, uint flags);
		[DllImport("mfplat.dll")]
		public static extern int MFShutdown();
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateMediaSession(IntPtr pConfiguration, out IMFMediaSession ppMediaSession);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateSourceResolver(out IMFSourceResolver ppISourceResolver);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateTopology(out IMFTopology ppTopo);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateTopologyNode(uint NodeType, out IMFTopologyNode ppNode);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateVideoRendererActivate(IntPtr hwndVideo, out IMFActivate ppActivate);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateAudioRendererActivate(out IMFActivate ppActivate);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFGetService(IUnknown punkObject, ref Guid guidService, ref Guid riid, out IUnknown ppvObject);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFRequireProtectedEnvironment(IMFPresentationDescriptor pPresentationDescriptor);
		
		public void Play()
		{
			PropVariant prop = new PropVariant()
			{
				vt = VT_I8,
				unionmember = 0,
			};
			if (HasVideo) VideoWnd.Show();
			mediaSession.Start(Guid.Empty, prop);
			state= "Playing";
		}
		
		public int InitializeMedia(string MediaFile, int dwrate, float dwvolume, bool IsFullScreen)
		{
			int hr;
			MFStartup(MF_VERSION, MFSTARTUP_FULL);
			MFCreateMediaSession(IntPtr.Zero, out mediaSession);
			
			MFCreateTopology(out IMFTopology topo);
			MFCreateSourceResolver(out IMFSourceResolver resolver);
			IUnknown unknown;
			try
			{
				resolver.CreateObjectFromURL(MediaFile, MF_RESOLUTION_MEDIASOURCE | MF_RESOLUTION_CONTENT_DOES_NOT_HAVE_TO_MATCH_EXTENSION_OR_MIME_TYPE,
					null, out uint objtype, out unknown);
			}
			catch
			{
				MessageBox.Show("不支持的媒体格式。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return 0;
			}
			IMFMediaSource source = unknown as IMFMediaSource;
			source.CreatePresentationDescriptor(out IMFPresentationDescriptor descriptor);
			if (MFRequireProtectedEnvironment(descriptor) == 0)
			{
				MessageBox.Show("媒体受保护,无法播放。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return 0;
			}
			descriptor.GetStreamDescriptorCount(out uint sdcount);
			for (uint i = 0; i < sdcount; i++)
			{
				descriptor.GetStreamDescriptorByIndex(i, out bool IsSelected, out IMFStreamDescriptor sd);
				if (!IsSelected)
					descriptor.SelectStream(i);
				sd.GetMediaTypeHandler(out IMFMediaTypeHandler typeHandler);
				typeHandler.GetMajorType(out Guid streamtype);
				IMFActivate renderer;
				if (streamtype == MFMediaType_Audio)
				{
					hr = MFCreateAudioRendererActivate(out renderer);
				}
				else if (streamtype == MFMediaType_Video)
				{
					hr = MFCreateVideoRendererActivate(VideoWnd.Handle, out renderer);
					HasVideo = true;
				}
				else
				{
					MessageBox.Show("不支持的媒体格式。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return 0;
				}
				hr = MFCreateTopologyNode(MF_TOPOLOGY_SOURCESTREAM_NODE, out IMFTopologyNode sourcenode);
				sourcenode.SetUnknown(MF_TOPONODE_SOURCE, source as IUnknown);
				sourcenode.SetUnknown(MF_TOPONODE_PRESENTATION_DESCRIPTOR, descriptor as IUnknown);
				sourcenode.SetUnknown(MF_TOPONODE_STREAM_DESCRIPTOR, sd as IUnknown);
				topo.AddNode(sourcenode);
				MFCreateTopologyNode(MF_TOPOLOGY_OUTPUT_NODE, out IMFTopologyNode outputnode);
				outputnode.SetObject(renderer as IUnknown);
				topo.AddNode(outputnode);
				outputnode.SetUINT32(MF_TOPONODE_NOSHUTDOWN_ON_REMOVE, 0);
				hr = sourcenode.ConnectOutput(0, outputnode, 0);
			}
			mediaSession.SetTopology(0, topo);
			Rate = dwrate;
			Volume = dwvolume;
			this.IsFullScreen = IsFullScreen;
			uint eventtype = 0;
			while (eventtype != MESessionTopologyStatus)
			{
				mediaSession.GetEvent(0, out IMFMediaEvent mediaevent);
				mediaevent.GetType(out eventtype);
				mediaevent = null;
			}
			Guid guid_ratecontrol = typeof(IMFRateControl).GUID;
			MFGetService(mediaSession as IUnknown, ref MF_RATE_CONTROL_SERVICE, ref guid_ratecontrol, out IUnknown _ratecontrol);
			IMFRateControl ratecontrol = _ratecontrol as IMFRateControl;
			hr = ratecontrol.SetRate(false, Rate >= 0 ? Rate * 7 / 10 + 1 : 1 / (1 + Rate / -10 * 7));
			try
			{
				Guid guid_volume = typeof(IMFStreamAudioVolume).GUID;
				Guid guid_audiopolicy = typeof(IMFAudioPolicy).GUID;
				hr = MFGetService(mediaSession as IUnknown, ref MR_STREAM_VOLUME_SERVICE, ref guid_volume, out IUnknown _volume);
				hr = MFGetService(mediaSession as IUnknown, ref MR_AUDIO_POLICY_SERVICE, ref guid_audiopolicy, out IUnknown _policy);
				IMFStreamAudioVolume volumecontrol = _volume as IMFStreamAudioVolume;
				IMFAudioPolicy policy = _policy as IMFAudioPolicy;
				volumecontrol.GetChannelCount(out uint channelcount);
				for (uint c = 0; c < channelcount; c++)
					volumecontrol.SetChannelVolume(c, Volume);
				policy.SetDisplayName(" ");
			}
			catch
			{

			}
			try
			{
				Guid guid_videocontrol = typeof(IMFVideoDisplayControl).GUID;
				hr = MFGetService(mediaSession as IUnknown, ref MR_VIDEO_RENDER_SERVICE, ref guid_videocontrol, out IUnknown _videocontrol);
				IMFVideoDisplayControl videocontrol = _videocontrol as IMFVideoDisplayControl;
				VideoWnd.videocontrol = videocontrol;
				if (IsFullScreen)
				{
					VideoWnd.Top = 0;
					VideoWnd.Left = 0;
					VideoWnd.Width = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
					VideoWnd.Height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
				}
				Rectangle videopos = new Rectangle(0, 0, VideoWnd.Width, VideoWnd.Height);
				videocontrol.SetVideoPosition(null, ref videopos);
			}
			catch
			{
			}
			hr = mediaSession.BeginGetEvent((IMFAsyncCallback)this, null);
			return 1;
		}

		public bool HasVideo;
		public Mediashow VideoWnd;
		public IMFMediaSession mediaSession;
		public string state = "";
		private float Volume;
		private int Rate;
		private bool TopologyPrepared;
		private bool IsFullScreen;
	}
}
#endif