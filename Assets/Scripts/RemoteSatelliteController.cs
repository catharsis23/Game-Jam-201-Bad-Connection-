using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteSatelliteController : MonoBehaviour
{
    public GameObject targettingLaser;
    private Rigidbody2D remoteSatelliteRb;
    private CircleCollider2D remoteSatelliteCC2D;

    public GameObject satellite;
    private float maxVelocity = 100;
    public bool isActive;
    public bool isFirst;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       if (!isActive)
        {
            if (transform.childCount > 0)
            {
                GameObject child = transform.GetChild(0).gameObject;
                child.SetActive(false);
            }
        } else if (isActive)
        {
            if (transform.childCount > 0)
            {
                GameObject child = transform.GetChild(0).gameObject;
                child.SetActive(true);
            }
        }
    }

    public void Launch()
    {

        //detach from parent
        transform.parent = null;
   
        //destroy targetting laser
        //Destroy(transform.GetChild(0).gameObject);

        //cannot have RB2D at launch, needs to generate once firing started
        remoteSatelliteRb = transform.gameObject.AddComponent<Rigidbody2D>();
        remoteSatelliteRb.gravityScale = 0;
        remoteSatelliteRb.drag = 0;
        remoteSatelliteRb.angularDrag = 0;
        remoteSatelliteRb.freezeRotation = true;


        //remoteSatelliteCC2D = gameObject.AddComponent<CircleCollider2D>();
       

        Debug.Log("Launching Satellite!");
        ThrustForward(1000);

    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(remoteSatelliteRb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(remoteSatelliteRb.velocity.y, -maxVelocity, maxVelocity);

        remoteSatelliteRb.velocity = new Vector2(x, y);

    }

    private void ThrustForward(float amount)
    {
        Vector2 force = transform.right * amount;
        Debug.Log("Launching with Force: " + force + " at rotation: " + transform.rotation);
        remoteSatelliteRb.AddForce(force);
        //ClampVelocity();
    }

    public void DeploySatellite()
    {
        Debug.Log("Deploying Satellite!");

        GameObject newSatellite = Instantiate(satellite, transform.position, transform.rotation);
        Destroy(gameObject);


        Collider2D myCollider = newSatellite.GetComponent<Collider2D>();
        int numColliders = 10;
        Collider2D[] colliders = new Collider2D[numColliders];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        // Set you filters here according to https://docs.unity3d.com/ScriptReference/ContactFilter2D.html
        int colliderCount = myCollider.OverlapCollider(contactFilter, colliders);

        //Debug.Log("Colliders Tocuhing: " + colliderCount);

        GameObject.Find("GameManager").GetComponent<GameManager>().remainingRemoteSatellites--;

        GameObject.Find("RemoteSatelliteManager").GetComponent<RemoteSatelliteManager>().ActivateFirstSatellite();


    }


}
