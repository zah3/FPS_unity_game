using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float start_health = 100f;
    public float current_health = 100f;
    public Image health_bar;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    bool _damaged;

    private void Update()
    {
        if (_damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        _damaged = false;
    }

    public void TakeDamage()
    {
        _damaged = true;
        health_bar.fillAmount = current_health / start_health;
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