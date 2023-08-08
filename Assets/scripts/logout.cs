using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logout : MonoBehaviour
{
    public GameObject login;
    public GameObject submitPage;
    public GameObject thispage;
    public Text uid;
    public Text account;
    public Text uname;

    public void onclick()
    {
        uid.text = "’À∫≈£∫";
        account.text = "ID£∫";
        uname.text = "”√ªß√˚£∫";
        staticVariable.uid = -1;
        staticVariable.user_name = "";
        staticVariable.account = "";
        staticVariable.token = "";
        login.SetActive(true);
        submitPage.SetActive(true);
        thispage.SetActive(false);
    }

}
