using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomObject : MonoBehaviour {

    public GameObject Canvas;

    public ActionManager[] Action;

    public GameObject Button;

    public GameObject TargetObj;

    private bool ButtonExist;

    private List<GameObject> ExistButton = new List<GameObject>();

    private bool delay;

	
	void Update ()
    {
        if (!Linker.Singleton.OverCanvas && Input.GetMouseButtonDown(0) && delay == false)
        {
            CanvasClose();
        }
    }

    private void LateUpdate()
    {
        delay = false;
    }


    private void OnMouseDown()
    {
        delay = true;

        if (!Linker.Singleton.OverCanvas)
        {
            Canvas.SetActive(true);
            Debug.Log("Canvas");
            ButtonInstantiation();
        }
        
    }

    public void ButtonInstantiation()
    {
        if (ButtonExist) return;

        for (int i = 0; i < Action.Length; i++)
        {
            if (Action[i].CheckActionNewObject())
            {
                GameObject button = Instantiate(Button, Canvas.transform);
                ExistButton.Add(button);

                if (!Action[i].CheckButtonAccess())
                {
                    button.GetComponent<Button>().interactable = false;
                }

                ActionButton actButtonComponent = button.GetComponent<ActionButton>();

                actButtonComponent.Index = i;
                actButtonComponent.RoomObjectComponent = this;

                button.GetComponentInChildren<Text>().text = Action[i].ActionName;
            }

            else
            {
                continue;
            }
        }

        ButtonExist = true;

    }

    public void SetIndexes(int index)
    {
        ActionManager suitableItem = Action[index];
        suitableItem.SetPressTime();

        GhostController.Singleton.MoveToObject(TargetObj.transform.position);
        GhostController.Singleton.Parameters = suitableItem;

        if (suitableItem.MinusableObject)
        {
            PlayerData.Singleton.RemoveBoughtProduct(suitableItem.MinusableObject.Name);
        }
        
    }

    public void CanvasClose()
    {
        Canvas.SetActive(false);
        
        foreach(GameObject button in ExistButton)
        {
            Destroy(button);
        }

        ExistButton.Clear();
        ButtonExist = false;
    }

        
    

}
