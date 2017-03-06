using UnityEngine;
using System.Collections;

public class WeaponHolder : MonoBehaviour
{
    public GameObject[] weapons;

    GameObject currentWeapon;
    int selectedWeapon;

	// Use this for initialization
	void Start ()
    {
        SelectWeapon(0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        float mWheel = InputManager.Instance.GetMouseWheel();

        if (mWheel < 0)
        {
            SelectWeapon((selectedWeapon+1) % weapons.Length);
        }
        else if (mWheel > 0)
        {
            SelectWeapon((((selectedWeapon-1) % weapons.Length) + weapons.Length ) % weapons.Length);
        }
	}

    void SelectWeapon(int index)
    {
        Destroy(currentWeapon);
        
        currentWeapon = (GameObject)Instantiate(weapons[index], transform);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        
        selectedWeapon = index;
    }
}
