using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnManager : MonoBehaviour
{
    public void startGame()
    {
        FindObjectOfType<gameModeManager>().startGame();
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
