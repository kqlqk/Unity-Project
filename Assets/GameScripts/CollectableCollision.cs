using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableCollision : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Collectable was triggered by player");

            SceneManager.LoadScene("TestLvl" + GlobalScript.currentLvl);
        }
        
    } 
}
