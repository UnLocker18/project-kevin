using System.Collections;
using UnityEngine;

public class MenuAnimations : MonoBehaviour
{
    private int randomPersonality;
    // Start is called before the first frame update
    void Start()
    {
        randomPersonality = Random.Range(0, 2);
        
        transform.GetChild(randomPersonality).gameObject.SetActive(true);

        if (randomPersonality == 2)
        {
            StartCoroutine("SpoAnimation");
        }        
    }

    private IEnumerator SpoAnimation()
    {
        Animator animator = transform.GetChild(2).GetComponent<Animator>();
        
        int i = 0;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            animator.SetInteger("SpoMove", i);

            if (i == 15) i = 0;
            i++;
        }
    }
}
