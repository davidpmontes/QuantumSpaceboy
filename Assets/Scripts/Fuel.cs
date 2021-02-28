using UnityEngine;

public class Fuel : MonoBehaviour, ITowable
{
    private Rigidbody2D rb2d;
    public bool Tractored { get; private set; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StopTow();
    }

    public void StartTractor()
    {
        StopTow();
        Tractored = true;
    }

    public void StartTow()
    {
        rb2d.gravityScale = 1;
        rb2d.bodyType = RigidbodyType2D.Dynamic;        
    }

    public void StopTow()
    {
        rb2d.gravityScale = 0;
        rb2d.bodyType = RigidbodyType2D.Static;
    }
}
