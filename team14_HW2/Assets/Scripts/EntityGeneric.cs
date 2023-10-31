using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGeneric : MonoBehaviour
{
    // public stats
    public float health;
    public float maxHealth;
    public float speed;
    public float attack;
    public float attackSpeed;

    public void TakesDamage(float damage)
    {
        // the entity takes damage
        health -= damage;
        if (DeathCheck()) Destroy(gameObject);
        if (health > maxHealth) health = maxHealth;
    }

    public bool DeathCheck()
    {
        // detects if an entity would die
        if (health <= 0) return true;
        return false;
    }

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
}
