#if PHYSICS_MODULE_PRESENT || PHYSICS_2D_MODULE_PRESENT
using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public class CollisionEventDispatcher : MonoBehaviour
    {
#if PHYSICS_MODULE_PRESENT
        public event System.Action<Collision, CollisionEventDispatcher> OnCollisionEntered;
        public event System.Action<Collision, CollisionEventDispatcher> OnCollisionStayed;
        public event System.Action<Collision, CollisionEventDispatcher> OnCollisionExited;

        public event System.Action<Collider, CollisionEventDispatcher> OnTriggerEntered;
        public event System.Action<Collider, CollisionEventDispatcher> OnTriggerStayed;
        public event System.Action<Collider, CollisionEventDispatcher> OnTriggerExited;
#endif

#if PHYSICS_2D_MODULE_PRESENT
        public event System.Action<Collision2D, CollisionEventDispatcher> OnCollisionEntered2D;
        public event System.Action<Collision2D, CollisionEventDispatcher> OnCollisionStayed2D;
        public event System.Action<Collision2D, CollisionEventDispatcher> OnCollisionExited2D;

        public event System.Action<Collider2D, CollisionEventDispatcher> OnTriggerEntered2D;
        public event System.Action<Collider2D, CollisionEventDispatcher> OnTriggerStayed2D;
        public event System.Action<Collider2D, CollisionEventDispatcher> OnTriggerExited2D;
#endif

#if PHYSICS_MODULE_PRESENT
        void OnCollisionEnter(Collision collision)
        {
            OnCollisionEntered?.Invoke(collision, this);
        }

        void OnCollisionExit(Collision collision)
        {
            OnCollisionExited?.Invoke(collision, this);
        }

        void OnCollisionStay(Collision collision)
        {
            OnCollisionStayed?.Invoke(collision, this);
        }

        void OnTriggerEnter(Collider other)
        {
            OnTriggerEntered?.Invoke(other, this);
        }

        void OnTriggerStay(Collider other)
        {
            OnTriggerStayed?.Invoke(other, this);
        }

        void OnTriggerExit(Collider other)
        {
            OnTriggerExited?.Invoke(other, this);
        }
#endif

#if PHYSICS_2D_MODULE_PRESENT
        void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered2D?.Invoke(collision, this);
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            OnCollisionStayed2D?.Invoke(collision, this);
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            OnCollisionExited2D?.Invoke(collision, this);
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEntered2D?.Invoke(other, this);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerStayed2D?.Invoke(other, this);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerExited2D?.Invoke(other, this);
        }
#endif
    }
}
#endif