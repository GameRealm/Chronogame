using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string path => Application.persistentDataPath + "/save.json";

    public static void Save(PlayerDataSO playerData)
    {
        // Серіалізація тільки даних
        string json = JsonUtility.ToJson(playerData.data, true);
        Debug.Log("Збережені дані: " + json); // Лог для перевірки
        File.WriteAllText(path, json);
        Debug.Log("Збережено у " + path);
    }


    public static void Load(PlayerDataSO playerData)
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("Завантажені дані: " + json);  // Лог для перевірки
            PlayerSaveData loadedData = JsonUtility.FromJson<PlayerSaveData>(json);
            if (loadedData != null)
            {
                playerData.data = loadedData;  // Завантажуємо дані у PlayerDataSO
                ChoiceTrigger.lastChoice = loadedData.lastChoice;
            }
            else
            {
                Debug.LogWarning("❌ Не вдалося завантажити SaveData із JSON.");
            }

            Debug.Log("Завантажено з " + path);
        }
        else
        {
            Debug.LogWarning("Файл сейву не знайдено.");
        }
    }

}
