using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCPlayer : MonoBehaviour
{
    public float speed = 6f;
    private Vector3 moveDir;
    private Rigidbody Rb;


    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        Rb.MovePosition(Rb.position + transform.TransformDirection(moveDir) * speed * Time.deltaTime);
    }
}
