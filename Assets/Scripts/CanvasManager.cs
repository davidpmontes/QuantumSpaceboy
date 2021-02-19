using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    [SerializeField] private Image fuelbar = default;
    [SerializeField] private Image weaponsBar = default;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeFuelBar(float percent)
    {
        if (Mathf.Approximately(percent, 0f))
        {
            Debug.Log("Can't change fuel by 0 percent.");
            return;
        }

        var newfillAmount = fuelbar.fillAmount + (percent * 0.01f);
        newfillAmount = Mathf.Max(0f, newfillAmount);
        newfillAmount = Mathf.Min(1f, newfillAmount);

        fuelbar.fillAmount = newfillAmount;
    }

    public void ChangeWeaponsBar(float percent)
    {

    }
}
