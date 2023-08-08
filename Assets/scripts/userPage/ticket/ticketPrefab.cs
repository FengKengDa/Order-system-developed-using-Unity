using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ticketInfo
{
    public int id;
    public string title;
    public string submitter;
    public string phoneNumber;
    public string reason;
    public string department;
    public string state;
    public string submitTime;
    public string submitterAss;
    public string submitterAssPhoneNumber;
    public string image;
}

public class ticketPrefab : MonoBehaviour
{
    public ticketInfo tinfo = new ticketInfo();
    public Text titleInfo;

    public void setTicketInfo(network.waitTicketsItem info)
    {
        if(info==null)
        {
            return;
        }
        tinfo.id = info.ticket_id;
        Debug.Log("here tid: " + tinfo.id);
        tinfo.title = info.title;
        tinfo.submitter = info.submitter;
        tinfo.phoneNumber = info.phone_nubmer;
        tinfo.reason = info.reason;
        tinfo.department = "";
        for(int i=0;i<info.departments.Count;i++)
        {
            tinfo.department += info.departments[i];
            if(i!=info.departments.Count-1)
            {
                tinfo.department += ", ";
            }
        }
        switch (info.state)
        {
            case 0:
                tinfo.state = "未审批";
                break;
            case 1:
                tinfo.state = "审批中";
                break;
            case 2:
                tinfo.state = "待接单";
                break;
            case 3:
                tinfo.state = "接单成功";
                break;
            case 4:
                tinfo.state = "已完成";
                break;
        default:
                break;
        }
        tinfo.submitterAss = info.submitter_ass;
        tinfo.submitterAssPhoneNumber = info.phone_number_ass;
        tinfo.image = info.image;
        titleInfo.text = "标题：" + tinfo.title;
    }

    public void onTicketClick()
    {
        eventCenter.PostEvent<ticketInfo>(staticVariable.showTicketInfo, tinfo);
    }

    private void OnDestroy()
    {
        Debug.Log("being destory");
    }


}
