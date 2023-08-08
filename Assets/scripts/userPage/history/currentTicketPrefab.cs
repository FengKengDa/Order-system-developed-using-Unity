using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentTicketPrefab : MonoBehaviour
{
    [HideInInspector]
    public network.currentTicketRoot ticketInfo;
    private bool hasTicket = false;

    public Text title;

    private void Start()
    {
        eventCenter.AddListener<network.currentTicketRoot>(staticVariable.loadCurrentTicket, loadInfo);
        eventCenter.AddListener<int>(staticVariable.finish_ticket, finish_ticket);

    }

    public void finish_ticket(int t)
    {
        ticketInfo = null;
        hasTicket = false;
        title.text = "";
    }

    public void loadInfo(network.currentTicketRoot r)
    {
        ticketInfo = r;
        if(ticketInfo.title == null)
        {
            hasTicket = false;
            return;
        }
        hasTicket = true;
        title.text = ticketInfo.title;
    }

    public void showCurrentTicketInfo()
    {
        eventCenter.PostEvent<network.currentTicketRoot>(staticVariable.showCurrentTicketInfo, ticketInfo);
    }


}
