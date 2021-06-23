using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Filo;
using System;

public class SpawnCable : MonoBehaviour
{
    [SerializeField] private GameObject cableObj;    
    [SerializeField] private CableBody[] bodies;
    [SerializeField] private bool startMode = false;
    [SerializeField] private bool triggerMode = false;

    private Cable cable;

    private void Awake()
    {
        cable = cableObj.GetComponent<Cable>();
    }

    void Start()
    {
        //if (cable == null) cable = GetComponent<Cable>();
        if (startMode) StartCoroutine("DelayedSpawn");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerMode && other.gameObject.name == "MainCharacter")
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        cable.links = new Cable.Link[bodies.Length];

        int i = 0;
        foreach (CableBody body in bodies)
        {
            Cable.Link link = new Cable.Link();

            if (body.GetType() == typeof(CableDisc))
            {
                link.type = Cable.Link.LinkType.Rolling;
            }
            else
            {
                link.type = Cable.Link.LinkType.Attachment;
            }

            link.body = body;

            cable.links[i] = link;
            i++;
        }        

        Instantiate(cableObj);
    }

    private IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(5f);

        Spawn();
    }
}
