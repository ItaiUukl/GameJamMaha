using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitions : MonoBehaviour
{
    enum LevelStates
    {
        Opening,
        Main
    }
    [SerializeField] private GameObject openingScene;
    [SerializeField] private GameObject mainLevel;
    [SerializeField] private List<Animator> elephantAnimators;

    private LevelStates _states;

    private void Start()
    {
        openingScene.SetActive(true);
        mainLevel.SetActive(false);
        _states = LevelStates.Opening;
        AudioManager.Instance.Play("Background");
    }

    private void Update()
    {
        switch (_states)
        {
            case LevelStates.Opening:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }
                else if (Input.anyKeyDown)
                {
                    openingScene.SetActive(false);
                    mainLevel.SetActive(true);
                    _states = LevelStates.Main;
                }
                break;
            case LevelStates.Main:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    elephantAnimators[0].Play("Elephant_out");
                    elephantAnimators[1].Play("Right_out");
                    StartCoroutine(ResetLevel());
                }
                break;
        }
    }

    private IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
