using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionDetector : MonoBehaviour
{
    public GameObject rocket;
    public GameObject connectionDisplay;
    private Color offColor = new Color32(230, 0, 0, 255);
    private Color onColor = new Color32(76, 187, 23, 255);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered Satellite!");
        rocket.GetComponent<PlayerController>().isConnected = true;
        connectionDisplay.GetComponent<Image>().color = onColor;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Left Satellite!");
        rocket.GetComponent<PlayerController>().isConnected = false;
        connectionDisplay.GetComponent<Image>().color = offColor;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Left Satellite!");
        rocket.GetComponent<PlayerController>().isConnected = true;
        connectionDisplay.GetComponent<Image>().color = onColor;

    }
}
