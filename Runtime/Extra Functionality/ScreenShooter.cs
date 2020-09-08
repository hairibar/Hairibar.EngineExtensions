#if SCREEN_CAPTURE_MODULE_PRESENT
using UnityEngine;
using NaughtyAttributes;

namespace Hairibar.EngineExtensions
{
    /// <summary>
    /// Provides screenshotting functionality for development. Not advisable for game releases.
    /// </summary>
    [ExecuteAlways, RemoveInRelease]
    public class ScreenShooter : MonoBehaviour
    {
        [Range(0, 1)]
        public float timeScale = 1;
        public KeyCode screenshotKey = KeyCode.P;
        [Range(1, 8)]
        public int supersize = 1;

        private float originalFixedDeltaTime;

        /// <summary>
        /// Takes a screenshot and saves it in the user's Pictures folder.
        /// </summary>
        [Button]
        public void TakeScreenshot()
        {
            //Create the directory in case it doesn't exist (if it does it's not a problem)
            string directoryPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures),
                Application.productName);
            System.IO.Directory.CreateDirectory(directoryPath);

            string filePath = System.IO.Path.Combine(directoryPath, 
                System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + " " + Screen.width * supersize + "x" + Screen.height * supersize + "px" + ".png");

            ScreenCapture.CaptureScreenshot(filePath, supersize);

            Debug.Log("Saved screenshot to " + filePath + ".");
        }


        private void Update()
        {
            if (!Application.isPlaying) return;

            Time.timeScale = timeScale;

            if (Input.GetKeyDown(screenshotKey)) TakeScreenshot();
        }

        private void Start()
        {
            originalFixedDeltaTime = Time.fixedDeltaTime;
        }
    }
}
#endif