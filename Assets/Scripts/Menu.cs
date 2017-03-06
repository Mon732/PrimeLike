using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject hud;
    public GameObject[] subMenus;
    public Toggle fullscreenToggle;
    public Dropdown resDropdown;
    public Slider fovSlider;
    public Text fovText;

    bool menuVisible;
    Menus currentMenu;
    Camera camera;

    //string[] testResolutions = new string[]{"640 x 480 @ 60Hz", "720 x 480 @ 60Hz", "720 x 576 @ 60Hz", "800 x 600 @ 60Hz", "1024 x 768 @ 60Hz", "1152 x 864 @ 75Hz", "1280 x 720 @ 59Hz", "1280 x 768 @ 59Hz", "1280 x 800 @ 59Hz", "1280 x 960 @ 60Hz", "1280 x 1024 @ 60Hz", "1360 x 768 @ 59Hz", "1366 x 768 @ 59Hz", "1440 x 900 @ 59Hz", "1600 x 900 @ 60Hz", "1600 x 1024 @ 59Hz", "1680 x 1050 @ 59Hz", "1920 x 1080 @ 60Hz"};

    [System.Serializable]
    public enum Menus
    {
        Main,
        Options
    };


	// Use this for initialization
	void Start ()
    {
        Unpause();

        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        resDropdown.ClearOptions();

        for (int i = 0; i < Screen.resolutions.Length; i++) //(Resolution res in Screen.resolutions)
        {
            Resolution res = Screen.resolutions[i];

            resDropdown.options.Add(new Dropdown.OptionData(res.ToString()));

            if (res.ToString() == Screen.currentResolution.ToString())
            {
                resDropdown.value = i;
                fullscreenToggle.isOn = Screen.fullScreen;
            }
        }

        resDropdown.RefreshShownValue();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (InputManager.Instance.GetKeyDown(InputManager.Controls.Menu))
        {
            if (menuVisible)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Pause()
    {
        GameManager.Instance.SetState(GameManager.GameState.Paused);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        menu.SetActive(true);
        hud.SetActive(false);
        menuVisible = true;
    }

    public void Unpause()
    {
        GameManager.Instance.SetState(GameManager.GameState.Game);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        SetMenu(0);
        menu.SetActive(false);
        hud.SetActive(true);
        menuVisible = false;
    }

    public void SetMenu(int newMenu)
    {
        currentMenu = (Menus)newMenu;

        for (int i = 0; i < subMenus.Length; i++)
        {
            if (newMenu == i)
            {
                subMenus[i].SetActive(true);
            }
            else
            {
                subMenus[i].SetActive(false);
            }
        }
    }

    public void SetResolution()
    {
        int value = resDropdown.value;
        Resolution res = Screen.resolutions[value];
        bool fullscreen = fullscreenToggle.isOn;

        Screen.SetResolution(res.width, res.height, fullscreenToggle.isOn, res.refreshRate);

        GameManager.Instance.settings.video.fullScreen = fullscreen;
        GameManager.Instance.settings.video.width = res.width;
        GameManager.Instance.settings.video.height = res.height;
    }

    public void SetFov()
    {
        fovSlider.value = (((int)fovSlider.value) / 5) * 5;
        fovText.text = fovSlider.value.ToString();
        camera.fieldOfView = fovSlider.value;
        GameManager.Instance.settings.video.fieldOfView = fovSlider.value;
    }
}
