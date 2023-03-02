using UnityEngine;

namespace Game.EscapeShootingChase
{
    public class FastAccessInitializer : MonoBehaviour
    {
        [SerializeField] private LayerMask _hittables;
        [SerializeField] private Hero _hero;


        private void Awake()
        {
            EscapeFastAccess.HittablesLayerMask = _hittables;
            EscapeFastAccess.Hero = _hero;
        }
    }
}
