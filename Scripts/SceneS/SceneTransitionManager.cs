using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public PlayerDataSO playerData;
    public string checkpointID = "default_checkpoint";
    private bool isTransitioning = false;
    private bool playerInside = false;

    [Header("Катсцена")]
    public PlayableDirector cutsceneDirector;

    void Update()
    {
        if (playerInside && !isTransitioning && Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(PlayCutsceneAndLoadScene("bossFight"));
            Debug.Log("Натиснуто G");
        }
    }

    private IEnumerator PlayCutsceneAndLoadScene(string sceneName)
    {
        isTransitioning = true;

        // 🔹 Відтворюємо катсцену, якщо є
        if (cutsceneDirector != null)
        {
            cutsceneDirector.Play();

            // Чекаємо завершення катсцени
            while (cutsceneDirector.state == PlayState.Playing)
            {
                yield return null;
            }
        }

        // 🔹 Плавне затемнення перед переходом (опційно)
        if (SceneFader.Instance != null)
            yield return SceneFader.Instance.FadeOut();

        // 🔹 Збереження прогресу
        if (playerData != null)
        {
            playerData.data.lastCheckpointID = checkpointID;
            playerData.data.nextSceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
            SaveSystem.Save(playerData);
        }

        // 🔹 Завантаження сцени
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
            playerData.data.nextSceneIndex = 3;
            SaveSystem.Save(playerData);
        }

        SceneManager.LoadScene(sceneName);
    }

}
