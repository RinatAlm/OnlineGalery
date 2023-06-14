using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ImagePasser : MonoBehaviour
{
    public static ImagePasser instance;
    public Sprite spriteToShow;
    private void Start()
    {
        if(instance==null)
        {
            instance = this;           
        }        
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(instance);
    }
}
