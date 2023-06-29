using KartGame.KartSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerID;

    public Checkpoint Checkpoint;
    public ArcadeKart Kart;

    [SerializeField] private int lapCount;
    [SerializeField] private int maxLapCount;
    public int LineCount;
    [SerializeField] int MaxLineCount;
    public bool CanFinish;
    public bool HasFinished;

    [SerializeField] private float cantMoveDelay = .5f;

    public event Action Finished;

    private void Start()
    {
        if (Kart == null) Kart = GetComponent<ArcadeKart>();
        lapCount = 1;
        maxLapCount = GameData.Instance.AmountOfLaps;
        if (maxLapCount == 1) CanFinish = true;
    }

    public void SetCheckpoint(Checkpoint cp)
    {
        // Set checkpoint position
        if (Checkpoint != cp) Checkpoint = cp;
        //Debug.Log($"Tried to set cp");

    }
    public void Respawn()
    {
        // Reset position to saved checkpoint
        // (Additional) Set velocity/speed to zero so the player doesnt respawn with super high speed
        if (Checkpoint != null && !HasFinished) StartCoroutine(PauseMovement());

    }

    IEnumerator PauseMovement()
    {
        Kart.SetCanMove(false);
        yield return new WaitForSeconds(cantMoveDelay / 20f);
        Kart.ResetRigidbody();
        yield return new WaitForSeconds(cantMoveDelay);
        Kart.SetRotation(Checkpoint.CheckpointRotation);
        Kart.gameObject.transform.position = Checkpoint.CheckpointPosition;
        Kart.SetCanMove(true);
    }

    /*
     * Returns true if crossed Finish Line
     */
    public bool CrossedLine(bool isFinish)
    {
        if (HasFinished) return false;
        if (isFinish)
        {
            if (CanFinish)
            {
                // Need to set player place / save it
                StopMovement();
                if (!GameData.Instance.LeaderboardList.Contains(PlayerID)) GameData.Instance.LeaderboardList.Add(PlayerID);
                Finished?.Invoke();
                HasFinished = true;
                return true;
            }
            if (LineCount <= 0)
            {
                lapCount++;
                LineCount = MaxLineCount;

            }
            CanFinish = lapCount == maxLapCount;
            return true;
        }

        LineCount--;
        return false;

    }

    private void StopMovement()
    {
        Kart.SetCanMove(false);
    }

    private void OnValidate()
    {
        LineCount = MaxLineCount;
    }
}
