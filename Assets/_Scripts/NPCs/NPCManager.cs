using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [Header("References")]
    public Dialogue dialogue;
    
    [Header("Conditions To Write Text")]
    public GameObject player;
    public int distanceOffset;
    public bool playerInRange;//If the player is near npc and 'talking' to it.
    public Dialogue[] NPCs = new Dialogue[2];
   

    // Start is called before the first frame update
    void Start()
    {
        Dialogue npc1 = NPCs[1];
        Dialogue npc2 = NPCs[2];

        npc1.lines = new string[] { "Hello I am the level 1 NPC", "I will help you through the game" };
        npc2.lines = new string[] { "I am the level 2 NPC", "How may I assist you" };
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceToPlayer();
    }
    public void CheckDistanceToPlayer()
    {
        for (int i = 0; i < NPCs.Length; i++)
        {
            if (Vector3.Distance(NPCs[i].transform.position, player.transform.position) < distanceOffset)
            {
               /* PlayerScript playerMovement = player.GetComponent<PlayerScript>();
                playerMovement.enabled = false;*/
                NPCs[i].StartDialogue();

            }
        }
    }
   
}
