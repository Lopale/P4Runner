using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{ 
    //La 1�re chose est donc de d�clarer 3 AudioClip que j'assignerai via l'inspecteur
    public AudioClip soundExplode;
    //Cr�ation d'une variable afin de garder une r�f�rence au composant AudioSource
    private AudioSource _audioSource;

    private ParticleSystem Explode;
    // Start is called before the first frame update
    void Start()
    {
        //On r�cup�re le composant AudioSource du gameObject et on l'assigne � la variable pr�vue � cet effet
        _audioSource = GetComponent<AudioSource>();

        Explode = GameObject.Find("Explode").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Fence avec " + other.name);

        Explode.Play();

        _audioSource.clip = soundExplode;       //Ceci permet d'assigner le soundTouch comme AudioClip par d�faut 
        _audioSource.Play();

    }
}
