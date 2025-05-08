using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public Vector3 position;
    public PlayerChoiceType lastChoice;

    public string currentScene;
    public string lastCheckpointID;

    public bool hasLantern;
    public bool lanternOn;
}
