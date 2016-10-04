using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public int levelToLoad;
    public GameObject padlock;
    public string loadPrompt;


    private int completedLevel;
    private bool inRange;
    private bool canLoadLevel;


    void Start()
    {
        completedLevel = PlayerPrefs.GetInt("Level Completed");

        if (completedLevel == 0)
        {
            completedLevel = 1;
        }

        canLoadLevel = levelToLoad <= completedLevel ? true : false;
        if(!canLoadLevel)
        {
            Instantiate(padlock, new Vector3(transform.position.x + 2f, 0f, transform.position.z), Quaternion.identity);
        }
    }

    void Update()
    {
        if (canLoadLevel && Input.GetButtonDown("Action") && inRange) {
            print("Pressed 'E' key.");
            SceneManager.LoadScene("Level " + levelToLoad);
        }
    }

    void OnTriggerStay(Collider other)
    {
        inRange = true;
        if (canLoadLevel) {
            loadPrompt = "Press [E] to load level " + levelToLoad.ToString();
        } else
        {
            loadPrompt = "Level" + levelToLoad.ToString() + " is locked.";
        }
    }

    void OnTriggerExit()
    {
        inRange = false;
        loadPrompt = "";
    }

    void OnGUI()
    {
        GUI.Label(new Rect(30, Screen.height * .9f, 200, 40), loadPrompt);
    }

}
