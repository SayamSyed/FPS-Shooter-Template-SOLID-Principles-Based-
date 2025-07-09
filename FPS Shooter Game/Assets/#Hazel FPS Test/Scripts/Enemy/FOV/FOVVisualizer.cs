using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FOVVisualizer : MonoBehaviour
{
    public EnemyDetectionConfig detectionConfig;

    public Transform originTransform; // Usually the enemy's transform or eye position

    private Mesh mesh;

    private void Start()
    {
        mesh = new Mesh();
        mesh.name = "FOV Mesh";
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void LateUpdate()
    {
        if (detectionConfig == null || originTransform == null) return;
        DrawFOV();
    }

    void DrawFOV()
    {
        float viewAngle = detectionConfig.viewAngle;
        float viewRadius = detectionConfig.detectionRange;
        int meshResolution = detectionConfig.meshResolution;

        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        Vector3[] vertices = new Vector3[stepCount + 2];
        int[] triangles = new int[stepCount * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = -viewAngle / 2 + stepAngleSize * i;
            Vector3 dir = DirFromAngle(angle);
            Vector3 worldOrigin = originTransform.position + Vector3.up * 1.5f;

            Vector3 rayDir = originTransform.rotation * dir;
            Vector3 endPoint = rayDir * viewRadius;

            // Check if we hit an obstacle
            if (Physics.Raycast(worldOrigin, rayDir, out RaycastHit hit, viewRadius, ~detectionConfig.ignoreVisionLayers))
            {
                endPoint = rayDir.normalized * hit.distance;
            }

            vertices[i + 1] = Quaternion.Inverse(originTransform.rotation) * endPoint;
        }

        for (int i = 0; i < stepCount; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    Vector3 DirFromAngle(float angleInDegrees)
    {
        return Quaternion.Euler(0, angleInDegrees, 0) * Vector3.forward;
    }
}
