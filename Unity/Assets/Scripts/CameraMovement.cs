using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0, 20f, 0);

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
