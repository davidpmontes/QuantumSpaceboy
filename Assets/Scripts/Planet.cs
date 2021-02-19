using UnityEngine;

public class Planet : MonoBehaviour, IDamageable
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
}
