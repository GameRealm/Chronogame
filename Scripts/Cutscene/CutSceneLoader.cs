using UnityEngine;
using UnityEngine.Playables;

public class CutsceneLoader : MonoBehaviour
{
    public PlayableDirector director;
    public PlayerDataSO playerData;

    public double rationalStartTime = 0.0;
    public double rationalCutoffTime = 22.5; 
    public double intuitiveStartTime = 23.0;

    void Start()
    {
        if (director == null || playerData == null)
        {
            Debug.LogError("PlayableDirector або PlayerDataSO не призначені!");
            return;
        }

        var pathChoice = playerData.data.lastChoice;

        switch (pathChoice)
        {
            case PlayerChoiceType.Rational:
                director.time = rationalStartTime;
                director.Play();
                Invoke(nameof(StopCutscene), (float)(rationalCutoffTime - rationalStartTime));
                break;

            case PlayerChoiceType.Intuitive:
                director.time = intuitiveStartTime;
                director.Play();
                break;

            default:
                Debug.LogWarning($"Невідомий шлях: {pathChoice}. Запуск з початку.");
                director.time = 0.0;
                director.Play();
                break;
        }
    }

    void StopCutscene()
    {
        director.Stop();
        Debug.Log("Катсцена зупинена (раціональний шлях)");
    }
}
