using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    // Convenient single instance reference for manager
    public static StatManager manager;
    public TextMeshProUGUI textRedGate, textBlueGate, textStrokes, textRoquet, textTitle;

    int strokes = 0;
    int totalWickets = 0;
    bool roquetActive = false;
    bool gameComplete = false;
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

        string displayText = "";
        if (gateNo > totalWickets)  // Check Progress
        {
            displayText += "Complete!";

            if (currentGate[0] > totalWickets && currentGate[1] > totalWickets)
            {
                TriggerVictory();
            }
        } 
        else
        {
            displayText += "Gate " + currentGate[ballIndex];
        }

        if (ballIndex == 0) // Check Color
        {
            textBlueGate.text = "Blue: " + displayText;
        } 
        else if (ballIndex == 1)
        {
            textRedGate.text = "Red: " + displayText;
        }
    }

    // Add a stroke
    internal void AddStroke()
    {
        if (gameComplete) return; // Ignore if game is completed

        strokes++;
        textStrokes.text = "Strokes: " + strokes;
        textRoquet.enabled = false;
        roquetActive = false;
    }

    // Eliminate a stroke for hitting a Roquet and celebrate
    internal void RoquetStroke()
    {
        if (gameComplete) return; // Ignore if game is completed

        // Ignore roquet if already active
        if (!roquetActive)
        {
            strokes--;
            textStrokes.text = "Strokes: " + strokes;
            textRoquet.enabled = true;
            roquetActive = true;
        }
    }

    internal void TriggerVictory()
    {
        // Set Game to Complete state
        textTitle.text = "Level Complete!";
        gameComplete = true;

        // Hide Roquet
        textRoquet.enabled = false;
        roquetActive = false;
    }
}
