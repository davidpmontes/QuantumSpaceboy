using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour, IEnemyDamageable
{
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite halfwaySprite;
    [SerializeField] private Sprite openedSprite;
    private SpriteRenderer spriteRenderer;    

    private void Awake()
    {        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(UpdateState());
    }

    private IEnumerator UpdateState()
    {
        while(true)
        {
            var randomClosedTime = Random.Range(1.5f, 2.5f);

            spriteRenderer.sprite = closedSprite;
            yield return new WaitForSeconds(randomClosedTime);

            spriteRenderer.sprite = halfwaySprite;
            yield return new WaitForSeconds(0.1f);

            spriteRenderer.sprite = openedSprite;
            var enemyBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.EnemyBullet);
            enemyBullet.SetActive(true);
            enemyBullet.GetComponent<EnemyBullet>().Init(transform.position + transform.up * 1f, transform.up * 3);
            yield return new WaitForSeconds(0.5f);

            spriteRenderer.sprite = halfwaySprite;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void DealDamage(int value, Vector2 normal, Vector3 position)
    {
        if (spriteRenderer.sprite == openedSprite)
        {
            var explosionGroup = ObjectPool.Instance.GetFromPoolInactive(Pools.ExplosionGroup);
            explosionGroup.SetActive(true);
            explosionGroup.GetComponent<ExplosionGroup>().Init(transform.position, 1, 5);
            Destroy(gameObject);
        }
        else
        {
            var damageEffect = ObjectPool.Instance.GetFromPoolInactive(Pools.DamageEffect);
            damageEffect.SetActive(true);
            damageEffect.GetComponent<DamageEffect>().Init(position, normal, 5);
        }
    }

    public void DealDamage(int value)
    {
    }
}
