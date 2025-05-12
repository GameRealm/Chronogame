using UnityEngine;
using System.Collections;

public enum PlayerChoiceType
{
    None,
    Rational,
    Intuitive
}

public class ChoiceTrigger : MonoBehaviour
{
    public ChoiceManager choiceManager;
    private PlayerState playerState;
    private Transform playerTransform;

    [Header("Вибір гравця (зберігається)")]
    public static PlayerChoiceType lastChoice = PlayerChoiceType.None;

    private bool hasTriggered = false;

    void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
        playerTransform = playerState?.transform;
    }
    public void TriggerChoice()
    {
        if (hasTriggered) return; 
        hasTriggered = true;


        choiceManager.onLeftChoice.AddListener(ChooseRational);
        choiceManager.onRightChoice.AddListener(ChooseIntuitive);

        choiceManager.ActivateChoice();
    }

    private void ChooseRational()
    {
        lastChoice = PlayerChoiceType.Rational;
        Debug.Log("Гравець обрав: Раціональний вибір");

        // ⬇️ Додаємо збереження вибору
        var playerData = Resources.Load<PlayerDataSO>("PlayerData");
        playerData.data.lastChoice = lastChoice;
        playerData.data.nextSceneIndex = 1;
        SaveSystem.Save(playerData);

        StartCoroutine(CompleteLevelAfterDelay(5f));
    }

    private void ChooseIntuitive()
    {
        lastChoice = PlayerChoiceType.Intuitive;
        Debug.Log("Гравець обрав: Інтуїтивний вибір");

        // ⬇️ Додаємо збереження вибору
        var playerData = Resources.Load<PlayerDataSO>("PlayerData");
        playerData.data.lastChoice = lastChoice;
        playerData.data.nextSceneIndex = 1;
        SaveSystem.Save(playerData);

        StartCoroutine(CompleteLevelAfterDelay(5f));
    }



    private IEnumerator CompleteLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Рівень завершено");

    }
}
