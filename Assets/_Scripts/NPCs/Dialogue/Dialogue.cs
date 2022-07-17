using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    [Header("For Writing Text")]
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    public int index;

    // Update is called once per frame
    void Update()
    {
        CheckForNextLine();
    }

    public void CheckForNextLine()
    {
        if (Input.GetMouseButtonDown(0) /*|| Input.GetKeyDown(KeyCode.E)*/)//The condition we want to start playing the dialouge so probably talking to an npc... 
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }
    
    IEnumerator TypeLine()
    {
        foreach (char character in lines[index].ToCharArray())
        {
            textComponent.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    public void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
