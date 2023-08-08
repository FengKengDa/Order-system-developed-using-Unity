using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banAllListener : MonoBehaviour
{
    public GameObject currentTicket;
    public GameObject ticket;
    public GameObject historyTicket;
    public GameObject paimeng;
    public GameObject errorInfo;
    public GameObject submitPage;

    // Start is called before the first frame update
    void Start()
    {
        eventCenter.AddListener<int>(staticVariable.banAllCloseChild, closeChild);
    }



    public void closeChild(int type)
    {
        if(type == 1)
        {
            currentTicket.SetActive(false);
        }
        if (type == 2)
        {
            ticket.SetActive(false);
        }
        if (type == 3)
        {
            historyTicket.SetActive(false);
        }
        if (type == 4)
        {
            paimeng.SetActive(false);
        }
        if (type == 5)
        {
            errorInfo.SetActive(false);
        }
        if (type == 6)
        {
            submitPage.SetActive(false);
        }
        for (int i=0;i<transform.childCount;i++)
        {
            if(transform.GetChild(i).gameObject.activeSelf)
            {
                return;
            }
        }
        gameObject.SetActive(false);
    }

}
