using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class InstructionCrocodile : MonoBehaviour
{
    public Transform brick2x3green;
    public Transform brick2x4bright_green;
    public Transform brick2x4green;
    public Transform brickwitheye1x1green;
    public Transform plate2x3green;
    public Transform plate2x10medium_blue;
    public Transform plateround1x1white;
    public Transform slopebrick452x2green;
    public GameObject BricksPanel;
    public GameObject BricksPanel2;
    public GameObject Check;

    private List<string> Instructions;
    private Dictionary<string, Transform> Models;
    public int currentStep;
    public Transform currentModel;

    void Start()
    {
        Instructions = ParseInstructions();
        Models = ParseModels();
        NextStep();
    }

    void Update()
    {

    }

    public void NextStep()
    {
        if (currentModel != null) Destroy(currentModel.gameObject);
        if (currentStep < Instructions.Count)
        {
            string[] args = Instructions[currentStep].Split(';');
            currentModel = Instantiate(Models[args[4]]);
            currentModel.transform.position =
                new Vector3(float.Parse(args[0], new CultureInfo("en-US")), float.Parse(args[1],
                new CultureInfo("en-US")), float.Parse(args[2], new CultureInfo("en-US")));
            currentModel.Rotate(0, 0, float.Parse(args[3]));
            currentStep++;
        }
        else Finish();
    }

    public List<string> ParseInstructions()
    {
        return new List<string>() { //x;y;z;rotateZ;name
        "4;0;10;90;plate2x10medium_blue",
        "6;0.4;5;90;brick2x3green",
        "6;0.4;8;90;brick2x3green",
        "3;0.4;10;0;brick2x4green",
        "3;0.4;2;0;brick2x4green",
        "8;1.6;5;180;slopebrick452x2green",
        "2;1.6;9;0;slopebrick452x2green",
        "8;1.6;9;180;slopebrick452x2green",
        "6;0.4;14;90;brick2x4green",
        "2;1.6;13;0;slopebrick452x2green",
        "6;1.6;5;90;brick2x3green",
        "6;1.6;2;90;brick2x4bright_green",
        "6;2.8;3;90;plate2x3green",
        "3;2.8;0;0;plateround1x1white",
        "4;2.8;0;0;plateround1x1white",
        "4;2.8;-1;0;plateround1x1white",
        "3;2.8;-1;0;plateround1x1white",
        "6;3.2;1;90;plate2x3green",
        "4;3.2;1;-90;brickwitheye1x1green",
        "6;3.2;2;90;brickwitheye1x1green"
        };
    }

    public Dictionary<string, Transform> ParseModels()
    {
        return new Dictionary<string, Transform>()
        {
            { "plate2x10medium_blue", plate2x10medium_blue },
            { "brick2x3green", brick2x3green },
            { "brick2x4green", brick2x4green },
            { "slopebrick452x2green", slopebrick452x2green },
            { "brick2x4bright_green", brick2x4bright_green },
            { "plate2x3green", plate2x3green },
            { "plateround1x1white", plateround1x1white },
            { "brickwitheye1x1green", brickwitheye1x1green }
        };
    }

    public void Finish()
    {
        BricksPanel.SetActive(false);
        BricksPanel2.SetActive(false);
        Check.SetActive(true);
    }
}
