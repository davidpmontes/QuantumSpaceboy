using UnityEngine;

public class GasCan : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        CanvasManager.Instance.ChangeFuelBar(100f);
        Destroy(gameObject);
    }
}
