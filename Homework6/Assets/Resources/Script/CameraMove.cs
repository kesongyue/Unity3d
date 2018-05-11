using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public FirstController sceneController;
    private Vector3 offset;
    // Use this for initialization  
    void Start()
    {
        sceneController = (FirstController)SSDirector.getInstance().currentScenceController;
        player = sceneController.player;
        offset = player.transform.position - this.transform.position;
    }

    // Update is called once per frame  
    void Update()
    {
        player = sceneController.player;
        if (sceneController.gameState == GameState.BEGIN)
            this.transform.position = player.transform.position - offset;
    }
}