using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoreObject : MonoBehaviour
{
    [TextArea]
    public string loreText = "Ваш текст тут";

    [Header("Налаштування відображення")]
    public float fontSize = 4f; // публічний розмір шрифту (0.04 * 100)
    public Vector3 textOffset = new Vector3(0, 0.2f, 0); // позиція над об'єктом
    public Vector2 textBoxSize = new Vector2(200, 100); // ширина і висота в пікселях

    private GameObject textObject;
    private bool isPlayerInZone = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && textObject == null && isPlayerInZone)
        {
            // Створення обгортки (Canvas)
            textObject = new GameObject("LoreTextCanvas");
            textObject.transform.SetParent(transform, false);
            textObject.transform.localPosition = textOffset;

            Canvas canvas = textObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;

            // Отримання Order in Layer від батьківського об'єкта
            Renderer parentRenderer = GetComponent<Renderer>();
            if (parentRenderer != null)
            {
                canvas.sortingOrder = parentRenderer.sortingOrder; // Встановлюємо те ж значення
                Debug.Log($"Order in Layer успадковано: {parentRenderer.sortingOrder}");
            }
            else
            {
                Debug.LogWarning("У батьківського об'єкта немає Renderer. Використовується sortingOrder = 0.");
                canvas.sortingOrder = 0; // Значення за замовчуванням
            }

            CanvasScaler scaler = textObject.AddComponent<CanvasScaler>();
            scaler.dynamicPixelsPerUnit = 10;

            textObject.AddComponent<GraphicRaycaster>();

            // Створення фону (Image)
            GameObject bg = new GameObject("Background");
            bg.transform.SetParent(textObject.transform, false);

            Image bgImage = bg.AddComponent<Image>();
            bgImage.color = new Color(0, 0, 0, 0.6f); // Темна прозора рамка

            RectTransform bgRect = bg.GetComponent<RectTransform>();
            bgRect.sizeDelta = textBoxSize;

            // Створення тексту
            GameObject textGO = new GameObject("Text");
            textGO.transform.SetParent(bg.transform, false);

            TextMeshProUGUI tmp = textGO.AddComponent<TextMeshProUGUI>();
            tmp.text = loreText;
            tmp.fontSize = fontSize;
            tmp.color = Color.white;
            tmp.alignment = TextAlignmentOptions.Midline;
            tmp.enableWordWrapping = true;

            RectTransform textRect = tmp.GetComponent<RectTransform>();
            textRect.sizeDelta = textBoxSize;
            textRect.localPosition = Vector3.zero;

            Debug.Log("LoreObject: Текст створений!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerInZone = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;

            if (textObject != null)
            {
                Destroy(textObject);
                Debug.Log("LoreObject: Текст видалений!");
            }
        }
    }
    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

}
