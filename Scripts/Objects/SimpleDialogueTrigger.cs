using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogueTrigger : MonoBehaviour
{
    public DialogueData data;

    public GameObject panelToShow;
    public TextMeshProUGUI textField;
    public Image portraitImage;

    private bool isPlayerInZone = false;
    private bool hasShown = false;

    void Update()
    {
        if (isPlayerInZone && !hasShown && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("🔵 Натиснуто E у зоні");
            if (data != null && data.dialogueLines.Length > 0 && panelToShow != null && textField != null)
            {
                DialogueLine line = data.dialogueLines[0];

                textField.text = line.dialogueText;

                if (portraitImage != null && line.characterSprite != null)
                    portraitImage.sprite = line.characterSprite;

                Debug.Log("🟣 Відображення діалогу: " + line.dialogueText);

                panelToShow.SetActive(true);
                hasShown = true;

                // Знищення дочірнього об'єкта з системою частинок
                ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>(); // Шукаємо першу систему частинок серед дочірніх об'єктів
                if (particleSystem != null)
                {
                    Destroy(particleSystem.gameObject); // Знищуємо сам об'єкт, що містить систему частинок
                    Debug.Log("🔥 Система частинок знищена");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("🟢 Хтось увійшов у тригер: " + collision.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("✅ Гравець увійшов у зону");
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInZone = false;

            if (panelToShow != null && panelToShow.activeSelf)
            {
                panelToShow.SetActive(false);
                Debug.Log("🚪 Гравець покинув зону — панель приховано");
            }

            // Опціонально: дозволити показ знову, якщо гравець повернеться
            hasShown = false;
        }
    }
}
