using System;
using UnityEngine;

namespace Game.Stealth
{
    public sealed class SimpleSpyAgentsDetectorModule : SpyAgentsDetectorModuleBase<SpyAgent>
    {
        [SerializeField] private WatcherAgent _watcherAgent;
        [SerializeField] private SpyAgent[] _spyAgents;


        public override int Detect(SpyAgent[] buffer)
        {
            int count = -1;
            bool lookingLeft = _watcherAgent.LokingLeft;
            float watcherX = transform.position.x;

            foreach (var agent in _spyAgents)
            {
                if (agent.IsMoving && CheckWatcherLookingSide(agent))
                {
                    buffer[++count] = agent;
                }
            }

            return ++count;


            bool CheckWatcherLookingSide(SpyAgent spy)
            {
                float spyX = spy.transform.position.x;
                return lookingLeft ? spyX < watcherX : spyX > watcherX;
            }
        }
    }
}
