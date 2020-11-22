using UnityEngine;

namespace Hairibar.EngineExtensions
{
    /// <summary>
    /// Mirrors the rendered image. Done at projection stage, so virtually no performance impact.
    /// </summary>
    [ExecuteAlways, RequireComponent(typeof(Camera)), AddComponentMenu("Rendering/Mirror Camera")]
    public class MirrorCamera : MonoBehaviour
    {
        //FIXME: When both mirrors are activated, things get funky.
        public bool mirrorHorizontally = true;
        public bool mirrorVertically = false;

        private new Camera camera;
        private bool originalInvertCulling;


        private void OnPreCull()
        {
            camera.ResetWorldToCameraMatrix();
            camera.ResetProjectionMatrix();
            camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(new Vector3((mirrorHorizontally) ? -1 : 1, (mirrorVertically) ? -1 : 1, 1));
        }

        private void OnPreRender()
        {
            originalInvertCulling = GL.invertCulling;
            GL.invertCulling = mirrorHorizontally || mirrorVertically;
        }

        private void OnPostRender()
        {
            GL.invertCulling = originalInvertCulling;
        }


        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        private void OnEnable()
        {
            if (!camera) camera = GetComponent<Camera>();
        }

        private void OnDisable()
        {
            camera.ResetWorldToCameraMatrix();
            camera.ResetProjectionMatrix();
        }
    }
}