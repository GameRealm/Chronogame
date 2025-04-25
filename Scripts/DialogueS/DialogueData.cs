using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string characterName;    // ��'� ���������
    public Sprite characterSprite;  // ������� ���������
    [TextArea(3, 100)]
    public string dialogueText;     // ����� ������
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Create New Dialogue")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] dialogueLines;
}
