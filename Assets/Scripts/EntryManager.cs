using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EntryManager : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("GaleryScene");
    }
}
