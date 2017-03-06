using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GrappleAndRevolver : Revolver
{
    public GameObject Hook;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponentInChildren<Animator>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<RectTransform>();
        leftHand = GameObject.FindGameObjectWithTag("LeftHand");
        rightHand = GameObject.FindGameObjectWithTag("RightHand");
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

        currentAmmo = maxAmmo;

        currentCooldown = 0;

        UpdateHUD();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.Instance.GetState() == GameManager.GameState.Game)
        {
            if (InputManager.Instance.GetKeyDown(InputManager.Controls.Fire))
            {
                if (currentAmmo > 0)
                {
                    Fire();
                }
            }
            
            if (InputManager.Instance.GetKeyDown(InputManager.Controls.Reload))
            {
                Reload();
            }

            if (InputManager.Instance.GetKeyDown(InputManager.Controls.FireAlt))
            {
                WindUp();
            }

            if (InputManager.Instance.GetKeyUp(InputManager.Controls.FireAlt))
            {
                crosshair.gameObject.GetComponent<Image>().material.color = Color.white;
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

            //if (GetTarget(out hit))
            {
                Instantiate(Hook, leftHand.transform.position, camera.transform.rotation);
            }

            currentCooldown = Time.time + altCooldown;
        }
    }

    void WindUp()
    {
        Debug.Log("windup");
        crosshair.gameObject.GetComponent<Image>().material.color = Color.green;
    }
}
