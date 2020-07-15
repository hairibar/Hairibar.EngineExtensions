using UnityEngine;

namespace Hairibar.EngineExtensions
{
    [System.Serializable]
    public abstract class Countdown
    {
        public float Length
        {
            get => _length;
            set => _length = value;
        }
        [SerializeField] float _length;

        public bool IsFinished => TimeAtStart + Length < CurrentTime;
        public float TimeAtStart { get; private set; } = float.NegativeInfinity;
        public float Progress => Mathf.Clamp01((CurrentTime - TimeAtStart) / _length);

        protected abstract float CurrentTime { get; }



        public void Start()
        {
            TimeAtStart = CurrentTime;
        }


        protected Countdown() { }

        protected Countdown(float length)
        {
            _length = length;
        }
    }
}
