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
    [SerializeField] private float RotateSpeed = 0.15f;
    
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

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), RotateSpeed);
        }
    }
}
