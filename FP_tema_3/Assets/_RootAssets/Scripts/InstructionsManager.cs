using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InstructionsManager : MonoBehaviour
{

    [SerializeField] private float instructionTime = 2f;
    
    [SerializeField] private List<Instruction> instructions;


    private float timeLeft;
    
    private bool instructionsGeneratorStarted = false;

    private GameManager gameManager;

    private Instruction currentInstruction;

    private Instruction GenerateRandomInstruction()
    {
        var rng = Random.Range(0, instructions.Count);
        return instructions[rng];
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        // print(GenerateRandomInstruction().messageText);
        timeLeft = 0f;
        // StartCoroutine(InstructionsGeneratorCoroutine());

    }

    


    private IEnumerator InstructionsGeneratorCoroutine()
    {
        while (timeLeft <= instructionTime)
        {
            if (timeLeft == 0f)
            {
                currentInstruction = GenerateRandomInstruction();
                gameManager.GetUIManager().ChangeInstructionText(currentInstruction.messageText);
                print($"changed instruction: {currentInstruction}");
            }
            timeLeft += Time.deltaTime;
            yield return null;
        }
        
        print("Game Over!");
        gameManager.StopGame();

    }

    public void StartInstructionsGeneratorCoroutine()
    {
        if (instructionsGeneratorStarted)
            return;
        instructionsGeneratorStarted = true;
        StartCoroutine(InstructionsGeneratorCoroutine());
    }

    public void ResetTimeLeft() => timeLeft = 0f;
    public float GetTimeLeft() => timeLeft;
    public float GetMaxTimeLeft() => instructionTime;
    public Instruction GetCurrentInstruction() => currentInstruction;

}


[System.Serializable]
public class Instruction
{
    public ZoneType correctDirection;
    public string messageText;
}

