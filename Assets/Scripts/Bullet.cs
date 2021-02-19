using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float lifespanSeconds = 5;
    private Vector2 velocity;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 position, Vector2 velocity)
    {
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
            collectableComponent.Collect();
        }

        if (collision.gameObject.TryGetComponent(out IDamageable damageableComponent))
        {
            damageableComponent.DealDamage(1);
        }

        StopAllCoroutines();
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectableComponent))
        {
            collectableComponent.Collect();
        }
        
        if (collision.gameObject.TryGetComponent(out IDamageable damageableComponent))
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
