using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeGrab : MonoBehaviour
{

    private bool isGrabbing = false; // Чи персонаж захопив край
    private Transform grabPoint; // Точка захоплення

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Перевірка на гравця
        {
            Debug.Log("Гравець у зоні захоплення!");
            isGrabbing = true;
            grabPoint = transform; // Прив'язка до центру об'єкта
            collision.transform.position = grabPoint.position; // Переміщення гравця
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero; // Зупиняємо рух гравця
            rb.bodyType = RigidbodyType2D.Kinematic; // Вимикаємо гравітацію
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isGrabbing)
        {
            Debug.Log("Гравець вийшов із зони захоплення.");
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic; // Відновлюємо фізику
            isGrabbing = false;
        }
    }
}
