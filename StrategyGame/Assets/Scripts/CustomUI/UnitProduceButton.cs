using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitProduceButton : MonoBehaviour
{
    [SerializeField] private UnitBaseSO unit;
    [SerializeField] StringEvent ProduceUnitEvent;
    [SerializeField] private Image buttonImage;

    public UnitBaseSO testingSo;

    public void SetButtonSO(UnitBaseSO SO) {
        unit = SO;
        buttonImage.sprite = SO.UISprite;

    }
    public void ButtonClicked() {
        ProduceUnitEvent?.Raise(unit.UnitName);
    }


}
