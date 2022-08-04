using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestUi : MonoBehaviour
{
    public QuestManager QuestManager;


    public GameObject c_objective1;
    public GameObject c_objective2;


    public string objective_a;
    public string objective_b;



    TextMeshProUGUI objective1Text;
    TextMeshProUGUI objective2Text;
    
    void Start()
    {

        objective1Text = c_objective1.GetComponent<TextMeshProUGUI>();
        objective2Text = c_objective2.GetComponent<TextMeshProUGUI>();

    }

    
    void Update()
    {
        if(QuestManager.objective1 == true)
        {


        objective1Text.text = objective_a;



        }
        if(QuestManager.objective1 && QuestManager.objective2 == true)
        {
            Destroy(c_objective1);
        objective2Text.text = objective_b;


        }

        

    }

}