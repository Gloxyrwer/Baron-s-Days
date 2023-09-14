using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        Vector3 temp = transform.position;
        temp.x = player.position.x;
        temp.y = transform.position.y;
        temp.z = transform.position.z;

        transform.position = temp;
    }
}