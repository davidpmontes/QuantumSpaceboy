using UnityEngine;

public interface IEnemyDamageable
{
    void DealDamage(int value);
    void DealDamage(int value, Vector2 normal, Vector3 position);
}
