using UnityEngine;

namespace Game.EscapeShootingChase
{
    public static class EscapeFastAccess
    {
        public static LayerMask HittablesLayerMask { get; internal set; }
        public static Hero Hero { get; internal set; }
    }
}
