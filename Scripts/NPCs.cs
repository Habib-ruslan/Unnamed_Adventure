using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCs : MonoBehaviour
{
    public GameObject particles;
    public Transform point;
    GameObject DialogWindow;
    GameObject ShopUI;
    string HeroClass;
    void Awake()
    {
        ShopUI = GameObject.Find("Canvas/Shop");
        DialogWindow = GameObject.Find("Canvas/Dialog");
        DialogWindow.SetActive(false);
    }
    void Start()
    {
        HeroClass = PlayerPrefs.GetString("Hero_Class");
    }

    void Update()
    {
        
    }
    public virtual void Particles()
    {
        Instantiate(particles, point.position, Quaternion.identity);
    }
    public virtual void Dialog()
    {
        DialogWindow.SetActive(true);
    }
    public virtual void ExitDialog()
    {
        DialogWindow.SetActive(false);
    }
    public void Shop()
    {
        ShopUI.GetComponent<GUI>().Open();
        DialogWindow.SetActive(false);
    }
    public void ShopExit()
    {
        ShopUI.GetComponent<GUI>().Close(false);
    }
    
}
