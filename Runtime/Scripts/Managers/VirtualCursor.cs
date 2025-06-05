/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: May 28, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace Kokowolo.Utilities
{
    public class VirtualCursor : MonoBehaviour
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        [Header("Cached References")]
        [SerializeField] RectTransform rectTransform;
        [SerializeField] Image image;

        [Header("Settings")]
        [SerializeField] bool _VisibleCursor = true;
        [SerializeField] bool _VisibleVirtual = true;
        [SerializeField] bool _FollowCursor = true;
        [SerializeField, Range(0, 1)] float _Interpolator = 1;
        [SerializeField] Vector2 _Size = new Vector2(100, 100);

        [Header("Data")]
        [SerializeField, ReadOnly] Vector2 _ScreenPoint;
        [SerializeField, ReadOnly] Vector2 _TargetScreenPoint;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public bool VisibleCursor
        {
            get => Cursor.visible;
            set
            {
                Cursor.visible = value;
                _VisibleCursor = value;
            }
        }

        public bool VisibleVirtual
        {
            get => rectTransform.gameObject.activeSelf;
            set 
            {
                rectTransform.gameObject.SetActive(value);
                _VisibleVirtual = value;
            }
        }

        public bool FollowCursor
        {
            get => _FollowCursor;
            set => _FollowCursor = value;
        }

        public float Interpolator
        {
            get => _Interpolator;
            set => _Interpolator = value.Clamp01();
        }

        public Vector2 Size
        {
            get => rectTransform.rect.size;
            set
            {
                rectTransform.sizeDelta = value;
                _Size = value;
            }
        }

        public Vector2 ScreenPoint
        {
            get => _ScreenPoint;
            private set
            {
                _ScreenPoint = value;
                if (VisibleVirtual)
                {
                    rectTransform.position = value;
                }
            }
        }

        public Vector2 ScreenPointNormalized
        {
            get => ScreenUtils.ToScreenPointNormalized(ScreenPoint);
            private set => ScreenPoint = ScreenUtils.ToScreenPoint(value);
        }

        public Vector2 TargetScreenPoint
        {
            get => _TargetScreenPoint;
            set => _TargetScreenPoint = value;
        }

        public Vector2 TargetScreenPointNormalized
        {
            get => _TargetScreenPoint;
            set => TargetScreenPoint = ScreenUtils.ToScreenPoint(value);
        }

        public Vector3 WorldPosition
        {
            get => ScreenUtils.ScreenPointToWorld(ScreenPoint);
        }

        public Sprite Sprite
        {
            get => image.sprite;
            set => image.sprite = value;
        }

        public Color Color
        {
            get => image.color;
            set => image.color = value;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        protected void Awake()
        {
            VisibleCursor = _VisibleCursor;
            VisibleVirtual = _VisibleVirtual;
            Size = _Size;
        }

        private void Update()
        {
            if (FollowCursor)
            {
                TargetScreenPoint = ScreenUtils.GetMouseScreenPoint();
            }
        }

        private void LateUpdate()
        {
            ScreenPoint = Vector2.Lerp(ScreenPoint, TargetScreenPoint, Interpolator);
        }

        public void SetCursorScreenPosition(Vector2 screenPoint)
        {
            InputManager.SetCursorScreenPosition(screenPoint);
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Editor
#if UNITY_EDITOR

        private void OnValidate()
        {
            Awake();
        }

#endif
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}