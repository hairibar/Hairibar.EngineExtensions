using System;
using UnityEngine.SceneManagement;

namespace Hairibar.EngineExtensions
{
    public static class SceneManagerExtensions
    {
        /// <summary>
        /// Calls SceneManager.LoadScene(), and calls the given Action as a callback on ActiveSceneChanged.
        /// </summary>
        public static void LoadScene(string scene, LoadSceneMode loadSceneMode, Action onSceneActiveCallback)
        {
            SceneManager.LoadScene(scene, loadSceneMode);

            SceneManager.activeSceneChanged += CallbackInvoker;

            void CallbackInvoker(Scene previousScene, Scene newScene)
            {
                SceneManager.activeSceneChanged -= CallbackInvoker;
                onSceneActiveCallback();
            }
        }
    }
}