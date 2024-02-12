/*
 * File Name: Ballistics.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 27, 2023
 * 
 * Additional Comments:
 *		File Line Length: 140
 *
 *      TODO: all references to `speed` should be replaced with `velocity` as a Vector3
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class Ballistics
    {
        /************************************************************/
        #region Properties

        private const bool GetLowerTrajectory = true;

        #endregion
        /************************************************************/
        #region Properties

        public static float Epsilon { get; set; } = 0.1f;

        // TODO: this should be a vector
        public static float GravityY { get; set; } = Physics.gravity.y;

        private static Vector2 _MinMaxAngleRange = new Vector2(-20, 15);
        public static Vector2 MinMaxAngleRange
        {
            get => _MinMaxAngleRange;
            set
            {
                _MinMaxAngleRange.x = Mathf.Clamp(value.x, -90, 90);
                _MinMaxAngleRange.y = Mathf.Clamp(value.y, -90, 90);
            }
        }

        private static float _TimeStepInterval = 0.05f;
        public static float TimeStepInterval
        {
            get => _TimeStepInterval;
            set => _TimeStepInterval = Mathf.Max(value, Epsilon);
        }

        private static float _MaxFlightDurationTime = 5f;
        public static float MaxFlightDurationTime 
        {
            get => _MaxFlightDurationTime;
            set => _MaxFlightDurationTime = value;
        }

        public static LayerMask LayerMask { get; set; }

        #endregion
        /************************************************************/
        #region Functions

        public static bool RaycastWithProjectileMotionTrajectoryPrediction(
            out List<Vector3> predictionPoints,
            out float angle, 
            Vector3 origin, 
            Vector3 target, 
            float speed,
            out RaycastHit hitInfo)
        {
            return RaycastWithProjectileMotionTrajectoryPrediction(
                out predictionPoints, out angle, MinMaxAngleRange, origin, target, speed, out hitInfo,
                TimeStepInterval, MaxFlightDurationTime, LayerMask
            );
        }

        /// <summary>
        /// see 'Angle θ required to hit coordinate (x, y)' at https://en.wikipedia.org/wiki/Projectile_motion for more information
        /// </summary>
        public static bool RaycastWithProjectileMotionTrajectoryPrediction(
            out List<Vector3> predictionPoints,
            out float angle, 
            Vector2 minMaxAngleRange,
            Vector3 origin, 
            Vector3 target, 
            float speed, 
            out RaycastHit hitInfo,
            float timeStepInterval, 
            float maxFlightDurationTime,
            LayerMask layerMask)
        {
            // input
            float g = GravityY;

            // fields
            Vector3 directionXZ = new Vector3(target.x - origin.x, 0, target.z - origin.z);
            float xf = directionXZ.magnitude; // TODO: subtract barrel distance from origin
            float yf = -target.y + origin.y;
            float v2 = speed * speed;

            // sqrt(v^4 - g(gx^2 + 2yv^2)) but before sqrt
            float value = v2 * v2 - g * (g * xf * xf + 2 * yf * v2);
            if (value < 0) value = 0; //throw new System.Exception("imaginary solution");

            // calculate two possible launch angles
            Vector2 launchAngles = new Vector2(v2 + Mathf.Sqrt(value), v2 - Mathf.Sqrt(value));
            launchAngles /= g * xf;
            launchAngles.x = Mathf.Atan(launchAngles.x) * Mathf.Rad2Deg; // high angle or steep trajectory
            launchAngles.y = Mathf.Atan(launchAngles.y) * Mathf.Rad2Deg; // low angle or shallow trajectory
            
            // prepare output and calculate angle
            angle = GetLowerTrajectory ? launchAngles.y : launchAngles.x;
            angle = Mathf.Clamp(angle, minMaxAngleRange.x, minMaxAngleRange.y);
            predictionPoints = GetProjectileMotionTrajectoryPrediction(
                origin, directionXZ, -angle, speed, out hitInfo, timeStepInterval, maxFlightDurationTime, layerMask
            );
            if (predictionPoints.Count < 2) return false;
            return Vector3.Distance(predictionPoints[^1], target) <= Epsilon;
        }

        public static List<Vector3> GetProjectileMotionTrajectoryPrediction(
            Vector3 origin, 
            Quaternion rotation, 
            float speed,
            out RaycastHit hitInfo)
        {
            return GetProjectileMotionTrajectoryPrediction(
                origin, rotation, speed, out hitInfo, TimeStepInterval, MaxFlightDurationTime, LayerMask
            );
        }

        public static List<Vector3> GetProjectileMotionTrajectoryPrediction(
            Vector3 origin, 
            Quaternion rotation, 
            float speed,
            out RaycastHit hitInfo,
            float timeStepInterval, 
            float maxFlightDurationTime, 
            LayerMask layerMask)
        {
            Vector3 direction = rotation * Vector3.forward;
            float angle = -(rotation.eulerAngles.x > 180 ? rotation.eulerAngles.x - 360 : rotation.eulerAngles.x);

            return GetProjectileMotionTrajectoryPrediction(
                origin, direction, angle, speed, out hitInfo, timeStepInterval, maxFlightDurationTime, layerMask
            );
        }

        private static List<Vector3> GetProjectileMotionTrajectoryPrediction(
            Vector3 origin, 
            Vector3 directionXZ,
            float angle, 
            float speed,
            out RaycastHit hitInfo,
            float timeStepInterval, 
            float maxFlightDurationTime, 
            LayerMask layerMask)
        {
            // input
            float g = -GravityY;
            TimeStepInterval = timeStepInterval;
            MaxFlightDurationTime = maxFlightDurationTime;
            LayerMask = layerMask;

            // fields
            Vector3 prev = origin;
            Vector3 next;
            directionXZ.y = 0;
            directionXZ = directionXZ.normalized;

            // initialization
            bool hasCollision = false;
            hitInfo = new RaycastHit();
            List<Vector3> predictionPoints = ListPool.Get<Vector3>();
            predictionPoints.Add(origin);

            // start march
            for (float t = timeStepInterval; t < maxFlightDurationTime && !hasCollision; t += timeStepInterval)
            {
                // calculate v0
                float v0_x = speed * Mathf.Cos(angle * Mathf.Deg2Rad);
                float v0_y = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

                // calculate y
                float yf = v0_y * t - 0.5f * g * t * t;

                // calculate x
                float xf = v0_x * t;
        
                // set next
                next = origin + Quaternion.LookRotation(directionXZ, Vector3.up) * new Vector3(0, yf, xf);

                // Raycast
                if (Raycasting.RaycastToDestinationPoint(prev, next, out hitInfo, layerMask))
                {
                    hasCollision = true;
                    next = hitInfo.point;
                }
                predictionPoints.Add(next);
                prev = next;
            }
            return predictionPoints;
        }
        
        #endregion
        /************************************************************/
    }
}