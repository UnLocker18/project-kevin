using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersonalityObject : Interactable
{

    public event Action<string> Personalities;

    [SerializeField] public bool child = false;
    [SerializeField] public bool boy = false;
    [SerializeField] public bool senior = false;
    private string personality;

    private void Start()
    {
        isInteractable = true;

        if (this.child == true)
        {
            this.personality = "child";
            this.boy = false;
            this.senior = false;
        }

        if (this.boy == true)
        {
            this.personality = "boy";
            this.child = false;
            this.senior = false;
        }
        if (this.senior == true)
        {
            this.personality = "senior";
            this.boy = false;
            this.child = false;
        }
    }

    public void ChangePersonality(string personality)
    { 

        //if (personality == "child")
        
        CheckPersonalities();

    }


    public void CheckPersonalities()
    {
        if (Personalities != null)
        {
            Personalities.Invoke(this.personality);
        }
    }


    
    public override void Interact(Transform mainCharacter)
    {

        ChangePersonality(this.personality);
        //Grab(mainCharacter);




    }
}
