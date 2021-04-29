using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Damage : MonoBehaviour
{
    public float count;
    public string mes;
    public string type;
    public Color color;
    private TextMesh text;
    public GameObject Self;

    private void Start()
    {
        text = GetComponent<TextMesh>();
        text.color = color;
        switch(type)
        {
            case "damage": text.text = "-" + count; break;
            case "exp": text.text = count + "exp"; break;
            case "message": text.text = mes; break;
            default: break; 
        }
    }
    private void Dstr() {Destroy(Self);}
}
