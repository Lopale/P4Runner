using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDistance
{
    void Add(float pDistance);
    string Get();
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

    public string Get()
    {
        string distanceAffiche = "Distance : " + Value.ToString() + " m";
        return distanceAffiche;
    }

    public void Add(float pDistance)
    {
        Value = (float)Math.Floor(pDistance);
        Debug.Log(Value);
    }


}