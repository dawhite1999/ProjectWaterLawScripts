using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    //references
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    //variables
    [SerializeField] bool isGrounded;
    [SerializeField] float speed = 0;
    float currSpeed = 0;
    [SerializeField] float runSpeed = 0;
    [SerializeField] float jumpHeight = 0;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float groundDistance = 0.4f;
    Vector3 velocity;
    AudioMan audioMan;
    private void Start() { currSpeed = speed; audioMan = FindObjectOfType<AudioMan>(); }
    private void Update() { Movement(); }

    public void Movement()
    {
        if (GetComponent<Player>().disableInput == false)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded == true && velocity.y < 0)
                velocity.y = -2f;
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * currSpeed * Time.deltaTime);
            if (Input.GetButtonDown("Jump") && isGrounded == true)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            //Running
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && isGrounded == true ){ currSpeed = runSpeed; }
            if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded == true) { currSpeed = speed; }
        }
    }
}
