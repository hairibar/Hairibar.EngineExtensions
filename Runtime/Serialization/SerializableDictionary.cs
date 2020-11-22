using System.Collections.Generic;
using UnityEngine;

namespace Hairibar.EngineExtensions.Serialization
{
    public abstract class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] List<TKey> keys = new List<TKey>();
        [SerializeField] List<TValue> values = new List<TValue>();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();

            if (keys.Count != values.Count)
                throw new System.Exception(string.Format($"There are {keys.Count} keys and {values.Count} values after deserialization. Make sure that both key and value types are serializable."));

            for (int i = 0; i < keys.Count; i++)
                this.Add(keys[i], values[i]);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }
    }
}
