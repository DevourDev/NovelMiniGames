using UnityEngine;

namespace DevourDev.Unity
{
    public class TransformFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private bool _followX;
        [SerializeField] private bool _followY;
        [SerializeField] private bool _followZ;

        private Transform _tr;


        private void Awake()
        {
            _tr = transform;
        }


        private void Update()
        {
            Vector3 targetPos = _target.position;
            Vector3 newPos = _tr.position;

            if (_followX)
                newPos.x = targetPos.x;

            if (_followY)
                newPos.y = targetPos.y;

            if (_followZ)
                newPos.z = targetPos.z;

            _tr.position = newPos;
        }
    }
}
