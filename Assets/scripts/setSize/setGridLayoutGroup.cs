using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setGridLayoutGroup : MonoBehaviour
{


    void Start()
    {
        gameObject.GetComponent<GridLayoutGroup>().cellSize = new Vector2(staticVariable.screen_width, 100);
    }
}
