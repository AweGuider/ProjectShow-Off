using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4Controller : MonoBehaviour
{
    [Header("Buttons Pressed")]
    [SerializeField]
    private bool _square;
    [SerializeField]
    private bool _cross;
    [SerializeField]
    private bool _circle;
    [SerializeField]
    private bool _triangle;
    [SerializeField]
    private bool _l1;
    [SerializeField]
    private bool _r1;
    [SerializeField]
    private bool _l2;
    [SerializeField]
    private bool _r2;
    [SerializeField]
    private bool _share;
    [SerializeField]
    private bool _options;

    [Header("Acceleration & Gyroscope")]
    //Gyroscope m_Gyro;
    //Vector3 m_Acceleration;
    float m_Gyro;
    float m_Acceleration;
    // Start is called before the first frame update
    void Start()
    {
        foreach (string s in Input.GetJoystickNames())
        {
            Debug.Log(s);

        }
        SetButtons();

        //m_Gyro = Input.gyro;
        //m_Gyro.enabled = true;

        //m_Acceleration = Input.acceleration;


    }

    //This is a legacy function, check out the UI section for other ways to create your UI
    void OnGUI()
    {

        m_Gyro = Input.GetAxis("Gyroscope");

        m_Acceleration = Input.GetAxis("Acceleration");
        ////Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        //GUI.Label(new Rect(500, 300, 200, 40), "Gyro rotation rate " + m_Gyro.rotationRate);
        //GUI.Label(new Rect(500, 350, 200, 40), "Gyro attitude" + m_Gyro.attitude);
        //GUI.Label(new Rect(500, 400, 200, 40), "Gyro enabled : " + m_Gyro.enabled);

        ////Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        //GUI.Label(new Rect(500, 500, 200, 40), $"Acceleration X: {m_Acceleration.x}, Y: {m_Acceleration.y}, Z: {m_Acceleration.z}");

        //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        GUI.Label(new Rect(500, 300, 200, 40), $"Gyro: {m_Gyro}");

        //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        GUI.Label(new Rect(500, 400, 200, 40), $"Acceleration: {m_Acceleration}");
    }

    private void SetButtons()
    {
        _square = Input.GetButtonDown("Square");
        _cross = Input.GetButtonDown("Cross");
        _circle = Input.GetButtonDown("Circle");
        _triangle = Input.GetButtonDown("Triangle");
        _l1 = Input.GetButtonDown("L1");
        _r1 = Input.GetButtonDown("R1");
        _l2 = Input.GetButtonDown("L2");
        _r2 = Input.GetButtonDown("R2");
        _share = Input.GetButtonDown("Share");
        _options = Input.GetButtonDown("Options");
    }
    private void UpdateButtons()
    {
        _square = Input.GetButtonDown("Square");
        _cross = Input.GetButtonDown("Cross");
        _circle = Input.GetButtonDown("Circle");
        _triangle = Input.GetButtonDown("Triangle");
        _l1 = Input.GetButtonDown("L1");
        _r1 = Input.GetButtonDown("R1");
        _l2 = Input.GetButtonDown("L2");
        _r2 = Input.GetButtonDown("R2");
        _share = Input.GetButtonDown("Share");
        _options = Input.GetButtonDown("Options");
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateButtons();
        if (_square)
        {
            Debug.Log("PRESSED Square");
        }

    }
}
