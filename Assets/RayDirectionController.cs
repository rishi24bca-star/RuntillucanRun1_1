
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class RayDirectionController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform Player_Gun;
    [SerializeField] private Camera cam;

    [Header("Ray Settings")]
    [SerializeField] private float distance = 4f;
    [SerializeField] private LayerMask groundLayer;
     private PlayerMovement playerMovement;
    private LineRenderer lineRenderer;

    private void Awake()
    {
playerMovement = GetComponent<PlayerMovement>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        if (cam == null || Player_Gun == null ||
            lineRenderer == null || Mouse.current == null)
        {
            return;
        }

        // Get mouse position
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // Camera ray through mouse position
        Ray ray = cam.ScreenPointToRay(mousePosition);

        Vector3 targetPoint;

        // Find the mouse target on the ground
        if (Physics.Raycast(ray, out RaycastHit hit,
            Mathf.Infinity, groundLayer))
        {
            targetPoint = hit.point;
        }
        else
        {
            // Fallback if ground is not hit
            targetPoint = ray.origin + ray.direction * 100f;
        }

        // Gun starting position
        Vector3 gunPosition = Player_Gun.position;
        gunPosition.y = -0.15f;
        // Keep target on the gun's horizontal plane
        targetPoint.y = -0.15f;

        // Calculate direction only
        Vector3 direction = playerMovement.Direction;

        // Prevent zero direction
        if (direction.sqrMagnitude < 0.0001f)
            return;

        direction.Normalize();

        // ALWAYS use the specified distance
        Vector3 endPoint = gunPosition + direction * distance/12;

        // Draw fixed-length line
        lineRenderer.SetPosition(0, gunPosition);
        lineRenderer.SetPosition(1, endPoint);

        lineRenderer.enabled = true;
        
    }
}
