using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public PlayerDataSO playerData;
    public string checkpointID = "default_checkpoint";
    private bool isTransitioning = false;
    private bool playerInside = false;

    void Update()
    {
        if (playerInside && !isTransitioning && Input.GetKeyDown(KeyCode.G))
        {
            SaveAndLoadScene("bossFight"); 
            Debug.Log("Натиснуто G");
        }
    }

    public void SaveAndLoadScene(string sceneName)
    {
        StartCoroutine(HandleSceneTransition(sceneName));
    }

    private IEnumerator HandleSceneTransition(string sceneName)
    {
        isTransitioning = true;
        yield return SceneFader.Instance.FadeOut();

        if (playerData != null)
        {
            playerData.data.lastCheckpointID = checkpointID;
            SaveSystem.Save(playerData);
        }

        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
