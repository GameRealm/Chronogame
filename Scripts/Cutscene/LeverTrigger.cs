using UnityEngine;
using UnityEngine.Playables;

public class LeverTrigger : MonoBehaviour
{
    public PlayableDirector cutscene;
    PlayerController controller;

    private bool isPlayerInZone = false;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
    }
    void Update()
    {
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.E))
        {
            if (cutscene != null)
            {
                cutscene.Play();
                if (controller != null)
                    controller.enabled = false; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerInZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerInZone = false;
    }
}
