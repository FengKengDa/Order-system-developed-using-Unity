using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class submitPageButton : MonoBehaviour
{
    public InputField title;
    public InputField address;
    public InputField reason;
    public GameObject fundlistContent;
    public GameObject departmentContent;
    public Image image;
    submitData submitdata;

    private void Start()
    {
        eventCenter.AddListener(staticVariable.submit_success, refresh_data);
        eventCenter.AddListener<string>(staticVariable.submit2, seturl);
    }

    public void refresh_data()
    {
        title.text = "";
        address.text = "";
        reason.text = "";
        for(int i=0;i<fundlistContent.transform.childCount;i++)
        {
            Destroy(fundlistContent.transform.GetChild(i).gameObject);
        }
        fundlistContent.GetComponent<RectTransform>().sizeDelta = new Vector2(fundlistContent.GetComponent<RectTransform>().sizeDelta.x, 0);
        for (int i = 0; i < departmentContent.transform.childCount; i++)
        {
            Destroy(departmentContent.transform.GetChild(i).gameObject);
        }
        departmentContent.GetComponent<RectTransform>().sizeDelta = new Vector2(fundlistContent.GetComponent<RectTransform>().sizeDelta.x, 0);
    }
    public void onSubmitButtonClicked()
    {
        //eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "按钮被点击了");
        if (title.text == "")
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "请输入标题");
            return;
        }
        if(address.text == "")
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "请输入案发地址");
            return;
        }
        if (reason.text == "")
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "请输入问题详细描述");
            return;
        }
        if(fundlistContent.transform.childCount==0)
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "请填写预算");
            return;
        }
        if (departmentContent.transform.childCount == 0)
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "请填写部门");
            return;
        }

        List<List<string>> fund = new List<List<string>>();
        for (int i=0;i<fundlistContent.transform.childCount;i++)
        {
            GameObject child = fundlistContent.transform.GetChild(i).gameObject;
            string rea, fun;
            rea = child.transform.GetChild(0).GetChild(0).GetComponent<InputField>().text;
            fun = child.transform.GetChild(1).GetChild(0).GetComponent<InputField>().text;
            if(rea == ""||fun =="")
            {
                eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "请输入完整的预算信息");
                return;
            }
            fund.Add(new List<string>() { rea, fun});
        }

        List<string> depa = new List<string>();
        for (int i = 0; i < fundlistContent.transform.childCount; i++)
        {
            GameObject child = departmentContent.transform.GetChild(i).gameObject;
            string dep;
            dep = child.transform.GetChild(0).GetChild(0).GetComponent<InputField>().text;
            if (dep == "")
            {
                eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "请输入完整的部门信息");
                return;
            }
            depa.Add(dep);
        }
        
        if(image.sprite == null)
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "请上传图片");
            return;
        }
        
        submitdata = new submitData();
        submitdata.title = title.text;
        submitdata.address = address.text;
        submitdata.reason = reason.text;
        submitdata.fund = fund;
        submitdata.depa = depa;

        DateTime dateTime = DateTime.Now;
        string strNowTime = string.Format("{0:D}{1:D}{2:D}{3:D}{4:D}{5:D}", dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        string imageTime = "http://8.134.67.143:7878/static/"+submitdata.title+strNowTime+".jpg";
        submitdata.image = imageTime;
        eventCenter.PostEvent<string>(staticVariable.upload_image, submitdata.title+strNowTime+".jpg");
        eventCenter.PostEvent<submitData>(staticVariable.submit, submitdata);
    }

    public void seturl(string url)
    {
        submitdata.image = url;
        eventCenter.PostEvent(staticVariable.submit, submitdata);
    }

}


public class submitData
{
    public string title;
    public string address;
    public string reason;
    public List<List<string>> fund;
    public List<string> depa;
    public string image;
}