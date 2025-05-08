using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    public Image characterPortrait;      
    public TextMeshProUGUI dialogueText; 
    public GameObject dialoguePanel;     

    private DialogueData currentDialogue;
    private int dialogueIndex = 0;

    public void StartDialogue(DialogueData dialogue)
    {
        if (dialogue == null)
        {
            Debug.LogError("DialogueData is NULL!");
            return;
        }

        currentDialogue = dialogue;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);

        ShowLine();
    }


    void ShowLine()
    {
        if (dialogueIndex < currentDialogue.dialogueLines.Length)
        {
            DialogueLine line = currentDialogue.dialogueLines[dialogueIndex];

            characterPortrait.sprite = line.characterSprite;
            dialogueText.text = line.dialogueText;

            StartCoroutine(WaitAndShowNextLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator WaitAndShowNextLine()
    {
        yield return new WaitForSeconds(6f);
        dialogueIndex++;
        ShowLine();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void PlayLine(string lineID)
    {
        DialogueLine line = FindLineByID(lineID);
        if (line != null)
        {
            characterPortrait.sprite = line.characterSprite;
            dialogueText.text = line.dialogueText;
        }
    }


    private DialogueLine FindLineByID(string lineID)
    {
        foreach (var line in currentDialogue.dialogueLines)
        {
            if (line.characterName.Equals(lineID)) 
            {
                return line;
            }
        }
        return null;
    }

    public void ShowEndingLine(PlayerChoiceType playerChoice)
    {
        string lineID = playerChoice == PlayerChoiceType.Rational ? "line_rational_end" : "line_intuitive_end";
        PlayLine(lineID);  
    }
}
