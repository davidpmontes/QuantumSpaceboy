using System.Collections;
using UnityEngine;

public class ExplosionGroup : MonoBehaviour
{
    public void Init(Vector3 position, float radius, int count)
    {
        transform.position = position;
        StartCoroutine(CreateExplosions(position, radius, count));
    }

    private IEnumerator CreateExplosions(Vector3 origin, float radius, int count)
    {        
        for(int i = 0; i < count; i++)
        {
            Vector3 loc = origin + Random.insideUnitSphere * radius;
            loc.z = 0;

            var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.Explosion);
            explosion.SetActive(true);
            explosion.GetComponent<Explosion>().Init(loc);

            yield return new WaitForSeconds(0.1f);
        }

        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
