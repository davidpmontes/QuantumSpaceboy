using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float lifespanSeconds = 5;
    private Vector2 velocity;
    private Rigidbody2D rb2d;
    private int idx;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 position, Vector2 velocity, int idx)
    {
        this.idx = idx;
        transform.position = position;
        this.velocity = velocity;
        StartCoroutine(DestroyAfterTime(lifespanSeconds));
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectableComponent))
        {
            collectableComponent.Collect(idx);
        }

        if (collision.gameObject.TryGetComponent(out IEnemyDamageable damageableComponent))
        {
            damageableComponent.DealDamage(1, collision.GetContact(0).normal, transform.position);
        }

        StopAllCoroutines();
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectableComponent))
        {
            collectableComponent.Collect(idx);
        }
        
        if (collision.gameObject.TryGetComponent(out IEnemyDamageable damageableComponent))
        {
            damageableComponent.DealDamage(1);
        }

        StopAllCoroutines();
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
