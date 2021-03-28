using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasManager : MonoBehaviour
{
    public static GameplayCanvasManager Instance { get; private set; }

    [SerializeField] private GameObject leftHUD;
    [SerializeField] private GameObject rightHUD;
    [SerializeField] private Image[] fuelbar = default;
    [SerializeField] private Image[] weaponsBar = default;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateFuelBar(int idx, float percent)
    {
        if (Mathf.Approximately(percent, 0f))
        {
            return;
        }

        var newfillAmount = fuelbar[idx].fillAmount + (percent * 0.01f);
        newfillAmount = Mathf.Max(0f, newfillAmount);
        newfillAmount = Mathf.Min(1f, newfillAmount);

        fuelbar[idx].fillAmount = newfillAmount;
    }

    public void ChangeWeaponsBar(float percent)
    {

    }

    public void SetLeftHUD(bool isVisible)
    {
        leftHUD.SetActive(isVisible);
    }

    public void SetRightHUD(bool isVisible)
    {
        rightHUD.SetActive(isVisible);
    }
}
