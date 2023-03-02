using UnityEngine;

namespace Game.EscapeShootingChase
{
    [RequireComponent(typeof(EscapeEnemy))]
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private AudioClip _shootSound;

        //private AudioSourcesPool _pool;
        private EscapeEnemy _enemy;


        private void Awake()
        {
            _enemy = GetComponent<EscapeEnemy>();
            _enemy.OnShoot += HandleShoot;
            //_pool = CachingAccessors.Get<AudioSourcesPool>();
        }

        private void HandleShoot(EscapeEnemy sender, Vector2 direction)
        {
            if (_shootSound == null)
                return;

            //var audioSource = _pool.GetForOneShot(_shootSound);
            //audioSource.transform.position = transform.position;
        }
    }
}
