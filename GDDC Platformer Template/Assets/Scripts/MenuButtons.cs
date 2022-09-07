using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    //This script is for the main menu
    //These methods allow the buttons present to switch between each other and to new scenes

    //This method allows for the main and submenu(s) to switch between each other
    public void ChangeMenu(GameObject thatMenu)
    {
        thatMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    //When the game is fully built, this command exits the program when attached button is clicked
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("The game has been quit");
    }

    //Loading a new level can be assigned to a button with this command, by selecting it in the inspector
    //The scene to load will need to be entered as an argument, with correct spelling
    public void LoadLevel(string levelName)
    {
        Debug.Log("Loading " + levelName + "...");
        SceneManager.LoadScene(levelName);
    }

}
