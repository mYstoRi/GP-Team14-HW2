using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject target;
    public GameObject ArrowPrefab;
    public float projectileSpeed;
    public float attackSpeed;

    private GameObject projectile;
    private float attackCD;

    public void Attack()
    {
        if (attackCD <= 0)
        {
            attackCD = 50 / attackSpeed;
            projectile = Instantiate(ArrowPrefab);
            projectile.transform.parent = gameObject.transform;
            projectile.transform.localPosition = Vector3.zero;
            projectile.GetComponent<ArrowBehavior>().target = target;
            projectile.GetComponent<Rigidbody>().velocity = (target.GetComponent<Transform>().position - transform.position).normalized * projectileSpeed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        attackCD = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attackCD > 0) attackCD -= 1;
    }
}
