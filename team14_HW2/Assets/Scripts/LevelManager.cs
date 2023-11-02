using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
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
        task.allowSceneActivation = true;
    }
}
