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

    [SerializeField] private GameObject UnitButtonPrefab;

    public void DisplayInfo(InfoStruct inf) {
        Name.text = inf.Name;
        DisplayImage.sprite = inf.UISprite;
        //DisplayImage.SetNativeSize();
        
        if (!inf.HasUnits()) {
            ProductionSubmenu.gameObject.SetActive(false);
           
        } else {
            ProductionSubmenu.gameObject.SetActive(true);
            for (int i = ProductionSubmenu.childCount - 1; i >= 0; i--) {
                if(ProductionSubmenu.GetChild(i).GetComponent<UnitProduceButton>()){
                    Destroy(ProductionSubmenu.GetChild(i).gameObject);
                }
                
            }

            foreach (UnitBaseSO unitSO in inf.ProducableUnits) {
                var button = Instantiate(UnitButtonPrefab, ProductionSubmenu).GetComponent<UnitProduceButton>();
                button.SetButtonSO(unitSO);

            }

            
        }

    }


}
