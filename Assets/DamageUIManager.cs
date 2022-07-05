using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUIManager : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;
    public CanvasGroup myGroup;

    // Update is called once per frame
    void Update()
    {
        DisplayBlood();
    }
    public void DisplayBlood()
    {
        if(playerHealth.currentRegenTimer > 0 && playerHealth.tookDamage)
        {
            //myGroup.alpha = 1;
            myGroup.alpha -= Time.deltaTime / 3;
        }
    }
}
