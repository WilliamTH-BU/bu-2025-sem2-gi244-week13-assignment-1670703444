using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 2f, 2f);
    }

    void Spawn()
    {
        // 1.18 stop moving left when the game is over
        GameObject player = GameObject.Find("Player");
        bool isGameOver = player.GetComponent<PlayerController>().gameOver;
        if (isGameOver)
        {
            return;
        }
        int random = Random.Range(0, 3);
        var obstacle = ObstacleObjectPool.Instance.Acquire(random);
        obstacle.transform.SetPositionAndRotation(spawnPoint.transform.position, transform.rotation);

        var moveLeft = obstacle.GetComponent<MoveLeft>();
        moveLeft.obstacle = obstacle;
        moveLeft.obstacleType = random;
    }
}
