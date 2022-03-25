using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider timeLeftSlider;
    [SerializeField] private TextMeshPro instructionText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform gameOverText;
    [SerializeField] private TextMeshProUGUI timeLeftText;

    public void ChangeInstructionText(string text) => instructionText.text = text;

    public void ChangeSliderValue(float value) => timeLeftSlider.value = value;

    public void ChangeScoreText(int value) => scoreText.text = $"Score: {value}";

    public void DisplayGameOverText() => gameOverText.gameObject.SetActive(true);

    public void ChangeTimeLeftText(float value) => timeLeftText.text = $"{value:N2}";

}
