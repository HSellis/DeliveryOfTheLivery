using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Control : MonoBehaviour
{
    public Button Button_Start;
    public Button Button_Quit;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Buttons are now working!");
        Button start = Button_Start.GetComponent<Button>();
        Button quit = Button_Quit.GetComponent<Button>();
        start.onClick.AddListener(ClickedStart);
        quit.onClick.AddListener(ClickedQuit);
    }

    void ClickedStart()
    {
        Debug.Log("The game begins!");
        SceneManager.LoadScene("MainScene");
    }

    void ClickedQuit()
    {
        Debug.Log("Game quitted. If it was a real game, not only would the application be upset, but it would also quit.");
        Application.Quit();
    }
}
