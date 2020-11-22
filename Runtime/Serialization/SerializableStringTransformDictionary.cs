using UnityEngine;

#pragma warning disable CA2229 // Implement serialization constructors
namespace Hairibar.EngineExtensions.Serialization
{
    [System.Serializable]
    public class SerializableStringTransformDictionary : SerializableDictionary<string, Transform> { }
}