using System;
using System.Collections;
using UnityEngine;

public class Fuel : MonoBehaviour, ITowable
{
    private Rigidbody2D rb2d;
    private ITower tower;
    public bool Tractored { get; private set; }
    public bool IsBeingTowed { get; private set; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StopTow();
    }
    public IEnumerator StartTractor(GameObject tractorZone, EventHandler towableObjectReceivedEvent)
    {
        StopTow();
        Tractored = true;

        while (transform.position.y > tractorZone.transform.position.y)
        {
            if (!Mathf.Approximately(transform.position.x, tractorZone.transform.position.x))
            {
                var newX = Mathf.MoveTowards(transform.position.x, tractorZone.transform.position.x, Time.deltaTime);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }

            transform.position += Vector3.down * Time.deltaTime;
            yield return null;
        }

        towableObjectReceivedEvent?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    public void StartTow(ITower tower)
    {
        this.tower = tower;
        IsBeingTowed = true;
        rb2d.gravityScale = 1;
        rb2d.bodyType = RigidbodyType2D.Dynamic;        
    }

    public void StopTow()
    {
        IsBeingTowed = false;
        tower?.RelinquishTow();
        rb2d.gravityScale = 0;
        rb2d.bodyType = RigidbodyType2D.Static;
    }
}
