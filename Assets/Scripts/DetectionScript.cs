using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DetectionScript : MonoBehaviour
{
    public Slider slider;
    public GameObject Attencion;
    public GameObject GameOver;
    public GameObject normal;
    public GameObject att;
    public FirstPlayerCamera fpc;
    public PlayerController pc;

    void Start()
    {
        Attencion.gameObject.SetActive(false);
        GameOver.gameObject.SetActive(false);
        normal.SetActive(true);
        att.SetActive(false);
    }

    void Update()
    {
        if (fpc.undetected)
        {
            slider.value -= 0.3f;
            if (slider.value <= 0)
            {
                slider.value = 0;
                fpc.undetected = false;
            }
        }
        if (slider.value.Equals(0))
            Attencion.gameObject.SetActive(false);
        if (slider.value >= 75)
            Attencion.gameObject.SetActive(true);
        if (slider.value >= 100)
        {
            Attencion.gameObject.SetActive(false);
            GameOver.gameObject.SetActive(true);
            Invoke("RestartLevel", 5);
        }

        if (slider.value.Equals(0))
        {
            normal.SetActive(true);
            att.SetActive(false);
        }
        else
        {
            normal.SetActive(false);
            att.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            fpc.undetected = false;
            slider.value += pc.step;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
            fpc.undetected = true;
    }

    void RestartLevel() { Application.LoadLevel("SampleScene"); }
}
