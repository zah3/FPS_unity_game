using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_enemy : MonoBehaviour
{

    public GameObject Player;
    UnityEngine.AI.NavMeshAgent agent;
    public float follow_distance = 50.0f;
    public float attack_distance = 30.0f;

    [Range(0.0f, 1.0f)]
    public float accuracy = 0.5f;



    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
    }

    private void Update()
    {
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);
        if(dist<follow_distance)
        agent.SetDestination(Player.transform.position);
       // if(dist<attack_distance)

}



}
