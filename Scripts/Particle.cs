using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float deathTime;
    public GameObject self;
    private void Start()
    {
        Destroy(self, deathTime);
    }
}
