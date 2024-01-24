using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState gameState;
    public bool started = false; 
    public Player playerPrefab;
    public Player player;
    public GameObject menuObject;

    void Awake()
	{
        if(gameState != null)
			GameObject.Destroy(gameState);
		else
			gameState = this;
		
		DontDestroyOnLoad(this);

        menuObject = GameObject.Find("Menu");
	}

    public void GameStart()
    {
        started = true;
        player = GameObject.Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        player.name = "Player";
    }
}
