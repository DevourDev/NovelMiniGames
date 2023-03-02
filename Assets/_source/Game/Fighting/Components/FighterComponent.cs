using System.Threading.Tasks;
using UnityEngine;

namespace Game.Fighting
{


    public abstract class FighterComponent : MonoBehaviour
    {
        private FighterOnScene _fighter;


        internal void SetFighter(FighterOnScene fighter)
        {
            _fighter = fighter;
        }


        public FighterOnScene Fighter => _fighter;
    }
}
