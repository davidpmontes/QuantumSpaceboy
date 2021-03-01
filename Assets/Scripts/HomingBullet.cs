using System.Collections;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private float lifespanSeconds = 5;
    private Vector2 velocity;
    private Rigidbody2D rb2d;
    private GameObject target;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 position, Vector2 velocity, GameObject target)
    {
        transform.position = position;
        this.velocity = velocity;
        this.target = target;
        StartCoroutine(DestroyAfterTime(lifespanSeconds));
        StartCoroutine(HomeInOnTargetForTime(1f));
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
    }

    private IEnumerator HomeInOnTargetForTime(float durationSeconds)
    {
        var endTime = Time.time + durationSeconds;
        while(true)
        {
            if (Time.time > endTime) break;

            if (target == null) break;
            var dir = (target.transform.position - transform.position).normalized;
            velocity += (Vector2)dir;
            velocity.Normalize();
            velocity *= 10;
            yield return new WaitForFixedUpdate();
        }        
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
