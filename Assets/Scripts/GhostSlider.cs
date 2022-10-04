using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostSlider : MonoBehaviour {

    private float Timer;
    private float StartTimer;

    public Image Slider;
    public GameObject GhostCanvas;

    public bool fill;

	void Update ()
    {
        if (!fill)
        {
            return;
        }

        Timer -= Time.deltaTime;
        Slider.fillAmount = (StartTimer - Timer)/StartTimer;

        if (Timer <= 0)
        {
            fill = false;

            GhostController.Singleton.Stop = false;
            GhostCanvas.SetActive(false);
            GhostController.Singleton.SetParameters();
        }
	}

    public void SliderFilling(int fillingTime)
    {
        fill = true;
        GhostCanvas.SetActive(true);

        Timer = fillingTime;
        StartTimer = fillingTime;     
    }
}
