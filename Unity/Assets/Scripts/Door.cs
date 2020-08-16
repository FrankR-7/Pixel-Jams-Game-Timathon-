using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static List<Transform> entities = new List<Transform>();
    private Vector3 close;
    private Vector3 open;
    
    private float speed = 5f;
    private int state; //0 - close, 1 - open

    // Start is called before the first frame update
    void Start()
    {
        close = transform.position;
        open = new Vector3(transform.position.x, transform.position.y - 2 * Generator.size, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        state = 0;
        foreach(Transform t in entities)
        {
            if (Math.Pow(transform.position.x - t.position.x, 2) + Math.Pow(transform.position.z - t.position.z, 2) <= 25f)
            {
                state = 1;
                break;
            }
        }

        if (state == 1)
            transform.position = Vector3.MoveTowards(transform.position, open, speed * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, close, speed * Time.deltaTime);
    }
}
