using UnityEngine;

#pragma warning disable CA1815 // Override equals and operator equals on value types
namespace Hairibar.EngineExtensions
{
    [System.Serializable]
    public struct MinMaxRange
    {
        public float min;
        public float max;

        public float GetRandomValue()
        {
            return Random.Range(min, max);
        }

        public bool Contains(float value)
        {
            return min <= value && value <= max;
        }

        public float Lerp(float t)
        {
            return Mathf.Lerp(min, max, t);
        }


#pragma warning disable CA1034 // Nested types should not be visible
        public sealed class SliderAttribute : System.Attribute
        {
            public float min;
            public float max;


            public SliderAttribute(float min, float max)
            {
                this.min = min;
                this.max = max;
            }
        }
    }
}
