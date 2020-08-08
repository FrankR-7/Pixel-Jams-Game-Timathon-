using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 10 ,0);
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
