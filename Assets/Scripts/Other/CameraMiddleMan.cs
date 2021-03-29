using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMiddleMan : MonoBehaviour
{
    public static CameraMiddleMan Instance { get; private set; }
    private List<GameObject> players = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Vector3 centerPosition = Vector3.zero;

        foreach(var player in players)
        {
            centerPosition += player.transform.position;
        }

        if (players.Count == 2) centerPosition /= 2f;

        transform.position = centerPosition;
    }

    public void AddPlayer(GameObject newPlayer)
    {
        players.Add(newPlayer);
    }
}
