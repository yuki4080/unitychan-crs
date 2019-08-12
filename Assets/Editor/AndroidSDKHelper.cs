#if UNITY_2019_1_OR_NEWER && UNITY_ANDROID && (UNITY_EDITOR_WIN || UNITY_EDITOR_OSX)
using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;

public static class AndroidSDKHelper
{
#if UNITY_EDITOR_WIN
    [MenuItem("Tools/Open android sdk folder as administrator", false, 100)]
    private static void OpenAndroidSdkFolderOnWindows()
    {
        var androidPlaybackEngineDirectory =
            BuildPipeline.GetPlaybackEngineDirectory(BuildTarget.Android, BuildOptions.None);
        var androidSdkTools = Path.Combine(androidPlaybackEngineDirectory, "SDK", "tools", "bin");
        var jdkPath = Path.Combine(androidPlaybackEngineDirectory, "Tools", "OpenJDK", "Windows");
        var cmd = Environment.GetEnvironmentVariable("ComSpec");

        if (!Directory.Exists(androidSdkTools) || !Directory.Exists(jdkPath) || !File.Exists(cmd))
        {
            return;
        }

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = cmd,
                Verb = "runas",
                UseShellExecute = true,
                Arguments = $"/K cd {androidSdkTools} & set \"JAVA_HOME={jdkPath}\""
            }
        };
        process.Start();
    }
#endif

#if UNITY_EDITOR_OSX
    [MenuItem("Tools/Open android sdk folder", false, 100)]
    private static void OpenAndroidSdkFolderOnMac()
    {
        var androidPlaybackEngineDirectory =
            BuildPipeline.GetPlaybackEngineDirectory(BuildTarget.Android, BuildOptions.None);
        var androidSdkTools = Path.Combine(androidPlaybackEngineDirectory, "SDK", "tools", "bin");

        if (!Directory.Exists(androidSdkTools))
        {
            return;
        }

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "open",
                Arguments = $"-a Terminal {androidSdkTools}"
            }
        };
        process.Start();
    }
#endif
}
#endif
