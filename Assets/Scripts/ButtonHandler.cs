using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public Sprite defaultSprite;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PassImage);
    }

    void PassImage()
    {
        Image img = GetComponent<Image>();
        if(!img.sprite.Equals(defaultSprite))
        {
            ImagePasser.instance.spriteToShow = img.sprite;
            SceneManager.LoadScene("OverviewScene");
        }       
    }
}   
    
