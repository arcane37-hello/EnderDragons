using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        
    }

    public void StartScene()
    {
        SceneManager.LoadScene(1);
    }
}
