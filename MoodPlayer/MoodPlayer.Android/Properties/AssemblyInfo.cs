using Android;
using Android.App;
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("MoodPlayer.Android")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("MoodPlayer.Android")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Add some common permissions, these can be removed if not needed
[assembly: UsesPermission(Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission(Manifest.Permission.Internet)]
//[assembly: UsesPermission(Manifest.Permission.AccessFineLocation)]
//[assembly: UsesPermission(Manifest.Permission.AccessCoarseLocation)]
//[assembly: UsesPermission(Manifest.Permission.AccessMockLocation)]
//[assembly: UsesPermission(Manifest.Permission.AccessMediaLocation)]
//[assembly: UsesPermission(Manifest.Permission.AccessLocationExtraCommands)]
//[assembly: UsesPermission(Manifest.Permission.AccessBackgroundLocation)]
//[assembly: UsesPermission(Manifest.Permission.LocationHardware)]
//[assembly: UsesPermission(Manifest.Permission.ChangeWifiState)]
//[assembly: UsesPermission(Manifest.Permission.AccessWifiState)]
//[assembly: UsesPermission(Manifest.Permission.ChangeWifiMulticastState)]
[assembly: UsesPermission(Manifest.Permission.ForegroundService)]
//[assembly: UsesPermission(Manifest.Permission.ManageMedia)]
//[assembly: UsesPermission(Manifest.Permission.ModifyAudioSettings)]
//[assembly: UsesPermission(Manifest.Permission.RecordAudio)]
//[assembly: UsesPermission(Manifest.Permission.ManageExternalStorage)]
//[assembly: UsesPermission(Manifest.Permission.ReadExternalStorage)]
//[assembly: UsesPermission(Manifest.Permission.WriteExternalStorage)]
[assembly: UsesPermission(Manifest.Permission_group.Display)]
//[assembly: UsesPermission(Manifest.Permission_group.Microphone)]
[assembly: UsesPermission(Manifest.Permission_group.Network)]
[assembly: UsesPermission(Manifest.Permission_group.Sensors)]
[assembly: UsesPermission(Manifest.Permission_group.Screenlock)]
//[assembly: UsesPermission(Manifest.Permission_group.HardwareControls)]
//[assembly: UsesPermission(Manifest.Permission_group.Storage)]
[assembly: UsesPermission(Manifest.Permission.WakeLock)]

[assembly: Application(UsesCleartextTraffic = true)]