using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public class CollisionEventDispatcher : MonoBehaviour
    {
        public event System.Action<Collision> OnCollisionEntered;
        public event System.Action<Collision> OnCollisionStayed;
        public event System.Action<Collision> OnCollisionExited;

        public event System.Action<Collider> OnTriggerEntered;
        public event System.Action<Collider> OnTriggerStayed;
        public event System.Action<Collider> OnTriggerExited;

        public event System.Action<Collision2D> OnCollisionEntered2D;
        public event System.Action<Collision2D> OnCollisionStayed2D;
        public event System.Action<Collision2D> OnCollisionExited2D;

        public event System.Action<Collider2D> OnTriggerEntered2D;
        public event System.Action<Collider2D> OnTriggerStayed2D;
        public event System.Action<Collider2D> OnTriggerExited2D;


        void OnCollisionEnter(Collision collision)
        {
            OnCollisionEntered?.Invoke(collision);
        }

        void OnCollisionExit(Collision collision)
        {
            OnCollisionExited?.Invoke(collision);
        }

        void OnCollisionStay(Collision collision)
        {
            OnCollisionStayed?.Invoke(collision);
        }


        void OnTriggerEnter(Collider other)
        {
            OnTriggerEntered?.Invoke(other);
        }

        void OnTriggerStay(Collider other)
        {
            OnTriggerStayed?.Invoke(other);
        }

        void OnTriggerExit(Collider other)
        {
            OnTriggerExited?.Invoke(other);
        }


        void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered2D?.Invoke(collision);
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            OnCollisionStayed2D?.Invoke(collision);
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            OnCollisionExited2D?.Invoke(collision);
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEntered2D?.Invoke(other);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerStayed2D?.Invoke(other);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerExited2D?.Invoke(other);
        }
    }
}
