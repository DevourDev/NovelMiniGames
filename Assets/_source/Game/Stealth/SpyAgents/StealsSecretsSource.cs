using System;
using UnityEngine;

namespace Game.Stealth
{
    public class StealthSecretDATA //todo: do
    {

    }
    public class StealsSecretsSource : MonoBehaviour
    {
        [SerializeField] private StealthSecret[] _secrets;


        private void Awake()
        {
            Array.Sort(_secrets, (a, b) => b.Radius.CompareTo(a.Radius)); //inversed
        }



        public void CheckForSpying(SpyAgent agent)
        {
            float sqrDist = (transform.position - agent.transform.position).sqrMagnitude;

            foreach (var secret in _secrets)
            {
                if (secret.Radius * secret.Radius > sqrDist)
                {
                    agent.AddSecret(secret);
                    continue;
                }

                break;
            }
        }
    }
}
