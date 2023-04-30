using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float cameraDistance = 5;

    public TextMeshProUGUI LiveriesStolenText;

    public GameObject GameOverPanel;
    public TextMeshProUGUI FinalLiveriesText;

    public Button RestartButton;
    public Button QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        GameOverPanel.SetActive(false);
        var thirdperson = vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        thirdperson.CameraDistance = cameraDistance;

        Button restartButton = RestartButton.GetComponent<Button>();
        restartButton.onClick.AddListener(RestartGame);
        Button quitButton = QuitButton.GetComponent<Button>();
        quitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLiveriesStolen(int liveriesStolen)
    {
        LiveriesStolenText.text = "Liveries stolen: " + liveriesStolen.ToString();
        FinalLiveriesText.text = "Liveries stolen: " + liveriesStolen.ToString();
        Debug.Log(LiveriesStolenText.text);
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        //Time.timeScale = 0.05f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Debug.Log("restart");
        //Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        SceneManager.LoadScene("MainScene");
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
