using System.Collections.Generic;
using DevourDev.Unity.Helpers;
using UnityEngine;

namespace Game.Fighting
{

    public sealed class DefenceComponent : FighterComponent
    {
        [SerializeField] private FighterArmorSo[] _initialArmors;

        private List<FighterArmorSo> _armors;
        private ArmorSummary _summary;


        private void Awake()
        {
            var gm = CachingAccessors.Get<FightingGameManager>();
            _summary = new(gm.ArmorCalculationMethod);
            _armors = new();
            _armors.AddRange(_initialArmors);
            Recalculate();
        }


        public void AddArmor(FighterArmorSo armor)
        {
            _armors.Add(armor);
            Recalculate();
        }

        public void RemoveArmor(FighterArmorSo armor)
        {
            _armors.Remove(armor);
            Recalculate();
        }

        private void Recalculate()
        {
            _summary.Clear();

            foreach (var armor in _armors)
            {
                armor.FillOptmizer(ref _summary);
            }
        }

        public float ProcessDamage(float incomingDamage)
        {
            return _summary.ProcessDamage(incomingDamage);
        }
    }
}
