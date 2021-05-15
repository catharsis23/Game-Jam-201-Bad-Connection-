using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int remainingRemoteSatellites;
    // Start is called before the first frame update
    void Start()
    {
        remainingRemoteSatellites = GameObject.Find("RemoteSatelliteManager").GetComponent<RemoteSatelliteManager>().startingNumberOfSatellites;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
