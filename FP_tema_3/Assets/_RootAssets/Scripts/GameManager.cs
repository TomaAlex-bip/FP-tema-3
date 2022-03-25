using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    
    [SerializeField] private InstructionsManager instructionsManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private InteractionsManager interactionsManager;

    [SerializeField] private Transform interactiveSquare;

    private int score = 0;
    private Vector2 initialSquarePosition;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        // interactiveSquare.gameObject.SetActive(false);
        initialSquarePosition = interactiveSquare.position;
    }

    private void Start()
    {
        StartInstructionsGeneratorCoroutine();
    }

    private void Update()
    {
        UpdateSlider();
        UpdateScore();
        UpdateTimeLeftText();
    }

    public InstructionsManager GetInstructionManager() => instructionsManager;
    public UIManager GetUIManager() => uiManager;

    public int GetScore() => score;

    public void StartInstructionsGeneratorCoroutine()
    {
        // interactiveSquare.gameObject.SetActive(true);
        instructionsManager.StartInstructionsGeneratorCoroutine();
        
    }

    public void CheckInstructionDone()
    {
        if (interactionsManager.PositionZone != instructionsManager.GetCurrentInstruction().correctDirection)
        {
            if (score > 0)
                score--;
            interactiveSquare.position = initialSquarePosition;
            return;
        }

        instructionsManager.ResetTimeLeft();
        interactiveSquare.position = initialSquarePosition;
        score++;
    }

    public void StopGame()
    {
        InputManager.Instance.gameObject.SetActive(false);
        interactiveSquare.gameObject.SetActive(false);
        uiManager.DisplayGameOverText();
    }

    private void UpdateSlider()
    {
        var maxTime = instructionsManager.GetMaxTimeLeft();
        var timeLeft = instructionsManager.GetTimeLeft();
        var sliderValue = (maxTime - timeLeft) / maxTime;
        uiManager.ChangeSliderValue(sliderValue);
    }
    
    private void UpdateScore() => uiManager.ChangeScoreText(score);

    private void UpdateTimeLeftText()
    {
        var maxTime = instructionsManager.GetMaxTimeLeft();
        var timeLeft = instructionsManager.GetTimeLeft();
        uiManager.ChangeTimeLeftText(maxTime - timeLeft);
    }


}
