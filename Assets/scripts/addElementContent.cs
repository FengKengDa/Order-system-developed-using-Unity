using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addElementContent : MonoBehaviour
{
    public GameObject obj;
    public GameObject content;
    public string contentType;

    public void addObj()
    {
        GameObject objins = Instantiate(obj, content.transform);
        eventCenter.PostEvent<int>(contentType, 0);
    }
}
