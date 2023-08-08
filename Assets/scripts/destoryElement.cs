using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryElement : MonoBehaviour
{
    public string contentType;
    public void des()
    {
        eventCenter.PostEvent<int>(contentType, -1);
        Debug.Log(contentType);
        Destroy(transform.parent.gameObject);
        
    }
}
