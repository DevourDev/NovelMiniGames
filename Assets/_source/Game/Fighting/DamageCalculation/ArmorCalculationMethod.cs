using UnityEngine;

namespace Game.Fighting
{
    public abstract class ArmorCalculationMethod : ScriptableObject
    {
        public abstract float Calculate(float incomingDamage, float armorValue);
    }
}
