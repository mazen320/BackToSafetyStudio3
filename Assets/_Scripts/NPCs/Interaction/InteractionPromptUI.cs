using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    //Script for the interaction pop-ups whenever the player goes near to an NPC. Attached to "Interaction Canvas" on the player...
    private Camera mainCam;
    public GameObject UIPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    public bool isDisplayed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        UIPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (isDisplayed)
        {
            var cameraRotation = mainCam.transform.rotation;
            transform.LookAt(transform.position + cameraRotation * Vector3.forward, cameraRotation * Vector3.up);
        }

    }
    public void OpenPanel(string promptText)
    {
        this.promptText.text = promptText;
        UIPanel.SetActive(true);
        isDisplayed = true;
    }
    public void ClosePanel()
    {
        isDisplayed = false;
        UIPanel.SetActive(false);
    }
}
