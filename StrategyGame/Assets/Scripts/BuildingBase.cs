using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingBase : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private BuildingBaseSO SO;
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private GridPlacer Placer;

    [SerializeField] private InfoEvent DisplayInfoEvent;

    private Color BaseMaterialColor;
    [SerializeField] private BuildingBaseSO TestingSO;


    private void Start() {
        Initialize(TestingSO);
    }

    public void Initialize(BuildingBaseSO so) {
        SO = so;
        Renderer.sprite = SO.GameSprite;
        BaseMaterialColor = Renderer.material.color;
        Placer.CellSize = SO.CellSize;

        SetGhost();
    }

    public void OnPointerDown(PointerEventData eventData) {
        
        if (Placer.isGhost) {
            if(eventData.button == PointerEventData.InputButton.Right) {
                CancelPlacement();
            }

            PlaceBuilding();
        } else {   
            DisplayInfoEvent?.Raise(SO.GetDisplayInfo());
        }
    }

    private void CancelPlacement() {
        Destroy(this.gameObject);
    }

    private void SetGhost() {
        Color ghostClr = BaseMaterialColor;
        ghostClr.a = 0.5f;

        Renderer.material.color = ghostClr;
        Placer.isGhost = true;


    }

    private void PlaceBuilding() {
        if (Placer.TryPlace()) {
            Renderer.material.color = BaseMaterialColor;
            Placer.isGhost = false;
        }    
    }


}
