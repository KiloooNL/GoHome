using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Rect playPosition = new Rect(400, 120, 140, 45);
    Rect playTimerPosition = new Rect(400, 175, 140, 45);
    Rect quitPosition = new Rect(400, 235, 140, 45);
    Rect gameNamePosition = new Rect(300, 30, 400, 75);

    public GUISkin skin;

    // enable timer?
    public static bool timerEnabled = false;

    void OnGUI()
    {
        // Assign gui skin
        GUI.skin = skin;

        GUI.Label(gameNamePosition, "Sphere Runner");

        if (GUI.Button(playPosition, "Play"))
        {
            // Play!
            print("Player pressed play (Menu -> Play)");
            print("Loading level one.");
            timerEnabled = false;
            SceneManager.LoadScene("Level 1");
        }
        if (GUI.Button(playTimerPosition, "Play (With Timer)"))
        {
            // Play with timer on.
            print("Player pressed play w/ timer (Menu -> Play w/ Timer)");
            print("Loading level one.");
            timerEnabled = true;
            SceneManager.LoadScene("Level 1");
        }

        if (GUI.Button(quitPosition, "Quit"))
        {
            // Quit game.
            print("Player Quit (Menu -> Quit)");
            Application.Quit();
        }

    }
}
