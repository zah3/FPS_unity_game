using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_enemy : MonoBehaviour
{

    public GameObject Player;
    UnityEngine.AI.NavMeshAgent agent;
    public float follow_distance = 50.0f;
    public float attack_distance = 30.0f;

    public ParticleSystem muzzle_flash;
    public AudioClip shoot_sound;
    private AudioSource audio_source;

    public Transform shoot_point;
    bool shoot = false;
    public float spreed_factor = 0.1f;
    public float range = 100f;
    public float damage = 10f;


    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Update()
    {
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);
        if (dist < follow_distance)
            agent.SetDestination(Player.transform.position);
        if (dist < attack_distance)
        {
            shoot = true;
            EnemyShoot();

        }
    }

    void EnemyShoot()
    {
        RaycastHit hit;
        Vector3 shoot_direction = shoot_point.transform.forward;
        shoot_direction.x += Random.Range(-spreed_factor, spreed_factor);
        shoot_direction.y += Random.Range(-spreed_factor, spreed_factor);
        if (Physics.Raycast(shoot_point.position, shoot_direction, out hit, range))
        {
            Debug.Log("enemy "+ hit.transform.name);
        }
        if (hit.transform.GetComponent<HealthController>())
        {
            hit.transform.GetComponent<HealthController>().AplyyDamage(damage);
        }
        if (hit.transform.GetComponent<PlayerHealth>())
        {
            hit.transform.GetComponent<PlayerHealth>().ApplyDamage(damage);
        }
        muzzle_flash.Play();
        //PlayShootSound();
    }

   /* private void PlayShootSound()
    {
        audio_source.PlayOneShot(shoot_sound);
    }*/


}




