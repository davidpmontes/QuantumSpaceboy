using UnityEngine;

public class GravitySource : MonoBehaviour
{
    [SerializeField] private bool isAttractive;
    [SerializeField] private float strength;
    private GameObject target = null;
    private IGravityInfluenced gravityInfluence = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
            gravityInfluence = collision.gameObject.GetComponent<IGravityInfluenced>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = null;
            gravityInfluence = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (target == null) return;
        if (gravityInfluence == null) return;

        var direction = isAttractive ? transform.position - target.transform.position : target.transform.position - transform.position;
        var distance = Vector2.Distance(target.transform.position, transform.position);
        gravityInfluence.Influence(strength * direction / distance);
    }
}
