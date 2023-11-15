using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : EntityGeneric
{
    private Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerProjectile") 
        { 
            TakesDamage(other.gameObject.GetComponent<ArrowBehavior>().attack);
            Destroy(other.gameObject);
            // play bullet hit sound here
        }
    }
    public override void Die()
    {
        if (anim != null) anim.SetBool("Die", true);
        if (TryGetComponent<EnemyMovement>(out EnemyMovement em)) em.enabled = false;
        IsDied = true;
        LevelManager.instance.Player.KillsCount++;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        TryGetComponent<Animator>(out anim);
        IsDied = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
