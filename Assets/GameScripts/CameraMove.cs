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
        offset = new Vector3(0f, 3.21f, -2f);
        
        transform.rotation = new Quaternion(75,0,0,180);
    }

    void Update()
    {
        transform.position = playerTransform.position + offset;
    }
}