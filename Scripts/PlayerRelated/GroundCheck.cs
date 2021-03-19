using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            //ground check true
            GetComponentInParent<PlayerMovement>().isGrounded2 = true;
            //resets walk speed if not holding shift after a jump
            if (GetComponentInParent<PlayerMovement>().isSprinting == false)
                GetComponentInParent<PlayerMovement>().currSpeed = GetComponentInParent<PlayerMovement>().speed;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
            GetComponentInParent<PlayerMovement>().isGrounded2 = false;
    }
}
