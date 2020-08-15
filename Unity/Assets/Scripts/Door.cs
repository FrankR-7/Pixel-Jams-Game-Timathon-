using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 dest;
    public static Transform player;
    private Vector3 _original;
    //enum state{Closed, Open}

    //private state CurrentState;
    
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _original = transform.position;
        //CurrentState = state.Closed;
        dest = new Vector3(transform.position.x, transform.position.y - 2 * Generator.size, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
