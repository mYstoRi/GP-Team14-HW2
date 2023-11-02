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

    // Test for taking damage , will be deleted later
    float test = 1;
    private void Update() 
    {
        if(test <= 0)
        {
            TakesDamage(1);
            test = 1;
        }
        test -= Time.deltaTime;
    }
}