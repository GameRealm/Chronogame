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

    [Header("Вибір гравця (зберігається)")]
    public static PlayerChoiceType lastChoice = PlayerChoiceType.None;

    private bool hasTriggered = false;

    public void TriggerChoice()
    {
        if (hasTriggered) return; // Не дозволяємо повторно
        hasTriggered = true;

        // Додаємо слухачів для вибору
        choiceManager.onLeftChoice.AddListener(ChooseRational);
        choiceManager.onRightChoice.AddListener(ChooseIntuitive);

        // Активуємо панель вибору
        choiceManager.ActivateChoice();
    }

    private void ChooseRational()
    {
        lastChoice = PlayerChoiceType.Rational;
        Debug.Log("Гравець обрав: Раціональний вибір");

        // Запускаємо корутину для завершення рівня через 5 секунд
        StartCoroutine(CompleteLevelAfterDelay(5f));
    }

    private void ChooseIntuitive()
    {
        lastChoice = PlayerChoiceType.Intuitive;
        Debug.Log("Гравець обрав: Інтуїтивний вибір");

        // Запускаємо корутину для завершення рівня через 5 секунд
        StartCoroutine(CompleteLevelAfterDelay(5f));
    }

    private IEnumerator CompleteLevelAfterDelay(float delay)
    {
        // Чекаємо задану кількість секунд
        yield return new WaitForSeconds(delay);

        // Завершуємо рівень (можна тут викликати метод для переходу на наступний рівень або іншу дію)
        Debug.Log("Рівень завершено");
        // Наприклад, можна викликати:
        // SceneManager.LoadScene("NextLevel");
    }
}
