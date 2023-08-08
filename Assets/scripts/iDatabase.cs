using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class iDatabase : MonoBehaviour
{
    //登录登出
    public static List<string> login(int uid, string password)
    {

        return new List<string>();
    }

    public static List<string> logout()
    {

        return new List<string>();
    }

    //提交工单 fundlist{ {原因，金额}，{原因，金额} }  department{ {部门，人数}，{部门，人数} }
    public static string submit(string title, string address, string reason, 
        List<List<string>> fundlist, List<string> department)
    {
        
        return "success";
    }

    //获取所有的代接ticket
    public static List<List<string>> getTicket(int uid)
    {

        return new List<List<string>>() {};
    }

    //接取工单
    public static string take_ticket(int uid, int tid, bool isAss)
    {
        


        return "";
    }

    //获取当前正在进行的工单
    public static List<string> get_current_ticket(int uid)
    {

        return new List<string>();
    }

    //发布协助工单
    public static string submit_ass(int tid, int uid, List<List<string>> department)
    {

        return "";
    }

    //提交工单
    public static string finish_ticket(int tid, int uid)
    {


        return "";
    }

    public static List<List<string>> get_history_ticket(int uid)
    {


        return new List<List<string>>() { };
    }

}