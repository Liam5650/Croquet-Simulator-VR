using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMonitor : MonoBehaviour
{
    public PortalManager manager;
    public PortalManager.FramePosition position;

    private void OnTriggerEnter(Collider other)
    {
        manager.FrameEntered(position, other);
    }

    private void OnTriggerExit(Collider other)
    {
        manager.FrameExited(position, other);
    }
}
