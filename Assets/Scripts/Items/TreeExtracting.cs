using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeExtracting : MonoBehaviour
{

    public int woodAmount;
    bool chopping;

    public CharacterAnimation playerAnimationScript;
    public PlayerInventoryHolder playerInventoryHolder;

    void Start()
    {
        woodAmount = 20;
    }

    public void StartExtracting()
    {
        playerAnimationScript.Stopped += new StoppingAllAnimations(StopExtracting);
        StartCoroutine("ExtractTree");
    }

    void StopExtracting()
    {
        StopCoroutine("ExtractTree");
        playerAnimationScript.Stopped -= new StoppingAllAnimations(StopExtracting);
    }

    IEnumerator ExtractTree()
    {
        while (woodAmount > 0)
        {
            yield return new WaitForSeconds(1f);
            woodAmount -= 10;

        }
        playerAnimationScript.Stopped -= new StoppingAllAnimations(StopExtracting);
        playerAnimationScript.StopAllAnimations();
        yield return null; 
    }

    void Update()
    {

    }

}
