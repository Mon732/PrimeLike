using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
    public float delay;

    ParticleSystem particleSystem;

	// Use this for initialization
	void Start ()
    {
        particleSystem = GetComponent<ParticleSystem>();

        Invoke("DisableEffects", delay - particleSystem.startLifetime);
        Destroy(gameObject, delay);
    }

    void DisableEffects()
    {
        particleSystem.Stop();
        GetComponent<SphereCollider>().enabled = false;
    }
}
