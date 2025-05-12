using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    [Header("Cutscene Settings")]
    public PlayableDirector timeline;
    public int cutsceneNumber;
    public bool requiresInteraction = false;

    private bool hasPlayed = false;
    private bool isPlayerInZone = false;

    private PlayerController player;
    private Animator playerAnimator;

    private Vector3 lastCutscenePosition;

    [Header("Managers")]
    public CutsceneManager cutsceneManager;
    public DialogueSystem dialogueSystem;
    public ChoiceManager choiceManager;
    [Header("Scene Objects")]
    public GameObject firstPowerObject;

    private void Update()
    {
        if (requiresInteraction && isPlayerInZone && !hasPlayed && Input.GetKeyDown(KeyCode.E))
        {
            TryPlayCutscene();
        }

          }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerInZone = true;
        player = other.GetComponent<PlayerController>();
        playerAnimator = other.GetComponent<Animator>();

        if (!requiresInteraction && !hasPlayed)
        {
            TryPlayCutscene();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }

    private void TryPlayCutscene()
    {

        if (cutsceneNumber == 2 && !cutsceneManager.isFirstCutsceneCompleted)
        {
            cutsceneManager.isFirstCutsceneCompleted = true;
            Debug.Log("Перша катсцена ще не завершена. Друга катсцена не відбудеться.");
            return;
        }

        if (player != null)
        {
            player.enabled = false;
        }

        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("Moving", 0);
        }

        if (timeline != null)
        {
            timeline.Play();
            timeline.stopped += OnCutsceneEnd;
        }

        hasPlayed = true;
    }

    private void OnCutsceneEnd(PlayableDirector director)
    {
        timeline.Stop();
        timeline.stopped -= OnCutsceneEnd;

        if (player != null)
        {
            lastCutscenePosition = player.transform.position; // ⬅️ ЗБЕРЕЖЕННЯ позиції ПІСЛЯ катсцени

            player.enabled = true;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("Moving", 0);
        }

        if (cutsceneNumber == 1)
        {
            cutsceneManager.CompleteFirstCutscene();
            cutsceneManager.isFirstCutsceneCompleted = true;
            Debug.Log("isFirstCutsceneCompleted = " + cutsceneManager.isFirstCutsceneCompleted);
        }

        if (cutsceneNumber == 3)
        {
            ShowPlayerChoice();
        }

        if (cutsceneNumber == 2 && firstPowerObject != null)
        {
            firstPowerObject.SetActive(true);
        }
    }


    private void ShowPlayerChoice()
    {
        choiceManager.ActivateChoice();

        choiceManager.onLeftChoice.RemoveAllListeners();
        choiceManager.onRightChoice.RemoveAllListeners();

        choiceManager.onLeftChoice.AddListener(() =>
        {
            dialogueSystem.ShowEndingLine(PlayerChoiceType.Rational);
            cutsceneManager.ProceedAfterChoice();
        });

        choiceManager.onRightChoice.AddListener(() =>
        {
            dialogueSystem.ShowEndingLine(PlayerChoiceType.Intuitive);
            cutsceneManager.ProceedAfterChoice();
        });

    }

    public void ResetCutsceneStatus()
    {
        hasPlayed = false;
    }
}
