using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

        void Start()
    {
        Invoke("StartGame", 5f);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        
    }
}
