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
    public GameObject pauseMenu;

    public bool gameOver;
    public bool win;
    // Start is called before the first frame update
    void Start()
    {
        remainingRemoteSatellites = GameObject.Find("RemoteSatelliteManager").GetComponent<RemoteSatelliteManager>().startingNumberOfSatellites;
        GameObject.Find("LevelCounter").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Level " + SceneManager.GetActiveScene().buildIndex;

    }

    // Update is called once per frame
    void Update()
    {
        

        if (gameOver && !win)
        {
            failMenuButtons.SetActive(true);
            int childrenCount = failMenuButtons.transform.childCount;
            //Debug.Log("Children: " + childrenCount);
            for (int i = 0; i < childrenCount; i++)
            {
                GameObject child = failMenuButtons.transform.GetChild(i).gameObject;
                child.SetActive(true);
                int childChildrenCount = child.transform.childCount;
                //Debug.Log("Children: " + childrenCount);
                for (int j = 0; j < childChildrenCount; j++)
                {
                    child.transform.GetChild(j).gameObject.SetActive(true);
                }
            }
        }

        if (win)
        {
            winText.SetActive(true);
            successMenuButtons.SetActive(true);
            int childrenCount = successMenuButtons.transform.childCount;
            //Debug.Log("Children: " + childrenCount);
            for (int i = 0; i < childrenCount; i++)
            {
                GameObject child = successMenuButtons.transform.GetChild(i).gameObject;
                child.SetActive(true);
                int childChildrenCount = child.transform.childCount;
                //Debug.Log("Children: " + childrenCount);
                for (int j = 0; j < childChildrenCount; j++)
                {
                    child.transform.GetChild(j).gameObject.SetActive(true);
                }
            }
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
        Time.timeScale = 1;

    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game!");
        SceneManager.LoadScene(0);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        HidePauseMenu();


    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HidePauseMenu()
    {

        Time.timeScale = 1;

        pauseMenu.SetActive(false);
        int childrenCount = pauseMenu.transform.childCount;
        Debug.Log("Children: " + childrenCount);
        for (int i = 0; i < childrenCount; i++)
        {
            GameObject child = pauseMenu.transform.GetChild(i).gameObject;
            child.SetActive(false);
            int childChildrenCount = child.transform.childCount;
            Debug.Log("Children: " + childrenCount);
            for (int j = 0; j < childChildrenCount; j++)
            {
                child.transform.GetChild(j).gameObject.SetActive(false);
            }
        }
    }

    public void DisplayPauseMenu()
    {
        Time.timeScale = 0;

        pauseMenu.SetActive(true);
        int childrenCount = pauseMenu.transform.childCount;
        Debug.Log("Children: " + childrenCount);
        for (int i = 0; i < childrenCount; i++)
        {
            GameObject child = pauseMenu.transform.GetChild(i).gameObject;
            child.SetActive(true);
            int childChildrenCount = child.transform.childCount;
            Debug.Log("Children: " + childrenCount);
            for (int j = 0; j < childChildrenCount; j++)
            {
                child.transform.GetChild(j).gameObject.SetActive(true);
            }
        }
    }
}
