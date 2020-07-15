using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public static class Vector2Extensions
    {
        public static Vector2 GetPerpendicularClockwise(this Vector2 v)
        {
            return new Vector2(v.y, -v.x);
        }

        public static Vector2 GetPerpendicularCounterClockwise(this Vector2 v)
        {
            return new Vector2(-v.y, v.x);
        }

        public static Vector2 Rotate(Vector2 vector, float angleInRadians)
        {
            Vector2 newVector = new Vector2
            {
                x = vector.x * Mathf.Cos(angleInRadians) - vector.y * Mathf.Sin(angleInRadians),
                y = vector.x * Mathf.Sin(angleInRadians) + vector.y * Mathf.Cos(angleInRadians)
            };
            return newVector;
        }
    }

}
