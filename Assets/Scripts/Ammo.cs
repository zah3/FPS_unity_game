using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public int amount;
    public GameObject gun;
   // public GameObject current;
    


    void OnTriggerEnter (Collider other)
    {
        if (other.name == "FPSController")
        {
            Debug.Log("AMMO");
            /*   current= GameObject.Find("FPSController");
             WeaponSwitch weaponSwitch = GetComponent<WeaponSwitch>();
             Debug.Log(weaponSwitch.index);

            // if (weaponSwitch.index == 0)
            if(count==0)
             {*/
            gun = GameObject.Find("FPS-AK47");     
                Weapon weapon = gun.GetComponent<Weapon>();
                weapon.bullets_left += amount;
                weapon.UpdateAmmoText();
        /*    }
            else
            {
               // if (weaponSwitch.index == 1)
               if(count==1)
                {
                    gun = GameObject.Find("FPS-M16");     
                    Weapon weapon = gun.GetComponent<Weapon>();
                    weapon.bullets_left += amount;
                    weapon.UpdateAmmoText();
                }
            }*/
           
            Destroy(gameObject);



        }
	}
	

}
