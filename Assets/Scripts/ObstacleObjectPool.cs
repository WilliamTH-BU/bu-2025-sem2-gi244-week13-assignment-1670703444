using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject obstacleBarrelPrefab;
    public GameObject obstacleBarrierPrefab;
    public GameObject obstacleStoneWallPrefab;
    public int poolSize = 10;

    private List<GameObject> obstacleBarrelPool;
    private List<GameObject> obstacleBarrierPool;
    private List<GameObject> obstacleStoneWallPool;

    private static ObstacleObjectPool instance;
    public static ObstacleObjectPool Instance { get => instance; }

    void Awake()
    {
        obstacleBarrelPool = new List<GameObject>();
        obstacleBarrierPool = new List<GameObject>();
        obstacleStoneWallPool = new List<GameObject>();

        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateObstacle();
        }
        yield return null;

    }
    private void CreateObstacle()
    {
        var barrel = Instantiate(obstacleBarrelPrefab);
        barrel.SetActive(false);
        obstacleBarrelPool.Add(barrel);

        var barrier = Instantiate(obstacleBarrierPrefab);
        barrier.SetActive(false);
        obstacleBarrierPool.Add(barrier);

        var stonewall = Instantiate(obstacleStoneWallPrefab);
        stonewall.SetActive(false);
        obstacleStoneWallPool.Add(stonewall);
    }
    public GameObject Acquire(int obstacleType)
    {
        if (obstacleBarrelPool.Count == 0 || obstacleBarrierPool.Count == 0 || obstacleStoneWallPool.Count == 0)
        {
            CreateObstacle();
        }

        switch (obstacleType)
        {
            case 0:
                var barrel = obstacleBarrelPool[0];
                obstacleBarrelPool.RemoveAt(0);
                barrel.SetActive(true);
                return barrel;
            case 1:
                var barrier = obstacleBarrierPool[0];
                obstacleBarrierPool.RemoveAt(0);
                barrier.SetActive(true);
                return barrier;
            case 2:
                var stonewall = obstacleStoneWallPool[0];
                obstacleStoneWallPool.RemoveAt(0);
                stonewall.SetActive(true);
                return stonewall;
        }
        return null;
    }

    public void Release(GameObject obstacle, int obstacleType)
    {
        switch (obstacleType)
        {
            case 0:
                obstacleBarrelPool.Add(obstacle);
                obstacle.SetActive(false);
                return;
            case 1:
                obstacleBarrierPool.Add(obstacle);
                obstacle.SetActive(false);
                return;
            case 2:
                obstacleStoneWallPool.Add(obstacle);
                obstacle.SetActive(false);
                return;
        }
    }
}
