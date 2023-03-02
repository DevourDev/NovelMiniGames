using System;
using DevourDev.Unity.Helpers;
using UnityEngine;

namespace Game.EscapeShootingChase
{
    public class EnemyStats
    {
        private float _projectileSpeed;
        private float _shootingScatter;
        private float _shootingRate;
        private Transform _shootingTarget;


        public float ProjectileSpeed { get => _projectileSpeed; set => _projectileSpeed = value; }
        public float ShootingScatter { get => _shootingScatter; set => _shootingScatter = value; }
        public float ShootingRate { get => _shootingRate; set => _shootingRate = value; }
        public Transform ShootingTarget { get => _shootingTarget; set => _shootingTarget = value; }
    }


    public class EscapeEnemy : MonoBehaviour
    {
        private EnemyStats _stats;

        private float _shootCoolDown;
        private ProjectilesPool _pool;
        private ContactFilter2D _filter;


        public void InitEnemy(EnemyStats stats)
        {
            _stats = stats;
        }


        public event System.Action<EscapeEnemy, Vector2> OnShoot;




        private void Start()
        {
            ResetShootCoolDown();
            _shootCoolDown /= UnityEngine.Random.Range(2, 10);
            _pool = CachingAccessors.Get<ProjectilesPool>();
            _filter = new()
            {
                layerMask = EscapeFastAccess.HittablesLayerMask,
                useLayerMask = true,
            };
        }

        private void ResetShootCoolDown()
        {
            _shootCoolDown = 1f / _stats.ShootingRate;
        }

        private void Update()
        {
            if ((_shootCoolDown -= Time.deltaTime) > 0f)
                return;

            ResetShootCoolDown();
            Shoot();
        }

        private void Shoot()
        {
            var bullet = _pool.Rent();
            var thisPos = transform.position;
            bullet.transform.position = thisPos;
            Vector2 targetPoint = _stats.ShootingTarget.position;
            float scatter = _stats.ShootingScatter;
            targetPoint.x += UnityEngine.Random.Range(-scatter, scatter);
            targetPoint.y += UnityEngine.Random.Range(-scatter, scatter);
            Vector2 direction = targetPoint - (Vector2)thisPos;
            direction.Normalize();
            Vector2 velocity = direction * _stats.ProjectileSpeed;
            bullet.Init(velocity, _filter);
            OnShoot?.Invoke(this, velocity);
        }
    }
}
