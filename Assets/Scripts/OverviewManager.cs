using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OverviewManager : MonoBehaviour
{
    public Image imageToShow;
    private void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        imageToShow.sprite = ImagePasser.instance.spriteToShow;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Return();
        }
    }
    public void Return()
    {
        SceneManager.LoadScene("GaleryScene");
    }
}
