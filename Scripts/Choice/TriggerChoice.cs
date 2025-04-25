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

    [Header("���� ������ (����������)")]
    public static PlayerChoiceType lastChoice = PlayerChoiceType.None;

    private bool hasTriggered = false;

    public void TriggerChoice()
    {
        if (hasTriggered) return; // �� ���������� ��������
        hasTriggered = true;

        // ������ �������� ��� ������
        choiceManager.onLeftChoice.AddListener(ChooseRational);
        choiceManager.onRightChoice.AddListener(ChooseIntuitive);

        // �������� ������ ������
        choiceManager.ActivateChoice();
    }

    private void ChooseRational()
    {
        lastChoice = PlayerChoiceType.Rational;
        Debug.Log("������� �����: ������������ ����");

        // ��������� �������� ��� ���������� ���� ����� 5 ������
        StartCoroutine(CompleteLevelAfterDelay(5f));
    }

    private void ChooseIntuitive()
    {
        lastChoice = PlayerChoiceType.Intuitive;
        Debug.Log("������� �����: ���������� ����");

        // ��������� �������� ��� ���������� ���� ����� 5 ������
        StartCoroutine(CompleteLevelAfterDelay(5f));
    }

    private IEnumerator CompleteLevelAfterDelay(float delay)
    {
        // ������ ������ ������� ������
        yield return new WaitForSeconds(delay);

        // ��������� ����� (����� ��� ��������� ����� ��� �������� �� ��������� ����� ��� ���� ��)
        Debug.Log("г���� ���������");
        // ���������, ����� ���������:
        // SceneManager.LoadScene("NextLevel");
    }
}
