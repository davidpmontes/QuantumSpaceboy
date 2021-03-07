using UnityEngine;

public class GasCan : MonoBehaviour, ICollectable
{
    public void Collect(int idx)
    {
        CanvasManager.Instance.UpdateFuelBar(idx, 100f);
        Destroy(gameObject);
    }
}
