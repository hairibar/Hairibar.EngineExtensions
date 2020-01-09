using UnityEngine;
using UnityEditor;

namespace Hairibar.EngineExtensions.Editor
{
    /// <summary>
    /// Adds a menu item that opens the package manifest.
    /// </summary>
    static class OpenManifestButton
    {
        const string MANIFEST_PATH = "Packages/manifest.json";

        [MenuItem("Window/Package Manifest", priority = 1501)]
        static void OpenPackageManifest()
        {
            EditorUtility.OpenWithDefaultApp(MANIFEST_PATH);
        }
    }
}
