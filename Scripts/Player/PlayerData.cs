using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerDataSO : ScriptableObject
{
    public PlayerSaveData data = new PlayerSaveData();

    public void CopyFrom(PlayerState stats, Transform player, string sceneName, string checkpointID, string nextSceneName)
    {
        data.position = player.position;
        data.lastChoice = ChoiceTrigger.lastChoice;
        data.currentScene = sceneName;
        data.lastCheckpointID = checkpointID;

        data.hasLantern = stats.HasLantern;
        data.lanternOn = stats.Lanter;

        // Оновлюємо індекс сцени
        int nextSceneIndex = SceneManager.GetSceneByName(nextSceneName).buildIndex;
        data.nextSceneIndex = nextSceneIndex;  

        Debug.Log($"nextSceneIndex оновлено: {data.nextSceneIndex}");  
    }


    public void ApplyTo(PlayerState stats, Transform player)
    {
        player.position = data.position;

        stats.HasLantern = data.hasLantern;
        stats.Lanter = false;

        PlayerController controller = player.GetComponent<PlayerController>();
    }
}
