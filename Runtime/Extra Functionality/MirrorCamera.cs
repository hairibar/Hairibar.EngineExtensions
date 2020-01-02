using UnityEngine;

namespace Hairibar.EngineExtensions
{
    [ExecuteAlways, RequireComponent(typeof(Camera)), AddComponentMenu("Rendering/Mirror Camera")]
    public class MirrorCamera : MonoBehaviour
    {
        [SerializeField] private bool horizontal = true;
        [SerializeField] private bool vertical = false;


        private new Camera camera;
        private bool originalInvertCulling;


        private void OnPreCull()
        {
            camera.ResetWorldToCameraMatrix();
            camera.ResetProjectionMatrix();
            camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(new Vector3((horizontal) ? -1 : 1, (vertical) ? -1 : 1, 1));
        }

        private void OnPreRender()
        {
            originalInvertCulling = GL.invertCulling;
            GL.invertCulling = horizontal || vertical;
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
    }
}

