using System.Collections.Generic;
using UnityEngine;

namespace Hairibar.EngineExtensions
{
    /// <summary>
    /// Provides functionality for getting primitive Meshes
    /// </summary>
    public static class PrimitiveHelper
    {
        private static Dictionary<PrimitiveType, Mesh> primitiveMeshes = new Dictionary<PrimitiveType, Mesh>();

        /// <summary>
        /// Wraps GameObject.CreatePrimitive() that makes the included Collider optional.
        /// </summary>
        public static GameObject CreatePrimitiveGameObject(PrimitiveType type, bool withCollider)
        {
            if (withCollider) { return GameObject.CreatePrimitive(type); }

            //If a collider is not requested, we make the GameObject ourselves. 
            //This way we skip having to destroy the Collider, avoiding some unnecessary garbage generation.
            GameObject gameObject = new GameObject(type.ToString());
            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = GetPrimitiveMesh(type);
            gameObject.AddComponent<MeshRenderer>();

            return gameObject;
        }

        /// <summary>
        /// The meshes are cached, so garbage is only generated the first time a specific mesh is requested.
        /// </summary>
        public static Mesh GetPrimitiveMesh(PrimitiveType type)
        {
            if (!primitiveMeshes.ContainsKey(type))
            {
                CreatePrimitiveMesh(type);
            }

            return primitiveMeshes[type];
        }


        private static Mesh CreatePrimitiveMesh(PrimitiveType type)
        {
            GameObject gameObject = GameObject.CreatePrimitive(type);
            Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
            Object.DestroyImmediate(gameObject);

            primitiveMeshes[type] = mesh;
            return mesh;
        }
    }
}