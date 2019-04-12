using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITableController : MonoBehaviour
{
    public GameObject tableRow;
    public GameObject tableCell;
    private Text[,] textCells;
    private string[,] texts;

    public string[,] Texts { get => texts; set => texts = value; }

    public void CreateTable()
    {
        textCells = new Text[texts.GetLength(0), texts.GetLength(1)];
        for (int i = 0; i < texts.GetLength(0); ++i)
        {
            GameObject row = Instantiate(tableRow, transform);
            for (int j = 0; j < texts.GetLength(1); ++j)
            {
                textCells[i, j] = Instantiate(tableCell, row.transform).GetComponentInChildren <Text>().GetComponent<Text>();
                textCells[i, j].text = texts[i, j];
            }
        }
    }
    public void UpdateTable()
    {
        for (int i = 0; i < texts.GetLength(0); ++i)
        {
            for (int j = 0; j < texts.GetLength(1); ++j)
            {
                textCells[i, j].text = texts[i, j];
            }
        }
    }
}
