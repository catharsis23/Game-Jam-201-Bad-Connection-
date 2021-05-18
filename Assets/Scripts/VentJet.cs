using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentJet : MonoBehaviour
{
    public ParticleSystem jetParticle;

    // Start is called before the first frame update
    void Start()
    {
        jetParticle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
