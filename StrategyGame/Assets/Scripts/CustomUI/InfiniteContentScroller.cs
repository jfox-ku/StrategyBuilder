using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteContentScroller : MonoBehaviour
{
    public ScrollRect scroller;
    Vector2 lastPos;

    public void ValueChanged(Vector2 changePos) {
        if (changePos != lastPos) {
            lastPos = changePos;
        }

        
        if(changePos.y <= 0f) {
            scroller.verticalNormalizedPosition = 0.99f;
            
        }

        if (changePos.y >= 1f) {
            scroller.verticalNormalizedPosition = 0.01f;
            
        }


    }
}
