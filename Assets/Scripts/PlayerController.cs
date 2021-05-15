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
        rocketRb.AddForce(transform.up * 10, ForceMode2D.Impulse);
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

            ThrustForward(yAxis);

            Rotate(transform, xAxis * -rotationSpeed);


            //handle launching and deploying satellite prefabs
            if (Input.GetKeyDown(KeyCode.Space) && isRemoteSatelliteLaunched == true)
            {
                activeSatellite.GetComponent<RemoteSatelliteController>().DeploySatellite();
                isRemoteSatelliteLaunched = false;
                //since sensor is always child make sure there are satellites
                if (transform.childCount > 1)
                {
                    activeSatellite = transform.GetChild(1).gameObject;
                    activeSatellite.GetComponent<RemoteSatelliteController>().isActive = true;

                }
            } else if (Input.GetKeyDown(KeyCode.Space) && isRemoteSatelliteLaunched == false)
            {
                if (transform.childCount > 1)
                {
                    activeSatellite = transform.GetChild(1).gameObject;
                    activeSatellite.GetComponent<RemoteSatelliteController>().Launch();
                    isRemoteSatelliteLaunched = true;
                }
                
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
        Vector2 force = transform.up * amount;
        rocketRb.AddForce(force);
        ClampVelocity();
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, amount);
    }

    #endregion


}
