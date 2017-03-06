using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour
{
    public float speed;
    public float duration;

    float time;
    Rigidbody rigidbody;

	// Use this for initialization
	void Start ()
    {
        time = Time.time + duration;
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = transform.forward * speed;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (time < Time.time)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision coll)
    {
        coll.gameObject.GetComponent<Renderer>().material.color = Color.green;
        Destroy(gameObject);
    }
}
