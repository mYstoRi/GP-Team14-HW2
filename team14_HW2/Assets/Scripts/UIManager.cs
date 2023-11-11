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
        Init();
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
}
