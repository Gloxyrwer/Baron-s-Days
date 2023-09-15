using System.Collections;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private GameMaster gm;
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;
    [SerializeField] private AudioSource deathSound;
    private bool _isDie;
    public bool IsDie => _isDie;

    [SerializeField] private GameObject deathScreen;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    private void Start()
    {
        _isDie = false;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    public void Die()
    {
        DisableComponents();
        StopMovement();
        _isDie = true;
        gm.deathsValue++;
        deathSound.Play();
        StartCoroutine(TurnDeathScreenAfterAnim());
    }

    private void DisableComponents()
    {
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    private void StopMovement()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    private IEnumerator TurnDeathScreenAfterAnim()
    {
        anim.SetBool("isSpike", true);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + 1f);
        deathScreen?.SetActive(true);
    }
}
