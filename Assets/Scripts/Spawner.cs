using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int gridHeight = 17;
    public int gridWidth = 9;
    public static Spawner instance;
    public GameObject fruit = null;
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
        if(!SnakePlayer2D.snakeMovePositionList.Contains(new Vector2Int(fruitPosition.x, fruitPosition.y)))
        {
            Instantiate(fruit, fruitPosition, Quaternion.identity);
        }
        else
        {
            SpawnFruit();
        }
    }
}