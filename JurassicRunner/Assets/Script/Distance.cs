using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDistance
{
    void Add(float pDistance);
    float Get();
    string GetAffiche();
}

public class Distance : MonoBehaviour, IDistance
{
    [SerializeField]
    private float Value = 0;

    void Awake()
    {
        ServicesLocator.RegisterService<IDistance>(this);
    }

    public Distance()
    {

        Value = 0;
    }

    public string GetAffiche()
    {
        string distanceAffiche = "Distance : " + Value.ToString() + " m";
        return distanceAffiche;
    }

    public float Get()
    {
        float distance = Value;
        return distance;
    }

    public void Add(float pDistance)
    {
        Value = (float)Math.Floor(pDistance);
        PlayerPrefs.SetFloat("DistanceTransmise", Value);
    }


}