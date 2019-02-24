using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    
    [SerializeField] private float enemyhealth = 100f;
    public GameObject enemy;
    // private Vector3=new Vector3(transform.position.x, transform.position.y, transform.position.z);
    // float x_position =.position.x;

    public void AplyyDamage(float damage)
    {
        //Debug.Log("DAMAGE");
        enemyhealth -= damage;
        if (enemyhealth <= 0f)
        {
            enemy.GetComponent<basic_enemy>().dead();
            Object.Destroy(this);
            //Destroy(this);
                }
        // GameObject box_particles = Instantiate(box_particles);


    }
}
