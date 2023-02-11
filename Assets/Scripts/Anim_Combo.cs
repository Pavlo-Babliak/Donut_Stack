using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Combo : MonoBehaviour
{
    public void Stop() 
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Animator>().Rebind();
    }
}
