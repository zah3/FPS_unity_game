using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    
    public float start_health = 100f;
    public float current_health = 100f;
    public Image health_bar;
    public Image damageImage; 
    public float flashSpeed = 5f;                             
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     
   
    
	public void TakeDamage()
    {
        health_bar.fillAmount = current_health / start_health;
        damageImage.color = flashColour;
        damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
	}

    public void ApplyDamage(float damage)
    {
        current_health -= damage;
        if (current_health <= 0f)
        {
            Destroy(gameObject);
        }
        TakeDamage();
    }
   
}
