using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float timeToWaitForGameOverScreen = 3f;

   public void LoadGame()
   {
        SceneManager.LoadScene("Game");
        ScoreKeeper.instance.ResetScore();
   }

   public void LoadMainMenu()
   {
        SceneManager.LoadScene("Main Menu");
   }

   public void LoadGameOverScreen()
   {
        StartCoroutine(WaitAndLoad("Game Over Screen", timeToWaitForGameOverScreen));
   }

   public void QuitGame()
   {
        Debug.Log("Quit");
        Application.Quit();
   }

   IEnumerator WaitAndLoad(string sceneName, float timeToWait)
   {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(sceneName);
   }
}
