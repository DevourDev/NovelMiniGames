using UnityEngine;
using Utils;

namespace DevourDev.Unity.Helpers
{
    public sealed class RandomMoverComponent : MonoBehaviour
    {
        [SerializeField] private SceneBounderBase _bounder;

        [SerializeField] private Vector2 _speedRange = new(1f, 2f);
        [SerializeField] private Vector2 _stayingOnOnePosTimeRange = new(2f, 4f);

        private Vector3 _distanceLeft;
        private Vector3 _velocity;

        private float _regenerateDestPointCD;


        public SceneBounderBase Bounder { get => _bounder; set => _bounder = value; }
        public Vector2 SpeedRange { get => _speedRange; set => _speedRange = value; }
        public Vector2 StayingOnOnePosTimeRange { get => _stayingOnOnePosTimeRange; set => _stayingOnOnePosTimeRange = value; }


        public void RegenerateDestPoint()
        {
            Vector3 dest;
            var min = _bounder.Min;
            var max = _bounder.Max;
            dest.x = UnityEngine.Random.Range(min.x, max.x);
            dest.y = UnityEngine.Random.Range(min.y, max.y);

            Vector3 curPos = transform.position;
            dest.z = curPos.z;

            _distanceLeft = dest - curPos;
            _velocity = _distanceLeft.normalized * UnityEngine.Random.Range(_speedRange.x, _speedRange.y);
        }


        private void Start()
        {
            ResetCD();
            RegenerateDestPoint();
        }


        private void Update()
        {
            CountDown();
            Move();
        }

        private void CountDown()
        {
            if (_regenerateDestPointCD < 0)
                return;

            if ((_regenerateDestPointCD -= Time.deltaTime) > 0)
                return;

            ResetCD();
            RegenerateDestPoint();
        }

        private void Move()
        {
            Vector3 movement = _velocity * Time.deltaTime;
            float sqrDist = movement.sqrMagnitude;

            if (sqrDist >= _distanceLeft.sqrMagnitude)
            {
                ChangePosition(_distanceLeft);
                ResetCD();
                RegenerateDestPoint();
            }
            else
            {
                _distanceLeft -= movement;
                ChangePosition(movement);
            }
        }

        private void ResetCD()
        {
            _regenerateDestPointCD = UnityEngine.Random.Range
                (_stayingOnOnePosTimeRange.x, _stayingOnOnePosTimeRange.y);
        }

        private void ChangePosition(Vector3 delta)
        {
            transform.position += delta;
        }
    }
}
