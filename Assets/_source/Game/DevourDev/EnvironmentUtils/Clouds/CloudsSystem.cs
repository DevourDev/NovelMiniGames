using UnityEngine;

namespace DevourDev.EnvironmentUtils.Clouds
{
    public class CloudsSystem : MonoBehaviour
    {
        private readonly struct Cloud
        {
            private readonly Transform _tr;
            private readonly float _extend;


            public Cloud(Transform tr)
            {
                _tr = tr;
                var spriteRenderer = tr.gameObject.GetComponent<SpriteRenderer>();
                var width = spriteRenderer.localBounds.size.x;
                _extend = width / 2f;
            }


            public Transform Tr => _tr;
            public float Extend => _extend;
        }


        [SerializeField] private Transform _boundMin;
        [SerializeField] private Transform _boundMax;

        [SerializeField] private Transform[] _cloudsInitializer;

        private Cloud[] _clouds;
        private float _windSpeed = 0.4f;
        private Vector3 _velocity;


        public float WindSpeed
        {
            get => _windSpeed;

            set
            {
                _windSpeed = value;
                _velocity = new Vector3(_windSpeed, 0, 0);
            }
        }


        private void Awake()
        {
            _velocity = new Vector3(_windSpeed, 0, 0);

            var arr0 = _cloudsInitializer;
            var c = arr0.Length;
            var arr1 = new Cloud[c];

            for (int i = 0; i < c; i++)
            {
                arr1[i] = new Cloud(arr0[i]);
            }

            _clouds = arr1;
        }


        private void Update()
        {
            if (_windSpeed == 0)
                return;

            Vector3 translation = _velocity * Time.deltaTime;
            bool toLeft = _windSpeed < 0f;
            float boundX = toLeft ? _boundMin.position.x : _boundMax.position.x;

            foreach (var cloud in _clouds)
            {
                bool outOfBound;
                float center = cloud.Tr.position.x;

                if (toLeft)
                {
                    outOfBound = center + cloud.Extend < boundX;
                }
                else
                {
                    outOfBound = center + cloud.Extend > boundX;
                }

                if (outOfBound)
                {
                    ReplaceCloud(cloud, !toLeft);
                }
                else
                {
                    cloud.Tr.position += translation;
                }
            }
        }

        private void ReplaceCloud(Cloud cloud, bool toLeft)
        {
            float y = UnityEngine.Random.Range(_boundMin.position.y, _boundMax.position.y);
            float x;

            if (toLeft)
            {
                x = _boundMin.position.x - cloud.Extend;
            }
            else
            {
                x = _boundMax.position.x + cloud.Extend;
            }

            cloud.Tr.position = new Vector3(x, y, 0);
        }
    }
}