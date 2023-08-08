using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct historyInfo
{
    public int id;
    public string title;
    public string submitter;
    public string phone_number;
    public string submitter_ass;
    public string phone_number_ass;
    public string participant;
    public string reason;
    public string department;
    public string submitter_time;
    public string finish_time;
    public int total_cost;
    public string image_path;
    public string approvalInfo;

}

public class historyPrefab : MonoBehaviour
{
    [HideInInspector]
    public historyInfo info = new historyInfo();
    public Text text;

    public void setInfo(network.TicketsItem t)
    {
        if(t == null || t.title == null)
        {
            return;
        }
        text.text = "主题：" + t.title;
        info.id = t.ticket_id;
        info.title = t.title;
        info.submitter = t.submitter;
        info.phone_number = t.phone_number;
        info.submitter_ass = (t.submitter_ass == null||t.submitter_ass=="")?"无":t.submitter_ass;
        info.phone_number_ass = (t.phone_number_ass == null || 
            t.phone_number_ass == "") ? "无" : t.phone_number_ass;
        info.participant = "";
        for (int i = 0; i < t.participants.Count; i++)
        {
            info.participant += t.participants[i];
            if (i != t.participants.Count - 1)
            {
                info.participant += ", ";
            }
        }
        info.participant = info.participant == "" ? "无" : info.participant;
        info.reason = t.reason;
        info.department = "";
        for(int i=0;i<t.departments.Count;i++)
        {
            info.department += t.departments[i];
            if(i!=t.departments.Count-1)
            {
                info.department += ", ";
            }
        }
        info.department = info.department == "" ? "无" : info.department;
        info.submitter_time = t.submitted_time;
        info.total_cost = t.cost;
        info.image_path = t.image_path;
        info.approvalInfo = "";
        for (int i = 0; i < t.approval_info.Count; i++)
        {
            info.approvalInfo += t.approval_info[i];
            if (i != t.approval_info.Count - 1)
            {
                info.approvalInfo += ", ";
            }
        }
        info.approvalInfo = info.approvalInfo == "" ? "无" : info.approvalInfo;
    }

    public void clickShowHistoryInfo()
    {
        eventCenter.PostEvent<historyInfo>(staticVariable.showHistoryInfo, info);
    }

}
