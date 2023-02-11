using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Settings_game : MonoBehaviour
{
    [Header("Save_Stacks")]
    [SerializeField] bool Save;

    [Header("Speed_Donut")]
    [SerializeField] public float Speed_move;
    [SerializeField] public float Speed_move_up;
    [SerializeField] public float Height_move;

    [Header("Board")]
    [SerializeField] public int Row;
    [SerializeField] public int Columns;

    [Header("Combo X"+ "\n"+"Element 0 = Combo X0" + "\n"+"Start from element 2")]
    [SerializeField] public string[] Combo_text_X;
    GameObject Board, wreckClone;
    private void Awake()
    {
        Board = GameObject.Find("Board5x7");
        if (PlayerPrefs.GetInt("Count_All_Stack") > 3)
        {
            Load_Game();
        }

    }
    void Start()
    {
       
    }
    public void Save_Game()
    {
        if (Save == true)
        {
            PlayerPrefs.SetInt("Score", GameObject.Find("ScoreCounter").transform.GetComponent<Score_all>().Score_);
            int k = 0;
            PlayerPrefs.SetInt("Count_All_Stack", GameObject.Find("Board5x7").transform.childCount);
            Board = GameObject.Find("Board5x7");
            for (int i = 3; i < Board.transform.childCount; i++)
            {
                PlayerPrefs.SetFloat("Stack" + i + ".x", Board.transform.GetChild(i).transform.position.x);
                PlayerPrefs.SetFloat("Stack" + i + ".y", Board.transform.GetChild(i).transform.position.y);
                PlayerPrefs.SetFloat("Stack" + i + ".z", Board.transform.GetChild(i).transform.position.z);
                PlayerPrefs.SetInt("Stack"+i+"Count_Dots", Board.transform.GetChild(i).transform.childCount);
                for (int j = 0; j < Board.transform.GetChild(i).transform.childCount; j++)
                {
                    for (int m = 0; m < 6; m++) 
                    {
                        if (Board.transform.GetChild(i).transform.GetChild(j).transform.name == GetComponent<Spawn_stack>().Donut_Spawn[m].name) 
                        {
                            k = m; 
                        }
                    }
                    PlayerPrefs.SetInt("Donut_Material"+i+"Stack"+j+"Mat", k);
                }
            }
        }
    }
    void Load_Game()
    {
        if (Save == true)
        {
            GameObject.Find("ScoreCounter").transform.GetComponent<Score_all>().Score_ = PlayerPrefs.GetInt("Score");
            for (int i = 3; i < PlayerPrefs.GetInt("Count_All_Stack"); i++)
            {
                wreckClone = Instantiate(GetComponent<Spawn_stack>().Stack_Spawn, new Vector3(PlayerPrefs.GetFloat("Stack" + i + ".x"), PlayerPrefs.GetFloat("Stack" + i + ".y"), PlayerPrefs.GetFloat("Stack" + i + ".z")), Quaternion.identity);
                wreckClone.transform.SetParent(Board.transform);
                wreckClone.transform.name = "Stack";
                if (i != PlayerPrefs.GetInt("Count_All_Stack") - 1)
                {
                    wreckClone.GetComponent<Stack>().start_alg = true;
                    wreckClone.GetComponent<Stack>().Step_2 = true;
                    wreckClone.GetComponent<Stack>().lock_donut = false;
                }
                else 
                { 
                    wreckClone.GetComponent<Stack>().last_donut = true;
                    GetComponent<Spawn_stack>().Stack = wreckClone;
                }
                for (int j = 0; j < PlayerPrefs.GetInt("Stack" + i + "Count_Dots"); j++)
                {
                    GameObject DonutClone = Instantiate(GetComponent<Spawn_stack>().Donut_Spawn[PlayerPrefs.GetInt("Donut_Material" + i + "Stack" + j + "Mat")], new Vector3(wreckClone.transform.position.x, wreckClone.transform.position.y - 0.1f + j * 0.3f, wreckClone.transform.position.z), GetComponent<Spawn_stack>().Donut_Spawn[PlayerPrefs.GetInt("Donut_Material" + i + "Stack" + j + "Mat")].transform.rotation);
                    DonutClone.transform.SetParent(wreckClone.transform);
                    DonutClone.name= GetComponent<Spawn_stack>().Donut_Spawn[PlayerPrefs.GetInt("Donut_Material" + i + "Stack" + j + "Mat")].name;
                    // DonutClone.transform.GetComponent<MeshRenderer>().material = GetComponent<Spawn_stack>().Donuts_mat[];
                }
            }
        }
        else PlayerPrefs.DeleteAll();
    }
}
