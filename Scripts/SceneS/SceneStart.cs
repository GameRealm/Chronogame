using UnityEngine;

public class SceneStart : MonoBehaviour
{
    public PlayerDataSO playerData;
    public PlayerState playerStats;
    public Transform player;

    private void Start()
    {
        SaveSystem.Load(playerData);
        playerData.ApplyTo(playerStats, player);
    }
}
