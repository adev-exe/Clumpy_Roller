using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
  // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))

       { 
           Debug.Log("exited game");
           Application.Quit();
       }
    }

     public void EasyMode ()
    {
        Debug.Log("Easy");
        SceneManager.LoadScene("ClumpyRollerGameEasy");
    }

    public void HardMode ()
    {
        Debug.Log("Hard");
        SceneManager.LoadScene("ClumpyRollerGameHard");
    }


    public static void RetryScreen ()
    {
        SceneManager.LoadScene("EndScreen");
    }

    public void MainMenuScreen ()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
