using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{

    public float strength = 15;
    public Vector3 direction;
    public bool coRoutineStarted = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(150 * direction, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("RemoteSatellite"))
        {
            float startTime = collision.gameObject.GetComponent<RemoteSatelliteController>().startTime;
            Debug.Log("Time Alive: " + (Time.time - startTime));
            if ((Time.time - startTime) > .15)
            {

                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(strength * direction, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (!coRoutineStarted)
            {
                StartCoroutine(PropelSub(collision));
                coRoutineStarted = true;
            }

        }

        if (collision.gameObject.CompareTag("RemoteSatellite"))
        {
            float startTime = collision.gameObject.GetComponent<RemoteSatelliteController>().startTime;
            Debug.Log("Time Alive: " + (Time.time - startTime));
            if ((Time.time - startTime) > .15)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce((strength / 5) * direction, ForceMode2D.Impulse);

            }
        }
    }

    IEnumerator PropelSub(Collider2D collision)
    {
        yield return new WaitForSeconds(1);
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(strength * direction, ForceMode2D.Impulse);
        coRoutineStarted = false;

    }
}
