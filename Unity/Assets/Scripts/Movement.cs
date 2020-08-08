using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController cont;
    public GameObject lantern;
    [SerializeField] float speed = 5;
    [SerializeField] Vector3 movement;
    [SerializeField] Vector3 offset = new Vector3(0,5,0);

    void Start()
    {
        cont = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        lantern.transform.position = gameObject.transform.position + offset;
        movement = new Vector3(Input.GetAxis("Horizontal")*speed, 0f, Input.GetAxis("Vertical")*speed);
        cont.Move(movement*Time.deltaTime);
    }
}
