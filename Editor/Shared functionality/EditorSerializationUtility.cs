using System.IO;
using UnityEditor;
using UnityEngine;

namespace Hairibar.EngineExtensions.Editor
{
    public static class EditorSerializationUtility
    {
        public static void Deserialize(string directory, object serializedObject, Object target)
        {
            string path = GetFilePath(directory, target);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                EditorJsonUtility.FromJsonOverwrite(json, serializedObject);
            }
        }

        public static void Serialize(string directory, object serializedObject, Object target)
        {
            Directory.CreateDirectory(directory);
            string path = GetFilePath(directory, target);

            if (!File.Exists(path)) File.Create(path).Dispose();

            File.WriteAllText(path, EditorJsonUtility.ToJson(serializedObject, false));
        }

        static string GetFilePath(string directory, Object target)
        {
            if (!directory.EndsWith("/")) directory += "/";
            return $"{directory}{target.GetInstanceID()}.json";
        }
    }
}
