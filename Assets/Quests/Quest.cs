using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Quest
{
    public enum Progress {NOT_AVAILABLE,AVAILABLE,ACCEPT,COMPLETE,DONE, REFUSED };
    public string title; 
    public int id;     
    public Progress progress; 
    public string description;
    public int nextQuest;

    public string questObjective;  
    public int questObjectiveCount; 
    public int questObjectiveRequirement; 

    public int moralitate;
    public int goldReward;
    public int itemReward;

}
