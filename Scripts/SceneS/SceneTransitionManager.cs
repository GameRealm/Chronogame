using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public PlayerDataSO playerData;
    public PlayerState playerStats;
    public Transform player;
    public string checkpointID;

    public void SaveAndLoadScene(string nextScene)
    {
        playerData.CopyFrom(playerStats, player, nextScene, checkpointID);
        SaveSystem.Save(playerData);

        SceneFader.Instance.FadeAndLoadScene(nextScene);

    }
}
