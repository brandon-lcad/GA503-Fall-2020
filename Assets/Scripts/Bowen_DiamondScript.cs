using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowen_DiamondScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(39.7, 60, 132);

        Vector3 newPosition = transform.position; // We store the current position
        newPosition.y = 70; // We set a axis, in this case the y axis
        transform.position = newPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
    }
}
