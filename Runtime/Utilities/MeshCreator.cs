using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public static class MeshCreator
    {
        public static Mesh CreateCone2D(float length, float angle, int subdivisions)
        {
            angle *= Mathf.Deg2Rad;
            float startAngle = -angle / 2;
            float endAngle = angle / 2;

            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[subdivisions + 1];
            Vector2[] uvs = new Vector2[vertices.Length];
            int[] triangles = new int[(subdivisions - 1) * 3];

            Vector2 centerVector = Vector2.right;
            //Make the vertices
            for (int i = 0; i < subdivisions; i++)
            {
                float t = i / ((float) subdivisions - 1);
                float currentAngle = Mathf.Lerp(startAngle, endAngle, t);
                Vector2 vertexPosition = Vector2Extensions.Rotate(centerVector, currentAngle) * length;

                vertices[i + 1] = vertexPosition;
            }
            mesh.SetVertices(vertices);

            //UVs
            for (int i = 0; i < vertices.Length; i++)
            {
                float u = (i - 1) / (float) subdivisions;
                float v = 1 - vertices[i].magnitude / length;
                uvs[i] = new Vector2(u, v);
            }
            mesh.SetUVs(0, uvs);

            // Create triangles
            for (int i = 0, triangleCount = 1; i < triangles.Length; i += 3, triangleCount++)
            {
                triangles[i] = 0;
                triangles[i + 2] = triangleCount;
                triangles[i + 1] = triangleCount + 1;
            }
            mesh.SetTriangles(triangles, 0);

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();

            return mesh;
        }
    }
}
