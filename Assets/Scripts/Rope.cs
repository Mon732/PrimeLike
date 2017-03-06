using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour
{
    GameObject leftHand;
    LineRenderer lineRenderer;


	// Use this for initialization
	void Start ()
    {
        leftHand = GameObject.FindGameObjectWithTag("LeftHand");
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPosition(0, leftHand.transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }
	
	// Update is called once per frame
	void Update ()
    {
        lineRenderer.SetPosition(0, leftHand.transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }
}
