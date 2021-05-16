using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int remainingRemoteSatellites;
    public bool isOutOfBounds;
    public bool isStranded;
    public GameObject outOfBoundsText;
    public GameObject strandedText;
    public GameObject winText;
    public GameObject failMenuButtons;
    public GameObject successMenuButtons;

    public bool gameOver;
    public bool win;
    // Start is called before the first frame update
    void Start()
    {
        remainingRemoteSatellites = GameObject.Find("RemoteSatelliteManager").GetComponent<RemoteSatelliteManager>().startingNumberOfSatellites;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (gameOver && !win)
        {
            failMenuButtons.SetActive(true);
        }

        if (win)
        {
            winText.SetActive(true);
            successMenuButtons.SetActive(true);
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GameObject.Find("Player").GetComponent<PlayerController>().isConnected = false;


        }

        if (isOutOfBounds && !win)
        {
            Debug.Log("Lost to the Depths!");
            outOfBoundsText.SetActive(true);
            gameOver = true;

        }

        if (isStranded && !win)
        {
            Debug.Log("Stranded in the Depths!");
            strandedText.SetActive(true);
            gameOver = true;

        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game!");
        Application.Quit();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
