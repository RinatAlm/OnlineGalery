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
    [SerializeField] private int counter = 0;
    public List<int> urlIndexToLoad = new List<int>();

    public GameObject imagePrefab;
    public GameObject loadingScreen;
    public Image fillImage;
    public RectTransform scrollViewContainer;
    private int imagesTotalNum = 66;
    public Scrollbar verticalScrollBar;
    private int offset = 8;
    public bool isRunning = false;
    public Sprite defaultSprite;
    public int startingPoint;
    public int finishingPoint;


    //[Header("Check")]
    //public GameObject checkPanel;
    //public Text isRunningText;
    //public Text urlToLoadText;
    //public Text listenerBarText;
    //public Text counterText;
    //public Text barValueText;

    private void Start()
    {  
        Screen.orientation = ScreenOrientation.Portrait;
        loadingScreen.SetActive(true);
        StartCoroutine(LoadingScreen());
        imagesTotalNum--;
        for (int i = 0; i <= imagesTotalNum; i++)//Createing 66 empty spaces for images in scroll view
        {
            CreateImage();
        }
        for (int i = 0; i < 10; i++)//Loading first 10 images
        {
            urlIndexToLoad.Add(i);
        }

        StartLoadingCoroutine();
    }

    void Update()
    {
        //isRunningText.text = "isRunning : " + isRunning.ToString();
        //urlToLoadText.text = "urlCount : " + urlIndexToLoad.Count.ToString();
        //listenerBarText.text = verticalScrollBar.onValueChanged.GetPersistentMethodName(0).ToString();
        //counterText.text = counter.ToString();
        //barValueText.text = verticalScrollBar.value.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadEntryScene();
        }
    }
    IEnumerator SetImage(string link)
    {

        int num = urlIndexToLoad[0];
        urlIndexToLoad.RemoveAt(0);


        UnityWebRequest request = UnityWebRequestTexture.GetTexture(link);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError)
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

    IEnumerator ImageLoader()
    {
        while (urlIndexToLoad.Count != 0)
        {
            isRunning = true;
            StartCoroutine(SetImage(defaultLink + (urlIndexToLoad[0] + 1).ToString() + ".jpg"));
            yield return new WaitForSeconds(0.5f);
        }
        isRunning = false;
    }

    private void CreateImage()
    {
        GameObject imageGameobject = Instantiate(imagePrefab, scrollViewContainer);
        
        images.Add(imageGameobject.transform.Find("OnlineImage").GetComponent<Image>());
    }

    public void LoadEntryScene()
    {
        SceneManager.LoadScene("EntryScene");
    }

    IEnumerator LoadingScreen()
    {

        while (fillImage.fillAmount != 1)
        {
            fillImage.fillAmount += 0.02f;
            yield return new WaitForSeconds(0.04f);
        }
        loadingScreen.SetActive(false);
    }


    public void ChangeBarValue()
    {

        counter = (int)(imagesTotalNum - (imagesTotalNum * verticalScrollBar.value));
        counter += offset;
        if (counter > imagesTotalNum)
            return;
        if (!urlIndexToLoad.Contains(counter) && images[counter].sprite == defaultSprite)
            urlIndexToLoad.Add(counter);
        StartLoadingCoroutine();
    }

    public void SetStartingPoint()
    {
        startingPoint = (int)(imagesTotalNum - (imagesTotalNum * verticalScrollBar.value));
    }

    public void SetFinishPoint()
    {
        finishingPoint = (int)(imagesTotalNum - (imagesTotalNum * verticalScrollBar.value));
        CalculateMissingSprites();
    }

    private void CalculateMissingSprites()
    {
        int step = finishingPoint - startingPoint;       
        if(step<0)
        {
            (startingPoint, finishingPoint) = (finishingPoint, startingPoint);
        }
        else if (step == 0)
        {
            return;
        }
       
        for(int i = startingPoint;i<finishingPoint;i++)
        {
            if (!urlIndexToLoad.Contains(i) && images[i].sprite == defaultSprite)
                urlIndexToLoad.Add(i);           
        }
        StartLoadingCoroutine();
    }

    public void StartLoadingCoroutine()
    {
        if (!isRunning)
        {
            StartCoroutine(ImageLoader());
        }

    }

    //public void CheckPanel()
    //{
    //    checkPanel.SetActive(!checkPanel.activeSelf);
    //}

}
