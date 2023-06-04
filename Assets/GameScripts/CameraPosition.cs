using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    private Transform playerTransform;
    public Vector3 offset = new Vector3(0f, 1.12f, 0.08f);

    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }

    private void Update()
    {
        Vector3 globalOffset = playerTransform.TransformDirection(offset);

        transform.position = playerTransform.position + globalOffset;
    }
}