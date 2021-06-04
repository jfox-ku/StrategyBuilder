using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Custom SO/New Bulding Base")]
public class BuildingBaseSO : ScriptableObject, IDisplayableInfo {

    public string BuildingName;
    public Vector2 CellSize;

    public Sprite GameSprite;
    public Sprite UISprite;

    public List<UnitBaseSO> ProducableUnits;
    public GameObject BuildingPrefab;
    public int CountToPool;

    public InfoStruct GetDisplayInfo() {
        var data = new InfoStruct {
            Name = BuildingName,
            UISprite = UISprite
        };

        if (ProducableUnits.Count != 0) data.ProducableUnits = ProducableUnits;
        else data.ProducableUnits = null;

        return data;
    }
    
}


