using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionDetector : MonoBehaviour
{
    public GameObject rocket;
    public GameObject sensor;
    public GameObject connectionDisplay;
    private Color offColor = new Color32(230, 0, 0, 255);
    private Color onColor = new Color32(76, 187, 23, 255);

    // Start is called before the first frame update
    void Start()
    {

        sensor.GetComponent<SpriteRenderer>().color = onColor;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered Satellite!");
        if (collision.CompareTag("Satellite"))
        {

            rocket.GetComponent<PlayerController>().isConnected = true;
            var sprite = Resources.Load<Sprite>("Signal/onsignal");
            connectionDisplay.GetComponent<Image>().sprite = sprite;
            sensor.GetComponent<SpriteRenderer>().color = onColor;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Satellite"))
        {

            Debug.Log("Left Satellite!");
            rocket.GetComponent<PlayerController>().isConnected = false;
            var sprite = Resources.Load<Sprite>("Signal/offsignal");
            connectionDisplay.GetComponent<Image>().sprite = sprite;
            sensor.GetComponent<SpriteRenderer>().color = offColor;
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Left Satellite!");
        if (collision.CompareTag("Satellite"))
        {
            rocket.GetComponent<PlayerController>().isConnected = true;
            var sprite = Resources.Load<Sprite>("Signal/onsignal");
            connectionDisplay.GetComponent<Image>().sprite = sprite;
            sensor.GetComponent<SpriteRenderer>().color = onColor;
        }

    }
}
