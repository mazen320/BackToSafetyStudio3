using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIManager : MonoBehaviour
{
    [SerializeField]
    private Text magazineText;
    [SerializeField]
    private Text reservesText;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateShotsLeft(int magazine)
    {
        magazineText.text = magazine.ToString();
    }
    public void UpdateReservesLeft(int reserves)
    {
        reservesText.text = reserves.ToString();
    }
}
