using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float masxDistance;

    private Vector3 aimWorldPosition;

    private void Awake()
    {
        if(mainCamera == null) mainCamera = Camera.main;
    }

    private void Update()
    {
        UpdateAimPosition();
    }

    private void UpdateAimPosition() 
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, masxDistance, groundMask)) 
        {
            aimWorldPosition = hit.point;
        }
    }

    public Vector3 GetAimWorldPosition() => aimWorldPosition;
}
