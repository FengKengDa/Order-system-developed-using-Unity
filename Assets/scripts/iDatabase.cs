using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class iDatabase : MonoBehaviour
{
    //��¼�ǳ�
    public static List<string> login(int uid, string password)
    {

        return new List<string>();
    }

    public static List<string> logout()
    {

        return new List<string>();
    }

    //�ύ���� fundlist{ {ԭ�򣬽��}��{ԭ�򣬽��} }  department{ {���ţ�����}��{���ţ�����} }
    public static string submit(string title, string address, string reason, 
        List<List<string>> fundlist, List<string> department)
    {
        
        return "success";
    }

    //��ȡ���еĴ���ticket
    public static List<List<string>> getTicket(int uid)
    {

        return new List<List<string>>() {};
    }

    //��ȡ����
    public static string take_ticket(int uid, int tid, bool isAss)
    {
        


        return "";
    }

    //��ȡ��ǰ���ڽ��еĹ���
    public static List<string> get_current_ticket(int uid)
    {

        return new List<string>();
    }

    //����Э������
    public static string submit_ass(int tid, int uid, List<List<string>> department)
    {

        return "";
    }

    //�ύ����
    public static string finish_ticket(int tid, int uid)
    {


        return "";
    }

    public static List<List<string>> get_history_ticket(int uid)
    {


        return new List<List<string>>() { };
    }

}