using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    [SerializeField] private float health=100f;
    // private Vector3=new Vector3(transform.position.x, transform.position.y, transform.position.z);
   // float x_position =.position.x;

    public void AplyyDamage(float damage)
    {
        //Debug.Log("DAMAGE");
        health -= damage;
        if (health <= 0f) Destroy(gameObject);
       // GameObject box_particles = Instantiate(box_particles);


    }
}
