using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    public static Transform player;

    private NavMeshAgent nav;
    private int state; //0 - idle, 1 - attack

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        nav = GetComponent<NavMeshAgent>();

        Door.entities.Add(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Pow(transform.position.x - player.position.x, 2) + Math.Pow(transform.position.z - player.position.z, 2) <= 25f)
        {
            state = 1;
        }

        if (state == 1)
        {
            nav.SetDestination(player.position);
        }
        else 
        { 
            //Enemy mechanics like rotating in regular intervals or something
        }
    }
}
