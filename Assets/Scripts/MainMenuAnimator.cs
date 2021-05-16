using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(Vector3.up * 10 * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
