using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    struct LevelInfo
    {
        public float PlayerHp;
    }
    [SerializeField] Image fadeImage;
    LevelInfo levelInfo;
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
    public void LoadLevel(int level)
    {
        if(level < 1 || level > 3) // We only have level 1~3
        {
            Debug.LogError("Level " + level + " does not exist!"); 
            return;
        }

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
        PlayerEntity playerEntity = FindObjectOfType<PlayerEntity>().GetComponent<PlayerEntity>();
        if(!playerEntity)
        {
            Debug.LogError("Cannot Find Player!");
            return;
        }

        levelInfo.PlayerHp = playerEntity.Health;
    }
    
    public void SetUpInNewLevel()
    {
        PlayerEntity playerEntity = FindObjectOfType<PlayerEntity>().GetComponent<PlayerEntity>();
        if(!playerEntity)
        {
            Debug.LogError("Cannot Find Player!");
            return;
        }

        playerEntity.Health = levelInfo.PlayerHp;
    }

    #region COROUTINES
    IEnumerator LoadNewLevelCoroutine(AsyncOperation task)
    {
        yield return StartCoroutine(FadingOutScene(task));
        SetUpInNewLevel();
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
