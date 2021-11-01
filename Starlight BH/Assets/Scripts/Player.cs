using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Weapon[] weaponArray;
    Rigidbody myRigidBody;
    int selectedWeapon = 0;
    const string FIRE1_INPUT = "Fire1";

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        FireWeapon();
    }

    // framerate independant update
    void FixedUpdate()
    {
        weaponArray[selectedWeapon].FixedUpdate();
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
            weaponArray[selectedWeapon].Shoot(shotDirection);
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return myRigidBody.transform.position;
    }
}
