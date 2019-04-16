using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class FirstPlayerCamera : MonoBehaviour
{
    public float sensivity = 150f;
    public Transform body;
    private float xAxisClamp;
    
    public bool undetected;

    public bool fanOn;
    public bool fanTrig;

    public bool cardTrig;
/*    public GameObject GameOver;

    public Text start;
    public GameObject ogjDoor;

    public bool takeCard;*/
    
    /*public float step;*/
    //public ParticleSystem ps;
/*    public Text PressE;*/
    
    void Start()
    {
        LookCursor();
        xAxisClamp = 0.0f;
        /*step = 2.0f;*/
        /*PressE.gameObject.SetActive(false);*/
        //ps.Stop();
        //ps.gameObject.SetActive(false);
    }
    
    /*private void Update()
    {
        Invoke("offStart", 5);
        if (fanTrig)
        {
            if (Input.GetKey(KeyCode.E) && !fanOn)
            {
                ps.gameObject.SetActive(true);
                ps.Play();
                fanOn = true;
            }
            else if (Input.GetKey(KeyCode.E) && fanOn)
            {
                ps.Stop();
                ps.gameObject.SetActive(false);
                fanOn = false;
            }
        }

        if (cardTrig)
        {
            if (Input.GetKey(KeyCode.E))
            {
                PressE.gameObject.SetActive(false);
                takeCard = true;
            }
        }
        //Debug.Log(step);
    }*/

    void LookCursor() { Cursor.lockState = CursorLockMode.Locked; }
    
    void FixedUpdate() { CameraRotation(); }

    void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity * Time.fixedDeltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        body.Rotate(Vector3.up * mouseX);
    }

    void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotatuon = transform.eulerAngles;
        eulerRotatuon.x = value;
        transform.eulerAngles = eulerRotatuon;
    }

/*    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("finish"))
        {
            start.gameObject.SetActive(true);
            start.text = "You win!!!";
            GameOver.SetActive(true);
            Invoke("restart", 5);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag.Equals("door") && takeCard)
        {
            ogjDoor.SetActive(false);
            PressE.gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag.Equals("door") && takeCard)
            PressE.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag.Equals("fan"))
        {
            PressE.gameObject.SetActive(true);
            fanTrig = true;
        }

        if (other.gameObject.name.Equals("cardTrig"))
        {
            PressE.gameObject.SetActive(true);
            cardTrig = true;
        }
        if (other.gameObject.tag.Equals("ps"))
            step = 0.1f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("fan"))
        {
            fanTrig = false;
            PressE.gameObject.SetActive(false);
        }
        if (other.gameObject.name.Equals("cardTrig"))
        {
            PressE.gameObject.SetActive(false);
            cardTrig = false;
        }
        if (other.gameObject.tag.Equals("ps"))
            step = 2.0f;
    }

    void offStart()
    {
        start.gameObject.SetActive(false);
    }

    void restart()
    {
        Application.LoadLevel("SampleScene");
    }*/
}
