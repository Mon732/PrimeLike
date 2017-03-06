using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SteamGauntletAndRevolver : Revolver
{
    public float jetRange;
    public float jetDuration;
    public GameObject steamJetPrefab;
    public GameObject steamCloudPrefab;

    GameObject steamJet;

    // Use this for initialization
    void Start()
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
    void Update()
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

            steamJet = (GameObject)Instantiate(steamJetPrefab, leftHand.transform.position, camera.transform.rotation, leftHand.transform);
            steamJet.GetComponent<ParticleSystem>().Play();
            Invoke("StopJet", jetDuration);

            if (GetTarget(out hit, jetRange))
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
                Instantiate(steamCloudPrefab, hit.point, Quaternion.identity);
            }

            currentCooldown = Time.time + altCooldown;
        }
    }

    void StopJet()
    {
        steamJet.GetComponent<ParticleSystem>().Stop();
        Destroy(steamJet);
    }
}
