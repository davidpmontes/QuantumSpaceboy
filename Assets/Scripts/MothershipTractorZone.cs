using System;
using UnityEngine;

public class MothershipTractorZone : MonoBehaviour
{
    public EventHandler towableObjectReceivedEvent;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Spaceboy.Instance.TractorTakesTowableObject();
        if (collision.gameObject.TryGetComponent(out ITowable component))
        {
            StartCoroutine(component.StartTractor(gameObject, towableObjectReceivedEvent));
        }
    }
}
