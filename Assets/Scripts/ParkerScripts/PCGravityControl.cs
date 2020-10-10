using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGravityControl : MonoBehaviour
{
    //nearby gravity to include in the orbit
    public PCGravityOrbit Gravity;

    //build the Rb beforehand
    private Rigidbody Rb;

    //how fast does it rotate (i think)
    public float RotationSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {

        //getting component
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Gravity) //if there is a set planet to orbit
        {
            //initiating the gravityup vector3
            Vector3 gravityUp = Vector3.zero;

            //the player's position minus the position of the planet, i think
            gravityUp = (transform.position - Gravity.transform.position).normalized;

            //the player's up is localup, i think
            Vector3 localUp = transform.up;

            //Quaternion targetrotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;

            //make the player smoothly rotate to face the correct direction
            transform.up = Vector3.Lerp(transform.up, gravityUp, RotationSpeed * Time.deltaTime);

            //push down for gravity
            Rb.AddForce((-gravityUp * Gravity.Gravity) * Rb.mass);
        }
    }
}
