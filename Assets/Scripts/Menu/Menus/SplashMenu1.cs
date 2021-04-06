using UnityEngine;

public class SplashMenu1 : SimpleMenu<SplashMenu1>
{
    public override void OnStartButtonPressed()
    {
        Close();
        SplashMenu2.Show();
    }
}
