using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]

public class ActionManager
{
    public Product NewActionObj;
    public Product MinusableObject;

    public float MoodCoefficient;
    public float CleanCoefficient;
    public float BellyfulCoefficient;
    public float RestCoefficient;

    public string ActionName;

    public int ActionTime;

    public float TimeDelay;
    private long pressTime;

    public void SetPressTime()
    {
        pressTime = DateTime.Now.Ticks;       
    }

    public bool CheckButtonAccess()
    {
        long currentTime = DateTime.Now.Ticks;
        return (currentTime - pressTime >= TimeDelay * TimeSpan.TicksPerSecond);        
    }

    public bool CheckActionNewObject()
    {
        if (NewActionObj == null) return true;

        if (PlayerData.Singleton.GetSavedProductAmount(NewActionObj.Name) == 0) return false;

        return true;
    }
}
