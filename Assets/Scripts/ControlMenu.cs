using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlMenu : MonoBehaviour
{
    public GameObject textPrefab;
    public GameObject buttonPrefab;
    public Vector3 startPosition;
    public Vector3 step;

    Vector3 currentPosition;

	// Use this for initialization
	void Start ()
    {
        currentPosition = startPosition;

        InputManager.Context[] contexts = (InputManager.Context[])System.Enum.GetValues(typeof(InputManager.Context));

        for (int j = 0; j < contexts.Length; j++)
        {
            GameObject sectionText = (GameObject)Instantiate(textPrefab, currentPosition, Quaternion.identity);
            sectionText.transform.SetParent(gameObject.transform, false);
            sectionText.GetComponent<Text>().text = contexts[j].ToString();

            currentPosition += step;

            InputManager.Controls[] controls = InputManager.Instance.GetControls(contexts[j]);

            for (int i = 0; i < controls.Length; i++)
            {
                GameObject controlButton = (GameObject)Instantiate(buttonPrefab, currentPosition, Quaternion.identity);
                controlButton.transform.SetParent(gameObject.transform, false);
                controlButton.GetComponent<ControlButton>().SetControl(controls[i]);

                currentPosition += step;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
