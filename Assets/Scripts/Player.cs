using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // VARIABLES FOR INPUT AND SPEED
    public float moveSpeed = 5.0f;
    private float horizontal, vertical;

    // VARIABLES FOR JUMP HEIGHT AND RIGIDBODY
    public float jumpHeight = 5.0f;
    public bool isGrounded = true; 

    private Rigidbody rb; 

    // Awake is called before the game starts
    void Awake() 
    {
        // Debug.Log("Awake!");
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called before each physics update
    void FixedUpdate() 
    {
        // JUMP WHEN SPACE IS TAPPED
        if (isGrounded && Input.GetKeyUp(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // GET THE PLAYER'S WASD INPUT
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // MOVE THIS GAMEOBJECT IN THAT DIRECTION
        Vector3 movement = new Vector3(horizontal, 0f, vertical); 
        transform.position += movement * moveSpeed * Time.deltaTime;

        // ROTATE THE PLAYER TOWARD THE MOVEMENT DIRECTION
        if (movement != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        }
    }


    //TODO -- Fix ground detection later!

    /*
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Ground") {
            isGrounded = true; 
        }
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }
    */
}
