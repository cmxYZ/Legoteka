using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class InstructionMaker : MonoBehaviour
{
    public Transform brick1x2green;
    public Transform brick1x2orange;
    public Transform brick1x2yellow;
    public Transform brick2x2yellow;
    public GameObject BricksPanel;
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
        "5;0;4;0;brick1x2yellow",
        "5;0;6;0;brick1x2yellow",
        "5;1.2;4;0;brick1x2yellow",
        "5;1.2;6;0;brick1x2orange",
        "5;2.4;4;0;brick1x2orange",
        "5;2.4;6;0;brick2x2yellow",
        "5;3.6;5;0;brick2x2yellow",
        "5;4.8;4;0;brick1x2yellow",
        "5;6.0;4;0;brick1x2orange",
        "5;7.2;4;0;brick2x2yellow",
        "5;8.4;4;0;brick1x2yellow",
        "2;0;1;0;brick1x2green",
        "1;1.2;1;0;brick1x2green",
        "2;2.4;1;0;brick1x2green"
        };
    }

    public Dictionary<string, Transform> ParseModels()
    {
        return new Dictionary<string, Transform>()
        {
            { "brick1x2green", brick1x2green },
            { "brick1x2orange", brick1x2orange },
            { "brick1x2yellow", brick1x2yellow },
            { "brick2x2yellow", brick2x2yellow }
        };
    }

    public void Finish()
    {
        BricksPanel.SetActive(false);
        Check.SetActive(true);
    }
}
