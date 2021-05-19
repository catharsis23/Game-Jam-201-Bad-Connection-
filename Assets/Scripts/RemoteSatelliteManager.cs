using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoteSatelliteManager : MonoBehaviour
{
    public int startingNumberOfSatellites;
    public int remainingSatellites;
    private string beaconText = "Signal Beacons Left: ";

    public GameObject remoteSatellite;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        canvas.transform.Find("RemainingSatellites").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = beaconText + startingNumberOfSatellites;

        remainingSatellites = startingNumberOfSatellites;
        GameObject player = GameObject.Find("Player");
        for (int i = 0; i < startingNumberOfSatellites; i++)
        {
            //GameObject startingSatellite = Instantiate(remoteSatellite, 
            //    new Vector2(player.transform.position.x - 1f, player.transform.position.y), player.transform.rotation,
            GameObject startingSatellite = Instantiate(remoteSatellite,
                player.transform, false);

            startingSatellite.transform.position = player.transform.position + new Vector3((-.33f * i), -2, 0); //+ new Vector3(-1f, (-.5f * i) + 1f);

            //startingSatellite.transform.SetParent(player.transform);
            if (i == 0)
            {
                Debug.Log("Activating First Remote");
                startingSatellite.GetComponent<RemoteSatelliteController>().isActive = true;
            }
            else
            {
                Debug.Log("Deactivating " + i + " Remote");
                startingSatellite.GetComponent<RemoteSatelliteController>().isActive = false;
            }
        }
    }

    public void ActivateFirstSatellite()
    {
        GameObject[] satellites = GameObject.FindGameObjectsWithTag("RemoteSatellite");

        Debug.Log("Number of remotes: " + satellites.Length);

        bool isFirstSatelliteActived = false;
        for (int i = 0; i < satellites.Length; i++)
        {

            if (satellites[i].GetComponent<RemoteSatelliteController>().isFirst == true)
            {
                satellites[i].GetComponent<RemoteSatelliteController>().isActive = true;

            }


        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCanvas()
    {
        Debug.Log("Updating Canvas");
        GameObject canvas = GameObject.Find("Canvas");
        canvas.transform.Find("RemainingSatellites").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = beaconText + remainingSatellites;
    }
}
