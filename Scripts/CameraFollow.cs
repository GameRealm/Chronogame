using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target; // Посилання на об'єкт героя

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 temp = transform.position;
            temp.x = target.position.x;
            temp.y = target.position.y;
            transform.position = temp; // Оновлюємо позицію камери
        }
    }
}
