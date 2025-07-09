using UnityEngine;

[CreateAssetMenu(fileName = "New Detection Config", menuName = "FPS/Detection Config")]
public class EnemyDetectionConfig : ScriptableObject
{
    [Header("FOV Settings")]
    public float viewAngle = 90f;
    public float detectionRange = 10f;
    public float sphereCastRadius = 0.25f;

    [Tooltip("Number of rays per degree of FOV. Higher = smoother mesh, lower = faster.")]
    [Range(1, 100)]
    public int meshResolution = 5;

    [Header("Movement")]
    public float stoppingDistance = 2f;
    
    [Header("Attack Settings")]
    public float attackRange = 8f;
    
    [Header("Gizmo Debugging")]
    public bool drawGizmos = true;
    public Color viewConeColor = new Color(1f, 0.25f, 0f, 0.5f);
    public Color rangeColor = Color.yellow;

    [Header("Layers to Ignore")]
    public LayerMask ignoreVisionLayers;
}