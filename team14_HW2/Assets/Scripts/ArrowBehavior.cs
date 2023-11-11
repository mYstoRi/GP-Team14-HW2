using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    public GameObject target;
    public float attack;

    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) transform.LookAt(target.transform);
        if ((startPosition - transform.position).magnitude > 30) Destroy(gameObject);
    }
}
