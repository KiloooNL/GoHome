using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Rect newGamePosition = new Rect(400, 120, 140, 45);
    //Rect newGameTimerPosition = new Rect(400, 120, 140, 45);
    Rect resumePosition = new Rect(400, 175, 140, 45);
    //Rect resumeTimerPosition = new Rect(400, 140, 140, 45);
    Rect quitPosition = new Rect(400, 235, 140, 45);
    Rect gameNamePosition = new Rect(300, 30, 400, 75);

    public int levelCompleted;

    public GUISkin skin;

    void Start()
    {
        levelCompleted = PlayerPrefs.GetInt("Level Completed");
    }

    void OnGUI()
    {
        // Assign gui skin
        GUI.skin = skin;

        GUI.Label(gameNamePosition, "Sphere Runner");

        if (GUI.Button(newGamePosition, "New Game"))
        {
            NewGame(false);
        }

        if(levelCompleted > 0)
        {
            if (GUI.Button(resumePosition, "Resume Game"))
            {
                resumeGame(false);
            }
        }

        if (GUI.Button(quitPosition, "Quit"))
        {
            // Quit game.
            print("Player Quit (Menu -> Quit)");
            Application.Quit();
        }

        /*
        TODO: Make this functional...

        if (GUI.Button(newGameTimerPosition, "New Game (With Timer)"))
        {
            NewGame(true);
        }
        
        if (GUI.Button(resumeTimerPosition, "Resume Game (With Timer)"))
        {

        }
        */

    }

    void NewGame(bool timer)
    {
        PlayerPrefs.SetInt("Level Completed", 0);
        if(timer == true)
        {

        }
        
        print("Loading level one.");
        SceneManager.LoadScene("Level 1");
    }

    void resumeGame(bool timer)
    {

        if(timer == true)
        {

        }

        print("Resuming game... from level" + levelCompleted.ToString());
        SceneManager.LoadScene(levelCompleted);
    }
}
