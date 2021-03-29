using UnityEngine;

public class Planet : MonoBehaviour, IEnemyDamageable
{
    [SerializeField] private int life;

    public void DealDamage(int value)
    {
        life -= value;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void DealDamage(int value, Vector2 normal, Vector3 position)
    {
        throw new System.NotImplementedException();
    }
}
