using UnityEngine;
using UnityEngine.UI;

public class TutorialTriggers : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tutorialText; // ������ � ������� �������

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �����������, �� � ������ � ��� "Player"
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
