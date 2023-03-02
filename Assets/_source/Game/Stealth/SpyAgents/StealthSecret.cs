using UnityEngine;

namespace Game.Stealth
{
    public class StealthSecret : MonoBehaviour
    {
        [SerializeField] private string _message;
        [SerializeField] private float _radius;


        public string Message => _message;
        public float Radius => _radius;
    }
}
