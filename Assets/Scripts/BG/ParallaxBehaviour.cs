using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField] private Transform followingTarget;
    [SerializeField, Range(0f, 1f)] private float parallaxStrength = 0.1f;
    [SerializeField] private bool disableVerticalParallax;
    private Vector3 targetPreviousPosition;

    private void Start()
    {
        if (!followingTarget)
            followingTarget = Camera.main.transform;

        targetPreviousPosition = new Vector3(followingTarget.position.x, followingTarget.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        Vector3 delta = followingTarget.position - targetPreviousPosition;

        if (disableVerticalParallax)
            delta.y = 0;

        targetPreviousPosition = followingTarget.position;

        transform.position += delta * parallaxStrength;
    }
}
