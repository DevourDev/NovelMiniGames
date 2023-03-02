using DevourDev.Unity.Helpers;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Projectile))]
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] private AudioClip _onHitSound;
        [SerializeField] private ParticleSystem _onHitParticles;
        [SerializeField] private TrailRenderer _trail;

        private Projectile _projectile;


        private void Awake()
        {
            _projectile = GetComponent<Projectile>();
            _projectile.OnHit += HandleHit;
            _projectile.OnInit += HandleInit;
        }

        private void HandleInit(Projectile projectile)
        {
            _trail.Clear();
        }

        private void HandleHit(Projectile arg1, Vector2 arg2)
        {

            if (_onHitSound != null)
            {
                //var pool = CachingAccessors.Get<AudioSourcesPool>();
                //var audioSource = pool.GetForOneShot(_onHitSound);
                //audioSource.transform.position = transform.position;
            }

            if (_onHitParticles != null)
            {

            }
        }
    }
}
