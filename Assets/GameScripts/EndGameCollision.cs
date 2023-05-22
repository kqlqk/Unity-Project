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
            
            // TEMP, TODO display menu
            SceneManager.LoadScene("Lvl" + GlobalScript.currentLvl);
        }
    }

}
