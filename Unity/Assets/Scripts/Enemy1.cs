using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    public static Transform player;

    private NavMeshAgent nav;
    private Vector3 init_pos;
    private float speed = 5f;
    private int rot_speed = 30;
    private int state; //0 - idle, 1 - attack

    //Enemy Stats
    private int max_health;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        init_pos = transform.position;
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 5f;

        Door.entities.Add(transform);

        max_health = 30 + (Player.level / 3) * 5;
        health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && Math.Pow(transform.position.x - player.position.x, 2) + Math.Pow(transform.position.z - player.position.z, 2) <= 25f)
        {
            state = 1;
        }

        if (player != null && state == 1)
        {
            nav.SetDestination(player.position);
        }

        if (Player.isInvisible)
            nav.isStopped = true;
        else
            nav.isStopped = false;
    }
}
