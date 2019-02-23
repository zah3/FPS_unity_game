using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    
    public float start_health = 100f;
    public float current_health = 100f;
    public Image health_bar;


    
	public void TakeDamage () {
        health_bar.fillAmount = current_health / start_health;
	}
}
