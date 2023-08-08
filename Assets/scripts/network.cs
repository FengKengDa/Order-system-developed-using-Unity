using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.Networking;

public class network : MonoBehaviour
{
    void Start()
    {
        eventCenter.AddListener<string, string>(staticVariable.login, Login);
        eventCenter.AddListener<submitData>(staticVariable.submit, Submit);
        eventCenter.AddListener(staticVariable.get_current_ticket, get_current_ticket);
        eventCenter.AddListener(staticVariable.get_history_ticket, get_history_ticket);
        eventCenter.AddListener<int>(staticVariable.finish_ticket, finish_ticket);
        eventCenter.AddListener(staticVariable.loadTicket, load_ticket);
        eventCenter.AddListener<int, bool>(staticVariable.take_ticket, take_ticket);
        eventCenter.AddListener<List<List<string>>, int>(staticVariable.submit_ass, submit_ass);
    }

    /// <summary>
    /// 登录：回调函数+协程+数据存储类
    /// </summary>
    /// <param name="account"></param>
    /// <param name="password"></param>
    public void Login(string account, string password)
    {
        StartCoroutine(ILogin(account, password));
    }
    IEnumerator ILogin(string account, string password)
    {
        eventCenter.PostEvent(staticVariable.callPaiMeng);
        JsonData data = new JsonData();
        data["account"] = account;
        data["password"] = password;
        byte[] postBytes = System.Text.Encoding.Default.GetBytes(data.ToJson());
        using (UnityWebRequest request = new UnityWebRequest("url", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(postBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            eventCenter.PostEvent(staticVariable.closePaiMeng);
            Debug.Log("Status Code: " + request.responseCode);
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                LoginRoot r = JsonMapper.ToObject<LoginRoot>(@request.downloadHandler.text);
                Debug.Log(request.downloadHandler.text);
                if(r.data.error != null)
                {
                    eventCenter.PostEvent<string>(staticVariable.setErrorInformation, r.data.error);
                }
                else
                {
                    //登录成功
                    if(r.data.account_type == 2)
                    {
                        staticVariable.user_type = 1;
                    }
                    else
                    {
                        staticVariable.user_type = 2;
                    }

                    staticVariable.uid = r.data.id;
                    staticVariable.user_name = r.data.username;
                    staticVariable.account = account;
                    staticVariable.token = r.data.token;
                    
                    eventCenter.PostEvent(staticVariable.loginSuccess);
                }
            }
        }
        
    }

    public class LoginData
    {
        public int id { get; set; }
        public string username { get; set; }
        public int account_type { get; set; }
        public string token { get; set; }
        public string error { get; set; }
    }

    public class LoginRoot
    {
        public int code { get; set; }
        public bool success { get; set; }
        public LoginData data { get; set; }
    }

    /// <summary>
    /// 提交工单：回调函数+协程+数据存储类
    /// </summary>
    public void Submit(submitData data)
    {
        StartCoroutine(ISubmit(data));
    }

    IEnumerator ISubmit(submitData subdata)
    {
        eventCenter.PostEvent(staticVariable.callPaiMeng);
        SubmitRoot root = new SubmitRoot();
        root.address = subdata.address;
        root.reason = subdata.reason;
        root.departments = subdata.depa;
        root.title = subdata.title;
        root.funds = new List<FundsItem>();
        root.image = subdata.image;
        for(int i=0;i<subdata.fund.Count;i++)
        {
            FundsItem t = new FundsItem();
            t.amount = int.Parse(subdata.fund[i][1]);
            t.reason = subdata.fund[i][0];
            root.funds.Add(t);
        }
        string data = JsonMapper.ToJson(root);
        Debug.Log(data);
        byte[] postBytes = System.Text.Encoding.Default.GetBytes(data);
        Debug.Log(postBytes.ToString());
        using (UnityWebRequest request = new UnityWebRequest("url", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(postBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer "+staticVariable.token);
            yield return request.SendWebRequest();

            eventCenter.PostEvent(staticVariable.closePaiMeng);
            Debug.Log("Status Code: " + request.responseCode);
            Debug.Log("ERROR: " + request.error);
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "提交成功");
                eventCenter.PostEvent(staticVariable.submit_success);
            }
        }
    }


    public class FundsItem
    {
        public string reason { get; set; }
        public int amount { get; set; }
    }

    public class SubmitRoot
    {
        public string title { get; set; }
        public string address { get; set; }
        public string reason { get; set; }
        public List<FundsItem> funds { get; set; }
        public List<string> departments { get; set; }
        public string image { get; set; }
    }

    /// <summary>
    /// 获取当前接取的工单
    /// </summary>
    public void get_current_ticket()
    {
        StartCoroutine(IGet_current_ticket());
    }
    IEnumerator IGet_current_ticket()
    {
        eventCenter.PostEvent(staticVariable.callPaiMeng);
        using (UnityWebRequest request = new UnityWebRequest("url", "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + staticVariable.token);
            yield return request.SendWebRequest();

            eventCenter.PostEvent(staticVariable.closePaiMeng);
            Debug.Log("Status Code: " + request.responseCode);
            Debug.Log("ERROR: " + request.error);
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                currentTicketRoot r = JsonMapper.ToObject<currentTicketRoot>(@request.downloadHandler.text);
                eventCenter.PostEvent<currentTicketRoot>(staticVariable.loadCurrentTicket, r);
                
            }
        }
    }
    public class currentTicketRoot
    {
        public int ticket_id { get; set; }
        public string title { get; set; }
        public string submitter { get; set; }
        public string phone_nubmer { get; set; }
        public string submitter_ass { get; set; }
        public string phone_number_ass { get; set; }
        public List<string> participants { get; set; }
        public string reason { get; set; }
        public List<string> departments { get; set; }
        public int state { get; set; }
        public string manager_id { get; set; }
    }

    /// <summary>
    /// 获取历史工单
    /// </summary>
    public void get_history_ticket()
    {
        StartCoroutine(IGet_history_ticket());
    }
    IEnumerator IGet_history_ticket()
    {
        eventCenter.PostEvent(staticVariable.callPaiMeng);
        using (UnityWebRequest request = new UnityWebRequest("url", "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + staticVariable.token);
            yield return request.SendWebRequest();

            eventCenter.PostEvent(staticVariable.closePaiMeng);
            Debug.Log("Status Code: " + request.responseCode);
            Debug.Log("ERROR: " + request.error);
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                historyRoot r = JsonMapper.ToObject<historyRoot>(@request.downloadHandler.text);
                eventCenter.PostEvent<historyRoot>(staticVariable.loadHistoryInfo, r);

            }
        }
    }

    public class TicketsItem
    {
        public int ticket_id { get; set; }
        public string title { get; set; }
        public string submitter { get; set; }
        public string phone_number { get; set; }
        public string reason { get; set; }
        public List<string> departments { get; set; }
        public int state { get; set; }
        public string submitted_time { get; set; }
        public string submitter_ass { get; set; }
        public string phone_number_ass { get; set; }
        public string image_path { get; set; }
        public int cost { get; set; }
        public List<string> participants { get; set; }
        public List<string> approval_info { get; set; }
    }

    public class historyRoot
    {
        public List<TicketsItem> tickets { get; set; }
    }


    public void load_ticket()
    {
        StartCoroutine(ILoad_ticket());
    }
    IEnumerator ILoad_ticket()
    {
        eventCenter.PostEvent(staticVariable.callPaiMeng);
        using (UnityWebRequest request = new UnityWebRequest("url", "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + staticVariable.token);
            yield return request.SendWebRequest();

            eventCenter.PostEvent(staticVariable.closePaiMeng);
            Debug.Log("Status Code: " + request.responseCode);
            Debug.Log("ERROR: " + request.error);
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                waitTicketRoot r = JsonMapper.ToObject<waitTicketRoot>(@request.downloadHandler.text);
                eventCenter.PostEvent<waitTicketRoot>(staticVariable.loadTicket_second, r);
            }
        }
    }
    public class waitTicketsItem
    {
        public int ticket_id { get; set; }
        public string title { get; set; }
        public string submitter { get; set; }
        public string phone_nubmer { get; set; }
        public string submitter_ass { get; set; }
        public string phone_number_ass { get; set; }
        public List<string> participants { get; set; }
        public string reason { get; set; }
        public List<string> departments { get; set; }
        public int state { get; set; }
        public string manager_id { get; set; }
        public string image { get; set; }
    }
    public class waitTicketRoot
    {
        public List<waitTicketsItem> tickets { get; set; }
    }


    /// <summary>
    /// 完成工单
    /// </summary>
    /// <param name="id"></param>
    public void finish_ticket(int id)
    {
        StartCoroutine(IFinish_ticket(id));
    }
    IEnumerator IFinish_ticket(int id)
    {
        eventCenter.PostEvent(staticVariable.callPaiMeng);
        finishRoot root = new finishRoot();
        root.ticket_id = id;
        string data = JsonMapper.ToJson(root);
        byte[] postBytes = System.Text.Encoding.Default.GetBytes(data);
        Debug.Log(postBytes.ToString());
        using (UnityWebRequest request = new UnityWebRequest("url", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(postBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + staticVariable.token);
            yield return request.SendWebRequest();

            eventCenter.PostEvent(staticVariable.closePaiMeng);
            Debug.Log("Status Code: " + request.responseCode);
            Debug.Log("ERROR: " + request.error);
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
        eventCenter.PostEvent<int>(staticVariable.banAllCloseChild, 1);
    }
    public class finishRoot
    {
        public int ticket_id { get; set; }
    }


    public void take_ticket(int tid, bool isAss)
    {
        StartCoroutine(ITake_ticket(tid, isAss));
    }
    IEnumerator ITake_ticket(int tid, bool isAss)
    {
        eventCenter.PostEvent(staticVariable.callPaiMeng);
        takeRoot rootass = new takeRoot();
        rootass.tid = tid;
        rootass.is_assist = isAss;
        Debug.Log("tid: " + tid);

        Debug.Log("isAss: " + isAss);
        string data = JsonMapper.ToJson(rootass);
        byte[] postBytes = System.Text.Encoding.Default.GetBytes(data);
        Debug.Log(postBytes.ToString());
        using (UnityWebRequest request = new UnityWebRequest("url", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(postBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + staticVariable.token);
            yield return request.SendWebRequest();

            eventCenter.PostEvent(staticVariable.closePaiMeng);
            Debug.Log("Status Code: " + request.responseCode);
            Debug.Log("ERROR: " + request.error);
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
        eventCenter.PostEvent<int>(staticVariable.banAllCloseChild, 2);
    }
    public class takeRoot
    {
        public int tid { get; set; }
        public bool is_assist { get; set; }
    }


    public void submit_ass(List<List<string>> l, int tid)
    {
        StartCoroutine(ISubmit_ass(l,tid));
    }
    IEnumerator ISubmit_ass(List<List<string>> l, int tid)
    {
        eventCenter.PostEvent(staticVariable.callPaiMeng);
        submitAssRoot r = new submitAssRoot();
        r.ticket_id = tid;
        r.requirements = new List<RequirementsItem>();
        for(int i=0;i<l.Count;i++)
        {
            RequirementsItem t = new RequirementsItem();
            t.department_name = l[i][0];
            t.total_num = int.Parse(l[i][1]);
            r.requirements.Add(t);
        }
        string data = JsonMapper.ToJson(r);
        byte[] postBytes = System.Text.Encoding.Default.GetBytes(data);
        Debug.Log(postBytes.ToString());
        using (UnityWebRequest request = new UnityWebRequest("url", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(postBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + staticVariable.token);
            yield return request.SendWebRequest();

            eventCenter.PostEvent(staticVariable.closePaiMeng);
            Debug.Log("Status Code: " + request.responseCode);
            Debug.Log("ERROR: " + request.error);
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
    public class RequirementsItem
    {
        public string department_name { get; set; }
        public int total_num { get; set; }
    }

    public class submitAssRoot
    {
        public int ticket_id { get; set; }
        public List<RequirementsItem> requirements { get; set; }
    }


}
