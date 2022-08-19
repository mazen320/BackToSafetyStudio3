using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestUi : MonoBehaviour
{
    public QuestManager QuestManager;
    public Waypoint Waypoint;


    public GameObject phone;
    public GameObject npc1;
    public GameObject ammoBox;
    public GameObject gunShop;
    public GameObject house;
    public GameObject church;
    public GameObject storageYard;

    ///////////////////////////////////
    public GameObject objectiveCanvas;
    public GameObject c_objective0;
    public GameObject c_objective1;
    public GameObject c_objective2;
    public GameObject c_objective3;
    public GameObject c_objective4;
    public GameObject c_objective5;
    public GameObject c_objective6;
    public GameObject c_objective7;


    public string objective_0;
    public string objective_1;
    public string objective_2;
    public string objective_3;
    public string objective_4;
    public string objective_5;
    public string objective_6;
    public string objective_7;


    TextMeshProUGUI objective0Text;
    TextMeshProUGUI objective1Text;
    TextMeshProUGUI objective2Text;
    TextMeshProUGUI objective3Text;
    TextMeshProUGUI objective4Text;
    TextMeshProUGUI objective5Text;
    TextMeshProUGUI objective6Text;
    TextMeshProUGUI objective7Text;



    private void Awake()
    {
        objective0Text = c_objective0.GetComponent<TextMeshProUGUI>();
        objective0Text.text = objective_0;

    }
    void Start()
    {
        QuestManager = Camera.main.GetComponent<QuestManager>();

        objectiveCanvas = GameObject.Find("objectives canvas");
        objective1Text = c_objective1.GetComponent<TextMeshProUGUI>();
        objective2Text = c_objective2.GetComponent<TextMeshProUGUI>();
        objective3Text = c_objective3.GetComponent<TextMeshProUGUI>();
        objective4Text = c_objective4.GetComponent<TextMeshProUGUI>();
        objective5Text = c_objective5.GetComponent<TextMeshProUGUI>();
        objective6Text = c_objective6.GetComponent<TextMeshProUGUI>();
        objective7Text = c_objective7.GetComponent<TextMeshProUGUI>();


    }


    void Update()
    {
        if (QuestManager.objective1 == true)
        {

            Destroy(c_objective0);

            objective1Text.text = objective_1;


            if (QuestManager.objective2 == true)
            {

                Destroy(c_objective1);



                objective2Text.text = objective_2;


            }

            if (QuestManager.objective3 == true)
            {

                Destroy(c_objective2);


                objective3Text.text = objective_3;


            }

            if (QuestManager.objective4 == true)
            {

                Destroy(c_objective3);


                objective4Text.text = objective_4;


            }

            if (QuestManager.objective5 == true)
            {

                Destroy(c_objective4);

                objective5Text.text = objective_5;


            }

            if (QuestManager.objective6 == true)
            {

                Destroy(c_objective5);

                objective6Text.text = objective_6;


            }

            if (QuestManager.objective7 == true)
            {

                Destroy(c_objective6);

                objective7Text.text = objective_7;


            }


            if (QuestManager.objective8 == true)
            {

                Destroy(c_objective7);
                Destroy(objectiveCanvas);




            }
        }



    }

}