using UnityEngine;
using System.Collections;

public class BricksGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    private Brick[,] grid;
    private Brick flyingBrick;
    private Camera mainCamera;
    private float y = 0;
    private Touch touch;
    public InstructionMaker instruction;
    private int step;
    public GameObject uiObjectWrong;
    public GameObject uiObjectRight;

    private void Start()
    {
        uiObjectWrong.SetActive(false);
        uiObjectRight.SetActive(false);
    }

    private void Awake()
    {
        grid = new Brick[GridSize.x, GridSize.y];
        
        mainCamera = Camera.main;
    }

    public void StartPlacingBrick(Brick buildingPrefab)
    {
        if (flyingBrick == null && instruction.currentStep > step)
        {
            flyingBrick = Instantiate(buildingPrefab);
            step++;
        }

        else if(flyingBrick != null && instruction.currentStep == step)
        {
            Destroy(flyingBrick.gameObject);
            flyingBrick = Instantiate(buildingPrefab);
        }

        else if (flyingBrick == null && instruction.currentStep == step)
        {
            flyingBrick = Instantiate(buildingPrefab);
        }
    }

    private void Update()
    {
        if(Input.touchCount > 0 && !CameraControll.movingState)
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
                    int z = Mathf.RoundToInt(worldPosition.z);

                    if (x < 0) x = 0;
                    if (x > GridSize.x - flyingBrick.Size.x) x = GridSize.x;
                    if (z < 0) z = 0;
                    if (z > GridSize.y - flyingBrick.Size.y) z = GridSize.y;


                    if (touch.phase == TouchPhase.Moved)
                    {
                        flyingBrick.transform.position = new Vector3(x, y, z);
                    }

                    if(flyingBrick.transform.position == instruction.currentModel.transform.position && flyingBrick.tag.Contains(instruction.currentModel.tag))
                    {
                        uiObjectRight.SetActive(true);
                        PlaceFlyingBrick(x, z);
                        instruction.NextStep();
                        StartCoroutine("Wait");
                    }

                    else if(flyingBrick.transform.position == instruction.currentModel.transform.position && !flyingBrick.tag.Contains(instruction.currentModel.tag))
                    {
                        uiObjectWrong.SetActive(true);
                        Destroy(flyingBrick.gameObject);
                        y = 0;
                        StartCoroutine("Wait");                        
                    }
                        
                }
                
            }
        }
        
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
        y = 0;
        flyingBrick = null;
    }

    public void IncreazeZ()
    {
        if (flyingBrick != null)
        {
            y += 1.2f;
            flyingBrick.transform.position += new Vector3(0, 1.2f, 0);
        }
        
    }

    public void DecreaseZ()
    {
        if (flyingBrick != null && y != 0)
        {
            y -= 1.2f;
            flyingBrick.transform.position -= new Vector3(0, 1.2f, 0);
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        uiObjectRight.SetActive(false);
        uiObjectWrong.SetActive(false);
    }

    public void Rotate()
    {
        flyingBrick.transform.Rotate(0, 0, 90);
    }
}
