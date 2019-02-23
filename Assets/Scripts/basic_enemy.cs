using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_enemy : MonoBehaviour
{

    public GameObject Player;
    UnityEngine.AI.NavMeshAgent agent;
    public float follow_distance = 50.0f;
    public float attack_distance = 30.0f;
    public float stop_distance = 10.0f;

    public ParticleSystem muzzle_flash;
    public AudioClip shoot_sound;
    private AudioSource audio_source;

    public Transform shoot_point;
    bool shoot = false;
    public float spreed_factor = 0.1f;
    public float range = 100f;
    public float damage = 10f;

    Animator animat;
    public bool is_dead = false;

    public Transform target;
    //public Transform engle;


    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animat = GetComponent<Animator>();
    }

    private void Update()
    {
        if (is_dead == false)
        {
            float dist = Vector3.Distance(Player.transform.position, this.transform.position);
            if (dist < follow_distance && dist > stop_distance)
            {
                agent.SetDestination(Player.transform.position);
                animat.SetBool("run", true);
            }

            if (dist < stop_distance)
            {
                animat.SetBool("run", false);
                agent.isStopped = true;
                Vector3 direction = target.position - transform.position;
                Quaternion rot = Quaternion.LookRotation(direction);
                Vector3 rotation = rot.eulerAngles;
                //engle.rotation = Quaternion.Euler(0f,rotation.y,0f);
                this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);


            }
            else
            {
                agent.isStopped = false;
            }


            if (dist < attack_distance)
            {
                shoot = true;
                EnemyShoot();
            }
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
                Debug.Log("enemy " + hit.transform.name);
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

    public void dead()
    {
        is_dead = true;
        animat.SetBool("dead", true);
        //agent.enabled = false;
       // agent.SetDestination(this.transform.position);
        agent.isStopped = true;
    }

   /* private void PlayShootSound()
    {
        audio_source.PlayOneShot(shoot_sound);
    }*/


}




