using UnityEngine;

public class SnakePlayer2D : MonoBehaviour
{
    public float speed = 5f;
    public float maxSpeed = 20f;
    public bool causeCameraShake = false;
    private bool speedBoost;
    private bool left;
    private bool right;
    private bool up;
    private bool down;
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;
    public float gridMoveTimer = 1f;
    public float gridMoveTimerMax = 1f;
    private float rotateLeft = 90f;
    private float rotateRight = -90f;
    private int snakeBodySize = 0;
    public static LinkedList<Vector2Int>.SingleLinkedList snakeMovePositionList;
    public static LinkedList<Transform>.SingleLinkedList snakeBodyPartTransformList;
    public Sprite snakeBodyPartSprite;

    private void Start()
    {
        gridPosition = new Vector2Int(0, 0);                                        // Set startposition
        gridMoveDirection = new Vector2Int(1, 0);                                   // Set initial movement direction
        right = true;                                                               // Initial movement is right
        snakeMovePositionList = new LinkedList<Vector2Int>.SingleLinkedList();      // List to hold the heads previous movements to be used by the bodyparts.
        snakeBodyPartTransformList = new LinkedList<Transform>.SingleLinkedList();  // List to hold the transforms of the bodyparts
        Spawner.instance.SpawnFruit();                                              // Spawn initial fruit
    }

    private void Update()
    {
        Inputs();
    }

    private void FixedUpdate()
    {
        GridMovement();
        DirectionMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (causeCameraShake)
        {
            GameCamera.instance.cameraShake.Shake();
        }

        switch (collision.gameObject.tag)
        {
            case "Fruit":
                Destroy(collision.gameObject);
                SnakeAteFruit();
                break;
            case "Wall":
                GameRules.instance.GameOver();
                break;
            case "Player":
                GameRules.instance.GameOver();
                break;
            default:
                break;
        }
    }

    private void Inputs()
    {
        if (Input.GetButtonDown("SpeedBoost"))
        {
            SpeedBoost();
        }
        if (Input.GetKeyDown(KeyCode.A) && !(left || right))
        {
            left = true;
            right = false;
            up = false;
            down = false;
        }
        if (Input.GetKeyDown(KeyCode.D) && !(left || right))
        {
            left = false;
            right = true;
            up = false;
            down = false;
        }
        if (Input.GetKeyDown(KeyCode.W) && !(up || down))
        {
            left = false;
            right = false;
            up = true;
            down = false;
        }
        if (Input.GetKeyDown(KeyCode.S) && !(up || down))
        {
            left = false;
            right = false;
            up = false;
            down = true;
        }
    }

    private void DirectionMovement()
    {
        if (left)
        {
            if(gridMoveDirection.y == 1)
            {
                transform.Rotate(0f, 0f, rotateLeft);
            }
            else if (gridMoveDirection.y == -1)
            {
                transform.Rotate(0f, 0f, rotateRight);
            }
            gridMoveDirection.x = -1;
            gridMoveDirection.y = 0;
        }
        if (right)
        {
            if (gridMoveDirection.y == 1)
            {
                transform.Rotate(0f, 0f, rotateRight);
            }
            else if (gridMoveDirection.y == -1)
            {
                transform.Rotate(0f, 0f, rotateLeft);
            }
            gridMoveDirection.x = 1;
            gridMoveDirection.y = 0;
        }
        if (up)
        {
            if (gridMoveDirection.x == 1)
            {
                transform.Rotate(0f, 0f, rotateLeft);
            }
            else if (gridMoveDirection.x == -1)
            {
                transform.Rotate(0f, 0f, rotateRight);
            }
            gridMoveDirection.x = 0;
            gridMoveDirection.y = 1;
        }
        if (down)
        {
            if (gridMoveDirection.x == 1)
            {
                transform.Rotate(0f, 0f, rotateRight);
            }
            else if (gridMoveDirection.x == -1)
            {
                transform.Rotate(0f, 0f, rotateLeft);
            }
            gridMoveDirection.x = 0;
            gridMoveDirection.y = -1;
        }
    }

    private void GridMovement()
    {
        gridMoveTimer += Time.deltaTime * speed;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;
            snakeMovePositionList.AddFirst(gridPosition);
            gridPosition += gridMoveDirection;
            
            if (snakeMovePositionList.Count() > snakeBodySize)
            {
                snakeMovePositionList.RemoveLast();
            }

            for (int i = 0; i < snakeBodyPartTransformList.Count(); i++)
            {
                Transform snakeBodyPartTransform = snakeBodyPartTransformList.GetDataAtIndex(i);
                Vector2 snakeMovePosition = snakeMovePositionList.GetDataAtIndex(i);
                snakeBodyPartTransform.position = snakeMovePosition;
            }
        }
        transform.position = new Vector3(gridPosition.x, gridPosition.y);
    }
    
    private void SnakeAteFruit()
    {
        GameRules.instance.OnFruitAdded();
        snakeBodySize++;
        if(speed < maxSpeed)
        {
            speed += 0.25f;
        }
        Spawner.instance.SpawnFruit();
        GrowBodyPart();
    }

    private void GrowBodyPart()
    {
        GameObject snakeBodyPartGameObject = new GameObject("SnakeBodyPart", typeof(SpriteRenderer), typeof(CircleCollider2D));
        snakeBodyPartGameObject.transform.position = new Vector2(20f, 20f);
        snakeBodyPartTransformList.AddLast(snakeBodyPartGameObject.transform);
        snakeBodyPartGameObject.transform.localScale = new Vector2(1.1f, 1.1f);
        snakeBodyPartGameObject.GetComponent<CircleCollider2D>().radius = 0.4f;
        snakeBodyPartGameObject.GetComponent<SpriteRenderer>().sprite = snakeBodyPartSprite;
        snakeBodyPartGameObject.GetComponent<SpriteRenderer>().color = Color.green;
        snakeBodyPartGameObject.tag = "Player";
    }

    private void SpeedBoost()
    {
        if(speed < maxSpeed && !speedBoost)
        {
            speedBoost = true;
            speed += 10f;
        }
        else if (speedBoost)
        {
            speed -= 10f;
            speedBoost = false;
        }
    }
}