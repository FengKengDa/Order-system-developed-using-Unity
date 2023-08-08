using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentTicketInfoPage : MonoBehaviour
{

    [HideInInspector]
    public int tid = -1;

    public GameObject submitPage;

    private void Start()
    {
        eventCenter.AddListener<int>(staticVariable.updateSubmitAssTicketButton, set_id);
    }

    public void set_id(int id)
    {
        tid = id;
    }

    public void finish_ticket()
    {
        eventCenter.PostEvent(staticVariable.finish_ticket, tid);
        tid = -1;
    }

    public void closeBtton()
    {
        eventCenter.PostEvent<int>(staticVariable.banAllCloseChild, 1);
    }

    public void open_submit_ass_page()
    {
        submitPage.SetActive(true);
        submitPage.GetComponent<submitAssPage>().tid = tid;
        tid = -1;
        gameObject.SetActive(false);
    }

    

}
