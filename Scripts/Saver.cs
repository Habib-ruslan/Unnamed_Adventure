using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saver : MonoBehaviour
{
    public int mon;
    public Text M;
    void Start()
    {
        mon = PlayerPrefs.GetInt("Money");

    }
    void Update()
    {
        M.text = mon.ToString();
    }
}
