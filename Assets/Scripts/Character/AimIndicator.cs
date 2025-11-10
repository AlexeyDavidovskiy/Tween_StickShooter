using UnityEngine;

public class AimIndicator : MonoBehaviour
{
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private float followSpeed;
    [SerializeField] private float aimYPosition;

    private void Update()
    {
        if(playerAim ==  null) return;

        Vector3 targetPos = playerAim.GetAimWorldPosition();
        targetPos.y += aimYPosition;

        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
