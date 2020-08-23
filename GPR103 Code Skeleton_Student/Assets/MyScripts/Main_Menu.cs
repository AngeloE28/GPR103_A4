using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Loads the easy mode scene
    public void Easy()
    {
        SceneManager.LoadScene(1);  // Easy mode is index 1
    }

    // Loads the easy mode scene
    public void Normal()
    {
        SceneManager.LoadScene(2);  // Normal mode is index 2
    }

    // Loads the easy mode scene
    public void Hard()
    {
        SceneManager.LoadScene(3);  // Hard mode is index 3
    }

    // Quits the build application
    public void QuitGame()
    {
        Application.Quit();
    }
}
