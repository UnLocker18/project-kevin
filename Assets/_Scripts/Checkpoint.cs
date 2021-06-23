using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    public int personality;
    private CheckpointManager checkpointManager;
    private UIManager uiManager;

    private void Start()
    {
        checkpointManager = FindObjectOfType<CheckpointManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint");
            checkpointManager.lastCheckpointPos = other.transform.position;
            checkpointManager.personality = personality;
            if (!activated && !checkpointManager.respawned)
            {
                uiManager.CheckpointReached();
                activated = true;
            }
        }
    }
}
