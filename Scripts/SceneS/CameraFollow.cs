using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public float yOffset = 2f; // �������� ������ ������

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 temp = transform.position;
            temp.x = target.position.x;
            temp.y = target.position.y + yOffset; // ������ ������� �� �� Y
            transform.position = temp;
        }
    }
}
