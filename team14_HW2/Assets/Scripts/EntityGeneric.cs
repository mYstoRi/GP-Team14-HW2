using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGeneric : MonoBehaviour
{
    #region PROPERTIES
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            UpdateHealth(value);
        }        
    }
    public float MaxHealth{ get{ return maxHealth; }}
    #endregion

    #region VARIABLES
    
    public float speed;
    public float attack;
    public float attackSpeed;
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    #endregion

    #region VIRTUAL METHODS
    public virtual void TakesDamage(float damage)
    {
        // the entity takes damage
        Health -= damage;
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
    public virtual void UpdateHealth(float newValue) 
    {
        if(newValue >= maxHealth)
        {
            health = maxHealth;
        }
        else if(newValue > 0)
        {
            health = newValue;
        }
        else if(newValue <= 0)
        {
            health = 0;
            Die();
        }
    }
    #endregion

    #region METHODS
    /* 
    This function is done in the property "Health"

    public bool DeathCheck()
    {
        // detects if an entity would die
        if (Health <= 0) return true;
        return false;
    }
    */

    public void UpdateAnimation()
    {
        // run this or override this for animations
        // including potential health bars
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
