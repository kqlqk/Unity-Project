using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagment;
public class mainmenu : MonoBehaviour
{
   public void PlayGame()
   {
    SceneManager.loadScene(SceneManager.GetActiveScene().buildIndex +1);
   }

   public void ExitGame()
   {
    Debug.log("Game Closed")
    Application.Quit();
   }
}
