using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshExtensions
{
    public static void ChangeMeshScale(Vector3 scaleFactor, Mesh mesh, bool recalculateNormals)
    {
        Vector3[] _baseVertices = mesh.vertices;
        Vector3[] vertices = new Vector3[_baseVertices.Length];

        for (var i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = _baseVertices[i];
            vertex.x *= scaleFactor.x;
            vertex.y *= scaleFactor.y;
            vertex.z *= scaleFactor.z;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        if (recalculateNormals)
            mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
