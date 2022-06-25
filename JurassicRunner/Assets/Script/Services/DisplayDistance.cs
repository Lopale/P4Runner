using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayDistance : MonoBehaviour
{
    public TextMeshProUGUI DistanceUI;
    public string distanceAffiche;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IDistance distance = ServicesLocator.GetService<IDistance>();
        DistanceUI.text = distance.GetAffiche();
    }
}
