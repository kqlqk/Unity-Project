using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public static string HORIZONTAL = "Horizontal";
    public static string VERTICAL = "Vertical";
    public static float speed = 5;

    void Start()
    {

    }


    void Update()
    {
        float horizontalInput = Input.GetAxis(HORIZONTAL);
        float verticalInput = Input.GetAxis(VERTICAL);

        if (horizontalInput == 0F && verticalInput == 0F)
        {
            return;
        }

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized * speed * Time.deltaTime;

        if (Physics.Raycast(transform.position, movement, out RaycastHit hitInfo, movement.magnitude))
        {
            Debug.Log("Collision detected!");
            return;
        }

        transform.position += movement;
    }
}
