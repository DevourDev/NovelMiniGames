using DevourDev.Pools;
using UnityEngine;

namespace Game
{
    public class ProjectilesPool : PoolableComponentsPool<Projectile>
    {
        [SerializeField] private Projectile _prefab;

        protected override Projectile CreateItem()
        {
            return Instantiate(_prefab);
        }

        protected override void DisposeItem(Projectile item)
        {
            if (item != null)
                Destroy(item.gameObject);
        }
    }
}
