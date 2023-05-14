using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{ 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Trigger player");
            //UnityEditor.EditorApplication.isPlaying = false;
            //Application.Quit();
        }
    }

}
