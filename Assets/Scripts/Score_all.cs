using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score_all : MonoBehaviour
{
    public int Score_, Combo_score;
    GameObject camera_, paret_Stack, cam_render;
    public GameObject UI_Destroy, UI_Unlock_Donut;
    bool Score_250, Score_500, Blue_Unlock, Brown_Unlock, Purple_Unlock, Pink_Unlock;
    GameObject prog_bar;
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Score: " + Score_;
        camera_ = GameObject.Find("Main Camera");
        paret_Stack = GameObject.Find("Board5x7").gameObject;
        prog_bar = GameObject.Find("Progress_bar").gameObject;
        cam_render = GameObject.Find("Camera_Render_Texture").gameObject;
        prog_bar.GetComponent<Slider>().maxValue = 50;
        if (PlayerPrefs.GetInt("Blue_Unlock") == 1) 
        {
            prog_bar.GetComponent<Slider>().minValue = 50;
            prog_bar.GetComponent<Slider>().maxValue = 100; 
            prog_bar.GetComponent<Slider>().value = Score_; 
            cam_render.transform.GetChild(0).transform.position = new Vector3(cam_render.transform.GetChild(0).transform.position.x - 1.2f, cam_render.transform.GetChild(0).transform.position.y, cam_render.transform.GetChild(0).transform.position.z);
        }
        if (PlayerPrefs.GetInt("Brown_Unlock") == 1) 
        { 
            prog_bar.GetComponent<Slider>().minValue = 100;
            prog_bar.GetComponent<Slider>().maxValue = 250;
            prog_bar.GetComponent<Slider>().value = Score_;
            cam_render.transform.GetChild(0).transform.position = new Vector3(cam_render.transform.GetChild(0).transform.position.x - 2.4f, cam_render.transform.GetChild(0).transform.position.y, cam_render.transform.GetChild(0).transform.position.z);
        }
        if (PlayerPrefs.GetInt("Purple_Unlock") == 1) 
        {
            prog_bar.GetComponent<Slider>().minValue = 250;
            prog_bar.GetComponent<Slider>().maxValue = 500; 
            prog_bar.GetComponent<Slider>().value = Score_;
            cam_render.transform.GetChild(0).transform.position = new Vector3(cam_render.transform.GetChild(0).transform.position.x - 3.6f, cam_render.transform.GetChild(0).transform.position.y, cam_render.transform.GetChild(0).transform.position.z);
        }
        if (PlayerPrefs.GetInt("Pink_Unlock") == 1) 
        {
            prog_bar.GetComponent<Slider>().minValue = 500; 
            prog_bar.GetComponent<Slider>().maxValue = 5000; 
            prog_bar.GetComponent<Slider>().value = Score_;
            cam_render.transform.GetChild(0).transform.position = new Vector3(cam_render.transform.GetChild(0).transform.position.x - 4.8f, cam_render.transform.GetChild(0).transform.position.y, cam_render.transform.GetChild(0).transform.position.z);
        }
    }
    public void Show_Score()
    {
        Score_ += 10 + Combo_score;
        Combo_score += 5;
        prog_bar.GetComponent<Slider>().value = Score_;
        GetComponent<TextMeshProUGUI>().text = "Score: " + Score_;
        if (Score_ >= 50 && Score_ < 100 && Blue_Unlock == false && PlayerPrefs.GetInt("Blue_Unlock") == 0)
        {
            prog_bar.GetComponent<Slider>().minValue = 50;
            prog_bar.GetComponent<Slider>().maxValue = 100;
            prog_bar.GetComponent<Slider>().value = 50;
            UI_Unlock_Donut.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Unlock Blue Donut";
            UI_Unlock_Donut.SetActive(true);
            Blue_Unlock = true;
            camera_.GetComponent<Spawn_stack>().Respawn = true;
            camera_.GetComponent<Spawn_stack>().Wait = true;
            PlayerPrefs.SetInt("Blue_Unlock", 1);
        }
        if (Score_ >= 100 && Score_ < 250 && Brown_Unlock == false && PlayerPrefs.GetInt("Brown_Unlock") == 0)
        {
            prog_bar.GetComponent<Slider>().minValue = 100;
            prog_bar.GetComponent<Slider>().maxValue = 250;
            prog_bar.GetComponent<Slider>().value = 100;
            UI_Unlock_Donut.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Unlock Brown Donut";
            UI_Unlock_Donut.SetActive(true);
            Brown_Unlock = true;
            camera_.GetComponent<Spawn_stack>().Respawn = true;
            camera_.GetComponent<Spawn_stack>().Wait = true;
            PlayerPrefs.SetInt("Brown_Unlock", 1);
        }
        if (Score_ >= 250 && Score_ < 500 && Purple_Unlock == false && PlayerPrefs.GetInt("Purple_Unlock") == 0)
        {
            prog_bar.GetComponent<Slider>().minValue = 250;
            prog_bar.GetComponent<Slider>().maxValue = 500;
            prog_bar.GetComponent<Slider>().value = 250;
            UI_Unlock_Donut.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Unlock Purple Donut";
            UI_Unlock_Donut.SetActive(true);
            Purple_Unlock = true;
            camera_.GetComponent<Spawn_stack>().Respawn = true;
            camera_.GetComponent<Spawn_stack>().Wait = true;
            PlayerPrefs.SetInt("Purple_Unlock", 1);
        }
        if (Score_ >= 500 && Pink_Unlock == false && PlayerPrefs.GetInt("Pink_Unlock") == 0)
        {
            prog_bar.GetComponent<Slider>().minValue = 500;
            prog_bar.GetComponent<Slider>().maxValue = 5000;
            prog_bar.GetComponent<Slider>().value = 500;
            prog_bar.transform.parent.GetChild(3).gameObject.SetActive(false);
            UI_Unlock_Donut.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Unlock Pink Donut";
            UI_Unlock_Donut.SetActive(true);
            Pink_Unlock = true;
            camera_.GetComponent<Spawn_stack>().Respawn = true;
            camera_.GetComponent<Spawn_stack>().Wait = true;
            PlayerPrefs.SetInt("Pink_Unlock", 1);
        }
        if (Score_ >= 250 && Score_ < 500 && Score_250 == false && PlayerPrefs.GetInt("Score_250") == 0)
        {
            UI_Destroy.SetActive(true);
            UI_Destroy.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Destroy Red Donuts";
            Destroy(paret_Stack.transform.GetChild(paret_Stack.transform.childCount - 1).gameObject);
            camera_.GetComponent<Spawn_stack>().Respawn = true;
            camera_.GetComponent<Spawn_stack>().Wait = true;
            Score_250 = true;
        }
        if (Score_ >= 500 && Score_500 == false && PlayerPrefs.GetInt("Score_500") == 0)
        {
            UI_Destroy.SetActive(true);
            UI_Destroy.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Destroy Yellow Donuts";
            Destroy(paret_Stack.transform.GetChild(paret_Stack.transform.childCount - 1).gameObject);
            camera_.GetComponent<Spawn_stack>().Respawn = true;
            camera_.GetComponent<Spawn_stack>().Wait = true;
            Score_500 = true;
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        camera_.GetComponent<Spawn_stack>().Wait = false;
        camera_.GetComponent<Spawn_stack>().Respawn = false;
        camera_.GetComponent<Spawn_stack>().Running_alg = false;
        camera_.GetComponent<Spawn_stack>().Start();
    }
    public void Destroy_Donuts()
    {
        if (Score_ >= 250 && Score_ < 500)
        {
            for (int i = 2; i < paret_Stack.transform.childCount; i++)
            {
                int Count_dots = paret_Stack.transform.transform.GetChild(i).transform.childCount;
                for (int j = 0; j < Count_dots; j++)
                {
                    if (paret_Stack.transform.GetChild(i).transform.GetChild(j).GetComponent<MeshRenderer>().material.name == camera_.GetComponent<Spawn_stack>().Donuts_mat[0].name + " (Instance)")
                    {
                        Destroy(paret_Stack.transform.GetChild(i).gameObject);
                    }
                }
            }
            PlayerPrefs.SetInt("Score_250", 1);
        }
        else if (Score_ >= 500)
        {
            for (int i = 2; i < paret_Stack.transform.childCount; i++)
            {
                int Count_dots = paret_Stack.transform.transform.GetChild(i).transform.childCount;
                for (int j = 0; j < Count_dots; j++)
                {
                    if (paret_Stack.transform.GetChild(i).transform.GetChild(j).GetComponent<MeshRenderer>().material.name == camera_.GetComponent<Spawn_stack>().Donuts_mat[1].name + " (Instance)")
                    {
                        Destroy(paret_Stack.transform.GetChild(i).gameObject);
                    }
                }
            }
            PlayerPrefs.SetInt("Score_500", 1);
        }
        StartCoroutine(wait());
        UI_Destroy.SetActive(false);
    }
    public void Unlock_Donut()
    {
        cam_render.transform.GetChild(0).transform.position = new Vector3(cam_render.transform.GetChild(0).transform.position.x - 1.2f, cam_render.transform.GetChild(0).transform.position.y, cam_render.transform.GetChild(0).transform.position.z);
        StartCoroutine(wait());
        UI_Unlock_Donut.SetActive(false);

    }
}
