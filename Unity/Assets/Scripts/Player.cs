using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
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
    public static int keys = 0;
    public static Dictionary<Item.ItemType, int> inv = new Dictionary<Item.ItemType, int>();
    public static bool isInvisible = false;
    public static float nextNotInvisible = 0f;
    public static bool isDewed = false;
    public static int nextNotDewed = 0;

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

        if (isInvisible && Time.time > nextNotInvisible)
            isInvisible = false;
        if (isDewed && (health == max_health || health == nextNotDewed))
        {
            health += 1;
            isDewed = false;
        }
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

    public static void UseItem(Item.ItemType item)
    {
        bool used = true;
        switch (item)
        {
        case Item.ItemType.StrengthPotion:
            max_health += 20;
            break;
        case Item.ItemType.HealPotion:
            if (health != max_health)
                health = max_health;
            else
                used = false;
            break;
        case Item.ItemType.InvisibilityPotion:
            if (!isInvisible)
            {
                isInvisible = true;
                nextNotInvisible = Time.time + 15f; //Invisible for 15 seconds
            }
            else
                used = false;
            break;
        case Item.ItemType.DewFlask:
            if (!isDewed)
            {
                isDewed = true;
                nextNotDewed = health + max_health * 20 / 100; //Refills 20% of health
            }
            else
                used = false;
            break;
        case Item.ItemType.AttackScroll:
            attack += 30;
            break;
        }

        if (used)
        {
            --inv[item];
            if (inv[item] == 0)
                inv.Remove(item);
            GameObject.FindObjectOfType<UI_Manager>().RefreshInv();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Checking if gameobject we collided with is an item
        if (hit.gameObject.GetComponent<Item>() != null)
        {
            Item.ItemType type = hit.gameObject.GetComponent<Item>().type;
            if (type == Item.ItemType.Key)
                ++keys;
            else if (type == Item.ItemType.Chest)
            {
                if (keys > 0)
                {
                    --keys;
                }
                else
                    return;
            }
            else
            {
                if (inv.ContainsKey(type))
                    ++inv[type];
                else
                    inv[type] = 1;

                if (type == Item.ItemType.Scrap && inv[type] == 4)
                {
                    inv.Remove(type);
                    if (inv.ContainsKey(Item.ItemType.AttackScroll))
                        ++inv[Item.ItemType.AttackScroll];
                    else
                        inv[Item.ItemType.AttackScroll] = 1;
                }
            }

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
