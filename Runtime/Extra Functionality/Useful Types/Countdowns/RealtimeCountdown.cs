using UnityEngine;

namespace Hairibar.EngineExtensions
{
    [System.Serializable]
    public class RealtimeCountdown : Countdown
    {
        protected override float CurrentTime => Time.unscaledTime;


        public RealtimeCountdown() { }

        public RealtimeCountdown(float length) : base(length) { }
    }
}
