using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PlayerDataSO playerData;
    public void PlayGame()
    {
        SceneManager.LoadScene("intro level");
    }

    public void ContinueGame()
    {
       
        if (playerData.data.nextSceneIndex == -1)
        {
            SceneManager.LoadScene("intro level");
        }
        else
        {
            SceneManager.LoadScene(playerData.data.nextSceneIndex);
        }
    }



    public void ExitGame()
    {
        Application.Quit();
    }
}
