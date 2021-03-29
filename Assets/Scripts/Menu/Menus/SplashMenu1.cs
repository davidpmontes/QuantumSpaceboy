using UnityEngine;

public class SplashMenu1 : SimpleMenu<SplashMenu1>
{
    public void OnNextButtonPressed()
    {
        Close();
        SplashMenu2.Show();
    }
}
