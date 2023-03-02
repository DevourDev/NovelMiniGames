using System.Drawing;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Utils
{

    public sealed class SceneBounderOld : MonoBehaviour
    {
        [SerializeField] private Transform _min;
        [SerializeField] private Transform _max;


        public Vector3 Min => _min.position;
        public Vector3 Max => _max.position;
        public Vector3 Center => (Min + Max) / 2f;


        public float ClampY(float y)
        {
            var min = Min;
            var max = Max;

            if (y < min.y)
                y = min.y;
            else if (y > max.y)
                y = max.y;

            return y;
        }

        public bool Contains(Vector3 point)
        {
            var min = Min;
            var max = Max;

            return point.x >= min.x && point.x <= max.x
                && point.y >= min.y && point.y <= max.y
                && point.z >= min.z && point.z <= max.z;
        }

        public bool Contains(Vector3 point, out Vector3Int outOfBounds)
        {
            var min = Min;
            var max = Max;
            outOfBounds = Vector3Int.zero;

            if (point.x < min.x)
                outOfBounds.x = -1;
            else if (point.x > max.x)
                outOfBounds.x = 1;

            if (point.y < min.y)
                outOfBounds.y = -1;
            else if (point.y > max.y)
                outOfBounds.y = 1;

            if (point.z < min.z)
                outOfBounds.z = -1;
            else if (point.z > max.z)
                outOfBounds.z = 1;

            return outOfBounds == Vector3Int.zero;
        }

    }

}
