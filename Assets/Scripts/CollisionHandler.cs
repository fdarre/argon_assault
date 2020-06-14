using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [Tooltip("in seconds")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("Fx prefab on player")] [SerializeField] GameObject deathFx;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        deathFx.SetActive(true);
        Invoke("ReloadScene", 1f);
    }

    //called by string reference
    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
