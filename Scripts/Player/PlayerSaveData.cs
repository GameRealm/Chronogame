using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public Vector3 position;
    public int health;
    public int maxHealth;
    public PlayerChoiceType lastChoice;

    public string currentScene;
    public string lastCheckpointID;
}

