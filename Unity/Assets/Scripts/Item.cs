using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType{
        Chest,
        Key,
        StrengthPotion,
        InvisibilityPotion,
        HealPotion,
        DewFlask,
        Scrap,
        AttackScroll
    };

    private Vector3 init_position;
    private Vector3 up;
    private Vector3 down;
    private int alt;
    private float speed = 0.5f;
    private int rot_speed = 40;
    [SerializeField] public ItemType type;

    void Start()
    {
        alt = 1;
        init_position = transform.position;
        up = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        down = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
    }

    void Update()
    {
        if (type != ItemType.Chest) //Don't want chest to rotate and move up and down
        {
            if (transform.position == up || transform.position == down)
                alt *= -1;
            if (alt == 1)
                transform.position = Vector3.MoveTowards(transform.position, up, speed * Time.deltaTime);
            else
                transform.position = Vector3.MoveTowards(transform.position, down, speed * Time.deltaTime);

            transform.Rotate(0, 0, rot_speed * Time.deltaTime);
        }

    }
}
