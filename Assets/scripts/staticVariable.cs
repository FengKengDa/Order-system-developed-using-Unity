using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staticVariable : MonoBehaviour
{
    public static int uid = -1;
    public static string user_name = "null";
    public static int user_type = -1;
    public static string account = "null";
    public static string token = "null";
    public static string department = "null";

    //屏幕大小
    public static float screen_width = UnityEngine.Screen.width;
    public static float screen_heigh = UnityEngine.Screen.height;
    //注册事件
    public static string screenSizeChanged = "screenSizeChanged";
    public static string setErrorInformation = "setErrorInformation";
    public static string banAllCloseChild = "banAllCloseChild";
    public static string callPaiMeng = "callPaiMeng";
    public static string closePaiMeng = "closePaiMeng";
    public static string showTicketInfo = "showTicetInfo";
    public static string loadTicket = "loadTicket";
    public static string updateTakeTicketButton = "updateTakeTicketButton";
    public static string loadCurrentTicket = "loadCurrentTicket";
    public static string showCurrentTicketInfo = "showCurrentTicketInfo";
    public static string updateSubmitAssTicketButton = "updateSubmitAssTicketButton";
    public static string showHistoryInfo = "showHistoryInfo";
    public static string loadHistoryInfo = "loadHistoryInfo";
    public static string loginSuccess = "loginSuccess";
    //网络注册事件
    public static string login = "login";
    public static string submit = "submit";
    public static string submit2 = "submit2";
    public static string get_current_ticket = "get_current_ticket";
    public static string get_history_ticket = "get_history_ticket";
    public static string upload_image = "upload_image";
    public static string finish_ticket = "finish_ticket";
    public static string loadTicket_second = "loadTicket_second";
    public static string take_ticket = "take_ticket";
    public static string submit_success = "submit_success";
    public static string submit_ass = "submit_ass";
}
