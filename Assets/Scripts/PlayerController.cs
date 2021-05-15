using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rocketRb;
    //  public GameObject ground;
    private float groundPosition;
    private float maxVelocity = 5;
    private float rotationSpeed = .5f;

    public bool isConnected = false;
    public bool isRemoteSatelliteLaunched = false;

    private GameObject activeSatellite;

    #region Monobehavior API

    // Start is called before the first frame update
    void Start()
    {
        rocketRb = GetComponent<Rigidbody2D>();
        rocketRb.AddForce(Vector3.down * 10, ForceMode2D.Impulse);
        isRemoteSatelliteLaunched = false;

    }

    // Update is called once per frame
    void Update()
    {
        float height = rocketRb.transform.position.y - groundPosition;

        if (isConnected)
        {

            //handle controls
            float yAxis = Input.GetAxis("Vertical");
            float xAxis = Input.GetAxis("Horizontal");

            ThrustForward(xAxis);
            ThrustUp(yAxis);

            //Rotate(transform, xAxis * rotationSpeed);


            //handle launching and deploying satellite prefabs
            if (Input.GetKeyDown(KeyCode.Space) && isRemoteSatelliteLaunched == false)
            {
                if (transform.childCount > 1)
                {
                    activeSatellite = transform.GetChild(1).gameObject;
                    activeSatellite.GetComponent<RemoteSatelliteController>().Launch();
                    isRemoteSatelliteLaunched = true;

                    realignRemoteSatellites();
                }

            }
            else if (Input.GetKeyDown(KeyCode.Space) && isRemoteSatelliteLaunched == true)
            {
                activeSatellite.GetComponent<RemoteSatelliteController>().DeploySatellite();
                isRemoteSatelliteLaunched = false;



            }


        }
    }


    #endregion

    #region Maneuvering API

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rocketRb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rocketRb.velocity.y, -maxVelocity, maxVelocity);

        rocketRb.velocity = new Vector2(x, y);

    }

    private void ThrustForward(float amount)
    {
        Vector2 force = transform.right * amount;
        rocketRb.AddForce(force);
        ClampVelocity();
    }

    private void ThrustUp(float amount)
    {
        Vector2 force = transform.up * amount;
        rocketRb.AddForce(force);
        ClampVelocity();
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, amount);
    }

    #endregion

    #region ammunition API

    private void realignRemoteSatellites()
    {
        GameObject[] satellites = GameObject.FindGameObjectsWithTag("Satellite");

        Debug.Log("Number of remotes: " + satellites.Length);

        bool isFirstSatelliteActived = false;
        for (int i = 0; i < satellites.Length; i++)
        {
            if (!satellites[i].GetComponent<RemoteSatelliteController>().isActive)
            {
                satellites[i].transform.Translate(Vector3.up * .33f);
                if (!isFirstSatelliteActived)
                {
                    Debug.Log("Activating Next Satellite");
                    satellites[i].GetComponent<RemoteSatelliteController>().isActive = true;
                    isFirstSatelliteActived = true;
                }
            }
        }
    }

    #endregion




}
