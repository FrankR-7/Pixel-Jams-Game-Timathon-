using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private CharacterController _controller;
    private Vector3 movement;
    [SerializeField] float MoveSpeed = 15f;
    [SerializeField] float RotateSpeed = 0.15f;
    [SerializeField] private float gravityScale;

    private Animator anim;
    
    void Awake()
    {
        _controller = GetComponent<CharacterController>();

        Enemy1.player = transform;
        Door.entities.Add(transform);
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal * MoveSpeed, movement.y, vertical * MoveSpeed);
        Vector3 relative = movement + transform.position;
        _controller.Move(movement * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            anim.SetBool("isRunning", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), RotateSpeed);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
}
