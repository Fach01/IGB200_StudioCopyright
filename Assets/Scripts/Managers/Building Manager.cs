using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public string buildingname;
    public int BudgetPoolLevel1 = 0;
    public int BudgetPoolLevel2 = 0;
    public int BudgetPoolLevel3 = 0;
    public void Start()
    {
        int level1budget = PlayerPrefs.GetInt(buildingname + "level1", BudgetPoolLevel1);
        BudgetPoolLevel1 = level1budget;
        int level2budget = PlayerPrefs.GetInt(buildingname + "level2", BudgetPoolLevel2);
        BudgetPoolLevel2 = level2budget;
        int level3budget = PlayerPrefs.GetInt(buildingname + "level2", BudgetPoolLevel3);
        BudgetPoolLevel3 = (level3budget);
    }

}
