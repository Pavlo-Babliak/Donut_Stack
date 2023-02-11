using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Stack : MonoBehaviour
{
    public void Anim_Donut() 
    {
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).GetComponent<Anim_Donut>().Anim_Add_Donut(2);
        for (int i=gameObject.transform.childCount-2; i >= 0; i--) 
        {
            gameObject.transform.GetChild(i).GetComponent<Anim_Donut>().Anim_Add_Donut(i);
        }
    }
    public void Stop_anim() 
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Animator>().Rebind();
        GameObject.Find("Main Camera").GetComponent<Spawn_stack>().Running_alg = false;
        Destroy(gameObject);
    }
}
