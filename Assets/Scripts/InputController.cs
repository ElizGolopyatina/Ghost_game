using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    void Start()
    {

    }

    void Update()
    {
        keyboardinput();
    }

    public void keyboardinput()
    {
        float axis = Input.GetAxis("Horizontal");

        if (axis != 0)
        {
            CameraController.Singleton.MoveCamera(axis);
        }
    }


}
