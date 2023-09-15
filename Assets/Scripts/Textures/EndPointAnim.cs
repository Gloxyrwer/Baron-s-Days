using UnityEngine;

public class EndPointAnim : MonoBehaviour
{
    private Animator anim;
    private GameObject player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isEnter", true);
            player.SetActive(false);
        }
    }
}
