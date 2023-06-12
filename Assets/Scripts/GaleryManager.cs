using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GaleryManager : MonoBehaviour
{
    [SerializeField] private string defaultLink;
    private List<Image> images = new List<Image>();
    [SerializeField] private int counter = 1;

    public GameObject imagePrefab;
    public GameObject loadingScreen;
    public Image fillImage;
    public RectTransform scrollViewContainer;
    public int imagesTotalNum = 66;
    


    private void Start()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadingScreen());
        for(int i =0; i < imagesTotalNum; i++)//Createing 66 empty spaces for images in scroll view
        {
            CreateImage(); 
        }
        for(int i = 0; i < 10; i++)//Loading first 10 images
        {
            StartCoroutine(SetImage(defaultLink + counter.ToString() + ".jpg"));
            counter++;
        }
    }

    IEnumerator SetImage(string link)
    {
        int num = counter - 1;
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(link);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogWarning(request.error);
        }
        else
        {          
            Texture2D mytexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite newSprite = Sprite.Create(mytexture, new Rect(0, 0, mytexture.width, mytexture.height), new Vector2(0.5f, 0.5f));
            images[num].sprite = newSprite;
        }
    }

    private void CreateImage()
    {
        GameObject imageGameobject = Instantiate(imagePrefab,scrollViewContainer);
        images.Add(imageGameobject.transform.Find("OnlineImage").GetComponent<Image>());
       
    }

    public void LoadEntryScene()
    {
        SceneManager.LoadScene("EntryScene");
    }

    IEnumerator LoadingScreen()
    {
        
        while(fillImage.fillAmount!=1)
        {
            
            fillImage.fillAmount += 0.02f;
            yield return new WaitForSeconds(0.04f);
        }
        loadingScreen.SetActive(false);
    }
}
