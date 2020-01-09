using UnityEditor;
using UnityEditor.ShortcutManagement;

namespace Hairibar.EngineExtensions.Editor
{
    /// <summary>
    /// Creates a shortcut action that toggles the inspector lock. Mapped to L by default.
    /// </summary>
    static class InspectorLockShortcut
    {
        [Shortcut("Toggle inspector lock.", UnityEngine.KeyCode.L)]
        static void ToggleInspectorLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }
    }
}

