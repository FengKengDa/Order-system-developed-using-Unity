using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadTicket : MonoBehaviour
{
    public GameObject ticketPrefab;
    public GameObject content;
    void Start()
    {
        eventCenter.AddListener<network.waitTicketRoot>(staticVariable.loadTicket_second, loadAllTicket);
        //loadAllTicket();
    }

    public void loadAllTicket(network.waitTicketRoot r)
    {
        int count = content.transform.childCount;
        for(int i = 0; i < count; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }

        if(r == null||r.tickets.Count == 0)
        {
            return;
        }
        for(int i=0;i<r.tickets.Count;i++)
        {
            GameObject t = Instantiate<GameObject>(ticketPrefab, content.transform);
            t.GetComponent<ticketPrefab>().setTicketInfo(r.tickets[i]);
        }
    }

}
