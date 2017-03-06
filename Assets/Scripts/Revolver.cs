using UnityEngine;
using System.Collections;

public class Revolver : Weapon
{

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
        }
    }
}
