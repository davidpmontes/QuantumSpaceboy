using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Vector3 position)
    {
        transform.position = position;
        StopAllCoroutines();
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        for(int i = 0; i < sprites.Length; i++)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(0.1f);
        }

        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
