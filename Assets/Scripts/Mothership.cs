using System;
using UnityEngine;

public class Mothership : MonoBehaviour
{
    private GameObject fuelLevel;

    private void Awake()
    {
        Utils.FindChildByNameRecursively(transform, "MothershipTractorZone").
            GetComponent<MothershipTractorZone>().towableObjectReceivedEvent += OnTowableObjectReceived;
        fuelLevel = Utils.FindChildByNameRecursively(transform, "FuelLevel");
    }

    private void OnTowableObjectReceived(object sender, EventArgs e)
    {
        var newFuelLevel = Math.Min(1f, fuelLevel.transform.localScale.y + 0.25f);
        fuelLevel.transform.localScale = new Vector3(1, newFuelLevel, 1);
    }
}
