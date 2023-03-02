using UnityEngine;

namespace Game
{
    public class Controller2D : MonoBehaviour
    {
        //todo: сделать компонент, обрабатывающий (в данном случае -
        //ограничивающий) изменение позиции (как в Баффи Берд)

        [SerializeField] private Transform _min;
        [SerializeField] private Transform _max;

        private Vector2 _velocity;


        public event System.Action<Controller2D, Vector2> OnMove;


        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }


        private void Update()
        {
            if (_velocity == Vector2.zero)
                return;

            Vector3 translation = _velocity * Time.deltaTime;
            Vector3 desired = transform.position + translation;
            Vector3 min = _min.position;
            Vector3 max = _max.position;

            if (desired.x < min.x)
            {
                desired.x = min.x;
            }
            else if (desired.x > max.x)
            {
                desired.x = max.x;
            }

            if(desired.y < min.y)
            {
                desired.y = min.y;
            }
            else if(desired.y > max.y)
            {
                desired.y = max.y;
            }

            transform.position = desired;
            OnMove?.Invoke(this, desired);
        }

    }
}
