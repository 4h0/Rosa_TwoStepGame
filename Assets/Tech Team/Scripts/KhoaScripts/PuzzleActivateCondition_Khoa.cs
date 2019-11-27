using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleActivateCondition_Khoa : MonoBehaviour
{
    public GameObject[] puzzleList;

    public int totalNUmberofQuestCompleted, whichPuzzle;

    private void Awake()
    {
        foreach(GameObject tempGameObject in puzzleList)
        {
            tempGameObject.SetActive(false);
        }

        totalNUmberofQuestCompleted = 0;
    }

    public void PuzzleActivationCheck()
    {
        totalNUmberofQuestCompleted++;

        if(totalNUmberofQuestCompleted > 4)
        {
            ActivatePuzzle();
        }
    }

    private void ActivatePuzzle()
    {
        totalNUmberofQuestCompleted -= 4;

        puzzleList[whichPuzzle].SetActive(true);
    }
}
