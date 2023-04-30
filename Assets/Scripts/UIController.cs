using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController instance { get; private set; }
    [SerializeField] public TMP_Text timer {get;}
    [SerializeField] public Slider healthSlider {get;}
    [SerializeField] public TMP_Text score {get;}

    private void Awake() {
        instance = this;
    }
}
