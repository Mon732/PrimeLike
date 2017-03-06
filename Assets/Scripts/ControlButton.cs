using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlButton : MonoBehaviour
{
    public Button button;
    public Text controlText;
    public Text buttonText;
    public InputManager.Controls control;

    GameObject inputPanel;
    bool checkingForInput;
    

	void Awake ()
    {
        inputPanel = GameObject.FindWithTag("InputPanel").transform.GetChild(0).gameObject;
        checkingForInput = false;
	}

    // Use this for initialization
    void Start()
    {
        SetControl(control);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (checkingForInput)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(keyCode))
                    {
                        InputManager.Instance.SetInput(control, keyCode, InputManager.Instance.GetInput(control).context);
                        SetControl(control);
                        inputPanel.SetActive(false);
                        checkingForInput = false;
                    }
                }
            }
        }
	}

    public void Clicked()
    {
        inputPanel.SetActive(true);
        checkingForInput = true;
    }

    public void SetControl(InputManager.Controls newControl)
    {
        control = newControl;
        controlText.text = control.ToString();
        buttonText.text = InputManager.Instance.GetInput(control).keyCode.ToString();
        button.interactable = InputManager.Instance.GetInput(control).rebindable;
    }
}
