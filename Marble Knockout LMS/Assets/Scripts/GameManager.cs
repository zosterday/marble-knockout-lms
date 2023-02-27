using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODOs: Fix marbles hitting barriers by doing the todo in the marble class
// Add the start menu
// Add an ending screen with the winner
// Make the marbles spawn with different colors and maybe textures 
// Add any other final details


public class GameManager : MonoBehaviour
{
    private const float xSpawnBound = 7f;

    private const float zSpawnBound = 8.7f;

    private const float MarbleRadius = 0.5f;

    private const string BarrierTag = "Barrier";

    private const float BarrierRemovalInterval = 2f;

    private int activeMarbleCount;

    public static GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new System.InvalidOperationException("Instance of GameManager is null");
            }

            return instance;
        }
    }

    private static GameManager instance;

    [SerializeField]
    private GameObject marblePrefab;

    private List<GameObject> barriers;

    private readonly List<GameObject> marbles = new();

    private void Awake()
    {
        instance = this;

        barriers = GameObject.FindGameObjectsWithTag(BarrierTag).ToList();

        SpawnMarbles();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(RemoveBarrier), 1f, BarrierRemovalInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnMarbles()
    {
        for (var i = 0; i < StateMachine.MarbleSpawnCount; i++)
        {
            var marbleSpawned = false;

            var tryCount = 0;

            while (!marbleSpawned && tryCount <= 20)
            {
                marbleSpawned = TrySpawnMarble();
                tryCount++;
            }
        }
    }

    private bool TrySpawnMarble()
    {
        // Generate Random Spawn Position
        var x = Random.Range(-xSpawnBound, xSpawnBound);
        var z = Random.Range(-zSpawnBound, zSpawnBound);
        var spawnPos = new Vector3(x, 0, z);

        // Check for collision
        var collision = Physics.OverlapSphere(spawnPos, MarbleRadius);
        if (collision.Length != 0)
        {
            return false;
        }

        // Instantiate marble prefab
        var marble = Instantiate(marblePrefab, spawnPos, Quaternion.identity);

        // Add marble to marbles list
        marbles.Add(marble);
        activeMarbleCount++;

        return true;
    }

    private void RemoveBarrier()
    {
        if (barriers.Count <= 0)
        {
            return;
        }

        var index = Random.Range(0, barriers.Count);

        barriers[index].SetActive(false);
        barriers.RemoveAt(index);
    }

    public void RemoveMarble(GameObject gameObject)
    {
        gameObject.SetActive(false);
        marbles.Remove(gameObject);
        activeMarbleCount--;
    }
}
