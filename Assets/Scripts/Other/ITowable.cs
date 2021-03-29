using System;
using System.Collections;
using UnityEngine;

public interface ITowable
{
    bool IsBeingTowed { get; }
    bool Tractored { get; }
    IEnumerator StartTractor(GameObject tractorZone, EventHandler towableObjectReceivedEvent);
    void StartTow(ITower tower);
    void StopTow();
}
