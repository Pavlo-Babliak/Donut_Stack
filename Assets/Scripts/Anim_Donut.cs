using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Anim_Donut : MonoBehaviour
{
    public RuntimeAnimatorController[] Donut_Anim = new RuntimeAnimatorController[6];
    void Start()
    {
        GetComponent<Animator>().enabled=false;
    }
    public void Play_Anim_Donut(GameObject new_pos) 
    {
        if (System.Convert.ToDouble(new_pos.transform.position.x.ToString("F2")) < System.Convert.ToDouble(gameObject.transform.parent.position.x.ToString("F2")) && System.Convert.ToDouble(new_pos.transform.position.y.ToString("F2")) == System.Convert.ToDouble(gameObject.transform.parent.position.y.ToString("F2"))) {  GetComponent<Animator>().runtimeAnimatorController = Donut_Anim[6];  }
        else if (System.Convert.ToDouble(new_pos.transform.position.x.ToString("F2")) > System.Convert.ToDouble(gameObject.transform.parent.position.x.ToString("F2")) && System.Convert.ToDouble(new_pos.transform.position.y.ToString("F2")) == System.Convert.ToDouble(gameObject.transform.parent.position.y.ToString("F2"))) {  GetComponent<Animator>().runtimeAnimatorController = Donut_Anim[5]; }
        else if (System.Convert.ToDouble(new_pos.transform.position.x.ToString("F2")) == System.Convert.ToDouble(gameObject.transform.parent.position.x.ToString("F2")) && System.Convert.ToDouble(new_pos.transform.position.z.ToString("F2")) > System.Convert.ToDouble(gameObject.transform.parent.position.z.ToString("F2"))) {  GetComponent<Animator>().runtimeAnimatorController = Donut_Anim[4];  }
        else if(System.Convert.ToDouble(new_pos.transform.position.x.ToString("F2")) == System.Convert.ToDouble(gameObject.transform.parent.position.x.ToString("F2")) && System.Convert.ToDouble(new_pos.transform.position.z.ToString("F2")) < System.Convert.ToDouble(gameObject.transform.parent.position.z.ToString("F2"))) {  GetComponent<Animator>().runtimeAnimatorController = Donut_Anim[3];  }
        GetComponent<Animator>().enabled = true;
    }
    public void Anim_Add_Donut(int Count) 
    {
        GetComponent<Animator>().runtimeAnimatorController = Donut_Anim[Count];
        GetComponent<Animator>().enabled = true;
    }
    public void Stop_Anim() 
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Animator>().Rebind();
    }
}
