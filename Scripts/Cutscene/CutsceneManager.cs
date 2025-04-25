using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    public bool isFirstCutsceneCompleted = false;

    public GameObject firstCutsceneTrigger;
    public GameObject secondCutsceneTrigger;

    [Header("Перехід між сценами")]
    public SceneTransitionManager sceneTransitionManager;
    public string nextSceneName;
    public string checkpointID = "cutscene_exit"; // можеш змінити на будь-яке

    public void CompleteFirstCutscene()
    {
        if (isFirstCutsceneCompleted) return;

        Debug.Log("Перша катсцена завершена!");

        CutsceneManager[] allManagers = FindObjectsOfType<CutsceneManager>();

        foreach (var manager in allManagers)
        {
            manager.isFirstCutsceneCompleted = true;
            Debug.Log($"Оновлено CutsceneManager на об'єкті: {manager.gameObject.name}");

            if (manager.secondCutsceneTrigger != null)
            {
                manager.secondCutsceneTrigger.SetActive(true);
            }
        }
    }

    // 👇 Цей метод викликатиметься після вибору
    public void ProceedAfterChoice()
    {
        if (sceneTransitionManager != null)
        {
            Debug.Log("Запускаємо перехід у сцену після вибору");

            sceneTransitionManager.checkpointID = checkpointID;
            sceneTransitionManager.SaveAndLoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("SceneTransitionManager не призначений!");
        }
    }
}
