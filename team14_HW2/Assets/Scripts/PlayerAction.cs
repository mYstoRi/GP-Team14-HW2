using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject target;
    public GameObject ArrowPrefab;
    public float projectileSpeed;
    public float attackSpeed;
    public float attackCD;

    private GameObject projectile;
    private Animator anim;

    public void Attack()
    {
        if (attackCD <= 0)
        {
            anim.SetBool("shooting", true);
            attackCD = 50 / attackSpeed;
            projectile = Instantiate(ArrowPrefab);
            projectile.transform.parent = null;
            projectile.transform.position = gameObject.transform.position + Vector3.up;
            projectile.GetComponent<ArrowBehavior>().target = target;
            projectile.GetComponent<Rigidbody>().velocity = (target.GetComponent<Transform>().position - Vector3.up - transform.position).normalized * projectileSpeed;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        attackCD = 0;
        anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("reflexes", attackSpeed * 1.1125f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attackCD > 0) attackCD -= 1;
    }
}
