using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController Singleton;

    private Camera _cameraComponent;

    public float Velocity;
    
    public Camera CameraComponent
    {
        get
        {
            if (!_cameraComponent)
            {
                _cameraComponent = GetComponent<Camera>();
            }
            return _cameraComponent;
        }
    }

    private void Awake()
    {
        Singleton = this;
    }


    //public void MoveCamera(Vector2 delta)
    //{
    //    Vector3 pos = transform.position;

    //    pos.x += delta.x * Time.deltaTime * Velocity;

    //    transform.position = pos;
    //}
    
    
    public void MoveCamera(float delta)
    {
        Vector3 pos = transform.position;

        pos.x += delta * Time.deltaTime * Velocity;

        transform.position = pos;
    }
}
