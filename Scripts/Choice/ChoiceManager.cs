using UnityEngine;
using UnityEngine.Events;

public class ChoiceManager : MonoBehaviour
{
    public GameObject choicePanel;
    public PlayerDataSO playerData; 

    [Header("Keybinds")]
    public KeyCode leftKey = KeyCode.Q;
    public KeyCode rightKey = KeyCode.E;

    [Header("Виклики")]
    public UnityEvent onLeftChoice;
    public UnityEvent onRightChoice;

    private bool isChoiceActive = false;

    public void ActivateChoice()
    {
        choicePanel.SetActive(true);
        isChoiceActive = true;
    }

    private void Update()
    {
       
        if (!isChoiceActive) return;

        if (Input.GetKeyDown(leftKey))
        {
            onLeftChoice?.Invoke();

            if (playerData != null)
            {
                playerData.data.lastChoice = PlayerChoiceType.Rational;
                SaveSystem.Save(playerData);
            }

            Hide();
        }

        if (Input.GetKeyDown(rightKey))
        {
            onRightChoice?.Invoke();

            if (playerData != null)
            {
                playerData.data.lastChoice = PlayerChoiceType.Intuitive;
                SaveSystem.Save(playerData);
            }

            Hide();
        }

    }
    private void Hide()
    {
        choicePanel.SetActive(false);
        isChoiceActive = false;
    }
}
