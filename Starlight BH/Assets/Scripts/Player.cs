using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Weapon[] weaponArray;
    [SerializeField] Transform playerModel;
    Rigidbody myRigidBody;
    public int selectedWeapon = 0;
    const string FIRE1_INPUT = "Fire1";

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        for(int iterator = 0; iterator < weaponArray.Length; iterator++)
        {
            weaponArray[iterator].InitWeapon(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FireWeapon();
        SwitchWeapon();
        LookAtCursor();
    }

    // framerate independant update
    void FixedUpdate()
    {
        for(int iterator = 0; iterator < weaponArray.Length; iterator++)
        {
            weaponArray[iterator].FixedUpdate(); // will cause a bug with burst weapons
        }
    }

    void LookAtCursor()
    {
        float mouseXFromCenter = Input.mousePosition.x - Screen.width / 2;
        float mouseYFromCenter = Input.mousePosition.y - Screen.height / 2;
        playerModel.transform.rotation = Quaternion.LookRotation(new Vector3(mouseXFromCenter, 0, mouseYFromCenter), Vector3.up);
    }

    void FireWeapon()
    {
        //Input.GetKey
        if(Input.GetButton(FIRE1_INPUT))
        {
            float mouseXFromCenter = Input.mousePosition.x - Screen.width / 2;
            float mouseYFromCenter = Input.mousePosition.y - Screen.height / 2;
            float hypothenuse = Mathf.Sqrt(mouseXFromCenter * mouseXFromCenter + mouseYFromCenter * mouseYFromCenter);
            Vector3 shotDirection = new Vector3(mouseXFromCenter / hypothenuse, 0, mouseYFromCenter / hypothenuse); // probably dont need to do these calculations anymore
            if(weaponArray.Length > selectedWeapon)
            {
                weaponArray[selectedWeapon].Shoot(shotDirection);
            }
        }
    }

    void SwitchWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && weaponArray.Length > 0)
        {
            selectedWeapon = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && weaponArray.Length > 1)
        {
            selectedWeapon = 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && weaponArray.Length > 2)
        {
            selectedWeapon = 2;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4) && weaponArray.Length > 3)
        {
            selectedWeapon = 3;
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return myRigidBody.transform.position;
    }
}
