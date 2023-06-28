using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LapLine : MonoBehaviour
{
    [SerializeField]
    private bool isFinishLine;


    public UnityEvent OnFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IKartTrigger trigger))
        {
            KartTrigger kart = (KartTrigger) trigger;
            Player p = kart.Player;


            if (isFinishLine && p.LineCount > 0)
            {
                return;
            }

            p.CrossedLine(isFinishLine);

        }
    }
}
