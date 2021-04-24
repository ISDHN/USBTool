using System;
using System.Runtime.InteropServices;
using System.Text;

namespace USBTool.Vds
{
	public struct VDS_SERVICE_PROP
	{
		public string pwszVersion;
		public uint ulFlags;
	}
	public struct VDS_DRIVE_LETTER_PROP
	{
		public char wcLetter;
		public Guid volumeId;
		public uint ulFlags;
		public bool bUsed;
	}
	public struct VDS_FILE_SYSTEM_TYPE_PROP
	{
		public VDS_FILE_SYSTEM_TYPE type;
		public string wszName;
		public uint ulFlags;
		public uint ulCompressionFlags;
		public uint ulMaxLableLength;
		public string pwszIllegalLabelCharSet;
	}
	public struct VDS_PACK_PROP
	{
		public Guid id;
		public string pwszName;
		public VDS_PACK_STATUS status;
		public uint ulFlags;
	}
	public struct VDS_VOLUME_PROP
	{
		public Guid id;
		public VDS_VOLUME_TYPE type;
		public VDS_VOLUME_STATUS status;
		public VDS_HEALTH health;
		public VDS_TRANSITION_STATE TransitionState;
		public uint ullSize;
		public uint ulFlags;
		public VDS_FILE_SYSTEM_TYPE RecommendedFileSystemType;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pwszName;
	}
	public struct VDS_FILE_SYSTEM_PROP
	{
		public VDS_FILE_SYSTEM_TYPE type;
		public Guid volumeId;
		public uint ulFlags;
		public ulong ullTotalAllocationUnits;
		public ulong ullAvailableAllocationUnits;
		public uint ulAllocationUnitSize;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pwszLabel;
	}
	public struct VDS_REPARSE_POINT_PROP
	{
		public Guid SourceVolumeId;
		public string pwszPath;
	}
	public struct VDS_INPUT_DISK
	{
		public Guid diskId;
		public uint ullSize;
		public Guid plexId;
		public uint memberIdx;
	}
	public enum VDS_PACK_STATUS
	{
		VDS_PS_UNKNOWN = 0,
		VDS_PS_ONLINE = 1,
		VDS_PS_OFFLINE = 4
	}
	public enum VDS_TRANSITION_STATE
	{
		VDS_TS_UNKNOWN = 0,
		VDS_TS_STABLE = 1,
		VDS_TS_EXTENDING = 2,
		VDS_TS_SHRINKING = 3,
		VDS_TS_RECONFIGING = 4,
		VDS_TS_RESTRIPING = 5
	}
	public enum VDS_FILE_SYSTEM_TYPE
	{
		VDS_FST_UNKNOWN = 0,
		VDS_FST_RAW = (VDS_FST_UNKNOWN + 1),
		VDS_FST_FAT = (VDS_FST_RAW + 1),
		VDS_FST_FAT32 = (VDS_FST_FAT + 1),
		VDS_FST_NTFS = (VDS_FST_FAT32 + 1),
		VDS_FST_CDFS = (VDS_FST_NTFS + 1),
		VDS_FST_UDF = (VDS_FST_CDFS + 1),
		VDS_FST_EXFAT = (VDS_FST_UDF + 1),
		VDS_FST_CSVFS = (VDS_FST_EXFAT + 1),
		VDS_FST_REFS = (VDS_FST_CSVFS + 1)
	}
	public enum VDS_SERVICE_FLAG
	{
		VDS_SVF_SUPPORT_DYNAMIC = 0x1,
		VDS_SVF_SUPPORT_FAULT_TOLERANT = 0x2,
		VDS_SVF_SUPPORT_GPT = 0x4,
		VDS_SVF_SUPPORT_DYNAMIC_1394 = 0x8,
		VDS_SVF_CLUSTER_SERVICE_CONFIGURED = 0x10,
		VDS_SVF_AUTO_MOUNT_OFF = 0x20,
		VDS_SVF_OS_UNINSTALL_VALID = 0x40,
		VDS_SVF_EFI = 0x80,
		VDS_SVF_SUPPORT_MIRROR = 0x100,
		VDS_SVF_SUPPORT_RAID5 = 0x200,
		VDS_SVF_SUPPORT_REFS = 0x400
	}
	public enum VDS_HEALTH
	{
		VDS_H_UNKNOWN = 0,
		VDS_H_HEALTHY = 1,
		VDS_H_REBUILDING = 2,
		VDS_H_STALE = 3,
		VDS_H_FAILING = 4,
		VDS_H_FAILING_REDUNDANCY = 5,
		VDS_H_FAILED_REDUNDANCY = 6,
		VDS_H_FAILED_REDUNDANCY_FAILING = 7,
		VDS_H_FAILED = 8,
		VDS_H_REPLACED = 9,
		VDS_H_PENDING_FAILURE = 10,
		VDS_H_DEGRADED = 11
	}
	public enum VDS_OBJECT_TYPE
	{
		VDS_OT_UNKNOWN = 0,
		VDS_OT_PROVIDER = 1,
		VDS_OT_PACK = 10,
		VDS_OT_VOLUME = 11,
		VDS_OT_VOLUME_PLEX = 12,
		VDS_OT_DISK = 13,
		VDS_OT_SUB_SYSTEM = 30,
		VDS_OT_CONTROLLER = 31,
		VDS_OT_DRIVE = 32,
		VDS_OT_LUN = 33,
		VDS_OT_LUN_PLEX = 34,
		VDS_OT_PORT = 35,
		VDS_OT_PORTAL = 36,
		VDS_OT_TARGET = 37,
		VDS_OT_PORTAL_GROUP = 38,
		VDS_OT_STORAGE_POOL = 39,
		VDS_OT_HBAPORT = 90,
		VDS_OT_INIT_ADAPTER = 91,
		VDS_OT_INIT_PORTAL = 92,
		VDS_OT_ASYNC = 100,
		VDS_OT_ENUM = 101,
		VDS_OT_VDISK = 200,
		VDS_OT_OPEN_VDISK = 201
	}
	public enum VDS_VOLUME_TYPE
	{
		VDS_VT_UNKNOWN = 0,
		VDS_VT_SIMPLE = 10,
		VDS_VT_SPAN = 11,
		VDS_VT_STRIPE = 12,
		VDS_VT_MIRROR = 13,
		VDS_VT_PARITY = 14
	}
	public enum VDS_PARTITION_STYLE
	{
		VDS_PST_UNKNOWN = 0,
		VDS_PST_MBR = 1,
		VDS_PST_GPT = 2
	}
	public enum VDS_QUERY_PROVIDER_FLAG
	{
		VDS_QUERY_SOFTWARE_PROVIDERS = 0x1,
		VDS_QUERY_HARDWARE_PROVIDERS = 0x2,
		VDS_QUERY_VIRTUALDISK_PROVIDERS = 0x4
	}
	public enum VDS_VOLUME_STATUS
	{
		VDS_VS_UNKNOWN = 0,
		VDS_VS_ONLINE = 1,
		VDS_VS_NO_MEDIA = 3,
		VDS_VS_FAILED = 5,
		VDS_VS_OFFLINE = 4
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("e0393303-90d4-4a97-ab71-e9b671ee2729")]
	public interface IVdsServiceLoader 
	{
		uint LoadService(string pwszMachineName,out IVdsService ppService);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("0818a8ef-9ba9-40d8-a6f9-e22833cc771e")]
	public interface IVdsService
	{
		uint IsServiceReady();       
		uint WaitForServiceReady();       
		uint GetProperties(out VDS_SERVICE_PROP pServiceProp);	
		uint QueryProviders(VDS_QUERY_PROVIDER_FLAG masks,out IEnumVdsObject ppEnum);	
		uint QueryMaskedDisks(out IEnumVdsObject ppEnum);		
		uint QueryUnallocatedDisks(out IEnumVdsObject ppEnum);		
		uint GetObject(Guid ObjectId,VDS_OBJECT_TYPE type,out object ppObjectUnk);		
		uint QueryDriveLetters(char wcFirstLetter,uint count,out VDS_DRIVE_LETTER_PROP[] pDriveLetterPropArray);	
		uint QueryFileSystemTypes(out VDS_FILE_SYSTEM_TYPE_PROP[] ppFileSystemTypeProps, out int plNumberOfFileSystems);	
		uint Reenumerate();
		uint Refresh();
		uint CleanupObsoleteMountPoints();
		uint Advise(object pSink,out uint pdwCookie);
		uint Unadvise(uint dwCookie);
		uint Reboot();
		uint SetFlags(uint ulFlags);		
		uint ClearFlags(uint ulFlags);		
	};
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("118610b7-8d94-4030-b5b8-500889788e4e")]
	public interface IEnumVdsObject
	{
		uint Next(uint celt,out IUnknown ppObjectArray,out uint pcFetched);
		uint Skip(uint celt);       
		uint Reset();     
		uint Clone(out IEnumVdsObject ppEnum);
		
	};
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("9aa58360-ce33-4f92-b658-ed24b14425b8")]
	public interface IVdsSwProvider 
	{
		uint QueryPacks(out IEnumVdsObject ppEnum);

		uint CreatePack(out IVdsPack ppPack);
		
	};
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("3b69d7f5-9d94-4648-91ca-79939ba263bf")]
	public interface IVdsPack 
	{
		uint GetProperties(out VDS_PACK_PROP pPackProp);       
		uint GetProvider(out object ppProvider);        
		uint QueryVolumes(out IEnumVdsObject ppEnum);     
		uint QueryDisks(out IEnumVdsObject ppEnum);      
		uint CreateVolume(VDS_VOLUME_TYPE type, ref VDS_INPUT_DISK pInputDiskArray,uint lNumberOfDisks,uint ulStripeSize,out object ppAsync);     
		uint AddDisk(Guid DiskId,VDS_PARTITION_STYLE PartitionStyle,bool bAsHotSpare);     
		uint MigrateDisks(Guid[] pDiskArray,uint lNumberOfDisks,Guid TargetPack,bool bForce, bool bQueryOnly,out uint pResults,out bool pbRebootNeeded);      
		uint ReplaceDisk( Guid OldDiskId,Guid NewDiskId, out object ppAsync);       
		uint RemoveMissingDisk(Guid DiskId); 
		uint Recover(out object ppAsync);       
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("88306bb2-e71f-478c-86a2-79da200a0f11")]
	public interface IVdsVolume 
	{
		uint GetProperties(ref VDS_VOLUME_PROP pVolumeProperties);       
		uint GetPack(out IVdsPack ppPack); 
		uint QueryPlexes(out IEnumVdsObject ppEnum);		
		uint Extend(VDS_INPUT_DISK[] pInputDiskArray, int lNumberOfDisks, out IUnknown ppAsync);		
		uint Shrink( uint ullNumberOfBytesToRemove,out IUnknown ppAsync);		
		uint AddPlex(Guid VolumeId,out IUnknown ppAsync);		
		uint BreakPlex(Guid plexId,out IUnknown ppAsync);		
		uint RemovePlex(Guid plexId,out IUnknown ppAsync);
		uint Delete(bool bForce);
		uint SetFlags(uint ulFlags,bool bRevertOnClose);		
		uint ClearFlags(uint ulFlags);		
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("ee2d5ded-6236-4169-931d-b9778ce03dc6")]
	public interface IVdsVolumeMF
    {
        uint GetFileSystemProperties(ref VDS_FILE_SYSTEM_PROP pFileSystemProp);     
        uint Format(VDS_FILE_SYSTEM_TYPE type,string pwszLabel,uint dwUnitAllocationSize,bool bForce,bool bQuickFormat,bool bEnableCompression,out IntPtr ppAsync); 
        uint AddAccessPath(string pwszPath);       
        uint QueryAccessPaths([MarshalAs(UnmanagedType.LPWStr)]StringBuilder pwszPathArray, out int plNumberOfAccessPaths);        
        uint QueryReparsePoints(out VDS_REPARSE_POINT_PROP[] ppReparsePointProps,out int plNumberOfReparsePointProps);        
        uint DeleteAccessPath(string pwszPath,bool bForce);       
        uint Mount();   
        uint Dismount(bool bForce, bool bPermanent);       
        uint SetFileSystemFlags(uint ulFlags);       
        uint ClearFileSystemFlags(uint ulFlags);
        
    }
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("d5d23b6d-5a55-4492-9889-397a3c2d2dbc")]
	public interface IVdsAsync
	{
		uint Cancel();
		uint Wait(out uint pHrResult, out IntPtr pAsyncOut);
		uint QueryStatus(out uint pHrResult, out uint pulPercentCompleted);
	}
}