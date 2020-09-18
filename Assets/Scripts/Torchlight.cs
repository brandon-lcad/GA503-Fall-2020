using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchlight : MonoBehaviour
{
    private Light lightObj;
    private float minIntensity = 0.8f;
    private float maxIntensity = 1.2f;
    private int smoothValues = 5;
    private Queue<float> smoothQueue;
    private float lastSum;

    // Start is called before the first frame update
    void Start()
    {
        smoothQueue = new Queue<float>(smoothValues);
        lightObj = GetComponent<Light>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Removes values above max count
        while (smoothQueue.Count >= smoothValues)
        {
            lastSum -= smoothQueue.Dequeue();
        }
        //Creates a new value in field, assigns
        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        //Changes value to newest one
        lastSum += newVal;

        lightObj.intensity = lastSum / smoothQueue.Count;
    }
}
