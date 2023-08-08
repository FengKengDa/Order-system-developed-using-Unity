using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using UnityEngine.Networking;

public class outsideListener : MonoBehaviour
{
    public GameObject banAll;

    public GameObject error_page;
    public Text error_text;

    public GameObject paimeng;

    public GameObject ticket_info_page;
    public GameObject ticket_info_content;
    public GameObject take_ticket_button;

    public GameObject current_ticket_page;
    public GameObject current_ticket_content;
    public GameObject submit_ticket_ass_button;
    public GameObject finish_button;

    public GameObject history_info_page;
    public GameObject history_info_content;

    public GameObject login_page;

    public GameObject user_page;

    public GameObject user_info_name;
    public GameObject user_info_id;
    public GameObject user_info_account;
    public GameObject user_info_page;

    public void hideAll()
    {
        error_page.SetActive(false);
        paimeng.SetActive(false);
        ticket_info_page.SetActive(false);
        current_ticket_page.SetActive(false);
        history_info_page.SetActive(false);
    }

    public void showInfo(string s)
    {
        hideAll();
        banAll.SetActive(true);
        error_page.SetActive(true);
        error_text.text = s;
    }

    public void waitPaimeng()
    {
        hideAll();
        banAll.SetActive(true);
        paimeng.SetActive(true);
    }

    public void closePaimeng()
    {
        banAll.SetActive(false);
        paimeng.SetActive(false);
    }

    public void showTicketInfo(ticketInfo info)
    {
        hideAll();
        banAll.SetActive(true);
        ticket_info_page.SetActive(true);
        ticket_info_content.transform.GetChild(0).GetComponent<Text>().text = info.title;
        ticket_info_content.transform.GetChild(1).GetComponent<Text>().text = "提交者：" + info.submitter;
        ticket_info_content.transform.GetChild(2).GetComponent<Text>().text = "联系方式：" + info.phoneNumber;
        ticket_info_content.transform.GetChild(3).GetComponent<Text>().text = "具体情况：" + info.reason;
        ticket_info_content.transform.GetChild(4).GetComponent<Text>().text = "部门：" + info.department;
        ticket_info_content.transform.GetChild(5).GetComponent<Text>().text = "状态：" + info.state;
        ticket_info_content.transform.GetChild(6).GetComponent<Text>().text = "协助工单发布者：" + info.submitterAss;
        ticket_info_content.transform.GetChild(7).GetComponent<Text>().text = "联系方式：" + info.submitterAssPhoneNumber;
        //TODO: 加载图片
        StartCoroutine(LoadImage(info.image));
        //StartCoroutine(LoadImage("http://8.134.67.143:7878/static/image1.jpeg"));
        //更新接单按钮信息
        Debug.Log("info.id: " + info.id);
        take_ticket_button.GetComponent<takeTicket>().set_id_isAss(info.id, info.submitterAss != null);
        //eventCenter.PostEvent<int, bool>(staticVariable.updateTakeTicketButton, info.id, info.submitterAss == null);
    }
    IEnumerator LoadImage(string url)
    {
        UnityWebRequest wr = new UnityWebRequest(url);
        DownloadHandlerTexture texD1 = new DownloadHandlerTexture(true);
        wr.downloadHandler = texD1;
        yield return wr.SendWebRequest();
        int width = 1920;
        int high = 1080;
        if (!wr.isNetworkError)
        {
            Texture2D tex = new Texture2D(width, high);
            tex = texD1.texture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            ticket_info_content.transform.GetChild(8).GetComponent<Image>().sprite = sprite;
        }
    }




