using System.Collections;
using UnityEngine;

public class MothershipTractorZone : MonoBehaviour
{
    private GameObject towableObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        towableObj = Spaceboy.Instance.TractorTakesTowableObject();        
        StartCoroutine(BringTowableObjectInside());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private IEnumerator BringTowableObjectInside()
    {
        while(towableObj.transform.position.y > transform.position.y)
        {
            if (!Mathf.Approximately(towableObj.transform.position.x, transform.position.x))
            {
                var newX = Mathf.MoveTowards(towableObj.transform.position.x, transform.position.x, Time.deltaTime);
                towableObj.transform.position = new Vector3(newX, towableObj.transform.position.y, towableObj.transform.position.z);
            }

            towableObj.transform.position += Vector3.down * Time.deltaTime;
            yield return null;
        }

        Destroy(towableObj);
        towableObj = null;
    }
}
