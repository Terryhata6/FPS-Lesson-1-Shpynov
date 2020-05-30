using System;

namespace Game
{
    public sealed class TimeRemaining : ITimeRemaining
    {
        #region ITimeRemaining

        public Action Method { get; }
        public bool IsRepeating { get; }
        public float Time { get; }
        public float CurrentTime { get; set; }
        public float Progress { get; set; }
        public float InverseTime { get; set; }


        #endregion


        #region ClassLifeCycles

        public TimeRemaining(Action method, float time, bool isRepeating = false)
        {
            Method = method;
            Time = time;
            CurrentTime = time;
            IsRepeating = isRepeating;
            if (time != 0)  InverseTime = 1.0f / time; 
            else InverseTime = 0.0f;
            Progress = CurrentTime * InverseTime;
        }

        #endregion
    }
}
