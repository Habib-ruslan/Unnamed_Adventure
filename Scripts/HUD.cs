using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("HP and Money")]
    public Image HP;
    public Text Money;
    public Image Avatar;
    public Sprite[] avatar;

    [Header("Skills")]
    public Image[] Skills;
    public Sprite[] skillsWarrior;
    public Sprite[] skillsWizard;
    public Sprite[] skillsTank;
    List<Sprite[]> skills;
    UI_Eff[] _Skills;

    int k;


    void Awake()
    {
        skills = new List<Sprite[]>();
        skills.Add(skillsWarrior);
        skills.Add(skillsWizard);
        skills.Add(skillsTank);

        switch(PlayerPrefs.GetString("Hero_Class"))
        {
            case "Voin": k=0; break;
            case "Mag": k=1; break;
            case "Tank": k=2; break;
        }
        Avatar.sprite = avatar[k];

        _Skills = new UI_Eff[Skills.Length];
        for (int i=0; i < Skills.Length; i++)
        {
            Skills[i].sprite = skills[k][i];
            _Skills[i] = Skills[i].GetComponentInChildren<UI_Eff>();
            _Skills[i].image.sprite = Skills[i].sprite;
        }

    }
    public void Zero(int num)
    {
        _Skills[num].image.fillAmount = 0;
    }
    public void EditTime(int num, float time)
    {
        _Skills[num].Timer = time;
    }
    public void EditColor(int num, Color col)
    {
        _Skills[num].image.color = col;
    }

}
