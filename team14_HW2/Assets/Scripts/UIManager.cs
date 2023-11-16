using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Image hpBar;
    [SerializeField] Image hpFullBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI killsCountText;
    PlayerEntity playerEntity;
    void Awake() 
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }
    void Start()
    {
        
    }
    void Init()
    {
        playerEntity = FindObjectOfType<PlayerEntity>().GetComponent<PlayerEntity>();
        UpdateHpUI(playerEntity.Health, playerEntity.MaxHealth);
    }

    public void UpdateHpUI(float currentVal, float maxVal)
    {
        float ratio = currentVal / maxVal;
        RectTransform hpFullBar_rect = hpFullBar.GetComponent<RectTransform>();
        RectTransform HpBar_rect = hpBar.GetComponent<RectTransform>();

        HpBar_rect.sizeDelta = new Vector2(hpFullBar_rect.rect.width * ratio, hpFullBar_rect.rect.height);
        hpText.text = currentVal + "/" + maxVal;
    }
    public void UpdateTimerUI(float time)
    {
        timerText.text = "Time: " + ShowNumber((int)(time / 60)) + ":" + ShowNumber((int)(time % 60));
    }
    public void UpdateKillsCountUI(int count)
    {
        killsCountText.text = "Kills: " + count + "/" + LevelManager.instance.KillsCountToNextLevel;
    }
    string ShowNumber(int num)
    {
        if( (int)(num/10) == 0 )
        {
            return "0" + num;
        }
        else return num.ToString();
    }
}
