using System.Runtime.InteropServices;
using DevourDev.Pools;
using DevourDev.Unity.Helpers;
using UnityEngine;

namespace Game
{
    public class Projectile : PoolableComponent<Projectile>
    {
        [SerializeField] private float _radius = 1f;

        private Vector2 _velocity;
        private ContactFilter2D _contactFilter;

        public event System.Action<Projectile, Vector2> OnHit;
        public event System.Action<Projectile> OnInit;



        public void Init(Vector2 velocity, ContactFilter2D filter)
        {
            _velocity = velocity;
            _contactFilter = filter;
            OnInit?.Invoke(this);
        }


        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 fromPoint = transform.position;
            Vector2 translation = _velocity * Time.deltaTime;

            //ray cast
            //var hit = Physics2D.Raycast(fromPoint, _velocity, translation.magnitude, _contactFilter.layerMask);

            //circle cast
            var hit = Physics2D.CircleCast(fromPoint, _radius, _velocity, translation.magnitude, _contactFilter.layerMask);

            //collider cast
            //var hits = _collider.MemCast(_velocity, _contactFilter, translation.magnitude);

            //if (hits.Length == 0)
            //{
            //    hit = default;
            //}
            //else
            //{
            //    Physics2DHelpers.TryFindClosest(hits, out hit);
            //}

            if (!hit)
            {
                transform.position += (Vector3)translation;
                return;
            }

            OnHit?.Invoke(this, hit.point);

            if (hit.collider.TryGetComponent<Hittable<Projectile>>(out var hittable))
            {
                hittable.Hit(this);
            }

            ReturnToPool();
        }

        protected override void HandleRentingInternal()
        {
            gameObject.SetActive(true);
        }

        protected override void HandleReturnedInternal()
        {
            gameObject.SetActive(false);
        }
    }
}