    public void loadInfo(network.currentTicketRoot detailInfo)
    {
        if (staticVariable.uid == -1)
        {
            return;
        }
        if (staticVariable.user_type != 1)
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "您非维修人员，无正在进行工单");
            return;
        }
        if(detailInfo == null)
        {
            return;
        }
        if (detailInfo.title == null) 
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "当前尚未接取工单");
            return;
        }

        banAll.SetActive(true);
        current_ticket_page.SetActive(true);

        current_ticket_content.transform.GetChild(0).GetComponent<Text>().text = detailInfo.title;
        current_ticket_content.transform.GetChild(1).GetComponent<Text>().text = "工单ID：" + detailInfo.ticket_id;
        current_ticket_content.transform.GetChild(2).GetComponent<Text>().text = "提交者：" + detailInfo.submitter;
        current_ticket_content.transform.GetChild(3).GetComponent<Text>().text = "联系方式：" + detailInfo.phone_nubmer;
        current_ticket_content.transform.GetChild(4).GetComponent<Text>().text = "协助工单发布者：" + detailInfo.submitter_ass;
        current_ticket_content.transform.GetChild(5).GetComponent<Text>().text = "联系方式：" + detailInfo.phone_number_ass;
        string participant = "";
        for(int i=0;i<detailInfo.participants.Count;i++)
        {
            participant += detailInfo.participants[i];
            if(i!=detailInfo.participants.Count-1)
            {
                participant += ", ";
            }
        }
        current_ticket_content.transform.GetChild(6).GetComponent<Text>().text = "参与人员：" + participant;
        current_ticket_content.transform.GetChild(7).GetComponent<Text>().text = "具体情况：" + detailInfo.reason;
        string department = "";
        for (int i = 0; i < detailInfo.departments.Count; i++)
        {
            department += detailInfo.departments[i];
            if (i != detailInfo.departments.Count - 1)
            {
                department += ", ";
            }
        }
        current_ticket_content.transform.GetChild(8).GetComponent<Text>().text = "部门：" + department;
        string state = "";
        switch(detailInfo.state)
        {
            case 0:
                state = "未审批";
                break;
            case 1:
                state = "审批中";
                break;
            case 2:
                state = "待接单";
                break;
            case 3:
                state = "接单成功";
                break;
            case 4:
                state = "已完成";
                break;
            default:
                break;
        }
        current_ticket_content.transform.GetChild(9).GetComponent<Text>().text = "状态：" + state;

        if(false)//(staticVariable.uid.ToString() != detailInfo.manager_id)
        {
            submit_ticket_ass_button.SetActive(false);
            finish_button.SetActive(false);
        }
        else
        {
            submit_ticket_ass_button.SetActive(true);
            finish_button.SetActive(true);
        }

        eventCenter.PostEvent<int>(staticVariable.updateSubmitAssTicketButton, detailInfo.ticket_id);

    }

    public void showHistoryInfo(historyInfo info)
    {
        hideAll();
        banAll.SetActive(true);
        history_info_page.SetActive(true);

        history_info_content.transform.GetChild(0).GetComponent<Text>().text = info.title;
        history_info_content.transform.GetChild(1).GetComponent<Text>().text = "工单ID："+info.id;
        history_info_content.transform.GetChild(2).GetComponent<Text>().text = "提交者："+info.submitter;
        history_info_content.transform.GetChild(3).GetComponent<Text>().text = "联系方式："+info.phone_number;
        history_info_content.transform.GetChild(4).GetComponent<Text>().text = "协助工单发布者："+info.submitter_ass;
        history_info_content.transform.GetChild(5).GetComponent<Text>().text = "联系方式："+info.phone_number_ass;
        history_info_content.transform.GetChild(6).GetComponent<Text>().text = "参与人员："+info.participant;
        history_info_content.transform.GetChild(7).GetComponent<Text>().text = "具体情况："+info.reason;
        history_info_content.transform.GetChild(8).GetComponent<Text>().text = "部门："+info.department;
        history_info_content.transform.GetChild(9).GetComponent<Text>().text = "状态：已完成";
        history_info_content.transform.GetChild(10).GetComponent<Text>().text = "提交时间："+info.submitter_time;
        //history_info_content.transform.GetChild(11).GetComponent<Text>().text = "完成时间："+info.finish_time;
        history_info_content.transform.GetChild(11).GetComponent<Text>().text = "总金额："+info.total_cost;
        history_info_content.transform.GetChild(12).GetComponent<Text>().text = "审批信息："+info.approvalInfo;
    }

    public void closeLoginPage()
    {
        login_page.SetActive(false);
        user_page.SetActive(true);
    }

    public void setUserInfoPage()
    {
        user_info_account.GetComponent<Text>().text = "账号：" + staticVariable.account;
        user_info_id.GetComponent<Text>().text = "ID：" + staticVariable.uid;
        user_info_name.GetComponent<Text>().text = "用户名：" + staticVariable.user_name;
        user_info_page.SetActive(false);
    }


    void Start()
    {
        eventCenter.AddListener<string>(staticVariable.setErrorInformation, showInfo);
        eventCenter.AddListener(staticVariable.callPaiMeng, waitPaimeng);
        eventCenter.AddListener(staticVariable.closePaiMeng, closePaimeng);
        eventCenter.AddListener<ticketInfo>(staticVariable.showTicketInfo, showTicketInfo);
        eventCenter.AddListener<network.currentTicketRoot>(staticVariable.showCurrentTicketInfo, loadInfo);
        eventCenter.AddListener<historyInfo>(staticVariable.showHistoryInfo, showHistoryInfo);
        eventCenter.AddListener(staticVariable.loginSuccess, closeLoginPage);
        eventCenter.AddListener(staticVariable.loginSuccess, setUserInfoPage);
    }
}
