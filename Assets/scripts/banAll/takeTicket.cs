using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeTicket : MonoBehaviour
{
    [HideInInspector]
    public int tid;
    [HideInInspector]
    public bool isAss;

    private void Start()
    {
        eventCenter.AddListener<int, bool>(staticVariable.updateTakeTicketButton, set_id_isAss);
    }

    public void set_id_isAss(int id, bool ass)
    {
        Debug.Log("have update id");
        tid = id;
        Debug.Log("have update id: " + tid);
        isAss = ass;
    }    

    public void take_ticket()
    {
        if(staticVariable.uid == -1 || tid == -1)
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "ÇëÏÈµÇÂ¼");
            return;
        }
        Debug.Log("ttttid: " + tid);
        eventCenter.PostEvent<int, bool>(staticVariable.take_ticket, tid, isAss);
    }

    public void close()
    {
        tid = -1;
        eventCenter.PostEvent<int>(staticVariable.banAllCloseChild, 2);
    }

}
