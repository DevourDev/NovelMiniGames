using UnityEngine;

namespace Game.Stealth
{
    public class StealthBonusSource : MonoBehaviour
    {
        [SerializeField] private float _bonus = 0.4f;
        [SerializeField] private bool _add;
        [SerializeField] private bool _remove;


        public float Process(float raw, bool add)
        {
            if ((add && _add) || (!add && _remove))
            {
                return raw + raw * _bonus;
            }

            return raw;
        }


    }
}
