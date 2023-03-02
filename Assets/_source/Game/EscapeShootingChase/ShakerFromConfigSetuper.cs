using Game.EscapeShootingChase.Config;
using Game.Helpers.Shake;
using UnityEngine;

namespace Game.EscapeShootingChase
{
    public class ShakerFromConfigSetuper : MonoBehaviour
    {
        private Shaker _shaker;


        private void Awake()
        {
            _shaker = GetComponent<Shaker>();
        }

        private void Start()
        {
            var cfg = EscapeShootingChaseExternalConfig.GetCachedConfig();

            if (cfg.CameraShakeEnabled)
            {
                var curve = new ShakeCurve2D(cfg.CameraShakeAmplXStart, cfg.CameraShakeAmplXEnd,
                cfg.CameraShakeAmplYStart, cfg.CameraShakeAmplYEnd,
                cfg.CameraShakeFrequencyStart, cfg.CameraShakeFrequencyEnd);
                _shaker.SetCurve(curve);
            }
            else
            {
                _shaker.ResetPosition();
                Destroy(_shaker);
            }

            Destroy(this);
        }
    }
}
