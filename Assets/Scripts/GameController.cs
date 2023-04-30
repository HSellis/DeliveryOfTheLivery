using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float cameraDistance = 5;

    public TextMeshProUGUI LiveriesStolenText;

    public GameObject GameOverPanel;
    public TextMeshProUGUI FinalLiveriesText;

    // Start is called before the first frame update
    void Start()
    {
        GameOverPanel.SetActive(false);
        var thirdperson = vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        thirdperson.CameraDistance = cameraDistance;
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
        Time.timeScale = 0.05f;
    }

    public void Restartgame()
    {
        GameOverPanel.SetActive(false);
        SceneManager.LoadScene("MainSceneTwo");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
