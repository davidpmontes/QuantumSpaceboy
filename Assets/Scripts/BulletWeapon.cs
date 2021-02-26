using UnityEngine;

public class BulletWeapon : MonoBehaviour, IWeapon
{
    private float nextFireTime = 0;
    private const float BULLET_SPEED = 300f;

    public void Init()
    {
        nextFireTime = Time.time;
    }

    public void SetTarget(GameObject target)
    {

    }

    public void Fire(float shipRotationRads, Rigidbody2D rb2d, Vector3 shipPosition, GameObject target)
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + 0.1f;
            var bullet = ObjectPool.Instance.GetFromPoolInactive(Pools.Bullet);
            bullet.SetActive(true);
            var position = shipPosition + (new Vector3(-Mathf.Cos(shipRotationRads), Mathf.Sin(shipRotationRads))) * 0.6f;
            var velocity = rb2d.velocity + (new Vector2(-Mathf.Cos(shipRotationRads), Mathf.Sin(shipRotationRads))) * BULLET_SPEED * Time.fixedDeltaTime;
            bullet.GetComponent<Bullet>().Init(position, velocity);
        }
    }
}
