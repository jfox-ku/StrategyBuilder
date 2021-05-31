using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoMenu : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI Name;
    [SerializeField]private Image DisplayImage;
    [SerializeField] private Transform ProductionSubmenu;

    public void DisplayInfo(InfoStruct inf) {
        Name.text = inf.Name;
        DisplayImage.sprite = inf.UISprite;
        if (!inf.HasUnits()) {
            ProductionSubmenu.gameObject.SetActive(false);
        } else {
            ProductionSubmenu.gameObject.SetActive(true);
        }

    }


}
