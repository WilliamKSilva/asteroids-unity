using UnityEngine;

public class Menu : MonoBehaviour
{
    public void NewGameClick()
    {
        GameState.gameState.GameStart();
        GameState.gameState.menuObject.SetActive(false);
    }

    public void QuitGameClick() 
    {
        GameState.gameState.started = false;
        Application.Quit();
    }
}
