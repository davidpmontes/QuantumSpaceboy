using UnityEngine;

public class HomingBulletWeapon : MonoBehaviour, IWeapon
{
    private float intervalSeconds = 0.3f;
    private float nextFireTime = 0;
    private const float BULLET_SPEED = 300f;
    private GameObject target;

    public void Init()
    {
        nextFireTime = Time.time;
    }

    public void Fire(float shipRotationRads, Rigidbody2D rb2d, Vector3 shipPosition, GameObject target, int idx)
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + intervalSeconds;
            var homingBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.HomingBullet);
            homingBullet.SetActive(true);
            var velocity = rb2d.velocity + (new Vector2(-Mathf.Cos(shipRotationRads), Mathf.Sin(shipRotationRads))) * BULLET_SPEED * Time.fixedDeltaTime;
            var position = shipPosition + (new Vector3(-Mathf.Cos(shipRotationRads), Mathf.Sin(shipRotationRads))) * 0.6f;
            homingBullet.GetComponent<HomingBullet>().Init(position, velocity, target, idx);
        }
    }
}
