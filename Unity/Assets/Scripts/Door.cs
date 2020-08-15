using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 dest;
    public static Transform player;

    private int state;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        dest = new Vector3(transform.position.x, transform.position.y - 2*Generator.size, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 5f)
            state = 1;

        if (state == 1)
            transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
    }
}
