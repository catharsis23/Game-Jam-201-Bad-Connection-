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
    public GameObject failMenuButtons;
    public bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        remainingRemoteSatellites = GameObject.Find("RemoteSatelliteManager").GetComponent<RemoteSatelliteManager>().startingNumberOfSatellites;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOutOfBounds)
        {
            Debug.Log("Lost to the Depths!");
            outOfBoundsText.SetActive(true);
            gameOver = true;
            
        }

        if (isStranded)
        {
            Debug.Log("Stranded in the Depths!");
            strandedText.SetActive(true);
            gameOver = true;

        }

        if (gameOver)
        {
            failMenuButtons.SetActive(true);
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
}
