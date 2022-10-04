using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linker : MonoBehaviour {

    public static Linker Singleton;

    public bool OverCanvas;

    private void Awake()
    {
        Singleton = this;
    }
}
