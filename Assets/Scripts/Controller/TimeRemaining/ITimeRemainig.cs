using System;

namespace Game
{
    public interface ITimeRemaining
    {
        Action Method { get; }
        bool IsRepeating { get; }
        float Time { get; }
        float CurrentTime { get; set; }
        float Progress { get; set; }
        float InverseTime { get; set; }
    }    
}
