using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public static class ParableTrajectory
    {
        public static Vector3 HitTargetAtTime(Vector3 startPosition, Vector3 targetPosition, Vector3 scaledGravity, float timeToTarget)
        {
            Vector3 AtoB = targetPosition - startPosition;
            Vector3 horizontal = GetHorizontalVector(AtoB, scaledGravity);
            float horizontalDistance = horizontal.magnitude;

            Vector3 vertical = GetVerticalVector(AtoB, scaledGravity);
            float verticalDistance = vertical.magnitude * Mathf.Sign(Vector3.Dot(vertical, -scaledGravity));

            float horizontalSpeed = horizontalDistance / timeToTarget;
            float verticalSpeed = (verticalDistance + ((0.5f * scaledGravity.magnitude) * timeToTarget * timeToTarget)) / timeToTarget;

            Vector3 launch = (horizontal.normalized * horizontalSpeed) - (scaledGravity.normalized * verticalSpeed);
            return launch;
        }


        public static Vector3 HitTargetByAngle(Vector3 startPosition, Vector3 targetPosition, Vector3 scaledGravity, float limitAngle)
        {
            if (limitAngle == 90 || limitAngle == -90)
            {
                return Vector3.zero;
            }

            Vector3 AtoB = targetPosition - startPosition;
            Vector3 horizontal = GetHorizontalVector(AtoB, scaledGravity);
            float horizontalDistance = horizontal.magnitude;

            Vector3 vertical = GetVerticalVector(AtoB, scaledGravity);
            float verticalDistance = vertical.magnitude * Mathf.Sign(Vector3.Dot(vertical, -scaledGravity));

            float angleX = Mathf.Cos(Mathf.Deg2Rad * limitAngle);
            float angleY = Mathf.Sin(Mathf.Deg2Rad * limitAngle);

            float gravityMag = scaledGravity.magnitude;

            if (verticalDistance / horizontalDistance > angleY / angleX)
            {
                return Vector3.zero;
            }

            float destSpeed = (1 / Mathf.Cos(Mathf.Deg2Rad * limitAngle)) * Mathf.Sqrt((0.5f * gravityMag * horizontalDistance * horizontalDistance) / ((horizontalDistance * Mathf.Tan(Mathf.Deg2Rad * limitAngle)) - verticalDistance));

            Vector3 launch = ((horizontal.normalized * angleX) - (scaledGravity.normalized * angleY)) * destSpeed;
            return launch;
        }


        public static Vector3[] HitTargetBySpeed(Vector3 startPosition, Vector3 targetPosition, Vector3 scaledGravity, float launchSpeed)
        {
            Vector3 AtoB = targetPosition - startPosition;
            Vector3 horizontal = GetHorizontalVector(AtoB, scaledGravity);
            float horizontalDistance = horizontal.magnitude;

            Vector3 vertical = GetVerticalVector(AtoB, scaledGravity);
            float verticalDistance = vertical.magnitude * Mathf.Sign(Vector3.Dot(vertical, -scaledGravity));

            float x2 = horizontalDistance * horizontalDistance;
            float v2 = launchSpeed * launchSpeed;
            float v4 = launchSpeed * launchSpeed * launchSpeed * launchSpeed;

            float gravMag = scaledGravity.magnitude;

            float launchTest = v4 - (gravMag * ((gravMag * x2) + (2 * verticalDistance * v2)));

            Vector3[] launch = new Vector3[2];

            if (launchTest < 0)
            {
                launch[0] = (horizontal.normalized * launchSpeed * Mathf.Cos(45.0f * Mathf.Deg2Rad)) - (scaledGravity.normalized * launchSpeed * Mathf.Sin(45.0f * Mathf.Deg2Rad));
                launch[1] = (horizontal.normalized * launchSpeed * Mathf.Cos(45.0f * Mathf.Deg2Rad)) - (scaledGravity.normalized * launchSpeed * Mathf.Sin(45.0f * Mathf.Deg2Rad));
            }
            else
            {
                float[] tanAngle = new float[2];
                tanAngle[0] = (v2 - Mathf.Sqrt(v4 - gravMag * ((gravMag * x2) + (2 * verticalDistance * v2)))) / (gravMag * horizontalDistance);
                tanAngle[1] = (v2 + Mathf.Sqrt(v4 - gravMag * ((gravMag * x2) + (2 * verticalDistance * v2)))) / (gravMag * horizontalDistance);

                float[] finalAngle = new float[2];
                finalAngle[0] = Mathf.Atan(tanAngle[0]);
                finalAngle[1] = Mathf.Atan(tanAngle[1]);
                launch[0] = (horizontal.normalized * launchSpeed * Mathf.Cos(finalAngle[0])) - (scaledGravity.normalized * launchSpeed * Mathf.Sin(finalAngle[0]));
                launch[1] = (horizontal.normalized * launchSpeed * Mathf.Cos(finalAngle[1])) - (scaledGravity.normalized * launchSpeed * Mathf.Sin(finalAngle[1]));
            }

            return launch;
        }


        static Vector3 GetHorizontalVector(Vector3 AtoB, Vector3 gravityBase)
        {
            Vector3 output;
            Vector3 perpendicular = Vector3.Cross(AtoB, gravityBase);
            perpendicular = Vector3.Cross(gravityBase, perpendicular);
            output = Vector3.Project(AtoB, perpendicular);
            return output;
        }


        static Vector3 GetVerticalVector(Vector3 AtoB, Vector3 gravityBase)
        {
            Vector3 output;
            output = Vector3.Project(AtoB, gravityBase);
            return output;
        }
    }
}
