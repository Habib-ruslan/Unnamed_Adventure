using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [Header("Объекты")]
    public GameObject[] buttonsWarrior;
    public GameObject[] buttonsWizard;
    public GameObject[] buttonsTank;
    List<GameObject[]> buttons;
    public GameObject[] iconWarrior;
    public GameObject[] iconWizard;
    public GameObject[] iconTank;
    List<GameObject[]> icons;
    public GameObject[] Window;
    public GameObject self;
    [Header("Текст")]
    public Text[] Cost; 
    public Text MoneyText;
    [Header("Другое")]
    public int[] cost;
    public string type;

    string HeroClass;
    Player player;

    int k;
    string UW;
    char[] _UW;

    void Start()
    {
        buttons = new List<GameObject[]>();
        buttons.Add(buttonsWarrior); 
        buttons.Add(buttonsWizard); 
        buttons.Add(buttonsTank);

        icons = new List<GameObject[]>();
        icons.Add(iconWarrior);
        icons.Add(iconWizard);
        icons.Add(iconTank);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        HeroClass = PlayerPrefs.GetString("Hero_Class");
        MoneyText.text = PlayerPrefs.GetInt("Money").ToString();
        switch(HeroClass)
        {
            case "Voin": k = 0; break;
            case "Mag": k=1; break;
            case "Tank": k=2; break;
        }
        Check(false);
        for(int i = 0; i< cost.Length; i++)
        {
            Cost[i].text = cost[i].ToString();
        }
    }

    public virtual void Buy(int num)
    {
        buttons[k][num].SetActive(false);
        int money = PlayerPrefs.GetInt("Money");
        money -= cost[num];
        PlayerPrefs.SetInt("Money", money);
        MoneyText.text = PlayerPrefs.GetInt("Money").ToString();

        string UW = PlayerPrefs.GetString("UnlockWeapons");
        char[] _UW = UW.ToCharArray(0, UW.Length);
        _UW[num+1] = '1';
        UW = new string(_UW);
        PlayerPrefs.SetString("UnlockWeapons", UW);

        //
        print(PlayerPrefs.GetString("UnlockWeapons"));
    }

    public void Open()
    {
        MoneyText.text = PlayerPrefs.GetInt("Money").ToString();
        MoneyText?.gameObject.SetActive(true);
        Check(true);
        Window[k].SetActive(true);
    }
    public void Open(int num)
    {
        MoneyText?.gameObject.SetActive(true);
        Check(true);
        Window[k+num].SetActive(true);
    }
    public void Active()
    {
        self.SetActive(true);
        Open();
    }
    public void Close(bool f)
    {
        MoneyText?.gameObject.SetActive(false);
        Window[k].SetActive(false);
        if(f) self?.SetActive(false);
    }
    public void Equip(int num)
    {
        PlayerPrefs.SetInt("Equip", num);
        player.Equip(num);
        Check(true);
    }
    void Check(bool f)
    {
        UW = PlayerPrefs.GetString("UnlockWeapons");
        _UW = UW.ToCharArray(0, UW.Length); 
        int equip = PlayerPrefs.GetInt("Equip");
        if (type == "Shop")
        {
            for(int i = 0; i<( UW.Length-1); i++)
            {
                if(_UW[i+1] == '1')
                { 
                    buttons[k][i].SetActive(false);
                }
                else if (f)
                {
                    buttons[k][i].SetActive(true); 
                }
            }
        }
        else
        {
            for(int i = 0; i<( UW.Length); i++)
            {
                if(_UW[i] == '0' || equip == i)
                { 
                    buttons[k][i].SetActive(false);
                    if(equip == i)
                        icons[k][i].SetActive(true);
                    else
                        icons[k][i].SetActive(false);
                }
                else if (f)
                {
                    buttons[k][i].SetActive(true); 
                }
            }
        }
    }
}
