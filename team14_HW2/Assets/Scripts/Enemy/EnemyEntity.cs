using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : EntityGeneric
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerProjectile") 
        { 
            TakesDamage(other.gameObject.GetComponent<ArrowBehavior>().attack);
            Destroy(other.gameObject);
            // play bullet hit sound here
        }
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
