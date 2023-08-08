using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class closeError : MonoBehaviour
{
    public void close()
    {
        eventCenter.PostEvent<int>(staticVariable.banAllCloseChild, 5);
    }
}
