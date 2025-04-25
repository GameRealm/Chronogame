using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoreObject : MonoBehaviour
{
    [TextArea]
    public string loreText = "��� ����� ���";

    [Header("������������ �����������")]
    public float fontSize = 4f; // �������� ����� ������ (0.04 * 100)
    public Vector3 textOffset = new Vector3(0, 0.2f, 0); // ������� ��� ��'�����
    public Vector2 textBoxSize = new Vector2(200, 100); // ������ � ������ � �������

    private GameObject textObject;
    private bool isPlayerInZone = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && textObject == null && isPlayerInZone)
        {
            // ��������� �������� (Canvas)
            textObject = new GameObject("LoreTextCanvas");
            textObject.transform.SetParent(transform, false);
            textObject.transform.localPosition = textOffset;

            Canvas canvas = textObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;

            // ��������� Order in Layer �� ������������ ��'����
            Renderer parentRenderer = GetComponent<Renderer>();
            if (parentRenderer != null)
            {
                canvas.sortingOrder = parentRenderer.sortingOrder; // ������������ �� � ��������
                Debug.Log($"Order in Layer �����������: {parentRenderer.sortingOrder}");
            }
            else
            {
                Debug.LogWarning("� ������������ ��'���� ���� Renderer. ��������������� sortingOrder = 0.");
                canvas.sortingOrder = 0; // �������� �� �������������
            }

            CanvasScaler scaler = textObject.AddComponent<CanvasScaler>();
            scaler.dynamicPixelsPerUnit = 10;

            textObject.AddComponent<GraphicRaycaster>();

            // ��������� ���� (Image)
            GameObject bg = new GameObject("Background");
            bg.transform.SetParent(textObject.transform, false);

            Image bgImage = bg.AddComponent<Image>();
            bgImage.color = new Color(0, 0, 0, 0.6f); // ����� ������� �����

            RectTransform bgRect = bg.GetComponent<RectTransform>();
            bgRect.sizeDelta = textBoxSize;

            // ��������� ������
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

            Debug.Log("LoreObject: ����� ���������!");
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
                Debug.Log("LoreObject: ����� ���������!");
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
