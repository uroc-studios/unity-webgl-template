using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using System.IO;

[InitializeOnLoad]
public static class WebGLTemplateImporter
{
    private const string package_name = "com.uroc.webgl-template";
    private const string target_path = "Assets/WebGLTemplates/UROC";

    static WebGLTemplateImporter()
    {
        Log("Registering Package Event Listeners");
        Events.registeredPackages += OnPackagesRegistered;
        Events.registeringPackages += OnRegisteringPackages;
    }

    private static void Log(string message)
    {
        Debug.Log($"[WebGLTemplateImporter] {message}");
    }
    
    private static void OnPackagesRegistered(PackageRegistrationEventArgs args)
    {
        Log($"OnPackagesRegistered");

        foreach (var package in args.added){
            Log($"[OnPackagesRegistered] Added package: {package.name} ({package.version})");
            if(package.name == package_name){
                OnInstall(package);
            }
        }

        foreach (var package in args.removed){
            Log($"[OnPackagesRegistered] Removed package: {package.name} ({package.version})");
            if(package.name == package_name){
                OnUninstall();
            }
        }

        foreach (var package in args.changedFrom){
            Log($"[OnPackagesRegistered] Changed from package: {package.name} ({package.version})");
        }

        foreach (var package in args.changedTo){
            Log($"[OnPackagesRegistered] Changed to package: {package.name} ({package.version})");
            if(package.name == package_name){
                OnInstall(package);
            }
        }
    }

    private static void OnRegisteringPackages(PackageRegistrationEventArgs args)
    {
        Log($"OnRegisteringPackages");
        foreach (var package in args.added){
            Log($"[OnRegisteringPackages] Added package: {package.name} ({package.version})");
        }

        foreach (var package in args.changedFrom){
            Log($"[OnRegisteringPackages] Changed from package: {package.name} ({package.version})");
        }

        foreach (var package in args.changedTo){
            Log($"[OnRegisteringPackages] Changed to package: {package.name} ({package.version})");
            if(package.name == package_name){
                OnInstall(package);
            }
        }

        foreach (var package in args.removed){
            Log($"[OnRegisteringPackages] Removed package: {package.name} ({package.version})");
            if(package.name == package_name){
                OnUninstall();
            }
        }
    }

    private static void OnInstall(UnityEditor.PackageManager.PackageInfo package){
        Log("OnInstall");
        ClearOutputDirectory();
        Directory.CreateDirectory(target_path);
        var sourcePath = Path.Combine(package.assetPath, "Template");
		FileUtil.CopyFileOrDirectory(sourcePath, targetPath);
        AssetDatabase.Refresh();
        Log($"Successfully copied files to {target_path}");
    }
    
    private static void ClearOutputDirectory()
    {
        Log("ClearOutputDirectory");
        if (!Directory.Exists(target_path))
        {
            Log($"Output Directory does not exist: {target_path}");
            return;
        }

        Log($"Deleting Existing Directory: {target_path}");
        File.Delete($"{target_path}.meta");
        Directory.Delete(target_path, true);
        AssetDatabase.Refresh();
    }

    private static void OnUninstall()
    {
        Log("OnUninstall");
        ClearOutputDirectory();
    }
    
}