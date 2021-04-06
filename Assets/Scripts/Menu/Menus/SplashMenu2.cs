using UnityEngine;

public class SplashMenu2 : SimpleMenu<SplashMenu2>
{
    [SerializeField] private GameObject cursor;
    private int numPlayers;
    
    private Vector2[] cursorLocations = new Vector2[] { 
                                            new Vector2(-75, -30),
                                            new Vector2(-75, -52),
                                            new Vector2(-75, -74)
                                        };

    private int idx;

    public override void OnOpen()
    {
        idx = 0;
        numPlayers = 1;
        cursor.GetComponent<RectTransform>().localPosition = cursorLocations[idx];
    }

    public override void OnStartButtonPressed()
    {
        Close();
        GameMenu.Show();
        ApplicationManager.Instance.StartPlaying();
    }

    public override void OnUpButtonPressed()
    {
        if (idx > 0) idx -= 1;
        cursor.GetComponent<RectTransform>().localPosition = cursorLocations[idx];
    }

    public override void OnDownButtonPressed()
    {
        if (idx < cursorLocations.Length - 1) idx++;
        cursor.GetComponent<RectTransform>().localPosition = cursorLocations[idx];
    }
}