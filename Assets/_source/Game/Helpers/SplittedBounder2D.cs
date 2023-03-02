using System;
using UnityEngine;

namespace Utils
{
    public sealed class SplittedBounder2D : SceneBounderBase
    {
        [SerializeField] private SceneBounderBase _bounder;
        [SerializeField] private bool _splitHorizontal = true;
        [Tooltip("Ratio of THIS part (TakeSecondPart is taken in the account)")]
        [SerializeField, Range(0f, 1f)] private float _ratio = 0.5f;
        [Tooltip("if SplitHorizontal: use lower part, else: right part")]
        [SerializeField] private bool _takeSecondPart;

#if UNITY_EDITOR
        [SerializeField] private Color _gizmoColor = Color.red * 0.4f;
#endif


        public override Vector3 Min
        {
            get
            {
                var min = _bounder.Min;
                var max = _bounder.Max;

                Vector3 splittedMin = min;

                if (_splitHorizontal)
                {
                    splittedMin.x = min.x;

                    if (!_takeSecondPart)
                    {
                        float height = (max.y - min.y) * _ratio;
                        splittedMin.y = max.y - height;
                    }
                    else
                    {
                        splittedMin.y = min.y;
                    }
                }
                else
                {
                    throw new NotImplementedException($"min point for vertically splitted bounder is not implemented");
                }

                return splittedMin;
            }
        }

        public override Vector3 Max
        {
            get
            {
                var min = _bounder.Min;
                var max = _bounder.Max;

                Vector3 splittedMax = max;

                if (_splitHorizontal)
                {
                    splittedMax.x = max.x;

                    if (!_takeSecondPart)
                    {
                        splittedMax.y = max.y;
                    }
                    else
                    {
                        float height = (max.y - min.y) * _ratio;
                        splittedMax.y = max.y - height;
                    }
                }
                else
                {
                    throw new NotImplementedException($"max point for vertically splitted bounder is not implemented");
                }

                return splittedMax;
            }
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            if (_bounder == null)
                return;

            var min = Min;
            var max = Max;
            UnityEditor.Handles.DrawSolidRectangleWithOutline(Rect.MinMaxRect(min.x, min.y, max.x, max.y), _gizmoColor, Color.black);
        }
#endif
    }
}