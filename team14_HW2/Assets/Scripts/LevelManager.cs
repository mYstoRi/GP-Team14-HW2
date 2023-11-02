using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    public static LevelManager instance;
    void Awake() 
    {
        if(instance != null)
        {
            Destroy(gameObject);
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

        string loadedLevelName = "Level_" + level;
        var task = SceneManager.LoadSceneAsync(loadedLevelName, LoadSceneMode.Single);

        task.allowSceneActivation = false;
        StartCoroutine(FadingOutScene(task));
    }

    IEnumerator FadingOutScene(AsyncOperation task)
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
        //while(!task.isDone) yield return null; 
        fadeImage.GetComponent<Image>().color = new Color(0,0,0,0);
    }
    IEnumerator FadingInScene(AsyncOperation task)
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
}
