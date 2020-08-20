using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private CharacterController _controller;
    private Vector3 movement;
    private float MoveSpeed = 15f;
    private float RotateSpeed = 0.15f;
    
    void Awake()
    {
        _controller = GetComponent<CharacterController>();

        Enemy1.player = transform;
        Door.entities.Add(transform);
        CameraMovement.target = transform;
    }

    
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal * MoveSpeed, 0f, vertical * MoveSpeed);
        Vector3 relative = movement + transform.position;
        _controller.Move(movement * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), RotateSpeed);
        }
    }
}
