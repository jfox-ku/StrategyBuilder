using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingBase : MonoBehaviour, IPointerDownHandler, IPoolable {
    [SerializeField] private BuildingBaseSO SO;
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private GridPlacer Placer;

    [SerializeField] private InfoEvent DisplayInfoEvent;
    [SerializeField] private GameObjEvent SelectedEvent;

    private Color BaseMaterialColor;
    [SerializeField] private BuildingBaseSO TestingSO;


    [SerializeField] private Transform FlagObject;
    public GridTile SpawnPoint;
    public GridTile SpawnDestination;


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
            } else {
            PlaceBuilding();
            }

        } else {
            if (eventData.button == PointerEventData.InputButton.Left) {
                SelectedEvent?.Raise(this.gameObject);
                DisplayInfoEvent?.Raise(SO.GetDisplayInfo());
            }
        }
    }

    private void CancelPlacement() {
        ObjectPoolManager.instance.ReturnToPool(this.gameObject,this);
    }

    private void SetGhost() {
        Color ghostClr = BaseMaterialColor;
        ghostClr.a = 0.5f;
        Renderer.material.color = ghostClr;
        Placer.isGhost = true;
    }


    private IEnumerator FlashInvalid() {
        Color flashGhostColor = BaseMaterialColor;
        flashGhostColor.a = 0.5f;
        flashGhostColor.g = 0.5f;
        flashGhostColor.b = 0.5f;

        Renderer.material.color = flashGhostColor;
        yield return new WaitForSeconds(0.2f);
        Color ghostClr = BaseMaterialColor;
        ghostClr.a = 0.5f;
        Renderer.material.color = ghostClr;

    }

    public bool isGhost() {
        return Placer.isGhost;
    }


    public void ProduceUnit(string unitName) {
        ObjectPoolManager PoolMan = ObjectPoolManager.instance;
        var unit = PoolMan.GetFromPoolString(unitName).GetComponent<UnitBase>();
        SpawnPoint = Placer.FindSpawnPoint();
        
        if (SpawnPoint!=null) {
            unit.InitializeAt(SpawnPoint);

            GridTile Alternate = null;
            if (SpawnDestination.isOccupied) {
                Alternate = Placer.GetViableNeighbour(SpawnDestination);
                if(Alternate!=null) unit.MoveTo(Alternate);
            } else {
                unit.MoveTo(SpawnDestination);
            }
        } else {
            PoolMan.ReturnToPool(unit.gameObject,unit);

            Debug.Log("Spawns point is blocked!");
        }

    }

    public void SetSpawnDestination(GridTile flagTile) {
        if (SO.ProducableUnits.Count == 0) return;
        FlagObject.position = Placer.GM.GetTileTransformPosition(flagTile);
        SpawnDestination = flagTile;
    }

    private void PlaceBuilding() {
        if (Placer.TryPlace()) {
            Renderer.material.color = BaseMaterialColor;
            Placer.isGhost = false;
            SpawnPoint = Placer.FindSpawnPoint();
            SetSpawnDestination(SpawnPoint);

            SelectedEvent?.Raise(this.gameObject);
            DisplayInfoEvent?.Raise(SO.GetDisplayInfo());
        } else {
            StartCoroutine(FlashInvalid());
        }
    }

    public bool CanProduceFromPool(string unitName) {
        List<UnitBaseSO> producableUnits = SO.ProducableUnits;
        for (int i = 0; i < producableUnits.Count; i++) {
            if(producableUnits[i].UnitName == unitName) {
                return true;
            }
        }
        return false;
    }

    public GameObject GetPoolPrefab() {
        return SO.BuildingPrefab;
    }

    public int PoolCount() {
        return SO.CountToPool;
    }

    public string PoolName() {
        return SO.BuildingName;
    }


}
