using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {
    public float range = 100f;
    public int full_mag = 30;
    public int bullets_left;
    public int current_mag;
    public float fire_rate = 0.1f;
    public Transform shoot_point;
    public ParticleSystem muzzle_flash;
    public GameObject hit_paricles;
    public GameObject bullet_holes;
    public float damage = 20f;
    public float aim_speed;
    public Vector3 aim_position; //= new Vector3(100f, 100f, 100f)
    public enum ShootMode { Auto, Semi }
    public ShootMode mode;
    public Text ammo_text;
    public AudioClip shoot_sound;
    public float spreed_factor = 0.1f;
   

    private Animator anim;
    private AudioSource audio_source;
    private bool is_reloading = false;
    private bool shoot_input;
    private Vector3 orginal_position;
    public bool is_aiming=false;
    

    float fire_timer;

    void Start()
    {
        current_mag = full_mag;
        bullets_left = 200;
        anim = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>();
        orginal_position = transform.localPosition;
        UpdateAmmoText();
    }

   
    void Update()
    {
        switch (mode)
        {
            case ShootMode.Auto:
                shoot_input = Input.GetButton("Fire1");
                break;
            case ShootMode.Semi:
                shoot_input = Input.GetButtonDown("Fire1");
                break;
        }
       // if (Input.GetButton("Fire1"))
       if(shoot_input)
        {
            if (current_mag > 0)
            {
                Fire();
            }
            else
            { 
              if(bullets_left>0)  Reload();
            }
        }

        if (fire_timer < fire_rate)
        {
            fire_timer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
            {
                if(current_mag<full_mag)
                 {
                     Reload();
                 }
            }

        AimDownSights();

    }

    private void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        is_reloading = info.IsName("Reload");
        anim.SetBool("Aim", is_aiming);
    }

    private void OnEnable()
    {
        UpdateAmmoText();
    }

    private void Fire()
    {
        
        if (fire_timer < fire_rate || current_mag<=0 || is_reloading) return;
        
        RaycastHit hit;
        
        if (is_aiming==false)
        {
            Vector3 shoot_direction = shoot_point.transform.forward;
            shoot_direction.x += Random.Range(-spreed_factor, spreed_factor);
            shoot_direction.y += Random.Range(-spreed_factor, spreed_factor);

            if (Physics.Raycast(shoot_point.position, shoot_direction, out hit, range))
            {
                Debug.Log(hit.transform.name);
                GameObject hit_particle_effect = Instantiate(hit_paricles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                hit_particle_effect.transform.SetParent(hit.transform);
                GameObject bullet_hole_effect = Instantiate(bullet_holes, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                bullet_hole_effect.transform.SetParent(hit.transform);
                Destroy(hit_particle_effect, 1f);
                Destroy(bullet_hole_effect, 30f);
                if (hit.transform.GetComponent<HealthController>())
                {
                    hit.transform.GetComponent<HealthController>().AplyyDamage(damage);
                }
                if (hit.transform.GetComponent<EnemyHealthController>())
                {
                    hit.transform.GetComponent<EnemyHealthController>().AplyyDamage(damage);
                }
            }
        }
        else
        {
            if (is_aiming)
            {
                if (Physics.Raycast(shoot_point.position, shoot_point.transform.forward, out hit, range))
                {
                    Debug.Log(hit.transform.name);
                    GameObject hit_particle_effect = Instantiate(hit_paricles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    hit_particle_effect.transform.SetParent(hit.transform);
                    GameObject bullet_hole_effect = Instantiate(bullet_holes, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                    bullet_hole_effect.transform.SetParent(hit.transform);
                    Destroy(hit_particle_effect, 1f);
                    Destroy(bullet_hole_effect, 30f);
                    if (hit.transform.GetComponent<HealthController>())
                    {
                        hit.transform.GetComponent<HealthController>().AplyyDamage(damage);
                    }
                    if (hit.transform.GetComponent<EnemyHealthController>())
                    {
                        hit.transform.GetComponent<EnemyHealthController>().AplyyDamage(damage);
                    }
                }
            }
        }



        anim.CrossFadeInFixedTime("Fire", 0.1f);
        muzzle_flash.Play();
        PlayShootSound();
       
        current_mag--;
        fire_timer = 0.0f;
        UpdateAmmoText();
    }

    private void Reload()
    {
        if (bullets_left <= 0 || is_reloading) return;
        anim.CrossFadeInFixedTime("Reload", 0.01f);
        int bullets_to_load = full_mag - current_mag;
        int bullets_to_deduct= (bullets_left >= bullets_to_load) ? bullets_to_load : bullets_left;
        bullets_left -= bullets_to_deduct;
        current_mag += bullets_to_load;
        UpdateAmmoText();
    }

    private void PlayShootSound()
    {
        audio_source.PlayOneShot(shoot_sound); //PlayCilpAtPoint()?????
    }

    private void AimDownSights()
    {
        if(Input.GetButton("Fire2") && is_reloading == false)
        {
            transform.localPosition = aim_position; //=  Vector3.Lerp(transform.localPosition, aim_position, Time.deltaTime*aim_speed);
            is_aiming = true;
            Debug.Log("is_aiming "+is_aiming);
        }
        else
        {
            transform.localPosition = orginal_position; //= Vector3.Lerp(transform.localPosition, orginal_position, Time.deltaTime * aim_speed);
            is_aiming = false;
        }
    }
    public void UpdateAmmoText()
    {
        ammo_text.text = "Ammo: "+current_mag+" / "+bullets_left;
        
    }
}