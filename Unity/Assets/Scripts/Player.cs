using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject mesh;
    private float horizontal;
    private float vertical;
    private CharacterController _controller;
    private Vector3 movement;
    [SerializeField] float MoveSpeed = 15f;
    [SerializeField] float RotateSpeed = 0.15f;
    [SerializeField] private float gravityScale;

    private Animator anim;

    //Player data which is going to be saved
    public static int level=1;
    public static int max_health=100;
    public static int health=100;
    public static int attack=50;
    //add inventory here

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        anim = mesh.GetComponent<Animator>();

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

        if (horizontal == 0 && vertical == 0)
            anim.SetBool("isRunning", false);
        else
            anim.SetBool("isRunning", true);

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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Checking if gameobject we collided with is an item
        if (hit.gameObject.GetComponent<Item>() != null)
        {
            /*
             * Each item is given an ID which is assigned to it in its respective Gameobject
             * ID - Item
             *  0 - Chest
             *  1 - Scrap
             *  2 - Strength Potion
             *  3 - Heal Potion
             *  4 - Invisibility Potion
             *  5 - Dew Flask
             *  6 - Chest Key
             */
            int type = hit.gameObject.GetComponent<Item>().ID;

            //Code to add object to inventory should be added here

            Destroy(hit.gameObject);
        }
        else if(hit.gameObject.name == "End(Clone)")
        {
            ++level;
            Door.entities = new List<Transform>();
            Enemy1.player = null;
            CameraMovement.target = null;
            GameObject.FindObjectOfType<UI_Manager>().Trigger_Loading();
            SceneManager.LoadScene(1);
        }
    }
}
