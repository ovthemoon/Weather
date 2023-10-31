using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ShowNormals : MonoBehaviour
{
    public float normalLength = 0.5f;
    public Color normalColor = Color.blue;

    private Mesh mesh;

    private void OnDrawGizmos()
    {
        if (mesh == null)
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            if (meshFilter)
            {
                mesh = meshFilter.sharedMesh;
            }
        }

        if (mesh)
        {
            Vector3[] vertices = mesh.vertices;
            Vector3[] normals = mesh.normals;

            for (int i = 0; i < vertices.Length; i++)
            {
                DrawNormal(vertices[i], normals[i]);
            }
        }
    }

    private void DrawNormal(Vector3 vertex, Vector3 normal)
    {
        Gizmos.color = normalColor;
        Vector3 worldVertex = transform.TransformPoint(vertex);
        Gizmos.DrawLine(worldVertex, worldVertex + transform.TransformDirection(normal) * normalLength);
    }
}
