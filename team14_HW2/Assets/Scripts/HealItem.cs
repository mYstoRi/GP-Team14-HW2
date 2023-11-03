using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    [SerializeField] private float Potency;
    // [SerializeField] private ParticleSystem mainParticle; (maybe can add trigger animation later)

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            // particles on trigger
            // mainParticle.Emit(50);
            
            // play sound here

            other.gameObject.GetComponent<PlayerEntity>().TakesDamage(-Potency);
            Destroy(gameObject);
        }
    }
}
