using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage; // Посилання на Image для затемнення
    public float fadeDuration = 1f; // Тривалість ефекту

    // Метод для запуску затемнення
    public void FadeIn()
    {
        StartCoroutine(FadeToColor(new Color(0, 0, 0, 0.5f))); // Чорний колір з прозорістю
    }

    // Метод для відновлення прозорості
    public void FadeOut()
    {
        StartCoroutine(FadeToColor(new Color(0, 0, 0, 0f))); // Повне відновлення прозорості
    }

    // Корутина для анімації зміни прозорості
    private IEnumerator FadeToColor(Color targetColor)
    {
        Color currentColor = fadeImage.color;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            fadeImage.color = Color.Lerp(currentColor, targetColor, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = targetColor; // Фіксуємо кінцевий колір
    }
}

