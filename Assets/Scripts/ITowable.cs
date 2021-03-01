using System;
using System.Collections;
using UnityEngine;

public interface ITowable
{
    bool Tractored { get; }
    IEnumerator StartTractor(GameObject tractorZone, EventHandler towableObjectReceivedEvent);
    void StartTow();
    void StopTow();
}
