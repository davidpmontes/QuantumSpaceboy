using System.Collections;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int idx;
    private Vector2 direction;
    private float speed;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Vector2 position, float degrees, float speed)
    {
        transform.position = position;
        direction = new Vector2(Mathf.Cos(degrees * Mathf.Deg2Rad), Mathf.Sin(degrees * Mathf.Deg2Rad));        
        this.speed = speed * 2;
        StopAllCoroutines();
        idx = 0;
        StartCoroutine(StartEffectCoroutine(1));
    }

    private IEnumerator StartEffectCoroutine(float durationSeconds)
    {
        float endTime = Time.time + durationSeconds;

        while(Time.time < endTime)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            spriteRenderer.sprite = sprites[idx];
            idx = (idx + 1) % sprites.Length;
            yield return new WaitForSeconds(0.025f);
        }

        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
