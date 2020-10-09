using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PCGravBody : MonoBehaviour
{
    PCGravAttract planet;
    public Rigidbody rigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PCGravAttract>();

        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        planet.Attract(transform);
    }
}
