using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button ButtonStart;
    public Button ButtonQuit;

    // Start is called before the first frame update
    void Start()
    {
        Button start = ButtonStart.GetComponent<Button>();
        Button quit = ButtonQuit.GetComponent<Button>();
        start.onClick.AddListener(ClickedStart);
        quit.onClick.AddListener(ClickedQuit);
    }

    void ClickedStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    void ClickedQuit()
    {
        Application.Quit();
    }
}