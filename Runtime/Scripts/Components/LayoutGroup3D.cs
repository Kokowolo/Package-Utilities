/*
 * File Name: LayoutGroup3D.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 23, 2023
 * 
 * Additional Comments:
 *		File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    // [ExecuteInEditMode]
    public class LayoutGroup3D : MonoBehaviour
    {
        /************************************************************/
        #region Enums

        private enum LayoutType
        {
            Radial,
            // Square
        }

        private enum ChildAlignment
        {
            Left,
            Right,
            CenterLeft,
            CenterRight
        }

        #endregion
        /************************************************************/
        #region Fields

        [Header("Settings")]
        [SerializeField] private LayoutType layoutType;
        [SerializeField] private ChildAlignment childAlignment;
        [SerializeField] private float radiusA = 1.0f;
        [SerializeField] private float radiusB = 1.0f;
        [SerializeField, Range(0, 360)] private float spacingAngle = 10.0f;
        [SerializeField, Range(0, 360)] private float offsetAngle = 10.0f;

        [Header("Additional Settings")]
        [SerializeField] private bool includeInactive = false;
        [SerializeField] private bool useEvenDistribution = true;
        [SerializeField] private bool useAdaptiveDistribution = true;

        [Header("Conditional Settings")]
        [SerializeField] private bool doubleIncludeLastIndex = false;
        [SerializeField, Range(0, 360)] private float arcAngleRange = 360f;
        [SerializeField, Range(0, 360)] private float adaptiveAngle = 360f;

        #endregion
    	/************************************************************/
        #region Properties

        private List<Vector3> _LocalPositions;
        public List<Vector3> LocalPositions
        {
            get
            {
                if (_LocalPositions == null)
                {
                    _LocalPositions = ListPool.Get<Vector3>();
                }
                return _LocalPositions;
            }
        }

        private List<Transform> _Children;
        private List<Transform> Children 
        {
            get
            {
                if (_Children == null)
                {
                    _Children = ListPool.Get<Transform>();
                }
                return _Children;
            }
        }

        #endregion
        /************************************************************/
        #region Functions

        private void OnDestroy()
        {
            ListPool.Add(_LocalPositions);
            ListPool.Add(_Children);
        }

        private void Start()
        {
            RefreshLayoutGroup(refreshTransforms: true);
        }

        private void RefreshChildrenList()
        {
            Children.Clear();
            Transform child;
            for (int i = 0; i < transform.childCount; i++)
            {
                child = transform.GetChild(i);
                if (includeInactive || child.gameObject.activeSelf) 
                {
                    Children.Add(child);
                }
            }
        }

        private void RefreshLayoutGroupSpacingAngle()
        {
            RefreshChildrenList();
            int count = Children.Count - (doubleIncludeLastIndex ? 1 : 0);

            if (count <= 0)
            {
                spacingAngle = 0;
            }
            else
            {
                float adaptiveAngleRange = count * adaptiveAngle;
                spacingAngle = (adaptiveAngleRange > arcAngleRange ? arcAngleRange : adaptiveAngleRange) / count; 
            }
        }

        private void RefreshLayoutGroupPositions()
        {
            LocalPositions.Clear();
            if (transform.childCount == 0) return;

            RefreshChildrenList();
            List<Vector3> positions = GetLayoutGroupPositions(numberOfPositions: Children.Count);
            LocalPositions.AddRange(positions);
            ListPool.Release(ref positions);
        }

        private List<Vector3> GetLayoutGroupPositions(int numberOfPositions)
        {
            List<Vector3> positions = ListPool.Get<Vector3>();
            for (int i = 0; i < numberOfPositions; i++)
            {
                float angle = i * spacingAngle + offsetAngle;
                switch (childAlignment)
                {
                    case ChildAlignment.Left:
                    {
                        // nada
                        break;
                    }
                    case ChildAlignment.Right:
                    {
                        angle *= -1;
                        break;
                    }
                    case ChildAlignment.CenterLeft:
                    {
                        angle -= (spacingAngle * (numberOfPositions - 1)) / 2;
                        break;
                    }
                    case ChildAlignment.CenterRight:
                    {
                        angle = -(angle - (spacingAngle * (numberOfPositions - 1)) / 2);
                        break;
                    }
                    default:
                    {
                        throw new System.Exception("Child Layout Undefined");
                    }
                }
                float radians = angle * Mathf.Deg2Rad;
                float x = radiusA * Mathf.Cos(radians);
                float z = radiusB * Mathf.Sin(radians);
                positions.Add(new Vector3(x, 0f, z)); // first index is `this` transform
            }
            return positions;
        }

        private void RefreshLayoutGroupTransforms()
        {
            if (transform.childCount == 0) return;
            
            for (int i = 0; i < LocalPositions.Count; i++)
            {
                Children[i].localPosition = LocalPositions[i];
            }
        }

        public void RefreshLayoutGroup(bool refreshTransforms)
        {
            // update spacing angle (if not manually set)
            if (useEvenDistribution) RefreshLayoutGroupSpacingAngle();

            // update local positions
            RefreshLayoutGroupPositions();

            // optional update child transforms
            if (refreshTransforms) RefreshLayoutGroupTransforms();
        }
        
        #endregion
        /************************************************************/
        #region Editor Only
#if UNITY_EDITOR

        private void OnValidate()
        {
            if (useAdaptiveDistribution)
            {
                useEvenDistribution = true;
            }
            else
            {
                adaptiveAngle = 360;
            }
            
            RefreshLayoutGroup(refreshTransforms: true);
        }

#endif
        #endregion
        /************************************************************/
        #region Debug
#if UNITY_EDITOR

        // HACK: compiles, but doesn't work :(

        // [Header("Debug Settings")]
        // [SerializeField, Min(0)] float gizmosRadius = 0.15f;
        // [SerializeField, Min(0)] int numberOfDebugLayoutPositions = 3;

        // private void OnDrawGizmosSelected()
        // {
        //     Gizmos.color = Color.cyan;
        //     List<Vector3> positions = GetLayoutGroupPositions(numberOfDebugLayoutPositions);
        //     for (int i = 0; i < positions.Count; i++)
        //     {
        //         Gizmos.DrawSphere(transform.position + positions[i], gizmosRadius);
        //     }
        //     ListPool.Add(positions);
        // }

#endif
        #endregion
        /************************************************************/
    }
}