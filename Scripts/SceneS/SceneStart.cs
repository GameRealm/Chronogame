using UnityEngine;

public class SceneStart : MonoBehaviour
{
    public PlayerDataSO playerData;
    public PlayerState playerStats;
    public Transform player;

    private void Start()
    {
        SaveSystem.Load(playerData);
        ChoiceTrigger.lastChoice = playerData.data.lastChoice;

        playerData.ApplyTo(playerStats, player);
    }

    private void Awake()
    {
        if (playerData == null)
        {
            playerData = Resources.Load<PlayerDataSO>("PlayerData"); 
        }

        if (playerData != null)
            SaveSystem.Load(playerData);
        else
            Debug.LogError("❌ PlayerDataSO не знайдено!");

        // Призначити глобальне поле
        ChoiceTrigger.lastChoice = playerData.data.lastChoice;

        Debug.Log("Обраний шлях: " + ChoiceTrigger.lastChoice);
    }

}
