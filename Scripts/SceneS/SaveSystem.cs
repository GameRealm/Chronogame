﻿using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string path => Application.persistentDataPath + "/save.json";

    public static void Save(PlayerDataSO playerData)
    {
        string json = JsonUtility.ToJson(playerData.data, true);
        File.WriteAllText(path, json);
        Debug.Log("Збережено у " + path);
    }

    public static void Load(PlayerDataSO playerData)
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerSaveData loadedData = JsonUtility.FromJson<PlayerSaveData>(json);
            if (loadedData != null)
            {
                playerData.data = loadedData;
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

    public static void Delete()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Сейв видалено.");
        }
    }
}
