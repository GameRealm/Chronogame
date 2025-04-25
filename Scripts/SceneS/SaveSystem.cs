using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string path => Application.persistentDataPath + "/save.json";

    public static void Save(PlayerDataSO playerData)
    {
        string json = JsonUtility.ToJson(playerData.data, true);
        File.WriteAllText(path, json);
        Debug.Log("��������� � " + path);
    }

    public static void Load(PlayerDataSO playerData)
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playerData.data = JsonUtility.FromJson<PlayerSaveData>(json);
            Debug.Log("����������� � " + path);
        }
        else
        {
            Debug.LogWarning("���� ����� �� ��������.");
        }
    }

    public static void Delete()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("���� ��������.");
        }
    }
}
