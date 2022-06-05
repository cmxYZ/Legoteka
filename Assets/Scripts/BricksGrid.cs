using UnityEngine;

public class BricksGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    private Brick[,] grid;
    private Brick flyingBrick;
    private Camera mainCamera;
    private float z = 0;
    private Touch touch;

    private void Awake()
    {
        grid = new Brick[GridSize.x, GridSize.y];
        
        mainCamera = Camera.main;
    }

    public void StartPlacingBrick(Brick buildingPrefab)
    {
        if (flyingBrick != null)
        {
            Destroy(flyingBrick.gameObject);
        }
        
        flyingBrick = Instantiate(buildingPrefab);
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (flyingBrick != null)
            {
                var groundPlane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = mainCamera.ScreenPointToRay(touch.position);


                if (groundPlane.Raycast(ray, out float position))
                {

                    Vector3 worldPosition = ray.GetPoint(position);
                    int x = Mathf.RoundToInt(worldPosition.x);
                    int y = Mathf.RoundToInt(worldPosition.z);

                    bool available = true;

                    if (x < 0 || x > GridSize.x - flyingBrick.Size.x) available = false;
                    if (y < 0 || y > GridSize.y - flyingBrick.Size.y) available = false;

                    if (available && IsPlaceTaken(x, y)) available = false;

                    if (touch.phase == TouchPhase.Moved)
                    {
                        flyingBrick.transform.position = new Vector3(x, z, y);
                    }

                    if(available && touch.tapCount == 2)
                        PlaceFlyingBrick(x, y);

                }
            }
        }
        
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBrick.Size.x; x++)
        {
            for (int y = 0; y < flyingBrick.Size.y; y++)
            {
                if (grid[placeX + x, placeY + y] != null) return true;
            }
        }

        return false;
    }

    private void PlaceFlyingBrick(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBrick.Size.x; x++)
        {
            for (int y = 0; y < flyingBrick.Size.y; y++)
            {
                grid[placeX + x, placeY + y] = flyingBrick;
            }
        }
        z = 0;

        flyingBrick = null;
    }

    public void IncreazeZ()
    {
        if (flyingBrick != null)
        {
            z += 1.2f;
            flyingBrick.transform.position += new Vector3(0, 1.2f, 0);
        }
        
    }

    public void DecreaseZ()
    {
        if (flyingBrick != null && z != 0)
        {
            z -= 1.2f;
            flyingBrick.transform.position -= new Vector3(0, 1.2f, 0);
        }

    }

    public void Rotate()
    {
        flyingBrick.transform.Rotate(0, 0, 90);
    }
}
