using UnityEngine;

public class Fuel : MonoBehaviour, ITowable
{
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StopTow();
    }

    public void StartTow()
    {
        rb2d.gravityScale = 1;
    }

    public void StopTow()
    {
        rb2d.gravityScale = 0;
    }
}
