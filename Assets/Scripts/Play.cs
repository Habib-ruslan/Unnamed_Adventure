using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void ThePlay()
    {
        SceneManager.LoadScene(1);
    }
    public void Test()
    {
        SceneManager.LoadScene(2);
    }
    public void TestEnd()
    {
        SceneManager.LoadScene(1);
    }
}
