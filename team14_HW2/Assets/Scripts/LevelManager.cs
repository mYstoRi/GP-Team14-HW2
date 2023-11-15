using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    
    public PlayerEntity Player { get { return FindObjectOfType<PlayerEntity>()?.GetComponent<PlayerEntity>(); } }
    public int KillsCountToNextLevel = 10; 
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
        SavePlayerInfo();
    }
    
    public void SavePlayerInfo()
    {
        if(!Player)
        {
            Debug.Log("Cannot Find Player!");
            return;
        }
        else levelInfo.isInitialized = true;
        
        levelInfo.PlayerHp = Player.Health;
        levelInfo.Timer = Player.SurviveTimer;
    }
    void SaveTimerInfo()
    {
        if(!Player)
        {
            Debug.Log("Cannot Find Player!");
            return;
        }
        
        levelInfo.Timer = Player.SurviveTimer;
    }
    public void SetUpInNewLevel()
    {
        if(!Player)
        {
            Debug.Log("Cannot Find Player!");
            return;
        }

        if(!levelInfo.isInitialized)
        {
            SavePlayerInfo();
            levelInfo.isInitialized = true;
        }
        else
        {
            Player.Health = levelInfo.PlayerHp;
            Player.SurviveTimer = levelInfo.Timer ;
        }
    }
    #region #LOAD_SCENE_METHODS
    public void GameOver()
    {
        Debug.Log("Gameover");
        levelInfo.isInitialized = false;
        LoadScene("GameOver", 0.1f);
    }
    public void Win()
    {
        levelInfo.isInitialized = false;
        LoadScene("Win", 0.5f);
    }
    public void ReloadCurrentLevel()
    {
        if(isLoading) return;

        SaveTimerInfo();

        string loadedLevelName = SceneManager.GetActiveScene().name;
        LoadScene(loadedLevelName, 0.5f);
    }
    public void LoadNextLevel()
    {
        string levelSceneName = SceneManager.GetActiveScene().name;

        if(levelSceneName == "Level_" + 1)
        {
            LoadLevel(2);
        }
        else if(levelSceneName == "Level_" + 2)
        {
            LoadLevel(3);
        }
        else if(levelSceneName == "Level_" + 3)
        {
            Win();
        }
    }
    static public void LoadMainMenu()
    {
        LoadScene("MainMenu", 0.2f);
    }
    static public void LoadLevel(int level)
    {
        if(level < 1 || level > 3) // We only have level 1~3
        {
            Debug.LogError("Level_ " + level + " does not exist!"); 
            return;
        }
        else if(instance.isLoading) return;

        instance.SavePlayerInfo();

        string loadedLevelName = "Level_" + level;
        LoadScene(loadedLevelName, 0.5f);
    }

    static public void LoadScene(string sceneName, float delay) 
    {
        if(instance.isLoading) return;

        instance.isLoading = true;
        var task = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        task.allowSceneActivation = false;
        instance.StartCoroutine(instance.LoadNewLevelCoroutine(task, delay));
    } 
    #endregion

    #region COROUTINES
    IEnumerator LoadNewLevelCoroutine(AsyncOperation task, float delay)
    {
        yield return new WaitForSeconds(delay);

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
