using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionButton : MonoBehaviour
{
    [SerializeField] private BuildingBaseSO building;
    [SerializeField] StringEvent ProduceGhostEvent;
    [SerializeField] private Image buttonImage;

    public BuildingBaseSO testingSo;

    private void Start() {
        SetButtonSO(testingSo);
    }

    public void SetButtonSO(BuildingBaseSO SO) {

        building = SO;
        buttonImage.sprite = SO.UISprite;

    }
    public void ButtonClicked() {
        ProduceGhostEvent?.Raise(building.BuildingName);
    }
    
}
