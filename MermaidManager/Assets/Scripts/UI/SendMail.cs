using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class SendMail : MonoBehaviour
{
    public Button MailBtn;
    List<Data_Selected_Option> OptionList = new List<Data_Selected_Option>();

    private void Start()
    {
        MailBtn.onClick.AddListener(OnClickMailBtn);
    }

    void OnClickMailBtn()
    {
        string mailto = "mermaidjy95@naver.com";

        string subject = EscapeURL("덤 제공 보고서 / " + DateTime.Now.ToString("yyyy/MM/dd"));

        string body = EscapeURL
        (
            GetTodayGift()
        );

        Application.OpenURL("mailto:" + mailto + "?subject=" + subject + "&body=" + body);
    }

    string EscapeURL(string url)
    {
        return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
    }

    string GetTodayGift()
    {
        var str = "";
        OptionList.Clear();
        var clientList = DBManager.Instance.ClientList;
        for (int i = 0; i < clientList.Count; i++)
        {
            var history = DBManager.Instance.LoadClientHistory(clientList[i]);
            if (history != null)
            {
                for (int a = 0; a < history.History.Count; a++)
                {
                    if (history.History[a].Date.Month == DateTime.Now.Month)
                    {
                        AddOption(history.History[a].ProductList);
                    }
                }
            }
        }

        int totalCash = 0;
        for (int i = 0; i < OptionList.Count; i++)
        {
            totalCash += OptionList[i].Option.Person_Per_Price * OptionList[i].Count;
        }

        str += DateTime.Now.Month + "월 총 지급 금액은 " + totalCash.ToString("C") + "원 입니다.";
        
        return str;
    }

    void AddOption(List<Data_Selected_Option> datas)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            var target = OptionList.Find(t => t.Option.Id == datas[i].Option.Id && t.Option.Option_Id == datas[i].Option.Option_Id);
            if (target != null)
            {
                target.Count += datas[i].Count;
            }
            else
            {
                var item = new Data_Selected_Option();
                item = datas[i];
                OptionList.Add(item);
            }
        }
    }
}
