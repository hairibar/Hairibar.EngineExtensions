using UnityEngine;

namespace Hairibar.EngineExtensions
{
    [System.Serializable]
    public class GametimeCountdown : Countdown
    {
        protected override float CurrentTime => Time.time;


        public GametimeCountdown() { }

        public GametimeCountdown(float length) : base(length) { }
    }
}
