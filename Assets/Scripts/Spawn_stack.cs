using System.Collections;
using TMPro;
using UnityEngine;

public class Spawn_stack : MonoBehaviour
{
    public GameObject Stack_Spawn;
    public GameObject[] Donut_Spawn = new GameObject[6];
    private GameObject hit1_1, score;
    GameObject wreckClone;
    public GameObject Stack;
    private Vector3 Move_position;
    public Material[] Donuts_mat = new Material[6];
    private bool Spawn_compleat, Move, Move_start, Compleat_move_bool;
    private double dist = 10;
    public bool Running_alg, Respawn, move, Wait;
    int Score_all;
    int m;
    int All_Donuts;
    public float T;
    public GameObject UI_Fail;
    public void Rest() { PlayerPrefs.DeleteAll(); Application.LoadLevel(0); }
    public void Start()
    {
        Application.targetFrameRate = 60;
        score = GameObject.Find("ScoreCounter").gameObject;
        Score_all = score.GetComponent<Score_all>().Score_;
        if (GameObject.Find("Board5x7").transform.childCount > 3)
        {
            if (GameObject.Find("Board5x7").transform.GetChild(GameObject.Find("Board5x7").transform.childCount - 1).GetComponent<Stack>().last_donut == false)
            {
                StartCoroutine(Spawn());
            }
        }
        else if (!PlayerPrefs.HasKey("Count_All_Stack")) { StartCoroutine(Spawn()); }
    }
    public IEnumerator Spawn()
    {
        if (Respawn == false && !GameObject.Find("Destroy_Donuts") && Wait == false && Running_alg == false)
        {
            All_Donuts = gameObject.transform.GetComponent<Settings_game>().Row * gameObject.transform.GetComponent<Settings_game>().Columns;
            if ((GameObject.Find("Board5x7").transform.childCount - 3) / All_Donuts < 1)
            {
                wreckClone = Instantiate(Stack_Spawn, new Vector3(0, 0.5f, -4), Quaternion.identity);
                wreckClone.transform.SetParent(GameObject.Find("Board5x7").transform);
                wreckClone.transform.name = "Stack";
                Stack = wreckClone;
                int Count_donuts = 0, Random_count;
                Random_count = Random.Range(0, 100);
                if ((GameObject.Find("Board5x7").transform.childCount - 3) / All_Donuts < 0.5f)
                {
                    if (Random_count <= 33) { Count_donuts = 0; }
                    if (Random_count > 33 && Random_count <= 66) { Count_donuts = 1; }
                    if (Random_count > 66 && Random_count <= 99) { Count_donuts = 2; }
                }
                if ((GameObject.Find("Board5x7").transform.childCount - 3) / All_Donuts >= 0.5f && (GameObject.Find("Board5x7").transform.childCount - 3) / All_Donuts < 0.8f)
                {
                    if (Random_count <= 40) { Count_donuts = 0; }
                    if (Random_count > 40 && Random_count <= 79) { Count_donuts = 1; }
                    if (Random_count > 79 && Random_count <= 99) { Count_donuts = 2; }
                }
                if ((GameObject.Find("Board5x7").transform.childCount - 3) / All_Donuts >= 0.8f)
                {
                    if (Random_count <= 68) { Count_donuts = 0; }
                    if (Random_count > 68 && Random_count <= 98) { Count_donuts = 1; }
                    if (Random_count > 98 && Random_count <= 99) { Count_donuts = 2; }
                }
                for (int i = 0; i <= Count_donuts; i++)
                {
                    Score_all = score.GetComponent<Score_all>().Score_;
                    if (Score_all <= 50) { m = Random.Range(0, 2); }
                    else if (Score_all > 50 && Score_all <= 100) { m = Random.Range(0, 3); }
                    else if (Score_all > 100 && Score_all <= 240) { m = Random.Range(0, 4); }
                    else if (Score_all > 250 && Score_all <= 490) { m = Random.Range(1, 5); }
                    else if (Score_all > 490) { m = Random.Range(2, 6); }
                    GameObject DonutClone = Instantiate(Donut_Spawn[m], new Vector3(wreckClone.transform.position.x, wreckClone.transform.position.y - 0.1f + i * 0.3f, wreckClone.transform.position.z), Donut_Spawn[m].transform.rotation);
                    DonutClone.name = Donut_Spawn[m].name;
                    DonutClone.transform.SetParent(wreckClone.transform);
                    //int k = m;
                    
                    //DonutClone.transform.GetComponent<MeshRenderer>().material = Donuts_mat[m];
                    wreckClone.GetComponent<Stack>().last_donut = true;
                ret:
                    if (wreckClone.transform.childCount == 3)
                    {
                        if (wreckClone.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == wreckClone.transform.GetChild(1).GetComponent<MeshRenderer>().material.name && wreckClone.transform.GetChild(0).GetComponent<MeshRenderer>().material.name == wreckClone.transform.GetChild(2).GetComponent<MeshRenderer>().material.name)
                        {
                            if (Score_all <= 50) { m = Random.Range(0, 2); }
                            else if (Score_all > 50 && Score_all <= 100) { m = Random.Range(0, 3); }
                            else if (Score_all > 100 && Score_all <= 240) { m = Random.Range(0, 4); }
                            else if (Score_all > 240 && Score_all <= 490) { m = Random.Range(1, 5); }
                            else if (Score_all > 490) { m = Random.Range(2, 6); }
                            wreckClone.transform.GetChild(Random.Range(0, 3)).GetComponent<MeshRenderer>().material = Donuts_mat[m];
                            goto ret;
                        }
                    }
                }
                GetComponent<Settings_game>().Save_Game();
            }
            else
            {
                UI_Fail.SetActive(true);
                PlayerPrefs.DeleteAll();
            }
            yield return new WaitForSeconds(1);
        }
        Respawn = true;
        Move_start = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Move == false && !GameObject.Find("Destroy_Donuts") && Wait == false && Running_alg == false && Compleat_move_bool==false)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < 5; i++)
                {
                    GameObject.Find("ColliderCubes").transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
                    if (hit.transform.name == "Row" + System.Convert.ToString(i))
                    {
                        hit.transform.GetChild(0).gameObject.SetActive(true);
                        Move_position = hit.transform.position;
                    }
                }
            }
            Spawn_compleat = true;
        }
        if (Wait == false)
        { StartCoroutine(Start_move()); }
        if (Input.GetMouseButtonUp(0) && Spawn_compleat == true && Move == false && Wait == false && Running_alg == false && Compleat_move_bool == false)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject.Find("ColliderCubes").transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
            }
            wreckClone = Stack;
            RaycastHit hit1;
            Ray ray1 = new Ray(wreckClone.transform.position, wreckClone.transform.forward);
            if (Physics.Raycast(ray1, out hit1))
            {
                hit1_1 = hit1.transform.gameObject;
                dist = Vector3.Distance(new Vector3(0, 0, Stack.transform.position.z), new Vector3(0, 0, hit1_1.transform.position.z));
                if (dist >= 1.5f)
                {
                    Move = true;
                }
                Spawn_compleat = false;
            }
        }
        if (Move == true && Move_start == true)
        {
            RaycastHit hit1;
            Ray ray1 = new Ray(wreckClone.transform.position, wreckClone.transform.forward);
            if (Physics.Raycast(ray1, out hit1))
            {
                hit1_1 = hit1.transform.gameObject;
            }
            Stack.transform.position = Vector3.MoveTowards(Stack.transform.position, new Vector3(Stack.transform.position.x, Stack.transform.position.y, hit1_1.transform.position.z - 1), 0.3f);
            wreckClone.GetComponent<Stack>().lock_donut = true;
            Running_alg = true;
            if (Vector3.Distance(new Vector3(0, 0, Stack.transform.position.z), new Vector3(0, 0, hit1_1.transform.position.z)) <= 1.00001f)
            {
                Move = false;
                Respawn = false;
                wreckClone.GetComponent<Stack>().last_donut = false;
                wreckClone.GetComponent<Stack>().lock_donut = false;
                wreckClone.GetComponent<Stack>().start_alg = true;
                wreckClone.GetComponent<Stack>().Step_0();
                Running_alg = false;
                Score_all = GameObject.Find("ScoreCounter").GetComponent<Score_all>().Score_;
                StartCoroutine(Spawn()); 
                
            }
        }
    }
    IEnumerator Start_move()
    {
        if (Wait == false)
        {
            if (Stack)
            {
                while (Vector3.Distance(Stack.transform.position, new Vector3(Move_position.x, Stack.transform.position.y, Stack.transform.position.z)) > 0.001f)
                {
                    Stack.transform.position = Vector3.MoveTowards(Stack.transform.position, new Vector3(Move_position.x, Stack.transform.position.y, Stack.transform.position.z), 0.2f);
                    yield return new WaitForSeconds(0.015f);
                }
            }
            else StartCoroutine(Spawn());
            Move_start = true;
        }
    }
    public void Save() { StartCoroutine(Save_game()); }
    public IEnumerator Save_game()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Settings_game>().Save_Game();
    }
    public void Compleat_move(float Timer)
    {
        StartCoroutine(Compleat_move1(Timer));
    }
    public IEnumerator Compleat_move1(float Timer)
    {
    r:
        Compleat_move_bool = true;
       Timer += 1.3f;
        if (Running_alg == false)
        {
            while (Timer > 0)
            {
                Timer -= Time.deltaTime;
                T = Timer;
            }
            if (Timer <= 0) 
            {
                for (int i = 2; i < GetComponent<Settings_game>().Combo_text_X.Length; i++)
                {
                    if (score.GetComponent<Score_all>().Combo_score / 5 == i)
                    {
                        GameObject.Find("Combo_text").GetComponent<TextMeshProUGUI>().text = GetComponent<Settings_game>().Combo_text_X[i];
                        GameObject.Find("Combo_text").GetComponent<Animator>().enabled = true;
                    }
                }
                score.GetComponent<Score_all>().Combo_score = 0;
                Compleat_move_bool = false;
            }
        }
        else 
        {
            yield return new WaitForSeconds(0.5f);
            goto r;
        }

        
    }
}
