using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private GameObject playerInputManagerControllerPrefab = default;
    [SerializeField] private GameObject canvasManagerPrefab = default;

    private void Awake()
    {
        Instantiate(canvasManagerPrefab);
        Instantiate(playerInputManagerControllerPrefab);
    }
}
