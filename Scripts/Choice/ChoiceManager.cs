using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChoiceManager : MonoBehaviour
{
    public GameObject choicePanel;

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
            Debug.Log("Натиснуто Q");  // Лог для перевірки, чи спрацьовує натискання Q
            onLeftChoice?.Invoke();
            Hide();
        }

        if (Input.GetKeyDown(rightKey))
        {
            Debug.Log("Натиснуто E");  // Лог для перевірки, чи спрацьовує натискання E
            onRightChoice?.Invoke();
            Hide();
        }
    }


    private void Hide()
    {
        choicePanel.SetActive(false);
        isChoiceActive = false;
    }
}
