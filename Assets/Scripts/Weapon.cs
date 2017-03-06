using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public float cooldown;
    public float altCooldown;
    public int maxAmmo;

    public GameObject bulletHoleDecal;

    protected Animator animator;
    protected Camera camera;
    protected RectTransform crosshair;
    protected GameObject leftHand;
    protected GameObject rightHand;
    protected HUD hud;
    protected float currentCooldown;
    protected int currentAmmo;

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

    protected void Fire()
    {
        if (currentCooldown < Time.time)
        {
            Debug.Log("fire");

            currentAmmo--;

            RaycastHit hit = new RaycastHit();

            if (GetTarget(out hit))
            {
                Debug.Log("fire");
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                Instantiate(bulletHoleDecal, hit.point + (hit.normal * 0.001f), Quaternion.FromToRotation(-Vector3.forward, hit.normal));
            }

            animator.SetBool("Firing", true);
            animator.Play("Fire", -1, 0.0f);

            currentCooldown = Time.time + cooldown;

            UpdateHUD();
        }
    }

    protected virtual void FireAlt()
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

    protected void Reload()
    {
        currentAmmo = maxAmmo;
        UpdateHUD();
    }

    protected bool GetTarget(out RaycastHit hit, float range = Mathf.Infinity)
    {
        Ray ray = camera.ScreenPointToRay(crosshair.position);
        int layerMask = ~((1 << 9) | (1 << 10) | (1 << 13));
        return Physics.Raycast(ray, out hit, range, layerMask, QueryTriggerInteraction.UseGlobal);
    }

    protected void UpdateHUD()
    {
        hud.ammoCountText.GetComponent<Text>().text = currentAmmo.ToString();
    }
}