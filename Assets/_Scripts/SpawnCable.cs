using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Filo;

public class SpawnCable : MonoBehaviour
{
    [SerializeField] private CableBody[] bodies;
    
    void Start()
    {
        StartCoroutine("DelayedSpawn");    
    }

    private IEnumerator DelayedSpawn()
    {
        Cable cable = GetComponent<Cable>();
        cable.enabled = false;

        yield return new WaitForSeconds(5f);        

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
                
        cable.enabled = true;
    }
}
