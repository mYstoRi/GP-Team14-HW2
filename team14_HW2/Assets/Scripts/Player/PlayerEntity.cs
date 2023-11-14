using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : EntityGeneric
{
    public override void UpdateHealth(float newValue)
    {
        base.UpdateHealth(newValue);
        UIManager.instance.UpdateHpUI(Health, MaxHealth);
    }

    IEnumerator Dying()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        anim.SetBool("dead", false);
    }

    public override void Die()
    {
        anim.SetBool("dead", true);
        IsDied = true;
        // restart game in the mean time (async load + fade)
        StartCoroutine(Dying());
    }

    // Test for taking damage , will be deleted later
    [SerializeField] bool enableTestingHp = false;
    float test = 1;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        IsDied = false;
    }

    private void Update() 
    {   
        if(!enableTestingHp) return;
        if(test <= 0)
        {
            TakesDamage(5);
            test = 1;
        }
        test -= Time.deltaTime;
    }
}
