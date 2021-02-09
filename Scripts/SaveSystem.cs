using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{   
    public string MyClass;
    public GameObject obj;
    public int money = 0;
    void Start()
    {
        if (PlayerPrefs.HasKey("Hero_Class"))
        {
            //
        }
        else 
        {
            obj.SetActive(true);
            PlayerPrefs.SetInt("Money", money);
        }
    }
    public void Tank()
    {
        MyClass = "Tank";
        PlayerPrefs.SetString("Hero_Class", MyClass);
    }
    public void Mag()
    {
        MyClass = "Mag";
        PlayerPrefs.SetString("Hero_Class", MyClass);
    }
    public void Voin()
    {
        MyClass = "Voin";
        PlayerPrefs.SetString("Hero_Class", MyClass);
    }
    //private void PPrefs()
    //{
        //#region PlayerPrefs.Set***
        //string saveValueS = "Памагите";
        //PlayerPrefs.SetString("LoL", saveValueS);

        //int saveValueI = 1337;
        //PlayerPrefs.SetInt("Название ключа", saveValueI);

        //float saveValueF = 220.4f;
        //PlayerPrefs.SetFloat("Название ключа", saveValueF);

        //#endregion
        
        //PlayerPrefs.GetString("LoL");
        //string s = PlayerPrefs.GetString("LoL");

        //bool check = PlayerPrefs.HasKey("LoL");

        //if (PlayerPrefs.HasKey("LoL"))
        //{
            //print("Эщкере");
       // }
        
    //}
}
