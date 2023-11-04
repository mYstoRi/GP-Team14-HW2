using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    [SerializeField] private float Potency;
    [SerializeField] private GameObject consumeParticle;

    private GameObject prefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            // particles on trigger
            prefab = Instantiate(consumeParticle);
            prefab.transform.parent = other.transform;
            prefab.transform.localPosition = Vector3.up * 0.5f;
            Destroy(prefab, 2.0f);
            
            // play sound here

            other.gameObject.GetComponent<PlayerEntity>().TakesDamage(-Potency);
            Destroy(gameObject);
        }
    }
}
