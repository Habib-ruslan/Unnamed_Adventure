using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saver : MonoBehaviour
{
    public int mon;
    public Text M;
    private void Start()
    {
        mon = PlayerPrefs.GetInt("Money");

    }
    private void Update()
    {
        M.text = mon.ToString();
    }
}
