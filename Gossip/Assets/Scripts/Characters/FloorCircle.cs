using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FloorCircle : MonoBehaviour
{
    [Header("Circle Settings")]
    public float radius = 1.0f;           
    public float thickness = 0.1f;        
    public int segments = 100;            
    public Color circleColor = Color.red; 
    public LayerMask groundLayer;         
    public bool showInEditor = true;      

    private LineRenderer lineRenderer;
    private Material _LineMat;
    [SerializeField] private CapsuleCollider _CapsuleCollider;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = true;

        // Create a unique instance of the first material to ensure changes apply
        lineRenderer.materials[0] = new Material(lineRenderer.materials[0]);
        _LineMat = lineRenderer.materials[0];

        
    }

    void Update()
    {
        UpdateCircle();
    }

    void UpdateCircle()
    {
        radius = _CapsuleCollider.radius;

        _LineMat.SetColor("_Color", circleColor);

        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;

        lineRenderer.startColor = circleColor;
        lineRenderer.endColor = circleColor;

        Vector3[] circlePoints = new Vector3[segments + 1];
        float angleStep = 360f / segments;
        Vector3 playerPosition = transform.position;

        float offset = thickness / 2;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            Vector3 pointPosition = playerPosition + new Vector3(x, 0, z);

            RaycastHit hit;
            if (Physics.Raycast(pointPosition + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, groundLayer))
            {
                pointPosition.y = hit.point.y + offset;
            }
            else
            {
                pointPosition.y = playerPosition.y + offset;
            }

            circlePoints[i] = pointPosition;
        }

        lineRenderer.positionCount = segments + 1;
        lineRenderer.SetPositions(circlePoints);
    }

    void OnDrawGizmos()
    {
        if (!showInEditor) return; 

        Gizmos.color = circleColor;
        Vector3 playerPosition = transform.position;

        Vector3 lastPoint = Vector3.zero;
        float angleStep = 360f / segments;

        float offset = thickness / 2;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            Vector3 pointPosition = playerPosition + new Vector3(x, 0, z);
            RaycastHit hit;

            if (Physics.Raycast(pointPosition + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, groundLayer))
            {
                pointPosition.y = hit.point.y + offset; 
            }

            if (i > 0)
            {
                Gizmos.DrawLine(lastPoint, pointPosition); 
            }
            lastPoint = pointPosition;
        }
    }
}
