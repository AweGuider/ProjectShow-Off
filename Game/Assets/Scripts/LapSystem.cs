using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LapSystem : MonoBehaviour
{
    [SerializeField]
    public static bool _canFinish;

    [SerializeField]
    private int _setAmountOfCheckpointsLeft;
    public static int amountOfCheckpointsLeft;

    [SerializeField]
    private bool _isFinishLine;


    public UnityEvent OnFinished;
    // Start is called before the first frame update
    void Start()
    {
        if (_setAmountOfCheckpointsLeft != 0) amountOfCheckpointsLeft = _setAmountOfCheckpointsLeft;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IKartTrigger trigger))
        {
            KartTrigger kart = (KartTrigger) trigger;
            Player p = kart.Player;

            if (_isFinishLine && amountOfCheckpointsLeft > 0)
            {
                return;
            }
        }
        if (other.CompareTag("Player"))
        {
            if (_isFinishLine && amountOfCheckpointsLeft > 0)
            {
                return;
            }

            if (_canFinish)
            {
                Debug.Log($"Name: {other.gameObject.name}");

                other.gameObject.GetComponentInParent<ArcadeKart>().SetCanMove(false);
                return;
            }

            amountOfCheckpointsLeft--;
            gameObject.SetActive(false);
            _canFinish = amountOfCheckpointsLeft == 0;
        }
        Debug.Log($"Amount of CP left: {amountOfCheckpointsLeft}");
        Debug.Log($"Can finish? {_canFinish}");
    }
}
