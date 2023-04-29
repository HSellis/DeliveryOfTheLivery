using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float cameraDistance = 5;

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
}
