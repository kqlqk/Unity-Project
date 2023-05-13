using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public static string HORIZONTAL = "Horizontal";
    public static string VERTICAL = "Vertical";
    public static float speed = 5;

    private bool isObtain;

    void Start()
    {

    }


    void Update()
    {
        GetControl();
    }

    private void GetControl()
    {
        Vector3 movementDirection = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            movementDirection += transform.forward.normalized;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection += -transform.right.normalized;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            movementDirection += -transform.forward.normalized;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection += transform.right.normalized;
        }

        if (movementDirection != Vector3.zero)
        {
            Vector3 rayOrigin = transform.position + movementDirection.normalized * GetComponent<Collider>().radius;
            Ray ray = new Ray(rayOrigin, movementDirection);
            RaycastHit hit;
    
            if (Physics.Raycast(ray, out hit, speed * Time.deltaTime))
            {
                Debug.Log("Collision detected");
                return;
            }
        }

        transform.position += movementDirection.normalized * speed * Time.deltaTime;
    }
}
