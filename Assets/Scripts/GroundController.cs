using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public static GroundController instance;
    [SerializeField] List<Ground> grounds;
    private Ground currentGround;
    private int currentGroundIndex;
    private Ground nextGround;
    [SerializeField] float groundSpeed;
    private float startGroundSpeed;
// [SerializeField] GameObject obstaclesParent;

int nextGroundIndex;

    void Start()
    {
        if (instance != null)
        {
            Debug.LogError("There cannot be more than one instance of GroundController in the scene");
            Destroy(gameObject);
            return;
        }
        instance = this;
        startGroundSpeed = groundSpeed;
        StartGame();

    }

    void Update()
    {
        if (PlayerController.GetGameState() == PlayerController.GameState.playing)
        {
        currentGround.transform.GetChild(0).Translate(Vector3.left * groundSpeed * 60f * Time.deltaTime);
        nextGround.transform.GetChild(0).Translate(Vector3.left * groundSpeed * 60f * Time.deltaTime);
            if (currentGround.GetStartPos().x <= -50)
                {
                    currentGround.ResetPosition();
                    currentGround = nextGround;
                    currentGroundIndex = nextGroundIndex;
                    Debug.Log("Setting CurrentGround to:" + currentGround.name);
                    SetNextGround();

                }
            }
    }

    public static void StartGame()
    {
        Debug.Log("Is using Ground StartGame");
        instance.SetGroundPosition();
        instance.SetCurrentGround();
        instance.SetNextGround();
        instance.groundSpeed = instance.startGroundSpeed;
    }

    private void SetGroundPosition()
    {
        foreach (Ground ground in grounds)
        {
            ground.ResetPosition();
        }
    }

    private void SetCurrentGround()
    {
        currentGroundIndex = Random.Range(0, grounds.Count);
        currentGround = grounds[currentGroundIndex];
        currentGround.SetStartPos(new Vector3(0, 0, 0));

        Obstacle[] obstacles = currentGround.GetComponentsInChildren<Obstacle>(true);
        obstacles[0].gameObject.SetActive(false);

        RandomizeObstaclePositions(obstacles);
        Debug.Log("Setting CurrentGround to:" + currentGround.name);
    }

    private void RandomizeObstaclePositions(Obstacle[] obstacles)
    {
      //  Obstacle[] obstacles = ground.GetComponentsInChildren<Obstacle>();
        foreach (Obstacle obstacle in obstacles)
        {
           // obstacle.gameObject.SetActive(true);
            obstacle.gameObject.transform.localPosition = new Vector3(obstacle.gameObject.transform.localPosition.x, Random.Range(17f, 21f), obstacle.gameObject.transform.localPosition.z);
            obstacle.gameObject.transform.localRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        }
    }

    private void SetNextGround()
    {
        do
        {
            nextGroundIndex = Random.Range(0, grounds.Count);
        } while (nextGroundIndex == currentGroundIndex);

        nextGround = grounds[nextGroundIndex];
        Obstacle[] obstacles = nextGround.GetComponentsInChildren<Obstacle>(true);

        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.gameObject.SetActive(true);
        }

        RandomizeObstaclePositions(obstacles);

        Debug.Log("Setting NextGround to:" + nextGround.name);
        AddNextGround();
    }

    private void AddNextGround()
    {
        nextGround.SetStartPos(currentGround.GetEndPos());
    }

    public static void Die()
    {
        instance.groundSpeed = 0;
    }


}
