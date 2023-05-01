using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController instance { get; private set; }
    [SerializeField] public Slider healthSlider;
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] public TMP_Text speedText;
    private float score;
    private void Start() {
        instance = this;
    }

    public void ScoreIncrement(float health) {
        score += health;
        scoreText.text = score + "";
    }

    public void SetSpeed(int speed) {
        speedText.text = speed + "";
    }
}
