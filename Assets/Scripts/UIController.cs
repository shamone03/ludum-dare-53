using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController instance { get; private set; }
    [SerializeField] public TMP_Text timer;
    [SerializeField] public Slider healthSlider;
    [SerializeField] public TMP_Text score;

    private void Awake() {
        instance = this;
    }
}
