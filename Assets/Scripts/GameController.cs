using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float cameraDistance = 5;

    public TextMeshProUGUI LiveriesStolenText;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Debug.Log(LiveriesStolenText.text);
    }
}
