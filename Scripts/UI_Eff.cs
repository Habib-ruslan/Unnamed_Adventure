using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Eff : MonoBehaviour
{
    public Image image;
    public float Timer;
    public string type;
    private void Update()
    {
        StartCoroutine(Amount());
    }
    private IEnumerator Amount()
    {
        if (type == "effect") 
        {
            if (image.fillAmount > 0)
                image.fillAmount -= 1/ Timer * Time.deltaTime;
        }
        else
        {
            if (image.fillAmount < 1)
                image.fillAmount += 1/Timer * Time.deltaTime;
        }
        yield return new WaitForSeconds(1f);
    }
}
