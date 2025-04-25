using UnityEngine;
using UnityEngine.UI;

public class TutorialTriggers : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tutorialText; // Панель з текстом підказки

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Переконайся, що в гравця є тег "Player"
        {
            tutorialText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
        }
    }
}
