using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour {

    public static PlayerData Singleton;

    public Resources Parameters;

    public Resources Coefficients;

    public int CoinAmount;
    public int CrystalAmount;

    public int CurrentLevel;
    public float CurrentLevelPoint;
    public Text LevelText;

    public int[] LevelPoint = new int[] {9, 20, 100, 300, 700, 1500, 4000, 8000, 16000, 32000};
    public GameObject[] ProductsOnLevel;

    public int StartLotteryTime;

    public int ProductAmount;

    private void Awake()
    {
        Singleton = this;
    }

    void Start ()
    {
        int timeBetweenLotteries = 21600;

        int lastTime = PlayerPrefs.GetInt("exitTime");
        int currentTime = GetCurrentTime();

        int startTime = PlayerPrefs.GetInt("startTime", currentTime);

        startTime = currentTime;
        PlayerPrefs.SetInt("startTime", startTime);

        int passedTime = currentTime - lastTime;

        StartLotteryTime = PlayerPrefs.GetInt("StartLotteryTime", GetCurrentTime() - timeBetweenLotteries);
        
        int passedLotteryTime = currentTime - StartLotteryTime;

        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        CurrentLevelPoint = PlayerPrefs.GetFloat("CurrentLevelPoint", 0);

        Parameters.Mood = PlayerPrefs.GetFloat("moodParameter", 80);
        Parameters.Bellyful = PlayerPrefs.GetFloat("bellyfulParameter", 80);
        Parameters.Rest = PlayerPrefs.GetFloat("restParameter", 80);
        Parameters.Clean = PlayerPrefs.GetFloat("cleanParameter", 80);

        Coefficients.Mood = PlayerPrefs.GetFloat("moodCoef", 0.2f);
        Coefficients.Bellyful = PlayerPrefs.GetFloat("bellyfulCoef", 0.2f);
        Coefficients.Rest = PlayerPrefs.GetFloat("restCoef", 0.1f);
        Coefficients.Clean = PlayerPrefs.GetFloat("cleanCoef", 0.2f);

        if (passedTime >= 0)
        {
            ChangingResources(Coefficients * -passedTime);
        }

        if (passedLotteryTime >= timeBetweenLotteries)
        {
            UIManager.Singleton.LotteryPanel.SetActive(true);
        }

        LevelText.text = CurrentLevel.ToString();

        CoinAmount = PlayerPrefs.GetInt("CoinAmount", 0);
        UIManager.Singleton.ChangeCoinText(CoinAmount);
        UIManager.Singleton.FillLevelSlider(CurrentLevelPoint, CurrentLevel);
    }
		
	void Update ()
    {
        ChangingResources(Coefficients * -Time.deltaTime);
    }

    public void AddLevelPoint(int pointAmount)
    {
        CurrentLevelPoint += pointAmount;
        LevelIncrease();

        Debug.Log("Current Level Point " + CurrentLevelPoint);

        UIManager.Singleton.FillLevelSlider(CurrentLevelPoint, CurrentLevel);

        UIManager.Singleton.ChangeLevelPointText(CurrentLevelPoint);
    }

    public void LevelIncrease()
    {
        if (CurrentLevelPoint > LevelPoint[CurrentLevel])
        {
            CurrentLevelPoint = 0;
            CurrentLevel += 1;
            LevelText.text = CurrentLevel.ToString();
        }

        Debug.Log("Current Level " + CurrentLevel);
    }

    public void ChangeCoinAmount(int someCoin)
    {
        CoinAmount += someCoin;
        UIManager.Singleton.ChangeCoinText(CoinAmount);

        PlayerPrefs.SetInt("CoinAmount", CoinAmount);
    }

    public void ChangeCrystalAmount(int someCrystal)
    {
        CrystalAmount += someCrystal;
    }

    public void ChangingCoef(Resources delta)
    {
        Coefficients.Mood += delta.Mood;
        Coefficients.Bellyful += delta.Bellyful;
        Coefficients.Rest += delta.Rest;
        Coefficients.Clean += delta.Clean;
    }

    public void ChangingResources(Resources delta)
    {
        changingMood(delta.Mood);
        changingBellyful(delta.Bellyful);
        changingRest(delta.Rest);
        changingClean(delta.Clean);
    }

    public void SaveBoughtProduct(string productKey)
    {
        int productAmount = PlayerPrefs.GetInt(productKey, 0);
        productAmount += 1;
        PlayerPrefs.SetInt(productKey, productAmount);
    }

    public int GetSavedProductAmount(string productKey)
    {
        return PlayerPrefs.GetInt(productKey, 0);
    }

    public void RemoveBoughtProduct(string productKey)
    {
        int productAmount = PlayerPrefs.GetInt(productKey, 0);
        productAmount -= 1;
        PlayerPrefs.SetInt(productKey, productAmount);
    }

    public bool IsProductBought(string productKey)
    {       
        if (PlayerPrefs.GetInt(productKey) == 0) return false;

        return true;
    }

    private void OnDestroy()
    {
        int exitTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerSecond);
        PlayerPrefs.SetInt("exitTime", exitTime);
        PlayerPrefs.SetInt("StartLotteryTime", StartLotteryTime);

        PlayerPrefs.SetFloat("moodParameter", Parameters.Mood);
        PlayerPrefs.SetFloat("bellyfulParameter", Parameters.Bellyful);
        PlayerPrefs.SetFloat("restParameter", Parameters.Rest);
        PlayerPrefs.SetFloat("cleanParameter", Parameters.Clean);

        PlayerPrefs.SetFloat("moodCoef", Coefficients.Mood);
        PlayerPrefs.SetFloat("bellyfulCoef", Coefficients.Bellyful);
        PlayerPrefs.SetFloat("restCoef", Coefficients.Rest);
        PlayerPrefs.SetFloat("cleanCoef", Coefficients.Clean);

        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.SetFloat("CurrentLevelPoint", CurrentLevelPoint);
    }



    public void changingMood(float moodCoef)
    {
        Parameters.Mood += moodCoef;

        if (Parameters.Mood >= 100)
        {
            Parameters.Mood = 100;
        }

        if (Parameters.Mood <= 0)
        {
            Parameters.Mood = 0;
        }

        UIManager.Singleton.SliderMoodChanging(Parameters.Mood / 100);      
    }

    public void changingBellyful(float bellyfulCoef)
    {
        Parameters.Bellyful += bellyfulCoef;

        if (Parameters.Bellyful >= 100)
        {
            Parameters.Bellyful = 100;
        }

        if (Parameters.Bellyful <= 0)
        {
            Parameters.Bellyful = 0;
        }

        UIManager.Singleton.SliderBellyfulChanging(Parameters.Bellyful / 100);
    }

    public void changingRest(float restCoef)
    {
        Parameters.Rest += restCoef;

        if (Parameters.Rest >= 100)
        {
            Parameters.Rest = 100;
        }

        if (Parameters.Rest <= 0)
        {
            Parameters.Rest = 0;
        }

        UIManager.Singleton.SliderRestChanging(Parameters.Rest / 100);
    }

    public void changingClean(float cleanCoef)
    {
        Parameters.Clean += cleanCoef;

        if (Parameters.Clean >= 100)
        {
            Parameters.Clean = 100;
        }

        if (Parameters.Clean <= 0)
        {
            Parameters.Clean = 0;
        }

        UIManager.Singleton.SliderCleanChanging(Parameters.Clean / 100);
    }

    public int GetCurrentTime()
    {
        return (int)(DateTime.Now.Ticks / TimeSpan.TicksPerSecond);
    }
}






