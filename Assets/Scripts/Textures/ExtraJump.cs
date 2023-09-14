using System.Collections;
using UnityEngine;

public class ExtraJump : MonoBehaviour
{
    private PlayerMovement player;
    private SpriteRenderer sr;
    private Collider2D col;

    [SerializeField] private float respawnTime = 2f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.jumpCount--;
            StartCoroutine(RespawnCoroutine());
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        sr.enabled = false;
        col.enabled = false;
        yield return new WaitForSeconds(respawnTime);
        sr.enabled = true;
        col.enabled = true;
    }
}
