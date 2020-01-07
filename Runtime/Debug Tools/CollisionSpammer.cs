using UnityEngine;

namespace Hairibar.EngineExtensions.Debug
{   
    /// <summary>
    /// Logs any collision events experienced by the GameObject to console.
    /// </summary>
    [RemoveInRelease, AddComponentMenu("Debug/Collision Spammer")]
    public class CollisionSpammer : MonoBehaviour
    {
        public bool onCollisionEnter = true;
        public bool onCollisionStay = true;
        public bool onCollisionExit = true;

        #region Spams
        private void SpamEntered(string myName, string othersName)
        {
            if (onCollisionEnter) Debug.Log($"<b>{myName}</b> ENTERED collision with <b>{othersName}</b>");
        }

        private void SpamStayed(string myName, string othersName)
        {
            if (onCollisionStay) Debug.Log($"<b>{myName}</b> STAYED collision with <b>{othersName}</b>");
        }

        private void SpamExited(string myName, string othersName)
        {
            if (onCollisionExit) Debug.Log($"<b>{myName}</b> EXITED collision with <b>{othersName}</b>");
        }
        #endregion

        #region Collision Messages
        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint contact = collision.GetContact(0);
            SpamEntered(contact.thisCollider.name, contact.otherCollider.name);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ContactPoint2D contact = collision.GetContact(0);
            SpamEntered(contact.collider.name, contact.otherCollider.name);
        }


        private void OnCollisionStay(Collision collision)
        {
            ContactPoint contact = collision.GetContact(0);
            SpamStayed(contact.thisCollider.name, contact.otherCollider.name);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            ContactPoint2D contact = collision.GetContact(0);
            SpamStayed(contact.collider.name, contact.otherCollider.name);
        }


        private void OnCollisionExit(Collision collision)
        {
            SpamExited(name, collision.collider.name);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            SpamExited(name, collision.collider.name);
        }
        #endregion
    }
}
