using UnityEngine;

public class SnakePlayer2D : MonoBehaviour
{
    public float speed = 4f;
    private bool left;
    private bool right;
    private bool up;
    private bool down;
    private bool clicked;
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;
    public float gridMoveTimer = 1f;
    public float gridMoveTimerMax = 1f;
    private float rotateLeft = 90f;
    private float rotateRight = -90f;
    private int snakeBodySize = 0;
    private LinkedList<Vector2Int>.SingleLinkedList snakeMovePositionList;
    private LinkedList<Transform>.SingleLinkedList snakeBodyPartTransformList;
    public Sprite snakeBodyPartSprite;

    private void Start()
    {
        gridPosition = new Vector2Int(0, 0);
        gridMoveDirection = new Vector2Int(1, 0);
        snakeMovePositionList = new LinkedList<Vector2Int>.SingleLinkedList();
        snakeBodyPartTransformList = new LinkedList<Transform>.SingleLinkedList();
        Spawner.instance.SpawnFruit();
        GameRules.instance.OnPlayerLifeAdded();
    }

    private void Update()
    {
        InputHandler();
    }
    private void FixedUpdate()
    {
        GridMovementHandler();
        MovementHandler();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Fruit":
                Destroy(collision.gameObject);
                SnakeAteFruit();
                break;
            case "Wall":
                GameRules.instance.playerLife = 1;
                GameRules.instance.OnPlayerLifeRemoved();
                break;
            case "Player":
                GameRules.instance.playerLife = 1;
                GameRules.instance.OnPlayerLifeRemoved();
                break;
            default:
                break;
        }
    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !(gridMoveDirection.x == 1 || gridMoveDirection.x == -1) && !clicked)
        {
            left = true;
            clicked = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !(gridMoveDirection.x == 1 || gridMoveDirection.x == -1) && !clicked)
        {
            right = true;
            clicked = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !(gridMoveDirection.y == 1 || gridMoveDirection.y == -1) && !clicked)
        {
            up = true;
            clicked = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !(gridMoveDirection.y == 1 || gridMoveDirection.y == -1) && !clicked)
        {
            right = false;
            down = true;
            clicked = true;
        }
    }

    private void MovementHandler()
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
            left = false;
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
            right = false;
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
            up = false;
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
            down = false;
        }
    }

    private void GridMovementHandler()
    {
        Vector2Int oldGridPosition = gridPosition;

        gridMoveTimer += Time.deltaTime * speed;

        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;

            snakeMovePositionList.AddToBeginning(gridPosition);

            gridPosition += gridMoveDirection;

            int snakeMovePositionLength = snakeMovePositionList.Count();

            if (snakeMovePositionLength >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAtIndex(snakeMovePositionLength - 1);
            }

            for (int i = 0; i < snakeBodyPartTransformList.Count(); i++)
            {
                Transform snakeBodyPartTransform = snakeBodyPartTransformList.GetDataAtIndex(i);
                Vector2Int snakeMovePosition = snakeMovePositionList.GetDataAtIndex(i);
                Vector3 snakeBodyPosition = new Vector3(snakeMovePosition.x, snakeMovePosition.y);
                snakeBodyPartTransform.position = snakeBodyPosition;
            }
        }
        transform.position = new Vector3(gridPosition.x, gridPosition.y);
        if (oldGridPosition != gridPosition)
        {
            clicked = false;
        }
    }

    private void SnakeAteFruit()
    {
        GameRules.instance.OnFruitAdded();
        snakeBodySize++;
        speed += 0.5f;
        Spawner.instance.SpawnFruit();
        GrowBodyPart();
    }

    private void GrowBodyPart()
    {
        GameObject snakeBodyPartGameObject = new GameObject("SnakeBodyPart", typeof(SpriteRenderer), typeof(CircleCollider2D));
        snakeBodyPartGameObject.transform.position = new Vector3(20f, 20f, 0);
        snakeBodyPartTransformList.AddToEnd(snakeBodyPartGameObject.transform);
        snakeBodyPartGameObject.GetComponent<SpriteRenderer>().sprite = snakeBodyPartSprite;
        snakeBodyPartGameObject.GetComponent<SpriteRenderer>().color = Color.green;
        snakeBodyPartGameObject.tag = "Player";
    }
}