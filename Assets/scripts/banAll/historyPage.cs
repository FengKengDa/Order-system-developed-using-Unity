using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class historyPage : MonoBehaviour
{
    public void closeHistory()
    {
        eventCenter.PostEvent<int>(staticVariable.banAllCloseChild, 3);
    }
}
