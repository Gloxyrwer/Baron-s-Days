using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject impactEffect;

    [SerializeField] private float speed = 30f;
    [SerializeField] private int damage = 20;

    [SerializeField] private float bulletFlightTime = 10f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        StartCoroutine(DeleteBulletAfterTime());
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private IEnumerator DeleteBulletAfterTime()
    {
        yield return new WaitForSeconds(bulletFlightTime);
        Destroy(gameObject);
    }
}
