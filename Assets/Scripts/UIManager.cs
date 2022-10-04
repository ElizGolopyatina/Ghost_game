using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GhostSlider GhostSliderComponent;

    public static UIManager Singleton;

    public Image MoodSlider;
    public Image BellyfulSlider;
    public Image RestSlider;
    public Image CleanSlider;

    public Image LevelSlider;

    public GameObject ShopPanel;

    public GameObject[] ProductPanel;
    public GameObject[] ScrollPanel;

    private GameObject LastPanel;
    private GameObject CurrentPanel;
    private GameObject CurrentScroll;

    public GameObject LotteryPanel;
    public GameObject YourWinnings;
    public Text Winnings;

    public Text CoinAmount;
    public Text LevelPointAmount;

    public GameObject CupboardPanel;
    public GameObject FridgePanel;

    private void Awake()
    {
        Singleton = this;
    }

    public void ChangeCoinText(int coinAmount)
    {
        CoinAmount.text = coinAmount.ToString();
    }

    public void ChangeLevelPointText(float pointAmount)
    {
        LevelPointAmount.text = pointAmount.ToString();
    }

    public void SliderMoodChanging(float deltaMood)
    {
        MoodSlider.fillAmount = deltaMood;
    }

    public void SliderBellyfulChanging(float deltaBellyful)
    {
        BellyfulSlider.fillAmount = deltaBellyful;
    }

    public void SliderRestChanging(float deltaRest)
    {
        RestSlider.fillAmount = deltaRest;
    }

    public void SliderCleanChanging(float deltaClean)
    {
        CleanSlider.fillAmount = deltaClean;
    }

    public void OpenShop()
    {
        ShopPanel.SetActive(true);
    }

    public void PanelOpen(int index)
    {       
        CurrentPanel = ProductPanel[index];
        CurrentScroll = ScrollPanel[index];
        CurrentScroll.SetActive(true);
        
        if (LastPanel != null) LastPanel.SetActive(false);

        ProductPanel[index].GetComponent<ShopManager>().ProductInstatiation();

        LastPanel = ScrollPanel[index];
    }

    public void CloseShop()
    {
        ShopPanel.SetActive(false);

        foreach(GameObject product in ProductPanel)
        {
            product.GetComponent<ShopManager>().DestroyProducts();
        }

        ScrollPanel[0].SetActive(false);
        ScrollPanel[1].SetActive(false);
        ScrollPanel[2].SetActive(false);

        LastPanel = null;
    }

    public void LotteryPlaying()
    {
        int prize =  Random.Range(10, 100);
        YourWinnings.SetActive(true);
        Winnings.text = prize.ToString();
        PlayerData.Singleton.ChangeCoinAmount(prize);
        PlayerData.Singleton.AddLevelPoint(5);

        PlayerData.Singleton.StartLotteryTime = PlayerData.Singleton.GetCurrentTime();
    }

    public void CloseLotteryPanel()
    {
        LotteryPanel.SetActive(false);
    }

    public void CloseCupboardPanel()
    {
        CupboardPanel.SetActive(false);

        Cupboard.Singleton.DestroyClothes();
    }

    public void CloseFridgePanel()
    {
        FridgePanel.SetActive(false);

        Fridge.Singleton.DestroyFood();
    }

    public void EndAction()
    {
        GhostSliderComponent.fill = false;
        GhostSliderComponent.GhostCanvas.SetActive(false);
        GhostController.Singleton.Stop = false; 
    }

    public void FillLevelSlider(float pointAmount, int currentLevel)
    {
        int maxLevelPoint = PlayerData.Singleton.LevelPoint[currentLevel];
        Debug.LogFormat("pointAmount {0} maxlevelPoint {1} filled {2}" , pointAmount, maxLevelPoint, pointAmount / maxLevelPoint);
        
        LevelSlider.fillAmount = (pointAmount / maxLevelPoint);
    }
}
