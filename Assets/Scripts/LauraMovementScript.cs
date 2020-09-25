using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauraMovementScript : MonoBehaviour
{
    public float moveSpeed;
    private float verticalInput;
    private float horizontalInput;

    public bool isGrounded = false;
    public float jumpHeight = 5;

    public Rigidbody rb;
    public Collider myCollider;
    //public Camera mainCamera;

    public float distToGround;

    // Start is called before the first frame update
    void Start()
    {
        distToGround = myCollider.bounds.extents.y;
    }

    void FixedUpdate()
    {
        Run();
        CheckGrounded();
    }

    private void Update()
    {
        //apparently this has to be in update to work smoothly, but I'm having trouble understanding why. Ask Brandon
        Jump();
        SetCamera();
    }

    void Run()
    {
        //movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        rb.AddForce(movement * moveSpeed * Time.fixedDeltaTime);

        //rotation
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 1f);
    }

    void Jump()
    {
        //JUMP WHEN SPACE IS TAPPED
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("hit space and is grounded");
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    void SetCamera()
    {
        Camera.main.transform.position = transform.position + new Vector3(0, 4, -5);
    }
}
