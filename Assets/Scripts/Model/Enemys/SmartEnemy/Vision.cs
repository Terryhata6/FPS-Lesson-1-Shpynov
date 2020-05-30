using UnityEngine;

namespace Game
{

    public sealed class Vision
    {
        #region Vision
        public float _activeAng = 35;
        #endregion
        #region Methods
        public bool VisionM(Transform player, Transform target, float ActiveDis, out float SqrCurrentDis)
        {
            return Distance(player, target, ActiveDis, out SqrCurrentDis) && Angle(player, target) && !CheckBloked(player, target);
        }

        private bool CheckBloked(Transform player, Transform target)
        {
            if (!Physics.Linecast(player.position, target.position, out var hit)) return true;
            return hit.transform != target;
        }

        private bool Angle(Transform player, Transform target)
        {
            var angle = Vector3.Angle(target.position - player.position, player.forward);
            return angle <= _activeAng;
        }

        private bool Distance(Transform player, Transform target, float ActiveDis, out float SqrCurrentDis)
        {
            SqrCurrentDis = (player.position - target.position).sqrMagnitude;
            return SqrCurrentDis <= ActiveDis * ActiveDis;
        }
        #endregion
    }

}