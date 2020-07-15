using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public class Rotator : MonoBehaviour
    {
        public float rotationSpeed = 200f;

        [Header("Direction")]
        public bool forwardX, forwardY, forwardZ;
        public bool backwardsX, backwardsY, backwardsZ;

        [Header("Space")]
        public bool localSpace = true;

        [Space]
        public UpdateLoop updateLoop;


        void Update()
        {
            if (updateLoop == UpdateLoop.Update)
            {
                Rotate();
            }
        }

        void FixedUpdate()
        {
            if (updateLoop == UpdateLoop.FixedUpdate)
            {
                Rotate();
            }
        }

        void Rotate()
        {
            Space space = (localSpace) ? Space.Self : Space.World;
            if (forwardX)
            {
                transform.Rotate(Time.deltaTime * rotationSpeed, 0, 0, space);
            }
            else if (backwardsX)
            {
                transform.Rotate(-Time.deltaTime * rotationSpeed, 0, 0, space);
            }

            if (forwardY)
            {
                transform.Rotate(0, Time.deltaTime * rotationSpeed, 0, space);
            }
            else if (backwardsY)
            {
                transform.Rotate(0, -Time.deltaTime * rotationSpeed, 0, space);
            }

            if (forwardZ)
            {
                transform.Rotate(0, 0, Time.deltaTime * rotationSpeed, space);
            }
            else if (backwardsZ)
            {
                transform.Rotate(0, 0, -Time.deltaTime * rotationSpeed, space);
            }
        }

        [System.Serializable]
        public enum UpdateLoop
        {
            Update,
            FixedUpdate
        }
    }
}
