using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorActivity : MonoBehaviour {

    void Update()
    {
        Linker.Singleton.OverCanvas = EventSystem.current.IsPointerOverGameObject();
    }
}
