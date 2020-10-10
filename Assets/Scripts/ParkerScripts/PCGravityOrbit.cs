using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGravityOrbit : MonoBehaviour
{

    //the center that all objects orbit
    public float Gravity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PCGravityControl>())
        {
            //if this object has a gravity script, set this as the planet
            other.GetComponent<PCGravityControl>().Gravity = this.GetComponent<PCGravityOrbit>();
        }
    }
}
