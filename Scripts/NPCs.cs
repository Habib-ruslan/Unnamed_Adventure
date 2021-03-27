using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCs : MonoBehaviour
{
    public GameObject particles;
    public Transform point;
    private GameObject dialogWindow;
    private GameObject ShopUI;
    private Text dialogText;
    private string HeroClass;
    public string[] text;
    public string[] bye;


    private void Awake()
    {
        ShopUI = GameObject.Find("Canvas/Shop");
        dialogWindow = GameObject.Find("Canvas/Dialog");
        dialogText = dialogWindow.GetComponentInChildren<Text>();
        dialogText.text = text[0];
        dialogWindow.SetActive(false);
    }
    private void Start()
    {
        HeroClass = PlayerPrefs.GetString("Hero_Class");
    }

    private void Update()
    {
        
    }
    protected virtual void Particles()
    {
        Instantiate(particles, point.position, Quaternion.identity);
    }
    public virtual void Dialog()
    {
        dialogWindow.SetActive(true);
    }
    public virtual void ExitDialog()
    {
        dialogWindow.SetActive(false);
    }
    public void Shop()
    {
        ShopUI.GetComponent<GUI>().Open();
        dialogWindow.SetActive(false);
    }
    public void ShopExit()
    {
        ShopUI.GetComponent<GUI>().Close(false);
    }
    public void Bye()
    {
        int n = Random.Range(0,4);
        dialogText.text = bye[n];
    }
}
