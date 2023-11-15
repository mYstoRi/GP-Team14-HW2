using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : EntityGeneric
{
    public float SurviveTimer
    {
        get
        {
            return surviveTimer;
        }
        set
        {
            UpdateTimer(value);
        }
    }

    #region VARIABLES
    [SerializeField] PlayerData playerData;
    [SerializeField] float surviveTime = 10;   //survive time in seconds, defult: 1 min 40 sec
    float surviveTimer = 0;
    private Animator anim;
    #endregion

    #region METHODS
    public override void UpdateHealth(float newValue)
    {
        base.UpdateHealth(newValue);
        UIManager.instance.UpdateHpUI(Health, MaxHealth);
    }
    public void UpdateTimer(float newValue)
    {
        if(newValue <= 0)
        {
            newValue = 0;
            if(SurviveTimer > 0)  LevelManager.instance.Invoke(nameof(LevelManager.instance.GameOver), 0.2f);
        }
        surviveTimer = newValue;
        UIManager.instance.UpdateTimerUI(newValue);
    }

    IEnumerator Dying()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        anim.SetBool("dead", false);
        LevelManager.instance.ReloadCurrentLevel();
    }

    public override void Die()
    {
        anim.SetBool("dead", true);
        IsDied = true;
        // restart game in the mean time (async load + fade)
        StartCoroutine(Dying());
    }
    public override void Initialize()
    {
        base.Initialize();
        PlayerData playerData = (PlayerData)data;
        surviveTime = playerData.SurviveTime;
        SurviveTimer = surviveTime;

        anim = GetComponent<Animator>();
        IsDied = false;
    }
    private void Awake() 
    {
        
    }

    private void Start()
    {
        this.Initialize();
    }

    private void Update() 
    {   
        SurviveTimer -= Time.deltaTime;

        #region TEST
        if(!enableTestingHp) return;
        if(test <= 0)
        {
            TakesDamage(5);
            test = 1;
        }
        test -= Time.deltaTime;
        #endregion
    }
    #endregion

    // Test for taking damage , will be deleted later
    float test = 1;
    [SerializeField] bool enableTestingHp = false;

}
