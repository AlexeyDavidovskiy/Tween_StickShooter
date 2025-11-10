using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move() 
    {
        Vector2 moveInput = playerInput.MoveInput;
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);

        if(direction.sqrMagnitude > 0.001f) 
        {
            characterController.Move(direction.normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void Rotate() 
    {
        if (playerAim == null) return;

        Vector3 aimTarget = playerAim.GetAimWorldPosition();
        Vector3 direction = aimTarget - transform.position;
        direction.y = 0f;

        if(direction.sqrMagnitude > 0.001f) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
        }
    }
}
