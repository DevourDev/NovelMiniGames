using UnityEngine;

namespace Game.Fighting
{
    public abstract class TemporaryComponent : MonoBehaviour
    {
        protected void Attach()
        {
            OnAttach();
        }

        protected void Detach()
        {
            OnDetach();
        }


        public abstract void OnAttach();
        public abstract void OnDetach();
    }
}
