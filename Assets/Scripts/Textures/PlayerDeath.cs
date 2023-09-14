using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private PlayerLife player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.Die();
        }
    }
}
