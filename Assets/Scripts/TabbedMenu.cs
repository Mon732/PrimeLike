using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TabbedMenu : MonoBehaviour
{
    public GameObject[] tabs;
    public GameObject[] subMenus;

    public Color NormalColour;
    public Color PressedColour;

    Menus currentMenu;

    [System.Serializable]
    public enum Menus
    {
        Video,
        Audio,
        Controls
    };

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnEnable()
    {
        SetMenu(0);
    }

    public void SetMenu(int newMenu)
    {
        currentMenu = (Menus)newMenu;

        for (int i = 0; i < subMenus.Length; i++)
        {
            if (newMenu == i)
            {
                subMenus[i].SetActive(true);
                tabs[i].GetComponent<Image>().color = PressedColour;
            }
            else
            {
                subMenus[i].SetActive(false);
                tabs[i].GetComponent<Image>().color = NormalColour;
            }
        }
    }
}
