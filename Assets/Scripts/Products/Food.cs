using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : Product {


    public override GameObject SetUpPrefab(Transform parent)
    {
        GameObject productPrefab = base.SetUpPrefab(parent);

        Button toEat = productPrefab.transform.GetChild(0).GetComponent<Button>();
        Button toPut = productPrefab.transform.GetChild(1).GetComponent<Button>();

        Text foodName = productPrefab.transform.GetChild(2).GetComponent<Text>();
        Text foodPrice = productPrefab.transform.GetChild(3).GetComponent<Text>();      

        if (LevelAccess > PlayerData.Singleton.CurrentLevel)
        {
            toEat.interactable = false;
            toPut.interactable = false;
        }

        toEat.onClick.AddListener(ToEat);
        toPut.onClick.AddListener(ToPut);

        Debug.Log("Yes");

        foodName.text = Name;
        foodPrice.text = CoinPrice.ToString();

        return productPrefab;
    }

    public void ToEat()
    {
        if (!ToBuyAndEat()) return;

        PlayerData.Singleton.ChangingResources(Coefficients);
    }

    public void ToPut()
    {
        if (!ToBuy()) return;

        Debug.Log(PlayerData.Singleton.ProductAmount);
    }

    public void ToEatFromFridge()
    {
        PlayerData.Singleton.ChangingResources(Coefficients);
    }
    
}
