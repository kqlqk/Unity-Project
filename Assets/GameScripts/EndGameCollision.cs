using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameCollision : MonoBehaviour
{ 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player was triggered by enemy");
            
            SceneManager.LoadScene("TestLvl" + GlobalScript.currentLvl);
        }
    }

}
