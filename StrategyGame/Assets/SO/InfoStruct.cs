using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InfoStruct 
{
    public string Name;
    public Sprite UISprite;
    public List<UnitBaseSO> ProducableUnits;

    public bool HasUnits() {
        if (ProducableUnits == null) return false;
        return ProducableUnits.Count > 0;
    }

}
