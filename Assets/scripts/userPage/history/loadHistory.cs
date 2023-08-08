using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadHistory : MonoBehaviour
{
    public GameObject historyPrefab;
    public GameObject content;

    private void Start()
    {
        eventCenter.AddListener<network.historyRoot>(staticVariable.loadHistoryInfo, load);
    }

    public void load(network.historyRoot r)
    {
        int count = content.transform.childCount;
        for(int i = 0; i < count; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }

        network.historyRoot info = r;
        if (info == null||info.tickets==null)
        {
            return;
        }
        for(int i=0;i<info.tickets.Count;i++)
        {
            GameObject hp = Instantiate(historyPrefab, content.transform);
            hp.GetComponent<historyPrefab>().setInfo(info.tickets[i]);
        }
    }


}
