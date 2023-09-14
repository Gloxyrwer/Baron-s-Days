using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement player;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float shootCooldown = 0.15f;
    [HideInInspector] public bool canShoot = true;
    [HideInInspector] public bool inputShoot;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        canShoot = true;
    }

    private void Update()
    {
        CheckInput();
        Shoot();
    }

    private void CheckInput()
    {
        if (player.canMove)
            inputShoot = Input.GetMouseButton(0);
    }

    private void Shoot()
    {
        if (inputShoot)
        {
            if (canShoot)
            {
                StartCoroutine(ShootCoroutine());
            }
        }
    }

    private IEnumerator ShootCoroutine()
    {
        anim.SetBool("isShooting", true);
        anim.SetTrigger("Shooting");

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        canShoot = false;

        yield return new WaitForSeconds(shootCooldown);

        canShoot = true;

        if (!inputShoot)
        {
            anim.SetBool("isShooting", false);
        }
    }
}
