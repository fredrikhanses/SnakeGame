﻿using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int gridHeight = 17;
    public int gridWidth = 9;
    public static Spawner instance;
    public GameObject fruit;
    private Vector3Int fruitPosition;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnFruit()
    {
        fruitPosition = new Vector3Int(Random.Range(-gridHeight + 1, gridHeight), Random.Range(-gridWidth + 1, gridWidth), 0);
        Instantiate(fruit, fruitPosition, Quaternion.identity);
    }
}