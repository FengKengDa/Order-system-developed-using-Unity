using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setAddContentSize : MonoBehaviour
{
    public int elementSize = 30;
    public string contentType;
    public GameObject content;

    private void Start()
    {
        eventCenter.AddListener<int>(contentType, setContentSize);
    }

    public void setContentSize(int num)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(300, elementSize * (content.transform.childCount+num));
    }

}
