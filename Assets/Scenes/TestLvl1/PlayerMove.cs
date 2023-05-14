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
            Ray ray = new Ray(transform.position, movementDirection);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, speed * Time.deltaTime))
            {
                if (hit.collider.tag.Equals("Obtain"))
                {
                    Debug.Log("Collision obtain detected");
                    return;
                }
            }
        }
        
        transform.position += movementDirection.normalized * speed * Time.deltaTime;
    }
}
