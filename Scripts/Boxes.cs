using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    public GameObject[] loot;
    public float[] ChanceLoot;
    public GameObject self;
    public GameObject particle;
    
    public void DestroyOther()
    {
        float f = 0;
        float r = Random.Range(0, 100f);
        for (int i = 0; i < loot.Length; i++)
        {
            if ((r > f) && (r <= f + ChanceLoot[i]))
            {
                Instantiate(loot[i], transform.position, Quaternion.identity);
                Instantiate(particle, transform.position, Quaternion.identity);
            }
            f+=ChanceLoot[i]; 
        }
        Destroy(self);
    }
}
