using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    // Convenient single instance reference for manager
    public static StatManager manager;
    public TextMeshProUGUI textRedGate, textBlueGate, textStrokes, textRoquet;

    int strokes = 0;
    int totalWickets = 0;
    bool roquetActive = false;
    int[] currentGate = { 1, 1 };   // Two balls

    private void Awake()
    {
        // Setup Singleton Status
        if (manager != null && manager != this)
        {
            Destroy(this.gameObject);
        } else
        {
            manager = this;
        }
    }

    private void Start()
    {
        // Count wickets in-game
        totalWickets = FindObjectsOfType<PortalManager>().Length;
    }

    // Update gate tracking
    internal void UpdateCurrentGate(int ballIndex, int gateNo)
    {
        // Update gate number, text display
        currentGate[ballIndex] = gateNo;
        if (ballIndex == 0)
        {
            textBlueGate.text = "Blue: Gate " + currentGate[ballIndex];
        } 
        else if (ballIndex == 1)
        {
            textRedGate.text = "Red: Gate " + currentGate[ballIndex];
        }
    }

    // Add a stroke
    internal void AddStroke()
    {
        strokes++;
        textStrokes.text = "Strokes: " + strokes;
        textRoquet.enabled = false;
        roquetActive = false;
    }

    // Eliminate a stroke for hitting a Roquet and celebrate
    internal void RoquetStroke()
    {
        // Ignore roquet if already active
        if (!roquetActive)
        {
            strokes--;
            textStrokes.text = "Strokes: " + strokes;
            textRoquet.enabled = true;
            roquetActive = true;
        }
    }
}
