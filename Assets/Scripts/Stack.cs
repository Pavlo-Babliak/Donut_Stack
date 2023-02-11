using System.Collections;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public bool last_donut, start_alg, Move;
    private GameObject hit1_1;
    public bool[] Count_hit = new bool[4];
    public double dist = 10;
    int k;
    public bool lock_donut, Step_2;
    GameObject Score_g, camera_;
    private void Start()
    {
        camera_ = GameObject.Find("Main Camera").gameObject;
        Score_g = GameObject.Find("ScoreCounter").gameObject;
    }
    private void Update()
    {
        if (camera_.GetComponent<Spawn_stack>().Running_alg == false && last_donut == false && Step_2 == true)
        {
            RaycastHit hit1;
            Ray ray1 = new Ray(gameObject.transform.position, gameObject.transform.forward * 10);
            if (Physics.Raycast(ray1, out hit1))
            {
                hit1_1 = hit1.transform.gameObject;
                dist = Vector3.Distance(new Vector3(0, 0, gameObject.transform.position.z), new Vector3(0, 0, hit1_1.transform.position.z));
                if (dist > 1.0001f)
                {
                    lock_donut = true;
                    Move = true;
                }
                else
                {
                    Move = false;
                    lock_donut = false;
                }
            }
            if (Move == true && Step_2 == true)
            {
                if (hit1_1 != null)
                {
                    lock_donut = true;
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, hit1_1.transform.position.z - 1), 0.1f);
                    if (Vector3.Distance(transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, hit1_1.transform.position.z - 1)) < 0.001f)
                    {
                        Move = false;
                        lock_donut = false;
                        camera_.GetComponent<Spawn_stack>().Running_alg = false;
                    }
                }
            }


            if (Step_2 == true && Move == false)
            {
                RaycastHit hit_Left;
                Ray ray_Left = new Ray(new Vector3(gameObject.transform.position.x - 0.7f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.right * -1);
                if (Physics.Raycast(ray_Left, out hit_Left, 0.5f) && camera_.GetComponent<Spawn_stack>().Running_alg == false)
                {
                    Ray_hit(hit_Left.transform.gameObject);
                    k = 2;
                }
                else Count_hit[2] = true;

                RaycastHit hit_Right;
                Ray ray_Right = new Ray(new Vector3(gameObject.transform.position.x + 0.7f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.right);
                if (Physics.Raycast(ray_Right, out hit_Right, 0.5f) && camera_.GetComponent<Spawn_stack>().Running_alg == false)
                {
                    Ray_hit(hit_Right.transform.gameObject);
                    k = 3;
                }
                else Count_hit[3] = true;

                RaycastHit hit_back;
                Ray ray_Back = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.7f), gameObject.transform.forward * -1);
                if (Physics.Raycast(ray_Back, out hit_back, 0.5f) && camera_.GetComponent<Spawn_stack>().Running_alg == false)
                {
                    Ray_hit(hit_back.transform.gameObject);
                    k = 1;
                }
                else Count_hit[1] = true;

                RaycastHit hit_Forward;
                Ray ray_Forward = new Ray(gameObject.transform.position, gameObject.transform.forward);
                if (Physics.Raycast(ray_Forward, out hit_Forward, 1) && camera_.GetComponent<Spawn_stack>().Running_alg == false)
                {
                    Ray_hit(hit_Forward.transform.gameObject);
                    k = 0;
                }
                else Count_hit[0] = true;
                if (Count_hit[0] == true && Count_hit[1] == true && Count_hit[2] == true && Count_hit[3] == true && last_donut == false)
                {
                    start_alg = true;
                }
            }
        }
    }
    public void Step_0()
    {
        RaycastHit hit_Forward;
        Ray ray_Forward = new Ray(gameObject.transform.position, gameObject.transform.forward);
        RaycastHit hit_Left;
        Ray ray_Left = new Ray(new Vector3(gameObject.transform.position.x - 0.7f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.right * -1);
        RaycastHit hit_Right;
        Ray ray_Right = new Ray(new Vector3(gameObject.transform.position.x + 0.7f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.right);
        if (Physics.Raycast(ray_Forward, out hit_Forward, 1) && hit_Forward.transform.name == "Stack" && hit_Forward.transform.childCount != 0 && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
        {
            if (gameObject.transform.childCount != 3)
            {
                if (gameObject.transform.childCount == 1 && hit_Forward.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Forward.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != hit_Forward.transform.GetChild(0).GetComponent<MeshRenderer>().material.name)
                {
                    StartCoroutine(IF_1(hit_Forward.transform.gameObject));
                }
                else if (hit_Forward.transform.childCount == 3 && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                {
                    StartCoroutine(IF_1(hit_Forward.transform.gameObject));
                }
                else if (Physics.Raycast(ray_Left, out hit_Left, 1) && hit_Left.transform.name == "Stack" && hit_Left.transform.childCount != 0 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                {
                    if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.childCount == 3 && hit_Left.transform.childCount == 2 && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_6(hit_Forward.transform.gameObject, hit_Left.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.childCount == 3 && hit_Left.transform.childCount == 2 && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Left.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.childCount == 2 && hit_Left.transform.childCount == 3 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Forward.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.childCount == 3 && hit_Left.transform.childCount == 3 && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Left.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.childCount == 3 && hit_Left.transform.childCount == 3 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Forward.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.childCount == 2 && hit_Left.transform.childCount == 3 && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_6(hit_Left.transform.gameObject, hit_Forward.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                    {
                        if (hit_Forward.transform.childCount == 1) { StartCoroutine(IF_1(hit_Forward.transform.gameObject)); }
                        else if (hit_Left.transform.childCount == 1) { StartCoroutine(IF_1(hit_Left.transform.gameObject)); }
                        else if (hit_Forward.transform.childCount == 2 && hit_Forward.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Forward.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.childCount == 2 && hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != hit_Left.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                        {
                            StartCoroutine(IF_1(hit_Left.transform.gameObject));
                        }
                        else if (hit_Forward.transform.childCount == 2 && hit_Forward.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != hit_Forward.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.childCount == 2 && hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Left.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                        {
                            StartCoroutine(IF_1(hit_Forward.transform.gameObject));
                        }
                        else Step_2 = true;
                    }
                    else if (hit_Left.transform.childCount == 2 && hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Left.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_2(hit_Left.transform.gameObject));
                    }
                    else if (hit_Forward.transform.childCount == 2 && hit_Forward.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Forward.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_2(hit_Forward.transform.gameObject));
                    }
                    else if (hit_Forward.transform.childCount >= 2 && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 1).GetComponent<MeshRenderer>().material.name != hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && hit_Left.transform.childCount >= 2 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 1).GetComponent<MeshRenderer>().material.name != hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && gameObject.transform.childCount == 1)
                    {
                        StartCoroutine(IF_3(hit_Forward.transform.gameObject, hit_Left.transform.gameObject));
                    }
                    else if (hit_Forward.transform.childCount == 1) { StartCoroutine(Step_one(hit_Forward.transform.gameObject, hit_Left.transform.gameObject)); }
                    else if (hit_Left.transform.childCount == 1) { StartCoroutine(Step_one(hit_Left.transform.gameObject, hit_Forward.transform.gameObject)); }
                    else Step_2 = true;
                }
                else if (Physics.Raycast(ray_Right, out hit_Right, 1) && hit_Right.transform.name == "Stack" && hit_Right.transform.childCount != 0 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                {
                    if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.childCount == 3 && hit_Right.transform.childCount == 2 && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_6(hit_Forward.transform.gameObject, hit_Right.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.childCount == 3 && hit_Right.transform.childCount == 2 && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Right.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.childCount == 2 && hit_Right.transform.childCount == 3 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Forward.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.childCount == 3 && hit_Right.transform.childCount == 3 && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Right.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.childCount == 3 && hit_Right.transform.childCount == 3 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Forward.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Forward.transform.childCount == 2 && hit_Right.transform.childCount == 3 && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_6(hit_Right.transform.gameObject, hit_Forward.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                    {
                        if (hit_Forward.transform.childCount == 1) { StartCoroutine(IF_1(hit_Forward.transform.gameObject)); }
                        else if (hit_Right.transform.childCount == 1) { StartCoroutine(IF_1(hit_Right.transform.gameObject)); }
                        else if (hit_Forward.transform.childCount == 2 && hit_Forward.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Forward.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.childCount == 2 && hit_Right.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != hit_Right.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                        {
                            StartCoroutine(IF_1(hit_Right.transform.gameObject));
                        }
                        else if (hit_Forward.transform.childCount == 2 && hit_Forward.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != hit_Forward.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.childCount == 2 && hit_Right.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Right.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                        {
                            StartCoroutine(IF_1(hit_Forward.transform.gameObject));
                        }
                        else Step_2 = true;
                    }
                    else if (hit_Right.transform.childCount == 2 && hit_Right.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Right.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_2(hit_Right.transform.gameObject));
                    }
                    else if (hit_Forward.transform.childCount == 2 && hit_Forward.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Forward.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_2(hit_Forward.transform.gameObject));
                    }
                    else if (hit_Forward.transform.childCount >= 2 && hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 1).GetComponent<MeshRenderer>().material.name != hit_Forward.transform.GetChild(hit_Forward.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && hit_Right.transform.childCount >= 2 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 1).GetComponent<MeshRenderer>().material.name != hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && gameObject.transform.childCount == 1)
                    {
                        StartCoroutine(IF_3(hit_Forward.transform.gameObject, hit_Right.transform.gameObject));
                    }
                    else if (hit_Forward.transform.childCount == 1) { StartCoroutine(Step_one(hit_Forward.transform.gameObject, hit_Right.transform.gameObject)); }
                    else if (hit_Right.transform.childCount == 1) { StartCoroutine(Step_one(hit_Right.transform.gameObject, hit_Forward.transform.gameObject)); }
                    else Step_2 = true;
                }
                else Step_2 = true;
            }
            else if (gameObject.transform.childCount == 3 && hit_Forward.transform.childCount == 1 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Forward.transform.GetChild(0).GetComponent<MeshRenderer>().material.name)
            {
                if (Physics.Raycast(ray_Left, out hit_Left, 1) && hit_Left.transform.name == "Stack" && hit_Left.transform.childCount == 2 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                {
                    StartCoroutine(IF_2(hit_Left.transform.gameObject));
                }
                else Step_2 = true;
            }
            else if (gameObject.transform.childCount == 3 && hit_Forward.transform.childCount == 2 && gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.name == hit_Forward.transform.GetChild(0).GetComponent<MeshRenderer>().material.name && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Forward.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
            {
                if (Physics.Raycast(ray_Left, out hit_Left, 1) && hit_Left.transform.name == "Stack" && hit_Left.transform.childCount == 1 && hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                {
                    StartCoroutine(IF_2(hit_Forward.transform.gameObject));
                }
                else Step_2 = true;
            }
            else if (gameObject.transform.childCount == 3 && hit_Forward.transform.childCount == 1 && gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.name == hit_Forward.transform.GetChild(0).GetComponent<MeshRenderer>().material.name)
            {
                if (Physics.Raycast(ray_Right, out hit_Right, 1) && hit_Right.transform.name == "Stack" && hit_Right.transform.childCount == 2 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                {
                    StartCoroutine(IF_2(hit_Right.transform.gameObject));
                }
                else Step_2 = true;
            }
            else Step_2 = true;
        }
        else if (Physics.Raycast(ray_Left, out hit_Left, 1) && hit_Left.transform.name == "Stack" && hit_Left.transform.childCount != 0  && hit_Left.transform.GetChild(hit_Left.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
        {
            if (gameObject.transform.childCount != 3)
            {
                if (gameObject.transform.childCount == 1 && hit_Left.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Left.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name)
                {
                    StartCoroutine(IF_1(hit_Left.transform.gameObject));
                }
                else if (hit_Left.transform.childCount == 3 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                {
                    StartCoroutine(IF_1(hit_Left.transform.gameObject));
                }
                else if (Physics.Raycast(ray_Right, out hit_Right, 1) && hit_Right.transform.name == "Stack" && hit_Right.transform.childCount != 0 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                {
                    if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.childCount == 3 && hit_Right.transform.childCount == 2 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_6(hit_Left.transform.gameObject, hit_Right.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.childCount == 3 && hit_Right.transform.childCount == 2 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Right.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.childCount == 2 && hit_Right.transform.childCount == 3 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Left.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.childCount == 3 && hit_Right.transform.childCount == 3 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Right.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.childCount == 3 && hit_Right.transform.childCount == 3 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Left.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                    {
                        if (hit_Left.transform.childCount == 1) { StartCoroutine(IF_1(hit_Left.transform.gameObject)); }
                        else if (hit_Right.transform.childCount == 1) { StartCoroutine(IF_1(hit_Right.transform.gameObject)); }
                        else if (hit_Left.transform.childCount == 2 && hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Left.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.childCount == 2 && hit_Right.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != hit_Right.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                        {
                            StartCoroutine(IF_1(hit_Right.transform.gameObject));
                        }
                        else if (hit_Left.transform.childCount == 2 && hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != hit_Left.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.childCount == 2 && hit_Right.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Right.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                        {
                            StartCoroutine(IF_1(hit_Left.transform.gameObject));
                        }
                        else Step_2 = true;
                    }
                    else if (hit_Left.transform.childCount == 2 && hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Left.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_2(hit_Left.transform.gameObject));
                    }
                    else if (hit_Left.transform.childCount >= 2 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 1).GetComponent<MeshRenderer>().material.name != hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && hit_Right.transform.childCount >= 2 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 1).GetComponent<MeshRenderer>().material.name != hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && gameObject.transform.childCount == 1)
                    {
                        StartCoroutine(IF_3(hit_Left.transform.gameObject, hit_Right.transform.gameObject));
                    }
                    else if (hit_Left.transform.childCount == 1) { StartCoroutine(Step_one(hit_Left.transform.gameObject, hit_Right.transform.gameObject)); }
                    else if (hit_Right.transform.childCount == 1) { StartCoroutine(Step_one(hit_Right.transform.gameObject, hit_Left.transform.gameObject)); }
                    else Step_2 = true;
                }
                else Step_2 = true;
            }
            else if (gameObject.transform.childCount == 3 && hit_Left.transform.childCount == 1 && gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.name == hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name)
            {
                if (Physics.Raycast(ray_Right, out hit_Right, 1) && hit_Right.transform.name == "Stack" && hit_Right.transform.childCount == 2 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                {
                    StartCoroutine(IF_2(hit_Right.transform.gameObject));
                }
                else Step_2 = true;
            }
            else if (gameObject.transform.childCount == 3 && hit_Left.transform.childCount == 2 && gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.name == hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name && hit_Left.transform.GetChild(1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
            {
                if (Physics.Raycast(ray_Right, out hit_Right, 1) && hit_Right.transform.name == "Stack" && hit_Right.transform.childCount == 1 && hit_Right.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name )
                {
                    StartCoroutine(IF_2(hit_Left.transform.gameObject));
                }
                else Step_2 = true;
            }
            else Step_2 = true;
        }
        else if (Physics.Raycast(ray_Right, out hit_Right, 1) && hit_Right.transform.name == "Stack" && hit_Right.transform.childCount != 0  && hit_Right.transform.GetChild(hit_Right.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
        {
            if (gameObject.transform.childCount != 3)
            {
                if (gameObject.transform.childCount == 1 && hit_Right.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Right.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != hit_Right.transform.GetChild(0).GetComponent<MeshRenderer>().material.name)
                {
                    StartCoroutine(IF_1(hit_Right.transform.gameObject));
                }
                else if (hit_Right.transform.childCount == 3 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                {
                    StartCoroutine(IF_1(hit_Right.transform.gameObject));
                }
                else if (Physics.Raycast(ray_Left, out hit_Left, 1) && hit_Left.transform.name == "Stack" && hit_Left.transform.childCount != 0 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                {
                    if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.childCount == 3 && hit_Left.transform.childCount == 2 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_6(hit_Right.transform.gameObject, hit_Left.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.childCount == 3 && hit_Right.transform.childCount == 2 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Right.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.childCount == 2 && hit_Right.transform.childCount == 3 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Left.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.childCount == 3 && hit_Right.transform.childCount == 3 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Right.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.childCount == 3 && hit_Right.transform.childCount == 3 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_1(hit_Left.transform.gameObject));
                    }
                    else if (gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                    {
                        if (hit_Right.transform.childCount == 1) { StartCoroutine(IF_1(hit_Right.transform.gameObject)); }
                        else if (hit_Left.transform.childCount == 1) { StartCoroutine(IF_1(hit_Left.transform.gameObject)); }
                        else if (hit_Left.transform.childCount == 2 && hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Left.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.childCount == 2 && hit_Right.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != hit_Right.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                        {
                            StartCoroutine(IF_1(hit_Right.transform.gameObject));
                        }
                        else if (hit_Left.transform.childCount == 2 && hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != hit_Left.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && hit_Right.transform.childCount == 2 && hit_Right.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Right.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                        {
                            StartCoroutine(IF_1(hit_Left.transform.gameObject));
                        }
                        else Step_2 = true;
                    }
                    else if (hit_Right.transform.childCount == 2 && hit_Right.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == hit_Right.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_2(hit_Right.transform.gameObject));
                    }
                    else if (hit_Left.transform.childCount >= 2 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 1).GetComponent<MeshRenderer>().material.name != hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && hit_Right.transform.childCount >= 2 && hit_Right.transform.GetChild(hit_Right.transform.childCount - 1).GetComponent<MeshRenderer>().material.name != hit_Right.transform.GetChild(hit_Right.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && gameObject.transform.childCount == 1)
                    {
                        StartCoroutine(IF_3(hit_Right.transform.gameObject, hit_Left.transform.gameObject));
                    }
                    else if (hit_Right.transform.childCount == 1) { StartCoroutine(Step_one(hit_Right.transform.gameObject, hit_Left.transform.gameObject)); }
                    else if (hit_Left.transform.childCount == 1) { StartCoroutine(Step_one(hit_Left.transform.gameObject, hit_Right.transform.gameObject)); }
                    else Step_2 = true;
                }
                else Step_2 = true;
            }
            else if (gameObject.transform.childCount == 3 && hit_Right.transform.childCount == 1 && gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.name == hit_Right.transform.GetChild(0).GetComponent<MeshRenderer>().material.name)
            {
                if (Physics.Raycast(ray_Left, out hit_Left, 1) && hit_Left.transform.name == "Stack" && hit_Left.transform.childCount == 2 && hit_Left.transform.GetChild(hit_Left.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name && hit_Left.transform.GetChild(hit_Left.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                {
                    StartCoroutine(IF_2(hit_Left.transform.gameObject));
                }
                else Step_2 = true;
            }
            else if (gameObject.transform.childCount == 3 && hit_Right.transform.childCount == 2 && gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.name == hit_Right.transform.GetChild(0).GetComponent<MeshRenderer>().material.name && hit_Right.transform.GetChild(1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
            {
                if (Physics.Raycast(ray_Left, out hit_Left, 1) && hit_Left.transform.name == "Stack" && hit_Left.transform.childCount == 1 && hit_Left.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                {
                    StartCoroutine(IF_2(hit_Right.transform.gameObject));
                }
                else Step_2 = true;
            }
            else Step_2 = true;
        }
        else Step_2 = true;
    }
    void Ray_hit(GameObject hit)
    {
        if (hit.transform.name == "Stack" && hit.transform.childCount != 0 && hit.transform.GetComponent<Stack>().last_donut == false && hit.transform.GetComponent<Stack>().Step_2 == true && Step_2 == true && gameObject.transform.childCount != 0 && Move == false )
        {
            if (hit.transform.childCount == 1 && transform.childCount == 3 && hit.transform.GetChild(hit.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(gameObject.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && hit.transform.GetChild(hit.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(gameObject.transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
            {
                StartCoroutine(IF_5(hit.transform.gameObject));
            }
            else if (hit.transform.GetChild(hit.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(gameObject.transform.childCount - 1).GetComponent<MeshRenderer>().material.name && gameObject.transform.childCount != 3 && gameObject.transform.childCount != 0 && hit.transform.childCount != 0)
            {
                if (start_alg == true && hit.transform.gameObject)
                {
                    if (hit.transform.childCount >= 2 && hit.transform.childCount != 3 && hit.transform.GetChild(hit.transform.childCount - 2).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(gameObject.transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
                    {
                        StartCoroutine(IF_4(hit.transform.gameObject));
                    }
                    else
                    {
                        lock_donut = true;
                        if (hit.transform.childCount == 3 && hit.transform.GetChild(hit.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == hit.transform.GetChild(hit.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && gameObject.transform.childCount == 2 && gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name != gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                        {
                            hit.transform.GetComponent<Stack>().lock_donut = false;
                            lock_donut = false;
                            goto Fin;
                        }
                        camera_.GetComponent<Spawn_stack>().Running_alg = true;
                        StartCoroutine(Move_Donut(hit.transform.gameObject));
                        hit.transform.GetComponent<Stack>().start_alg = false;
                        if (Vector3.Distance(hit.transform.GetChild(hit.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) <= 0.001f)
                        {
                            if (hit.transform.childCount > 0)
                            {
                                if (hit.transform.GetChild(hit.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(gameObject.transform.childCount - 2).GetComponent<MeshRenderer>().material.name && gameObject.transform.childCount != 3)
                                {
                                    if (start_alg == true && hit.transform.gameObject)
                                    {
                                        camera_.GetComponent<Spawn_stack>().Running_alg = true;
                                        StartCoroutine(Move_Donut(hit.transform.gameObject));
                                    }
                                }
                                else hit.transform.GetComponent<Stack>().start_alg = true;
                            }
                        }
                    Fin:
                        Count_hit[k] = false;
                    }
                }
            }
        }
    }
    public IEnumerator IF_1(GameObject obj_child_1)
    {
        camera_.GetComponent<Spawn_stack>().Running_alg = true;
        obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(gameObject);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, camera_.GetComponent<Settings_game>().Height_move, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position = Vector3.MoveTowards(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.1f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.SetParent(gameObject.transform);
        gameObject.transform.GetComponent<Anim_Stack>().Anim_Donut();
        if (obj_child_1.transform.childCount == 0) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(obj_child_1); }
        if (obj_child_1.transform.childCount == 3) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(obj_child_1); }
        obj_child_1.GetComponent<Stack>().lock_donut = false;
        gameObject.GetComponent<Stack>().lock_donut = false;
        obj_child_1.transform.GetComponent<Stack>().start_alg = true;
        start_alg = true;
        Step_2 = true;
        camera_.GetComponent<Spawn_stack>().Running_alg = false;
        camera_.GetComponent<Spawn_stack>().Save();
        if (gameObject.transform.childCount == 3 && transform.GetChild(0).GetComponent<MeshRenderer>().material.name == transform.GetChild(1).GetComponent<MeshRenderer>().material.name && transform.GetChild(0).GetComponent<MeshRenderer>().material.name == transform.GetChild(2).GetComponent<MeshRenderer>().material.name) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(gameObject); }
    }
    public IEnumerator IF_2(GameObject obj_child_1)
    {
        camera_.GetComponent<Spawn_stack>().Running_alg = true;
        transform.GetChild(gameObject.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(obj_child_1);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y + 0.3f, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y + 0.3f, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.SetParent(obj_child_1.transform);
        obj_child_1.transform.GetComponent<Anim_Stack>().Anim_Donut();
        if (obj_child_1.transform.childCount == 3) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(obj_child_1); }
        if (obj_child_1.transform.childCount == 0) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(obj_child_1); }
        obj_child_1.GetComponent<Stack>().lock_donut = false;
        gameObject.GetComponent<Stack>().lock_donut = false;
        obj_child_1.transform.GetComponent<Stack>().start_alg = true;
        start_alg = true;
        Step_2 = true;
        camera_.GetComponent<Spawn_stack>().Running_alg = false;
        camera_.GetComponent<Spawn_stack>().Save();
        if (gameObject.transform.childCount == 0) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(gameObject); }
    }
    public IEnumerator IF_3(GameObject obj_child_1, GameObject obj_child_2)
    {
        camera_.GetComponent<Spawn_stack>().Running_alg = true;
        obj_child_1.GetComponent<Stack>().lock_donut = true;
        obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(gameObject);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position = Vector3.MoveTowards(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.SetParent(gameObject.transform);
        gameObject.transform.GetComponent<Anim_Stack>().Anim_Donut();
        obj_child_2.GetComponent<Stack>().lock_donut = true;
        obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(gameObject);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position, new Vector3(obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_2 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position = Vector3.MoveTowards(obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position, new Vector3(obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_2 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.x, obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.y, obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.z), new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.SetParent(gameObject.transform);
        gameObject.transform.GetComponent<Anim_Stack>().Anim_Donut();
        obj_child_1.GetComponent<Stack>().lock_donut = false;
        obj_child_2.GetComponent<Stack>().lock_donut = false;
        gameObject.GetComponent<Stack>().lock_donut = false;
        obj_child_1.transform.GetComponent<Stack>().start_alg = true;
        obj_child_2.transform.GetComponent<Stack>().start_alg = true;
        start_alg = true;
        Step_2 = true;
        camera_.GetComponent<Spawn_stack>().Running_alg = false;
        camera_.GetComponent<Spawn_stack>().Save();
        if (transform.childCount == 3) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(gameObject); }
    }
    public IEnumerator IF_4(GameObject obj_child_1)
    {
        camera_.GetComponent<Spawn_stack>().Running_alg = true;
        transform.GetChild(gameObject.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(obj_child_1);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y + 0.3f, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y + 0.3f, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.SetParent(obj_child_1.transform);
        obj_child_1.transform.GetComponent<Anim_Stack>().Anim_Donut();
        obj_child_1.GetComponent<Stack>().lock_donut = false;
        gameObject.GetComponent<Stack>().lock_donut = false;
        obj_child_1.transform.GetComponent<Stack>().start_alg = true;
        start_alg = true;
        camera_.GetComponent<Spawn_stack>().Running_alg = false;
        if (obj_child_1.transform.childCount == 3) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(obj_child_1.transform.gameObject); }
        camera_.GetComponent<Spawn_stack>().Save();
        if (gameObject.transform.childCount == 0) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(gameObject); }
    }
    public IEnumerator IF_5(GameObject obj_child_1)
    {
        camera_.GetComponent<Spawn_stack>().Running_alg = true;
        transform.GetChild(gameObject.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(obj_child_1);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y + 0.3f, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y + 0.3f, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.SetParent(obj_child_1.transform);
        obj_child_1.transform.GetComponent<Anim_Stack>().Anim_Donut();
        transform.GetChild(gameObject.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(obj_child_1);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y + 0.3f, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y + 0.3f, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.SetParent(obj_child_1.transform);
        obj_child_1.transform.GetComponent<Anim_Stack>().Anim_Donut();
        obj_child_1.GetComponent<Stack>().lock_donut = false;
        gameObject.GetComponent<Stack>().lock_donut = false;
        obj_child_1.transform.GetComponent<Stack>().start_alg = true;
        start_alg = true;
        camera_.GetComponent<Spawn_stack>().Running_alg = false;
        if (obj_child_1.transform.childCount == 3) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(obj_child_1.transform.gameObject); }
        camera_.GetComponent<Spawn_stack>().Save();
    }
    public IEnumerator IF_6(GameObject obj_child_1, GameObject obj_child_2)
    {
        camera_.GetComponent<Spawn_stack>().Running_alg = true;
        transform.GetChild(transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(obj_child_2);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_2 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(new Vector3(transform.GetChild(transform.childCount - 1).transform.position.x, transform.GetChild(transform.childCount - 1).transform.position.y, transform.GetChild(transform.childCount - 1).transform.position.z), new Vector3(obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.x, obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.y + 0.3f, obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_2 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            transform.GetChild(transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(transform.GetChild(transform.childCount - 1).transform.position.x, transform.GetChild(transform.childCount - 1).transform.position.y, transform.GetChild(transform.childCount - 1).transform.position.z), new Vector3(obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.x, obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.y + 0.3f, obj_child_2.transform.GetChild(obj_child_2.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        transform.GetChild(transform.childCount - 1).transform.SetParent(obj_child_2.transform);
        obj_child_2.transform.GetComponent<Anim_Stack>().Anim_Donut();
        obj_child_2.GetComponent<Stack>().lock_donut = false;
        obj_child_2.transform.GetComponent<Stack>().start_alg = true;
        if (obj_child_2.transform.childCount == 3) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(obj_child_2.transform.gameObject); }
        obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(gameObject);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position = Vector3.MoveTowards(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, camera_.GetComponent<Settings_game>().Height_move, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.SetParent(gameObject.transform);
        gameObject.transform.GetComponent<Anim_Stack>().Anim_Donut();
        obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(gameObject);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position = Vector3.MoveTowards(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.SetParent(gameObject.transform);
        gameObject.transform.GetComponent<Anim_Stack>().Anim_Donut();
        obj_child_1.GetComponent<Stack>().lock_donut = false;
        gameObject.GetComponent<Stack>().lock_donut = false;
        obj_child_1.transform.GetComponent<Stack>().start_alg = true;
        start_alg = true;
        Step_2 = true;
        camera_.GetComponent<Spawn_stack>().Running_alg = false;
        camera_.GetComponent<Spawn_stack>().Save();
        if (transform.childCount == 3) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(gameObject); }
    }
   
    public IEnumerator Step_one(GameObject obj_child_1, GameObject obj_child_more)
    {
        camera_.GetComponent<Spawn_stack>().Running_alg = true;
        obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(gameObject);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.position, new Vector3(obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.position.x,camera_.GetComponent<Settings_game>().Height_move, obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_more == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.position = Vector3.MoveTowards(obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.position, new Vector3(obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.position.x, camera_.GetComponent<Settings_game>().Height_move, obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_more == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.position.x, obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.position.y, obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.position.z), new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        obj_child_more.transform.GetChild(obj_child_more.transform.childCount - 1).transform.SetParent(gameObject.transform);
        gameObject.transform.GetComponent<Anim_Stack>().Anim_Donut();
        if (obj_child_more.transform.childCount == 0) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(obj_child_more); }
        transform.GetChild(gameObject.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(obj_child_1);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x,camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y + 0.3f, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y + 0.3f, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.SetParent(obj_child_1.transform);
        obj_child_1.transform.GetComponent<Anim_Stack>().Anim_Donut();
        transform.GetChild(gameObject.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(obj_child_1);
        camera_.GetComponent<Spawn_stack>().Compleat_move(2);
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, camera_.GetComponent<Settings_game>().Height_move, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
            yield return new WaitForSeconds(0.015f);
        }
        while (Vector3.Distance(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position, new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y + 0.3f, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z)) > 0.001f)
        {
            if (gameObject == null || obj_child_1 == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), new Vector3(obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.x, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.y + 0.3f, obj_child_1.transform.GetChild(obj_child_1.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
            yield return new WaitForSeconds(0.015f);
        }
        gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.SetParent(obj_child_1.transform);
        obj_child_1.transform.GetComponent<Anim_Stack>().Anim_Donut();
        if (obj_child_1.transform.childCount == 0) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(obj_child_1); }
        if (obj_child_1.transform.childCount == 3) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(obj_child_1); }
        obj_child_1.GetComponent<Stack>().lock_donut = false;
        if (obj_child_more) { obj_child_more.GetComponent<Stack>().lock_donut = false; }
        gameObject.GetComponent<Stack>().lock_donut = false;
        obj_child_1.transform.GetComponent<Stack>().start_alg = true;
        if (obj_child_more) { obj_child_more.transform.GetComponent<Stack>().start_alg = true; }
        start_alg = true;
        Step_2 = true;
        camera_.GetComponent<Spawn_stack>().Running_alg = false;
        camera_.GetComponent<Spawn_stack>().Save();
        if (gameObject.transform.childCount == 0) { Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(gameObject); }
    }
    public IEnumerator Move_Donut(GameObject obj)
    {
        camera_.GetComponent<Spawn_stack>().Running_alg = true;
        if (gameObject != null && obj != null)
        {
            obj.transform.GetChild(obj.transform.childCount - 1).GetComponent<Anim_Donut>().Play_Anim_Donut(gameObject);
            camera_.GetComponent<Spawn_stack>().Compleat_move(2);
            while (Vector3.Distance(obj.transform.GetChild(obj.transform.childCount - 1).transform.position, new Vector3(obj.transform.GetChild(obj.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, obj.transform.GetChild(obj.transform.childCount - 1).transform.position.z)) > 0.001f)
            {
                if (gameObject == null || obj == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
                obj.transform.GetChild(obj.transform.childCount - 1).transform.position = Vector3.MoveTowards(obj.transform.GetChild(obj.transform.childCount - 1).transform.position, new Vector3(obj.transform.GetChild(obj.transform.childCount - 1).transform.position.x,  camera_.GetComponent<Settings_game>().Height_move, obj.transform.GetChild(obj.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move_up);
                yield return new WaitForSeconds(0.015f);
            }
            while (Vector3.Distance(obj.transform.GetChild(obj.transform.childCount - 1).transform.position, new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z)) > 0.001f)
            {
                if (gameObject == null || obj == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
                obj.transform.GetChild(obj.transform.childCount - 1).transform.position = Vector3.MoveTowards(new Vector3(obj.transform.GetChild(obj.transform.childCount - 1).transform.position.x, obj.transform.GetChild(obj.transform.childCount - 1).transform.position.y, obj.transform.GetChild(obj.transform.childCount - 1).transform.position.z), new Vector3(gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.x, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.y + 0.3f, gameObject.transform.GetChild(gameObject.transform.childCount - 1).transform.position.z), 0.05f * camera_.GetComponent<Settings_game>().Speed_move);
                if (gameObject == null || obj == null) { lock_donut = false; camera_.GetComponent<Spawn_stack>().Running_alg = false; break; }
                yield return new WaitForSeconds(0.015f);
            }
            obj.transform.GetChild(obj.transform.childCount - 1).transform.SetParent(gameObject.transform);
            gameObject.transform.GetComponent<Anim_Stack>().Anim_Donut();
            if (obj.transform.childCount == 0)
            {
                obj.transform.GetComponent<Stack>().lock_donut = false;
                lock_donut = false;
                start_alg = true;
                obj.transform.GetComponent<Stack>().start_alg = true;
                camera_.GetComponent<Spawn_stack>().Running_alg = false;
                StopAllCoroutines();
                Score_g.transform.GetComponent<Score_all>().Show_Score();
                Destroy(obj.transform.gameObject);
            }
            camera_.GetComponent<Spawn_stack>().Save();
            if (gameObject.transform.childCount == 3)
            {
                if (gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.name == gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.name)
                {
                    obj.transform.GetComponent<Stack>().start_alg = true;
                    obj.transform.GetComponent<Stack>().lock_donut = false;
                    camera_.GetComponent<Spawn_stack>().Running_alg = false;
                    StopAllCoroutines();
                    Score_g.transform.GetComponent<Score_all>().Show_Score();
                    Destroy(gameObject);
                }
            }
            if (gameObject.transform.childCount == 0) { obj.transform.GetComponent<Stack>().lock_donut = false; obj.transform.GetComponent<Stack>().start_alg = true; GameObject.Find("Main Camera").GetComponent<Spawn_stack>().Running_alg = false; StopAllCoroutines(); Score_g.transform.GetComponent<Score_all>().Show_Score(); Destroy(gameObject); }
        }
        if (obj.transform.childCount > 0 && obj.transform.GetChild(obj.transform.childCount - 1).GetComponent<MeshRenderer>().material.name == transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.name)
        {
            obj.transform.GetComponent<Stack>().start_alg = false;
        }
        else obj.transform.GetComponent<Stack>().start_alg = true;
        start_alg = true;
        lock_donut = false;
        camera_.GetComponent<Spawn_stack>().Running_alg = false;
        camera_.GetComponent<Spawn_stack>().Save();
    }
}

