using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BricksGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);
    private List<Vector3> brickPos;
    private Brick[,] grid;
    private Brick flyingBrick;
    private Camera mainCamera;
    private float y = 0;
    private Touch touch;
    public InstructionMaker instruction;
    private int step;
    public GameObject uiObjectWrong;
    public GameObject uiObjectRight;
    private Stack<Brick> posStack;
    private Stack<Transform> instrStack;

    private void Start()
    {
        uiObjectWrong.SetActive(false);
        uiObjectRight.SetActive(false);
    }

    private void Awake()
    {
        grid = new Brick[GridSize.x, GridSize.y];
        brickPos = new List<Vector3>();
        mainCamera = Camera.main;
        posStack = new Stack<Brick>();
        instrStack = new Stack<Transform>();
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
                Vector3 offset = mainCamera.transform.up * 0.1f;
                ray.origin += offset;

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
                        flyingBrick.transform.position = new Vector3(x, instruction.currentModel.position.y, z);
                    }
                    
                    if (flyingBrick.transform.position == instruction.currentModel.transform.position && flyingBrick.tag.Contains(instruction.currentModel.tag))
                    {
                        brickPos.Add(flyingBrick.transform.position);
                        uiObjectRight.SetActive(true);
                        PlaceFlyingBrick(x, z);
                        instrStack.Push(instruction.currentModel);
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
        CheckBrick();
        
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
        posStack.Push(flyingBrick);
        
        y = 0;
        flyingBrick = null;
    }

    public void CheckBrick()
    {
        if (flyingBrick == null && touch.phase == TouchPhase.Moved)
            Events.InvokeIfNull();
        else
            Events.InvokeIfNotNull();
    }

    

    //public void IncreazeZ()
    //{

    //    if (flyingBrick != null)
    //    {
    //        y += 1.2f;
    //        flyingBrick.transform.position += new Vector3(0, 1.2f, 0);
    //    }
        
    //}

    //public void DecreaseZ()
    //{
    //    if (flyingBrick != null && y != 0)
    //    {
    //        y -= 1.2f;
    //        flyingBrick.transform.position -= new Vector3(0, 1.2f, 0);
    //    }

    //}

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        uiObjectRight.SetActive(false);
        uiObjectWrong.SetActive(false);
    }

    public void ReverseStep()
    {
        instruction.currentStep -= 1;
        Destroy(posStack.Pop().gameObject);
        Destroy(instruction.currentModel.gameObject);
        var ic = instrStack.Pop();
        Instantiate(ic.gameObject);
    }
}
