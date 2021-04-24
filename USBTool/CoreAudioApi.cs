using System;
using System.Runtime.InteropServices;
using USBTool.MediaFoundation;

namespace USBTool.CoreAudioApi
{
	public enum ERole
	{
		eConsole,
		eMultimedia,
		eCommunications,
		ERole_enum_count
	}

	public enum EDataFlow
	{
		eRender,
		eCapture,
		eAll,
		EDataFlow_enum_count
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
	public interface IAudioEndpointVolume
	{
		uint RegisterControlChangeNotify(IUnknown pNotify);
		uint UnregisterControlChangeNotify(IUnknown pNotify);
		uint GetChannelCount(out uint pnChannelCount);
		uint SetMasterVolumeLevel(float fLevelDB, ref Guid pguidEventContext);
		uint SetMasterVolumeLevelScalar(float fLevel, ref Guid pguidEventContext);
		uint GetMasterVolumeLevel(out float pfLevelDB);
		uint GetMasterVolumeLevelScalar(out float pfLevel);
		uint SetChannelVolumeLevel(uint nChannel, float fLevelDB, ref Guid pguidEventContext);
		uint SetChannelVolumeLevelScalar(uint nChannel, float fLevel, ref Guid pguidEventContext);
		uint GetChannelVolumeLevel(uint nChannel, out float pfLevelDB);
		uint GetChannelVolumeLevelScalar(uint nChannel, out float pfLevel);
		[PreserveSig]
		uint SetMute(bool bMute, ref Guid pguidEventContext);
		uint GetMute(out bool pbMute);
		uint GetVolumeStepInfo(out uint pnStep, out uint pnStepCount);
		uint VolumeStepUp(ref Guid pguidEventContext);
		uint VolumeStepDown(ref Guid pguidEventContext);
		uint QueryHardwareSupport(out uint pdwHardwareSupportMask);
		uint GetVolumeRange(out float pflVolumeMindB, out float pflVolumeMaxdB, out float pflVolumeIncrementdB);
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("D666063F-1587-4E43-81F1-B948E807363F")]
	public interface IMMDevice
	{
		uint Activate(ref Guid iid, uint dwClsCtx, IntPtr pActivationParams, out IUnknown ppInterface);
		uint OpenPropertyStore(uint stgmAccess, out IUnknown ppProperties);
		uint GetId(out string ppstrId);
		uint GetState(out uint pdwState);
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
	public interface IMMDeviceEnumerator
	{
		uint EnumAudioEndpoints(EDataFlow dataFlow, uint dwStateMask, out IUnknown ppDevices);
		uint GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppEndpoint);
		uint GetDevice(string pwstrId, out IMMDevice ppDevice);

		uint RegisterEndpointNotificationCallback(IUnknown pClient);

		uint UnregisterEndpointNotificationCallback(IUnknown pClient);
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("BFA971F1-4D5E-40BB-935E-967039BFBEE4")]
	public interface IAudioSessionManager
	{
		uint GetAudioSessionControl(ref Guid AudioSessionGuid, bool StreamFlags, out IUnknown SessionControl);

		uint GetSimpleAudioVolume(ref Guid AudioSessionGuid, bool StreamFlags, out ISimpleAudioVolume AudioVolume);
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8")]
	public interface ISimpleAudioVolume
	{
		uint SetMasterVolume(float fLevel, ref Guid EventContext);
		uint GetMasterVolume(out float pfLevel);
		uint SetMute(bool bMute, ref Guid EventContext);
		uint GetMute(out bool pbMute);
	}
}