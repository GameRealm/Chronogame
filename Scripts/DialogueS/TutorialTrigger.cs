using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public DialogueData tutorialDialogue; 

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player") && tutorialDialogue != null)
        {
            hasTriggered = true;

            DialogueSystem dialogueSystem = FindObjectOfType<DialogueSystem>();
            if (dialogueSystem != null)
            {
                dialogueSystem.StartDialogue(tutorialDialogue);
            }
        }
    }
}
