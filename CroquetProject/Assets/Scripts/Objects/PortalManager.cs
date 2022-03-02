using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{

    public enum FramePosition
    {
        Front,
        Back
    }

    struct Ball {

        internal GameObject gameObject;
        internal BallState state;

        internal Ball(GameObject newGameObject)
        {
            gameObject = newGameObject;
            state = BallState.Undetected;
        }
    }

    internal string ballTag = "Ball";
    List<Ball> balls;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum BallState
    {
        Undetected,     // Ball has not entered or scored
        Entering,       // Ball has entered front frame
        Inside,         // Ball is in front and back frames
        Exiting,        // Ball is in back frame
        Scored,         // Ball has left frame with a score
        Returning       // Ball is entering back frame after having scored
    }

    internal void FrameEntered(FramePosition position, Collider other)
    {
        int ballIndex = GetBallIndex(other.gameObject); // Find the ball
        if (ballIndex == -1) { return; }                // if not a ball: exit
        Ball currentBall = balls[ballIndex];            // Fetch reference

        if (position == FramePosition.Front)
        {
            switch (currentBall.state) {
                case BallState.Undetected:
                    currentBall.state = BallState.Entering;
                    break;
                case BallState.Exiting:
                    currentBall.state = BallState.Inside;
                    break;
                case BallState.Returning:
                    currentBall.state = BallState.Inside;
                    // TODO: Deduct score
                    break;
            }
        }
        else if (position == FramePosition.Back)
        {
            switch (currentBall.state)
            {
                case BallState.Scored:
                    currentBall.state = BallState.Returning;
                    break;
                case BallState.Entering:
                    currentBall.state = BallState.Inside;
                    break;
            }
        }
    }

    internal void FrameExited(FramePosition position, Collider other)
    {
        int ballIndex = GetBallIndex(other.gameObject); // Find the ball
        if (ballIndex == -1) { return; }                // if not a ball: exit
        Ball currentBall = balls[ballIndex];            // Fetch reference

        if (position == FramePosition.Front)
        {
            switch (currentBall.state)
            {
                case BallState.Entering:
                    currentBall.state = BallState.Undetected;
                    break;
                case BallState.Inside:
                    currentBall.state = BallState.Exiting;
                    break;
            }
        }
        else if (position == FramePosition.Back)
        {
            switch (currentBall.state)
            {
                case BallState.Inside:
                    currentBall.state = BallState.Entering;
                    break;
                case BallState.Exiting:
                    currentBall.state = BallState.Scored;
                    // TODO: Score a point
                    break;
                case BallState.Returning:
                    currentBall.state = BallState.Scored;
                    break;
            }
        }
    }

    internal int GetBallIndex(GameObject ballRef)
    {

        // Is it a ball?
        if (ballRef.CompareTag(ballTag))
        {
            // Search for ball in struct
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].gameObject == ballRef.gameObject)
                {
                    return i;
                }
            }

            // Add the ball to the struct list if missing
            balls.Add(new Ball(ballRef.gameObject));
            return balls.Count - 1;
        }
        else
        {
            return -1; // Object not a ball
        }
    }

    /*
     * Flags for being in front, being in back
     * 
     * Sequence for goal (per ball): front, front & back, back, none 
     * Reversable for negative goal
     * 
     * 
     * 
     * if other tag is ball, add to ball array on entry
     * then it's a matter of checking other to see if it's inside the struct array, and seeing where it goes accordingly and updating the state (after checking)
     * 
     */
}
