using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Transform cameraTransform;
    public float speed = 3.01f;
    public float mouseSensitivity = 3.0f;
    private float xRotation = 0f;
    private Animator animator;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = GameObject.FindWithTag("MainCamera").transform;
        animator = GetComponent<Animator>();
    }
    
    //animations

    void Update()
    {
        GetControl();
        RotateCamera();
    }

    private void GetControl()
    {
        Vector3 movementDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movementDirection += transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementDirection += -transform.right;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementDirection += -transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementDirection += transform.right;
        }

        if (movementDirection != Vector3.zero)
        {
            Vector3 targetPosition = transform.position + movementDirection.normalized * speed * Time.deltaTime;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, movementDirection, out hit, speed * Time.deltaTime))
            {
                return;
            }
        }

        transform.position += movementDirection.normalized * speed * Time.deltaTime;
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        transform.Rotate(Vector3.up * mouseX); 
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

}
