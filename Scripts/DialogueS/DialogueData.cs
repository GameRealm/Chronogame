using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string characterName;    // Ім'я персонажа
    public Sprite characterSprite;  // Портрет персонажа
    [TextArea(3, 100)]
    public string dialogueText;     // Текст репліки
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Create New Dialogue")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] dialogueLines;
}
