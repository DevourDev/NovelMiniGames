using UnityEngine;

namespace DevourDev.Unity.Helpers
{
    public abstract class ContainingInRect : MonoBehaviour
    {
        private bool _inRect;


        public bool InRect => _inRect;


        protected virtual void OnEnable()
        {
            _inRect = true;
            //InitialCheck();
        }

        private void InitialCheck()
        {
            _inRect = CheckInternal();

            if (_inRect)
            {
                HandleEnteringRect();
            }
            else
            {
                HandleLeavingRect();
            }
        }

        protected void Check()
        {
            bool inRect = CheckInternal();

            if (_inRect == inRect)
                return;

            _inRect = inRect;

            if (_inRect)
            {
                HandleEnteringRect();
            }
            else
            {
                HandleLeavingRect();
            }
        }


        protected virtual bool CheckInternal()
        {
            return GetRect().Contains(transform.position);
        }

        protected abstract void HandleEnteringRect();
        protected abstract void HandleLeavingRect();

        protected abstract Rect GetRect();

    }
}
