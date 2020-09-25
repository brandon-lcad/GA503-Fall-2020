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
    public float groundDistance = 1.02f; 
    public bool onGround = true; 

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

        // DETECT WHETHER OR NOT THE PLAYER IS ON THE GROUND
        RaycastHit hit;
        Vector3 center = transform.position + GetComponent<CapsuleCollider>().center;
        Debug.DrawRay(center, Vector3.down * groundDistance, Color.red);

        if (Physics.Raycast(center, Vector3.down, out hit, groundDistance)) {
            if (hit.transform.gameObject.tag != "Player") {
                onGround = true;
            }
        } else {
            onGround = false;
        }

        // JUMP WHEN SPACE IS TAPPED
        if (onGround && Input.GetKeyUp(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

    }
}
