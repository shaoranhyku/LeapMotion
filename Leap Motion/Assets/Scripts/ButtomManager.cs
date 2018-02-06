using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomManager : MonoBehaviour {

    // Referencia al componente Audio Source
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        // Recuperamos la referencia al componente Audio Source
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
            audioSource.Play();
    }
}
