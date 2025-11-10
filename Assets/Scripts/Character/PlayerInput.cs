using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    public Vector2 MoveInput {  get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsShooting { get; private set; }
    public bool IsReloading { get; private set; }
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();

        playerInputActions.Player.Move.performed += OnMove;
        playerInputActions.Player.Move.canceled += OnMove;

        playerInputActions.Player.Look.performed += OnLook;
        playerInputActions.Player.Look.canceled += OnLook;

        playerInputActions.Player.Shoot.performed += ctx => IsShooting = ctx.ReadValueAsButton();
        playerInputActions.Player.Shoot.canceled += ctx => IsShooting = false;

        playerInputActions.Player.Reload.performed += ctx => IsReloading = ctx.ReadValueAsButton();
        playerInputActions.Player.Reload.canceled += ctx => IsReloading = false;

        playerInputActions.Player.Pause.performed += ctx => IsPaused = true;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext ctx) 
    {
        LookInput = ctx.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        IsPaused = false;
    }
}
