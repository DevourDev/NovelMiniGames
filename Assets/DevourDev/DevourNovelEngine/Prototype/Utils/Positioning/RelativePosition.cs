using System;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Utils
{
    [System.Serializable]
    public struct RelativePosition : IEquatable<RelativePosition>
    {
        private const float _min = 0f;
        private const float _max = 1f;

        [SerializeField, Range(_min, _max)] private float _x;
        [SerializeField, Range(_min, _max)] private float _y;
        [SerializeField, Range(_min, _max)] private float _z;

        private static readonly RelativePosition _left = FromX(0.2f);
        private static readonly RelativePosition _centre = FromX(0.5f);
        private static readonly RelativePosition _right = FromX(0.8f);


        public RelativePosition(float x, float y, float z)
        {
            _x = Clamp(x);
            _y = Clamp(y);
            _z = Clamp(z);
        }


        public static RelativePosition Left => _left;
        public static RelativePosition Centre => _centre;
        public static RelativePosition Right => _right;


        public static RelativePosition FromX(float x)
        {
            return new RelativePosition(x, 0.5f, 0.5f);
        }

        public static Vector3 RelativeToAbsolute(RelativePosition relative, SceneBounderOld bounder)
        {
            Vector3 min = bounder.Min;
            Vector3 max = bounder.Max;

            Vector3 absolute;
            absolute.x = Lerp(min.x, max.x, relative._x);
            absolute.y = Lerp(min.y, max.y, relative._y);
            absolute.z = Lerp(min.z, max.z, relative._z);

            return absolute;
        }


        private static float Clamp(float raw)
        {
            if (raw < _min)
                return _min;

            if (raw > _max)
                return _max;

            return raw;
        }

        private static float Lerp(float a, float b, float normalized)
        {
            return a + (b - a) * normalized;
        }

        public override bool Equals(object obj)
        {
            return obj is RelativePosition position && Equals(position);
        }

        public bool Equals(RelativePosition other)
        {
            return _x == other._x &&
                   _y == other._y &&
                   _z == other._z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_x, _y, _z);
        }

        public static bool operator ==(RelativePosition left, RelativePosition right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RelativePosition left, RelativePosition right)
        {
            return !(left == right);
        }
    }

}
