using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EntryManager : MonoBehaviour
{

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }
        public void LoadMainScene()
    {
        SceneManager.LoadScene("GaleryScene");
    }

        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        }
    }
