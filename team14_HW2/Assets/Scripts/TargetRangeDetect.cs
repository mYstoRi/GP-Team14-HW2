using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRangeDetect : MonoBehaviour
{
    private GameObject target;
    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        player = gameObject.transform.parent.gameObject;
        target = player.GetComponent<PlayerAction>().target;
    }

    private void OnTriggerStay(Collider other)
    {
        if (target != null && target.TryGetComponent(out EnemyEntity targetEE) && targetEE.IsDied)
        {
            player.GetComponent<PlayerAction>().target = null;
            target = null;
        }

        if (target == null || Vector3.Distance(other.transform.position, player.transform.position) < Vector3.Distance(target.transform.position, player.transform.position))
        {
            if ( other.TryGetComponent(out EnemyEntity enemy) && !enemy.IsDied)
            {
                player.GetComponent<PlayerAction>().target = other.gameObject;
                target = other.gameObject;
                print(other.gameObject.name);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(target)) target = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
