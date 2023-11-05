using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject target;
    public GameObject ArrowPrefab;
    public float projectileSpeed;
    public float attackCD;

    private GameObject projectile;
    private Animator anim;

    private bool is_airborne;
    private float attackSpeed;

    public void Attack()
    {
        is_airborne = !Physics.Raycast(transform.position, Vector3.down, 0.2f);

        if (attackCD <= 0 && !is_airborne)
        {
            anim.SetBool("shooting", true);
            attackCD = 50 / attackSpeed;
            projectile = Instantiate(ArrowPrefab);
            projectile.transform.parent = null;
            projectile.transform.position = gameObject.transform.position + Vector3.up;
            projectile.GetComponent<ArrowBehavior>().target = target;
            projectile.GetComponent<ArrowBehavior>().attack = gameObject.GetComponent<EntityGeneric>().attack;
            projectile.GetComponent<Rigidbody>().velocity = (target.GetComponent<Transform>().position - Vector3.up - transform.position).normalized * projectileSpeed;
        }
        else if (is_airborne) attackCD = 50 / attackSpeed + 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        attackSpeed = gameObject.GetComponent<EntityGeneric>().attackSpeed;
        attackCD = 0;
        anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("reflexes", attackSpeed * 1.1125f);
        is_airborne = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attackCD > 0) attackCD -= 1;
        if (target != null && (transform.position - target.transform.position).magnitude > 30.0f) target = null;
    }
}
