using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private Camera cam;
    private float cameraHalfWidth;
    private float offsetX;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();
        cameraHalfWidth = cam.aspect * cam.orthographicSize;
    }

    private void LateUpdate()
    {
        float playerX = player.position.x;
        float cameraLeftBoundary = transform.position.x - cameraHalfWidth;
        float cameraRightBoundary = transform.position.x + cameraHalfWidth;

        if (playerX < cameraLeftBoundary)
        {
            offsetX = cameraLeftBoundary - cameraRightBoundary;
            transform.Translate(new Vector3(offsetX, 0f, 0f));
        }
        else if (playerX > cameraRightBoundary)
        {
            offsetX = cameraRightBoundary - cameraLeftBoundary;
            transform.Translate(new Vector3(offsetX, 0f, 0f));
        }
    }
}