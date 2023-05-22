using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform playerTransform;
    public Vector3 offset;
    
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        offset = new Vector3(0, 3.61f, -1.75f);
    }

    void Update()
    {
        transform.position = playerTransform.position + offset;
    }
}
