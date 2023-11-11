using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject approachingTarget = null;


    [Header("Gathering Options")]
    public bool enemyGatheringEnabled = false;

    public float enemyGatherDistance = 50f; // how close to another enemy will them start gathering up as a group

    public float enemyGroupDistance = 10f; // how close to another enemy is defined as grouped

    [Header("Behavior2Player Options")]
    // for mages that will try to keep a distance with the player
    // and to deny enemy from bumping into player (over coliding)
    public float closestDistance2Player = 4f;


    [Header("Movement Options")]
    public float turnSpeed = 1f;

    public float moveSpeed = 2f;


    [Header("Approaching Options")]
    public bool angularApproachEnabled = true;

    [Range(Mathf.PI/4f, Mathf.PI/3f)]
    public float approachAngle = 1f;

    [Range(0f, 1f)]
    public float flipApproachAngleChance = .99f;
    public bool approachAngleFliped = false;


    void Update()
    {
        ApproachTarget();
    }

    void FixedUpdate()
    {
        UpdateApproachingTarget();
        if(Random.Range(0f, 1f) > flipApproachAngleChance) approachAngleFliped = !approachAngleFliped;
    }


    // Compare all Player/Enemy's distance to decide a target to approach
    private void UpdateApproachingTarget()
    {
        float closestDistance = float.MaxValue;

        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance >= closestDistance) continue;
            approachingTarget = player;
            closestDistance = distance;
        }

        if(enemyGatheringEnabled)
        {
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if(distance >= closestDistance) continue;
                if(distance <= enemyGroupDistance) continue; // already formed a group
                if(distance >= enemyGatherDistance) continue; // out of gathering radius
                approachingTarget = enemy;
                closestDistance = distance;
            }
        }
    }


    private Vector3 CalculateApproachVector(float __approachAngle)
    {
        // step.1: distance of A, C = L, L*sin(theta)*cos(theta)=movement
        // step.2: (c_y - a_y)/(c_x - a_x)= m_1
        // step.3: l' = L*sin(theta), Per = l'^2/L
        // step.4: k_x = (Per * c_x) + ((L - Per) * a_x); k_y versa
        // step.5: m_2 = -1/m_1 ; if m_1 = 0, another solution
        // step.6: move_x = (movement) / (m_2**2 + 1)^(1/2)
        // step.7: move_y = (movement^2 - move_x^2)^(1/2)
        // step.8: b_x_1 = k_x + move_x ; b_x_2 = k_x - move_x, y versa
        // step.9: (c_x - b_x)(a_y - b_y) > (c_y - b_y)(a_x - b_x) if b_x b_y is true

        if(!approachingTarget) return Vector3.zero;

        Vector3 A = transform.position;
        Vector3 C = approachingTarget.transform.position;
        Vector3 AC = C-A;

        if(__approachAngle == 0f) return AC;

        float per = Mathf.Pow(AC.magnitude*Mathf.Sin(__approachAngle), 2f) / AC.magnitude;
        Vector3 K = new Vector3(C.x + ((AC.magnitude-per)/AC.magnitude)*(A.x-C.x), 0f, C.z + ((AC.magnitude-per)/AC.magnitude)*(A.z-C.z));

        if(AC.z == 0 || AC.x == 0) return Vector3.zero;
        float m_KB = -AC.x/AC.z;
        float L_KB = AC.magnitude*Mathf.Sin(__approachAngle)*Mathf.Cos(__approachAngle);
        Vector3 KB = new Vector3(L_KB/Mathf.Sqrt(Mathf.Pow(m_KB, 2f)+1f), 0f, Mathf.Sqrt(Mathf.Pow(L_KB, 2f) * Mathf.Pow(m_KB, 2f) / (Mathf.Pow(m_KB, 2f)+1)));

        Vector3 B1 = K + KB;
        Vector3 B2 = K - KB;

        if((C.x-B1.x)*(A.z-B1.z) > (C.z-B1.z)*(A.x-B1.x)) return (approachAngleFliped?B2:B1)-A;
        if((C.x-B2.x)*(A.z-B2.z) > (C.z-B2.z)*(A.x-B2.x)) return (approachAngleFliped?B1:B2)-A;
        return Vector3.zero;
    }

    private void ApproachTarget()
    {
        if(!approachingTarget) 
        {
            GetComponent<Animator>().SetBool("isMoving", false);
            return;
        }
        else GetComponent<Animator>().SetBool("isMoving", true);

        Vector3 direction = approachingTarget.transform.position - transform.position;

        float __approachAngle = (approachingTarget.CompareTag("Enemy")||!angularApproachEnabled)?0f:approachAngle;
        Vector3 approachDirection = Vector3.Scale(CalculateApproachVector(__approachAngle), new Vector3(1, 0, 1));
        transform.position = transform.position + approachDirection.normalized*moveSpeed*Time.deltaTime;
        if(Vector3.Distance(transform.position, approachingTarget.transform.position) < closestDistance2Player)
        {
            Vector3 fixingDirection = Vector3.Scale(transform.position-approachingTarget.transform.position, new Vector3(1, 0, 1));
            transform.position = approachingTarget.transform.position + fixingDirection.normalized*closestDistance2Player;
        }

        // rotate to look at the moving direction smoothly
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }
}