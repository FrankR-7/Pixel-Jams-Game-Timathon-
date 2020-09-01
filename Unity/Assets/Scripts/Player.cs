using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //public GameObject mesh;
    private float horizontal;
    private float vertical;
    private CharacterController _controller;
    private Vector3 movement;
    [SerializeField] float MoveSpeed = 15f;
    [SerializeField] float RotateSpeed = 0.15f;
    [SerializeField] private float gravityScale;

    private Animator anim;
    [SerializeField] private float cdr = 1;
    private float FireStartTime = 0;

    //Player data which is going to be saved
    public int level=1;
    public int attack=50;
    public int keys = 0;
    public Dictionary<Item.ItemType, int> inv = new Dictionary<Item.ItemType, int>();
    public bool isInvisible = false;
    public float nextNotInvisible = 0f;
    public bool isDewed = false;
    public int nextNotDewed = 0;
    
    private HealthSystem Health;
    private bool invulnerable;
    private float invulTime = 0.25f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Door.entities.Clear();
        Door.entities.Add(transform);
        Enemy1.player = transform;
        Health = new HealthSystem(100);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.transform.tag)
        {
            case "Enemy":
                if (!invulnerable)
                {
                    Health.TakeDamage(5);
                    print(Health.Health);
                    StartCoroutine(JustHurt());
                }
                break;
            case "Finish":
                SceneManager.LoadScene(0);
                break;
        }
    }

    private IEnumerator JustHurt()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulTime);
        invulnerable = false;
    }

    void Update()
    {
        PlayerMovement();
        
        Click();

        if (transform.position.y < -10)
        {
            SceneManager.LoadScene(0);
        }
        
    }

    private void Click()
    {
        if (Input.GetMouseButton(0) && Time.time > FireStartTime + cdr)
        {
            anim.SetTrigger("Slash");
            FireStartTime = Time.time;
            print("Slashed!");
        }
    }

    

    private void PlayerMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal * MoveSpeed, 0f, vertical * MoveSpeed);
        _controller.SimpleMove(movement);
        

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
