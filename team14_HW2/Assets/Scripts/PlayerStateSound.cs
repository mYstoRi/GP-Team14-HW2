using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateSound : MonoBehaviour
{
    public GameObject pwerup;
    public GameObject hurt;
    public ParticleSystem heart;
    public ParticleSystem star;
    public bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Heart")
        {
            Instantiate(pwerup, transform.position, Quaternion.identity);
            var em = heart.emission;
            em.enabled = true;
            heart.Play();
        }
        if(other.gameObject.name == "Arrow")
        {
            Instantiate(hurt, transform.position, Quaternion.identity);
            var em = star.emission;
            em.enabled = true;
            star.Play();
        }
    }
}
