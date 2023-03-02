using UnityEngine;

namespace Game
{
    public abstract class Hittable<THitter> : MonoBehaviour
    {
        public void Hit(THitter hitter)
        {
            HandleHit(hitter);
        }

        protected abstract void HandleHit(THitter hitter);
    }
}
