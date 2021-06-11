using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaginaryCharacter : MonoBehaviour
{

    //public GameObject environmentInteractionsGO;
    [SerializeField] private PersonalityObject personalityObject;




    // Start is called before the first frame update
    void Start()
    {
        //environmentInteractions = gameObject.GetComponent<EnvironmentInteractions>();
        this.gameObject.GetComponent<Renderer>().enabled = false;
        //personalityObject = GetComponent<PersonalityObject>();
       
        personalityObject.Personalities += appear;
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void appear(string personality)
    {
        if (string.IsNullOrEmpty(personality))
        {
            throw new System.ArgumentException($"'{nameof(personality)}' non può essere null o vuoto.", nameof(personality));
        }

        if (personality == "child")
            this.gameObject.GetComponent<Renderer>().enabled = true;
        //else
        if (personality=="boy")
            this.gameObject.GetComponent<Renderer>().enabled = false;

    }

}
