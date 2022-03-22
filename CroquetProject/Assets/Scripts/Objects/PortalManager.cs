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

    class Ball {

        internal GameObject gameObject;
        internal BallState state;
        internal int ballIndex;

        internal Ball(GameObject newGameObject, int newBallIndex)
        {
            gameObject = newGameObject;
            state = BallState.Undetected;
            ballIndex = newBallIndex;
        }

        internal void ChangeState(BallState newState)
        {
            state = newState;
        }
    }

    List<Ball> balls = new List<Ball>();
    public int gateNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Auto Update visual identifier of this gate's number
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

        BallTracker ballTracker = balls[ballIndex].gameObject.GetComponent<BallTracker>(); // The next gate the ball is supposed to score

        if (position == FramePosition.Front)
        {
            switch (balls[ballIndex].state) {
                case BallState.Undetected:  // Ball that is supposed to enter this gate has entered the front
                    if (ballTracker.nextGate == gateNumber)
                    {
                        balls[ballIndex].ChangeState(BallState.Entering);
                    }
                    break;
                case BallState.Exiting:     // Ball hasn't been scored but has returned to the center from the back
                    balls[ballIndex].ChangeState(BallState.Inside);
                    break;
                case BallState.Returning:   // Ball has returned to the gate center somehow and the gate is un-scored
                    balls[ballIndex].ChangeState(BallState.Inside);
                    ballTracker.UnscoreGate();  // Deduct Score
                    break;
            }
        }
        else if (position == FramePosition.Back)
        {
            switch (balls[ballIndex].state)
            {
                case BallState.Scored:      // Ball is supposed to enter the next gate but came back
                    if (ballTracker.nextGate == gateNumber + 1) 
                    {
                        balls[ballIndex].ChangeState(BallState.Returning);
                    }
                    break;  
                case BallState.Entering:    // Ball is now between entrance and exit
                    balls[ballIndex].ChangeState(BallState.Inside);
                    break;
            }
        }
    }

    internal void FrameExited(FramePosition position, Collider other)
    {
        int ballIndex = GetBallIndex(other.gameObject); // Find the ball
        if (ballIndex == -1) { return; }                // if not a ball: exit

        BallTracker ballTracker = balls[ballIndex].gameObject.GetComponent<BallTracker>(); // The next gate the ball is supposed to score

        if (position == FramePosition.Front)
        {
            switch (balls[ballIndex].state)
            {
                case BallState.Entering:    // Ball did not pass through entrance after all and becomes undetected again
                    balls[ballIndex].ChangeState(BallState.Undetected);
                    break;
                case BallState.Inside:      // Ball has moved through the center and entered the exit side of the wicket
                    balls[ballIndex].ChangeState(BallState.Exiting);
                    break;
            }
        }
        else if (position == FramePosition.Back)
        {
            switch (balls[ballIndex].state)
            {
                case BallState.Inside:      // Ball has moved back from the inside and is now in the entrance side of the wicket
                    balls[ballIndex].ChangeState(BallState.Entering);
                    break;
                case BallState.Exiting:     // Ball has left the exit side of the wicket and a point is scored
                    balls[ballIndex].ChangeState(BallState.Scored);
                    ballTracker.ScoreGate();    // Score a point
                    break;
                case BallState.Returning:   // Ball returned but exited again; no score deduction necessary
                    balls[ballIndex].ChangeState(BallState.Scored);
                    break;
            }
        }
    }

    internal int GetBallIndex(GameObject ballRef)
    {

        // Is it a ball?
        BallTracker ball = ballRef.GetComponent<BallTracker>();
        if (ball != null)
        {
            // Search for ball in struct
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].ballIndex == ball.ballIndex)
                {
                    return i;
                }
            }

            // Add the ball to the struct list if missing
            balls.Add(new Ball(ballRef.gameObject, ball.ballIndex));
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
