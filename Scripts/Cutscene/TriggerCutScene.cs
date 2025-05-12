using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutscene : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;
    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            hasPlayed = true;

            if (cutsceneDirector != null)
            {
                cutsceneDirector.Play();
                Debug.Log("🎬 Катсцена запущена через тригер.");
            }
            else
            {
                Debug.LogWarning("⚠️ PlayableDirector не призначений!");
            }
        }
    }
}
