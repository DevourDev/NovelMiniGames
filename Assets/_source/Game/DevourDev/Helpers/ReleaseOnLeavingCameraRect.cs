using DevourDev.Pools;
using UnityEngine;

namespace DevourDev.Unity.Helpers
{
    [RequireComponent(typeof(ForcedReleasableComponent))]
    public sealed class ReleaseOnLeavingCameraRect : ContainingInRect
    {
        [SerializeField] private float _extraTime = 5f;

        private ForcedReleasableComponent _forcedReleasable;
        private static Camera _cam;
        private const float _checkCoolDown = 1f;

        private float _checkCoolDownLeft;
        private float _extraTimeLeft;


        private Camera Cam
        {
            get
            {
                if (_cam == null)
                    _cam = Camera.main;

                return _cam;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ResetCheckCoolDown();
            ResetExtraTime();
        }

        private void ResetExtraTime()
        {
            _extraTimeLeft = _extraTime;
        }

        private void Awake()
        {
            _forcedReleasable = GetComponent<ForcedReleasableComponent>();



            ResetCheckCoolDown();
        }

        private void ResetCheckCoolDown()
        {
            _checkCoolDownLeft = _checkCoolDown;
        }

        private void Update()
        {
            if ((_checkCoolDownLeft -= Time.deltaTime) > 0)
                return;

            ResetCheckCoolDown();
            Check();
            ExtraTimeCheck();
        }

        private void ExtraTimeCheck()
        {
            if (InRect)
            {
                ResetExtraTime();
                return;
            }

            _extraTimeLeft -= _checkCoolDown;

            if (_extraTimeLeft > 0)
                return;

            _forcedReleasable.Release();
        }

        protected override bool CheckInternal()
        {
            var viewPortPos = Cam.WorldToViewportPoint(transform.position);
            return (viewPortPos.x >= 0 && viewPortPos.x <= 1 && viewPortPos.y >= 0 && viewPortPos.y <= 1);
        }


        protected override void HandleEnteringRect()
        {
            ResetExtraTime();
        }

        protected override void HandleLeavingRect()
        {
            //_forcedReleasable.Release();
        }

        protected override Rect GetRect()
        {
            return _cam.rect;
        }
    }
}
