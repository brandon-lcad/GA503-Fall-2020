using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFollow : MonoBehaviour {
    public GameObject follow;
    private Vector3 offset; 
    float distance;
    Vector3 lastPosition, moveDirection;

    void Start() {
        offset = transform.position - follow.transform.position; 
        distance = offset.magnitude;
        lastPosition = follow.transform.position;
    }

    void LateUpdate() {
        moveDirection = follow.transform.position - lastPosition;

        if (moveDirection != Vector3.zero) { 
            moveDirection = moveDirection.normalized;
            transform.position = follow.transform.position - moveDirection * distance;
            transform.LookAt(follow.transform.position); 
            lastPosition = follow.transform.position;
        }
    }

}
