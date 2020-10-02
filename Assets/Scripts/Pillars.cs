using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillars : MonoBehaviour
{
    [SerializeField] float raiseHeight = 0f;
    [SerializeField] float startingHeight = -7.15f;

    [SerializeField] float raiseSpeed;

    public bool raising;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(transform.position.x, startingHeight, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y >= raiseHeight)
        {
            raising = false;
        }
        if (transform.localPosition.y <= startingHeight)
        {
            raising = true;
        }

        if(raising)
        {
            transform.position += new Vector3(0, raiseSpeed *Time.deltaTime,0);
        }
        else
        {
            transform.position -= new Vector3(0, raiseSpeed * Time.deltaTime,0);
        }

        
    }
}
