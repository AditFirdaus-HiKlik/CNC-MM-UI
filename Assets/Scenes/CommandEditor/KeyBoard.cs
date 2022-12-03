using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoard : MonoBehaviour
{
    public GameObject[] boards;
    public int currentBoard = 0;

    public void SwitchBoard(int board)
    {
        if (board < 0) board = boards.Length - 1;
        if (board > boards.Length - 1) board = 0;

        boards[currentBoard].SetActive(false);
        boards[board].SetActive(true);

        currentBoard = board;
    }

    public void SwitchOffset(int offset) => SwitchBoard(currentBoard + offset);
}
