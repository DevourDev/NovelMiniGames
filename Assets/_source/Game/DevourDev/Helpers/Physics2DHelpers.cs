using System;
using UnityEngine;

namespace DevourDev.Unity.Helpers
{
    public static class CastComponents2DHelpers<TComp> where TComp : UnityEngine.Component
    {
        private static readonly TComp[] _compsBuffer = new TComp[1024];
        private static readonly ReadOnlyMemory<TComp> _compsMem = new(_compsBuffer);


        public static ReadOnlyMemory<TComp> MemRayCast(Vector2 origin, Vector2 direction, float distance)
        {
            return MemRayCast(origin, direction, Physics2DHelpers.AllDetectingFilter, distance);
        }

        public static ReadOnlyMemory<TComp> MemRayCast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, float distance)
        {
            var mem = Physics2DHelpers.MemRayCast(origin, direction, contactFilter, distance);
            var span = mem.Span;

            var buffer = _compsBuffer;
            int compsCount = -1;
            int c = span.Length;

            for (int i = 0; i < c; i++)
            {
                if (span[i].collider.TryGetComponent<TComp>(out var desiredComp))
                {
                    buffer[++compsCount] = desiredComp;
                }
            }

            ++compsCount;

            if (compsCount > 0)
            {
                return _compsMem[..compsCount];
            }

            return ReadOnlyMemory<TComp>.Empty;
        }
    }

    public static class Physics2DHelpers
    {
        public static readonly ContactFilter2D AllDetectingFilter = new()
        {
            useDepth = false,
            useLayerMask = false,
            useNormalAngle = false,
            useOutsideDepth = false,
            useOutsideNormalAngle = false,
            useTriggers = true,
        };


        private static readonly RaycastHit2D[] _hitsBuffer = new RaycastHit2D[1024];
        private static readonly ReadOnlyMemory<RaycastHit2D> _hitsMem = new(_hitsBuffer);


        public static ReadOnlyMemory<RaycastHit2D> MemRayCast(Vector2 origin, Vector2 direction, float distance)
        {
            return MemRayCast(origin, direction, AllDetectingFilter, distance);
        }

        public static ReadOnlyMemory<RaycastHit2D> MemRayCast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, float distance)
        {
            var count = Physics2D.Raycast(origin, direction, contactFilter, _hitsBuffer, distance);

            if (count == 0)
                return ReadOnlyMemory<RaycastHit2D>.Empty;

            return _hitsMem[..count];
        }


        public static ReadOnlyMemory<RaycastHit2D> MemCast(this Collider2D collider, Vector2 direction, float distance)
        {
            int count = collider.Cast(direction, _hitsBuffer, distance, false);

            if (count == 0)
                return ReadOnlyMemory<RaycastHit2D>.Empty;

            return _hitsMem[..count];
        }

        public static ReadOnlyMemory<RaycastHit2D> MemCast(this Collider2D collider, Vector2 direction, ContactFilter2D filter, float distance)
        {
            int count = collider.Cast(direction, filter, _hitsBuffer, distance, false);

            if (count == 0)
                return ReadOnlyMemory<RaycastHit2D>.Empty;

            return _hitsMem[..count];
        }


        public static RaycastHit2D MemCastClosest(this Collider2D collider, Vector2 direction, ContactFilter2D filter, float maxDistance)
        {
            if (TryFindClosest(MemCast(collider, direction, filter, maxDistance), out var closest))
                return closest;

            return default;
        }

        public static bool TryFindClosest(ReadOnlyMemory<RaycastHit2D> hits, out RaycastHit2D closest)
        {
            closest = default;
            var count = hits.Length;

            if (count == 0)
                return false;

            float closestDist = float.PositiveInfinity;
            var span = hits.Span;

            for (int i = 0; i < count; i++)
            {
                var hit = span[i];
                var dist = hit.distance;

                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = hit;
                }
            }

            return closest;
        }
    }
}
