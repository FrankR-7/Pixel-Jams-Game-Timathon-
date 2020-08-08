using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private Vector3 movement;
    [SerializeField] private int MoveSpeed;
    [SerializeField] private int RotateSpeed;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    
    void Update() //todo - make the player turn
    {
        movement = new Vector3(Input.GetAxis("Horizontal")*MoveSpeed, 0f, Input.GetAxis("Vertical")*MoveSpeed);
        Vector3 relative = movement + transform.position;
        _controller.Move(movement*Time.deltaTime);
    }
}
