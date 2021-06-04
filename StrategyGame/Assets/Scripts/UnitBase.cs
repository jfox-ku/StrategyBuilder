using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitBase : MonoBehaviour, IPointerDownHandler, IPoolable
{
    [SerializeField] private UnitBaseSO SO;
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private GridMover Mover;

    [SerializeField] private InfoEvent DisplayInfoEvent;
    [SerializeField] private GameObjEvent SelectedEvent;

    private Color BaseMaterialColor;
    [SerializeField] private UnitBaseSO TestingSO;

    private void Start() {
        Initialize(TestingSO);
    }

    public bool InitializeAt(GridTile Tile) {
        if (Tile.isOccupied) return false;
        Mover.MoverInitialize(Tile);
        return true;

    }

    public void MoveTo(GridTile endTile) {
        Mover.Move(endTile);
    }

    public void Initialize(UnitBaseSO so) {
        SO = so;
        Renderer.sprite = SO.GameSprite;
        BaseMaterialColor = Renderer.material.color;
        
    }

    public GameObject GetPoolPrefab() {
        return SO.UnitPrefab;
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            DisplayInfoEvent?.Raise(SO.GetDisplayInfo());
            SelectedEvent?.Raise(this.gameObject);
        }
           
    }

    public int PoolCount() {
        return SO.CountToPool;
    }

    public string PoolName() {
        return SO.UnitName;
    }

    
}
