using System;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Reflection;

[InitializeOnLoad]
public static class WebGLTemplateImporter
{
    private const string package_name = "com.uroc.webgl-template";
    private const string target_path = "Assets/WebGLTemplates/UROC";

    static WebGLTemplateImporter()
    {
        Log("Registering Package Event Listeners");
        // Events.registeringPackages += OnRegisteringPackages;
        Events.registeredPackages += OnPackagesRegistered;
    }

    private static void Log(string message)
    {
        
        Debug.Log($"[WebGLTemplateImporter] {message}");
    }
    
    private static void OnPackagesRegistered(PackageRegistrationEventArgs args)
    {
        foreach (var package in args.added){
            // Log($"Added package: {package.name} ({package.version})");
            if(package.name == package_name){
                OnInstall(package);
            }
        }

        foreach (var package in args.removed){
            // Log($"Removed package: {package.name}");
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
        FileUtil.CopyFileOrDirectory($"{sourcePath}/index.html", $"{target_path}/index.html");
        FileUtil.CopyFileOrDirectory($"{sourcePath}/thumbnail.png", $"{target_path}/thumbnail.png");
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
        Directory.Delete(target_path, true);
        AssetDatabase.Refresh();
    }

    private static void OnUninstall()
    {
        Log("OnUninstall");
        ClearOutputDirectory();
    }
    
    // private static void OnRegisteringPackages(PackageRegistrationEventArgs args)
    // {
    //     Debug.Log("OnRegisteringPackages called");
    //     // Guard Clause: No changes, so do nothing.
    //     if (args.added.Count == 0 && args.removed.Count == 0) return;
    //
    //     // Handle package removal before the package is uninstalled
    //     var removedPackage = args.removed.FirstOrDefault(p => p.name == package_name);
    //     if (removedPackage != null)
    //     {
    //         Debug.Log($"Removing {package_name}. Deleting copied files from {target_path}");
    //         
    //         // Guard Clause: Don't try to delete if the directory doesn't exist.
    //         if (!Directory.Exists(target_path)) return;
    //
    //         Directory.Delete(target_path, true);
    //         AssetDatabase.Refresh();
    //         Debug.Log($"Successfully deleted folder at {target_path}");
    //         return; // Exit as the purpose was removal
    //     }
    //
    //     // Handle package installation or update
    //     var addedPackage = args.added.FirstOrDefault(p => p.name == package_name);
    //     if (addedPackage != null)
    //     {
    //         OnInstall(addedPackage);
    //     }
    // }
    
}