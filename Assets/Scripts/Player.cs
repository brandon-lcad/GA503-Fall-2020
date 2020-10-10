using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float health = 0;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Slider healthSlider;
    private bool dead = false; 

    public float Health {
        get { return health; }
        set {
            health = value;
            healthSlider.value = Mathf.Clamp(value / 100, 0, 1);
        }
    }

    [Header("Hazards")]
    [SerializeField] private bool isBurning = false;
    [SerializeField] private float burningDamage = 10f;

    [Header("Move")]
    [SerializeField] private float movement = 0f; 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float snapAngle = 90f;

    [Header("Jump")]
    [SerializeField] public float jumpHeight = 5f;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float groundDistance = 1.02f;

    [Header("Input")]
    [SerializeField] private Vector2 playerInput;

    private Rigidbody rb;
    private Animator animator;

    // Awake is called before the game starts
    void Awake() 
    {
        // Debug.Log("Awake!");
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        rotateSpeed *= 100f;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
        if (!dead) {
            // Get WASD or arrow key input
            playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // Translate the input relative to the camera
            Vector3 rootDirection = transform.forward;
            Vector3 moveDirection = new Vector3(playerInput.normalized.x, 0, playerInput.normalized.y);
            Vector3 cameraDirection = Camera.main.transform.forward; cameraDirection.y = 0f;
            Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, Vector3.Normalize(cameraDirection));
            moveDirection = referentialShift * moveDirection;

            // Rotate the player toward the movement direction
            if (playerInput != Vector2.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

                if (Quaternion.Angle(transform.rotation, targetRotation) > snapAngle) {
                    transform.rotation = targetRotation;
                } else {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                }
            }

            // Update the transform based on input and speed
            float moveMagnitude = Mathf.Clamp(Mathf.Abs(playerInput.magnitude), 0, 1);
            transform.position += (rootDirection * moveMagnitude) * moveSpeed * Time.deltaTime;
            movement = Mathf.Abs(moveMagnitude);
            animator.SetFloat("Movement", movement);

            // Detect whether or not we are on the ground
            RaycastHit hit;
            Vector3 center = transform.position + GetComponent<CapsuleCollider>().center;
            Debug.DrawRay(center, Vector3.down * groundDistance, Color.red);

            if (Physics.Raycast(center, Vector3.down, out hit, groundDistance)) {
                if (hit.transform.gameObject.tag != "Player") {
                    isGrounded = true;
                }
            } else {
                isGrounded = false;
            }

            animator.SetBool("Jump", !isGrounded);

            // Jump when the space bar key is tapped (goes up)
            if (isGrounded && Input.GetKeyUp(KeyCode.Space)) {
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }

            // Do damage while the Player is standing in lava
            if (isBurning) {
                Health -= burningDamage * Time.deltaTime;
            }

            if (health <= 0) {
                if (!dead) {
                    dead = true;
                    Debug.Log("You died!");
                    animator.SetTrigger("Die");
                }
            }
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Lava") { 
            isBurning = true;
        } 
    }

    void OnCollisionExit(Collision other) 
    {
        if (other.gameObject.tag == "Lava") {
            isBurning = false;
        }
    }
}
