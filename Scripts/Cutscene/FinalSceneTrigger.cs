using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement; // ������ ��� ������ � ������������� ����

public class FinalSceneTrigger : MonoBehaviour
{
    public PlayableDirector finalSceneDirector; // PlayableDirector ��� �������� ��������
    public PlayerDataSO playerData; // ��� ������ ��� �������� ������ �����

    public double rationalStartTime = 0.0; // ��� ������ ��� ������������� �����
    public double intuitiveStartTime = 10.0; // ��� ������ ��� ����������� �����
    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ��������, �� ������� ������ � ������ � �� �������� �� �� ��������
        if (!hasPlayed && other.CompareTag("Player"))
        {
            hasPlayed = true; // ������������, �� �������� ��������

            if (finalSceneDirector != null && playerData != null)
            {
                // �������� ���� ����� ������
                var pathChoice = playerData.data.lastChoice;

                // ������ ���������� ��� ������� �� ������ �����
                switch (pathChoice)
                {
                    case PlayerChoiceType.Rational:
                        finalSceneDirector.time = rationalStartTime;
                        finalSceneDirector.Play();
                        Invoke(nameof(StopCutscene), 12.0f); // ���������� �������� ����� 12 ������ ��� ������������� �����
                        break;

                    case PlayerChoiceType.Intuitive:
                        finalSceneDirector.time = intuitiveStartTime;
                        finalSceneDirector.Play();
                        // ������ ����, ��� ��������� ������� �� ��������� ���� ���� ���������� ��������
                        finalSceneDirector.stopped += OnFinalSceneFinished;
                        break;

                    default:
                        Debug.LogWarning("�������� ���� ������, ��������� � �������.");
                        finalSceneDirector.time = 0.0; // ���� ���� �� ����������, ��������� � �������
                        finalSceneDirector.Play();
                        break;
                }

                Debug.Log("Գ������ �������� �����������.");
            }
            else
            {
                Debug.LogError("FinalSceneDirector ��� PlayerDataSO �� ���������!");
            }
        }
    }

    // ����� ��� ������� ��������
    void StopCutscene()
    {
        if (finalSceneDirector.state == PlayState.Playing)
        {
            finalSceneDirector.Stop();
            Debug.Log("�������� ��������� �� 12 ������ (������������ ����).");
        }

        LoadMainMenu();
    }

    // �������� ���������� �������� (��� ����������� �����)
    void OnFinalSceneFinished(PlayableDirector director)
    {

            LoadMainMenu();
            Debug.Log("Գ������ �������� ���������, ������� �� ��������� ����.");
        
    }

    // ������������ ��������� ����
    void LoadMainMenu()
    {
        SceneManager.LoadScene("Main menu"); // ������ "Main menu" �� ����� ����� ���� ����� ��������� ����
        Debug.Log("������� �� ��������� ����.");
    }
}
