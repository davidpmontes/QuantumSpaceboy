using UnityEngine;

public class SplashMenu2 : SimpleMenu<SplashMenu2>
{
    public void OnNextButtonPressed()
    {
        Close();
        MainMenu.Show();
    }
}
