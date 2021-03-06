using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rocketRb;
    //  public GameObject ground;
    private float groundPosition;
    private float maxVelocity = 10;
    private float rotationSpeed = 75f;

    public bool isConnected = false;
    public bool isRemoteSatelliteLaunched = false;

    private GameObject activeSatellite;
    private bool isStilStranded = false;

    public ParticleSystem bubbleParticles;
    public bool isPaused = false;

    #region Monobehavior API

    // Start is called before the first frame update
    void Start()
    {
        rocketRb = GetComponent<Rigidbody2D>();
        rocketRb.AddForce(Vector3.down * 1000, ForceMode2D.Impulse);
        rocketRb.drag = .7f;

        isRemoteSatelliteLaunched = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isConnected)
        {




            //only rotate while key down, do not move either
            if (Input.GetKey(KeyCode.Space) && isRemoteSatelliteLaunched == false)
            {

              


            }
            else
            {
                //handle controls
                float yAxis = Input.GetAxis("Vertical");
                float xAxis = Input.GetAxis("Horizontal");

                ThrustForward(xAxis);
                ThrustUp(yAxis);

            }

            


        }

    }

    private void Update()
    {
        if((Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.M) || Input.GetKey(KeyCode.P)) && !isPaused)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().DisplayPauseMenu();
        }




        //check to see if velocity is zero, this is a game over condition
        if (!isConnected && rocketRb.velocity == Vector2.zero)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().isStranded = true;


        }

        if (isConnected)
        {




            //only rotate while key down, do not move either
            if (Input.GetKey(KeyCode.Space) && isRemoteSatelliteLaunched == false && DoSatellitesRemain())
            {

                if (activeSatellite == null)
                {
                    Debug.Log("Setting active satellite");
                    for (int i = 1; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).CompareTag("RemoteSatellite"))
                        {
                            if (transform.GetChild(i).gameObject.GetComponent<RemoteSatelliteController>().isActive == true)
                            {
                                activeSatellite = transform.GetChild(i).gameObject;

                            }
                        }
                    }
                }
                rocketRb.velocity = Vector2.zero;
                activeSatellite.transform.parent = null;

                //handle controls while space is down, only rotate satellites
                //both axis are used because player naturally tries to use both to rotate
                float xAxis = Input.GetAxis("Horizontal");
                float yAxis = Input.GetAxis("Vertical");
                Rotate(activeSatellite.transform, -xAxis * rotationSpeed);
                Rotate(activeSatellite.transform, yAxis * rotationSpeed);


            }
            else
            {
                

            }

            //launch and deploy on key up not down
            if (Input.GetKeyUp(KeyCode.Space) && isRemoteSatelliteLaunched == false)
            {

                //activeSatellite = transform.GetChild(1).gameObject;

                if (activeSatellite != null)
                {
                    activeSatellite.GetComponent<RemoteSatelliteController>().Launch(transform.localScale.x);
                    isRemoteSatelliteLaunched = true;

                    RealignRemoteSatellites();
                }


            }
            else if (Input.GetKeyUp(KeyCode.Space) && isRemoteSatelliteLaunched == true)
            {
                if (activeSatellite != null)
                {
                    activeSatellite.GetComponent<RemoteSatelliteController>().DeploySatellite();
                    isRemoteSatelliteLaunched = false;
                    activeSatellite = null;
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


        Vector2 force = transform.right * amount * (rocketRb.mass);
        rocketRb.AddForce(force, ForceMode2D.Impulse);
        if (amount > 0)
        {
            transform.localScale = new Vector3(.5f, .5f, .5f);
        }
        if (amount < 0)
        {
            transform.localScale = new Vector3(-.5f, .5f, .5f);
        }
        ClampVelocity();
    }

    private void ThrustUp(float amount)
    {
        Vector2 force = transform.up * amount * (rocketRb.mass);
        rocketRb.AddForce(force, ForceMode2D.Impulse);

        ClampVelocity();
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, amount * Time.deltaTime);
    }

    #endregion

    #region ammunition API

    private void RealignRemoteSatellites()
    {
        GameObject[] satellites = GameObject.FindGameObjectsWithTag("RemoteSatellite");

        //Debug.Log("Number of remotes: " + satellites.Length);

        bool isFirstSatelliteActived = false;
        for (int i = 0; i < satellites.Length; i++)
        {
            if (!satellites[i].GetComponent<RemoteSatelliteController>().isActive)
            {
                satellites[i].transform.Translate(Vector3.right * .33f);
                if (!isFirstSatelliteActived)
                {
                    //Debug.Log("Activating Next Satellite");
                    satellites[i].GetComponent<RemoteSatelliteController>().isFirst = true;
                    isFirstSatelliteActived = true;
                }
            }
        }
    }

    #endregion

    IEnumerator ValidateVelocityIsZero()
    {
        yield return new WaitForSeconds(.5f);
        if (!isConnected && rocketRb.velocity == Vector2.zero)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().isStranded = true;
        }

    }

    public bool DoSatellitesRemain()
    {
        return GameObject.Find("GameManager").GetComponent<GameManager>().remainingRemoteSatellites > 0;

    }

    #region



    #endregion




}
