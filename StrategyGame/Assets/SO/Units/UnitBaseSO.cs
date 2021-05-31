using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit",menuName = "Custom SO/New Unit Base")]
public class UnitBaseSO : ScriptableObject, IDisplayableInfo
{
    public string UnitName;
    public Sprite GameSprite;
    public Sprite UISprite;

    public GameObject UnitPrefab;
    public int CountToPool;

    public InfoStruct GetDisplayInfo() {
        var data = new InfoStruct {
            Name = UnitName,
            UISprite = UISprite,
            ProducableUnits = null
        };

        return data;
    }

    
}
