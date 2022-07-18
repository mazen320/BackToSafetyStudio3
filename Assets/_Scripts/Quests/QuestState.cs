using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class QuestState
{
    public abstract void StartState(Quest quest);
    public abstract void UpdateState(Quest quest);
}
