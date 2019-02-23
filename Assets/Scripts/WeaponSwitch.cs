using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

    [SerializeField] private GameObject[] weapons;
    [SerializeField] private float swich_deley;
    public int index;
    private bool is_swiching;

	void Start () {
        InitalizeWeapon();

    }
	
    private void InitalizeWeapon()
    {
        for(int i=0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[0].SetActive(true);
    }

    private void WeaponSwitcher(int new_index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[new_index].SetActive(true);
    }
	
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !is_swiching)
        {
            index++;
            if (index >= weapons.Length)
            {
                index = 0;
            }
            StartCoroutine(Deley(index));
            //WeaponSwitcher(index);
        }
        else
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && !is_swiching)
            {
                index--;
                if (index <0)
                {
                    index = weapons.Length -1 ;
                }
                StartCoroutine(Deley(index));
            }
        }
	}

    private IEnumerator Deley(int new_index)
    {
        is_swiching = true;
    
        yield return new WaitForSeconds(swich_deley);
        is_swiching = false;
        WeaponSwitcher(new_index);
    }
}
