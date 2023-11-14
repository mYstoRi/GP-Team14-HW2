using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    struct LevelInfo
    {
        public bool isInitialized;
        public float PlayerHp;
        public float Timer;
    }
    [SerializeField] Image fadeImage;
    LevelInfo levelInfo = new ();
    bool isLoading = false;
    public static LevelManager instance;
    void Awake() 
    {
        if(instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Delete duplicated LevelManager successfully!");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        levelInfo.isInitialized = false;
        SaveInfo();
    }
    public void ReloadCurrentLevel()
    {
        if(isLoading) return;

        isLoading = true;
        string loadedLevelName = SceneManager.GetActiveScene().name;
        var task = SceneManager.LoadSceneAsync(loadedLevelName, LoadSceneMode.Single);

        task.allowSceneActivation = false;
        StartCoroutine(LoadNewLevelCoroutine(task));
    }
    public void LoadLevel(int level)
    {
        if(level < 1 || level > 3) // We only have level 1~3
        {
            Debug.LogError("Level_ " + level + " does not exist!"); 
            return;
        }
        else if(isLoading) return;

        isLoading = true;
        SaveInfo();
        string loadedLevelName = "Level_" + level;
        var task = SceneManager.LoadSceneAsync(loadedLevelName, LoadSceneMode.Single);

        task.allowSceneActivation = false;
        StartCoroutine(LoadNewLevelCoroutine(task));
    }

    static public void LoadTestLevel() 
    {
        string loadedLevelName = "Scene1";
        var task = SceneManager.LoadSceneAsync(loadedLevelName, LoadSceneMode.Single);

        task.allowSceneActivation = false;
        instance.StartCoroutine(instance.FadingOutScene(task));
    } 

    public void SaveInfo()
    {
        PlayerEntity playerEntity = FindObjectOfType<PlayerEntity>()?.GetComponent<PlayerEntity>();
        if(!playerEntity)
        {
            Debug.LogError("Cannot Find Player!");
            return;
        }
        else levelInfo.isInitialized = true;
        
        levelInfo.PlayerHp = playerEntity.Health;
        levelInfo.Timer = playerEntity.SurviveTimer;
    }
    
    public void SetUpInNewLevel()
    {
        PlayerEntity playerEntity = FindObjectOfType<PlayerEntity>().GetComponent<PlayerEntity>();
        if(!playerEntity)
        {
            Debug.LogError("Cannot Find Player!");
            return;
        }

        if(!levelInfo.isInitialized)
        {
            SaveInfo();
            levelInfo.isInitialized = true;
        }
        else
        {
            playerEntity.Health = levelInfo.PlayerHp;
            playerEntity.SurviveTimer = levelInfo.Timer ;
        }
    }

    #region COROUTINES
    IEnumerator LoadNewLevelCoroutine(AsyncOperation task)
    {
        yield return StartCoroutine(FadingOutScene(task));
        SetUpInNewLevel();
        isLoading = false;
    }
    public IEnumerator FadingOutScene(AsyncOperation task)
    {
        float progress = 0;
        while(progress <= 1)
        {
            fadeImage.GetComponent<Image>().color = new Color(0,0,0,progress);
            progress += Time.deltaTime * 1.5f;
            yield return null;
        }

        fadeImage.GetComponent<Image>().color = new Color(0,0,0,1);
        task.allowSceneActivation = true;
        while(!task.isDone) yield return null; 
        fadeImage.GetComponent<Image>().color = new Color(0,0,0,0);
    }
    public IEnumerator FadingInScene(AsyncOperation task)
    {
        float progress = 0;
        while(progress <= 1)
        {
            fadeImage.GetComponent<Image>().color = new Color(0,0,0,progress);
            progress += Time.deltaTime * 1.5f;
            yield return null;
        }

        fadeImage.GetComponent<Image>().color = new Color(0,0,0,1);
        task.allowSceneActivation = true;
    }
    #endregion
}
