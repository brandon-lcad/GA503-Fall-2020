using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightController : MonoBehaviour
{
    public enum TODController
    {
        Morning,
        Day,
        Evening,
        Night
    }
    public TODController timeController;
    public bool AutoRotate;
    [Range(0.5f, 10.0f)]
    public float rotationTimeScalar = 1;

    private Light Sun;
    private Light Moon;

    private float timeOfDay;

    void Start()
    {
        Sun = GameObject.FindWithTag("Sun").GetComponent<Light>();
        Moon = GameObject.FindWithTag("Moon").GetComponent<Light>();
    }

    void LateUpdate()
    {
        if (!AutoRotate)
        {
            if (timeController == TODController.Morning)
            {
                transform.rotation = Quaternion.Euler(0, Quaternion.identity.y, Quaternion.identity.z);
            }
            if (timeController == TODController.Day)
            {
                transform.rotation = Quaternion.Euler(90, Quaternion.identity.y, Quaternion.identity.z);
            }
            if (timeController == TODController.Evening)
            {
                transform.rotation = Quaternion.Euler(180, Quaternion.identity.y, Quaternion.identity.z);
            }
            if (timeController == TODController.Night)
            {
                transform.rotation = Quaternion.Euler(270, Quaternion.identity.y, Quaternion.identity.z);
            }
        }

        if (AutoRotate)
        {
            transform.Rotate(new Vector3(-rotationTimeScalar * Time.deltaTime, 0, 0));

            #region Time Controllers
            if (transform.rotation.eulerAngles.y >= 0 && transform.rotation.eulerAngles.y <= 39)
            {
                timeController = TODController.Evening;
                Debug.Log("Evening");
            }
            if (transform.rotation.eulerAngles.y >= 40 && transform.rotation.eulerAngles.y <= 219)
            {
                timeController = TODController.Day;
                Debug.Log("Noon");
            }
            if (transform.rotation.eulerAngles.y >= 220 && transform.rotation.eulerAngles.y <= 269)
            {
                timeController = TODController.Morning;
                Debug.Log("Morning");
            }
            if (transform.rotation.eulerAngles.y >= 270 && transform.rotation.eulerAngles.y <= 358.0f)
            {
                timeController = TODController.Night;
                Debug.Log("Night");
            }
            if (transform.rotation.eulerAngles.y >= 360.0f)    //Resets rotation
            {
                transform.rotation = Quaternion.Euler(0, Quaternion.identity.y, Quaternion.identity.z);
            }
            #endregion

            if (timeController == TODController.Evening)
            {
                Sun.intensity = Mathf.Lerp(1, 0, Time.deltaTime * 3 * rotationTimeScalar);
            }

            if (timeController == TODController.Morning)
            {
                Sun.intensity = Mathf.Lerp(0, 1, Time.deltaTime * 3 * rotationTimeScalar);
            }
        }
    }
}
