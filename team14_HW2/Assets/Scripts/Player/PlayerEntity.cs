using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] float surviveTime = 100;   //survive time in seconds, defult: 1 min 40 sec
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

    private void Start()
    {
        anim = GetComponent<Animator>();
        IsDied = false;
        SurviveTimer = surviveTime;
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
