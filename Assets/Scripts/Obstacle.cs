using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Rotation")]
    public bool canRotate = false; 
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] [Range(0, 90)] private int xRotation, yRotation, zRotation;

    [Header("Position")]
    public bool canMove = false;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private GameObject positionA, positionB;
    private Vector3 lastPosition, nextPosition;

    private GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (canMove) {
            lastPosition = positionA.transform.position;
            nextPosition = positionB.transform.position;
            transform.position = lastPosition;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Update rotation over time
        if (canRotate) {
            transform.Rotate(new Vector3(xRotation, yRotation, zRotation) * (rotationSpeed * Time.deltaTime));
        }
        

        // Update position between A and B
        if (canMove) {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, (movementSpeed * Time.deltaTime));

            if (transform.position == nextPosition) {
                if (nextPosition == positionA.transform.position) {
                    nextPosition = positionB.transform.position;
                } else if (nextPosition == positionB.transform.position) {
                    nextPosition = positionA.transform.position;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == player) {
            player.transform.SetParent(transform, true);
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject == player) {
            player.transform.parent = null;
        }
    }


}
