using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PistolAndRevolver : Revolver
{

    // Use this for initialization
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<RectTransform>();
        leftHand = GameObject.FindGameObjectWithTag("LeftHand");
        rightHand = GameObject.FindGameObjectWithTag("RightHand");

        currentCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetState() == GameManager.GameState.Game)
        {
            if (InputManager.Instance.GetKeyDown(InputManager.Controls.Fire))
            {
                Fire();
            }

            if (InputManager.Instance.GetKeyDown(InputManager.Controls.FireAlt))
            {
                FireAlt();
            }
        }
    }

    protected override void FireAlt()
    {
        if (currentCooldown < Time.time)
        {
            Debug.Log("altfire");
            RaycastHit hit = new RaycastHit();
            
            if (GetTarget(out hit))
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
            }

            currentCooldown = Time.time + altCooldown;
        }
    }
}
