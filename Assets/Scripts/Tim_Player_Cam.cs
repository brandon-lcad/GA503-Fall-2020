using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Tim_Player_Cam : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float health = 0;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Slider healthSlider;

    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            healthSlider.value = Mathf.Clamp(value / 100, 0, 1);
        }
    }

    [Header("Hazards")]
    [SerializeField] private bool isBurning = false;
    [SerializeField] private float burningDamage = 10f;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float snapAngle = 90f;
    [SerializeField] private Vector3 cameraOffset;

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
        cameraOffset = transform.position - Camera.main.transform.position;
        Health = maxHealth;
        rotateSpeed *= 100f;
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called before each physics update
    void FixedUpdate()
    {
        // JUMP WHEN SPACE IS TAPPED
        if (isGrounded && Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get WASD or arrow key input
        // playerInput = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalInput, 0, verticalInput)*moveSpeed*Time.deltaTime;
        Camera.main.transform.position = transform.position + cameraOffset;

        // First Person View shit tim puts in




        // Update the transform based on input and speed

        //transform.position += playerInput * moveSpeed * Time.deltaTime;

        // Detect whether or not we are on the ground
        RaycastHit hit;
        Vector3 center = transform.position + GetComponent<CapsuleCollider>().center;
        Debug.DrawRay(center, Vector3.down * groundDistance, Color.red);

        if (Physics.Raycast(center, Vector3.down, out hit, groundDistance))
        {
            if (hit.transform.gameObject.tag != "Player")
            {
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }

        // Jump when the space bar key is tapped (goes up)
        if (isGrounded && Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

        // Do damage while the Player is standing in lava
        if (isBurning)
        {
            Health -= burningDamage * Time.deltaTime;
        }

        if (health <= 0)
        {
            Debug.Log("You died!");
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Lava")
        {
            isBurning = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Lava")
        {
            isBurning = false;
        }
    }
}
