using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage; // ��������� �� Image ��� ����������
    public float fadeDuration = 1f; // ��������� ������

    // ����� ��� ������� ����������
    public void FadeIn()
    {
        StartCoroutine(FadeToColor(new Color(0, 0, 0, 0.5f))); // ������ ���� � ���������
    }

    // ����� ��� ���������� ���������
    public void FadeOut()
    {
        StartCoroutine(FadeToColor(new Color(0, 0, 0, 0f))); // ����� ���������� ���������
    }

    // �������� ��� ������� ���� ���������
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

        fadeImage.color = targetColor; // Գ����� ������� ����
    }
}

