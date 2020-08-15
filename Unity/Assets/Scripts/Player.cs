using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private CharacterController _controller;
    [SerializeField] private Vector3 movement;
    [SerializeField] private int MoveSpeed;
    [SerializeField] private int RotateSpeed;
    
    void Start()
    {
        CameraMovement.target = transform;
        Door.player = transform;

        _controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        //Movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal*MoveSpeed, 0f, vertical*MoveSpeed);
        Vector3 relative = movement + transform.position;
        _controller.Move(movement*Time.deltaTime);

        //Rotation - Well Woah idk if this was what u planned to do so feel free to do your thing
        if (horizontal > 0)
            transform.rotation = Quaternion.Euler(0, 90, 0);
        else if (horizontal < 0)
            transform.rotation = Quaternion.Euler(0, -90, 0);
        if (vertical > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (vertical < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
