using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] Vector3 startPos;
    [SerializeField] float thrust;
    [SerializeField] float gravity;
    [SerializeField] Canvas HUD;
    private bool shouldFall = true;
	private Rigidbody rb3d;
    private int points = 0;
    private int highScore = 0;
    private bool isAlive = true;
    [SerializeField] GameObject replayButton;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject highSoreText;
    [SerializeField] GameObject pointsText;
    private GameState gameState = GameState.start;
    

    public enum GameState
    {
        start,
        playing,
        death
    }

    void Start()
    {
        if (instance != null)
        {
            Debug.LogError("There cannot be more than one instance of GroundController in the scene");
            Destroy(gameObject);
            return;
        }
        instance = this;

        Debug.Log("START: " + gameState);
        ResetGame();

     
    }

    public static GameState GetGameState()
    {
        return instance.gameState;
    }

    public static void SetGameState(GameState state)
    {
        instance.gameState = state;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && instance.gameState == GameState.playing)
        {
            Debug.Log("Clicked to add force to player");
            rb3d.velocity = Vector3.zero;
            rb3d.AddForce(new Vector3(0, thrust, 0));
            SoundController.PlayJumpSound();
        }

        if (Input.GetMouseButtonDown(0) && instance.gameState == GameState.start)
        {
            Debug.Log("Clicked to start! State is: " + instance.gameState);
            instance.gameState = GameState.playing;
            startText.SetActive(false);
            gameObject.AddComponent<Rigidbody>();
            rb3d = GetComponent<Rigidbody>();
            rb3d.velocity = Vector3.zero;
            rb3d.AddForce(new Vector3(0, thrust, 0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with:" + collision.collider.name);
        GroundController.Die();
        isAlive = false;
        SoundController.PlayDieSound();
        replayButton.SetActive(true);
        instance.gameState = GameState.death;
        if (points > highScore)
        {
            highScore = points;
            highSoreText.GetComponent<Text>().text = "Highscore: " + highScore;
        }
    }

    //Add points and show it in the HUD
    private void OnTriggerEnter(Collider other)
    {
        points++;
        HUD.GetComponentInChildren<Text>().text = points.ToString();

    }

    public void ReplayOnClicked()
    {
        Debug.Log("REPLAY ON CLICKED : " + instance.gameState);
        ResetGame();
        GroundController.StartGame();
        Destroy(rb3d);
    }

    private void ResetGame()
    {
        Debug.Log("ResetGame : " + instance.gameState);
        instance.gameState = GameState.start;
        points = 0;
        pointsText.GetComponent<Text>().text = points.ToString();
        replayButton.SetActive(false);
      //  Destroy(rb3d);
        transform.position = startPos;
        startText.SetActive(true);
        // rb3d = GetComponent<Rigidbody>();
        
    }
}
