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
        if (!PlayerPrefs.HasKey("Hero_Class"))
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
}
