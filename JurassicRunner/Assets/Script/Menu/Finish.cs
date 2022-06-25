using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public TextMeshProUGUI DistanceUI;
    // Start is called before the first frame update
    void Start()
    {
        DistanceUI.text = "Distance Parcourue : "+PlayerPrefs.GetFloat("DistanceTransmise").ToString()+" m";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
