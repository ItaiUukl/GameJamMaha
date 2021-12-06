using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningLevelManager : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.Play("Background");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
    }

    public void LoadMainScene()
    {
        // TODO: fixme
        SceneManager.LoadScene(1);
    }
}
