using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MatrizController : MonoBehaviour
{
    public int Rows;
    public int Columns;

    public GameObject Linha, Coluna;
    public static List<CellController> MatrixCells = new List<CellController>();

    private void Start()
    {
        UpdateMatrixCells();
    }


    [ContextMenu("Tabuleiro/Criar Matriz")]
    public void CreateMatrixContext()
    {
        ClearMatrix();
        for(int x=0; x < Rows; x++)
        {
            GameObject row = Instantiate(Linha, Vector3.zero, Quaternion.identity, transform);
            row.name = $"Linha {x+1}";
            for(int y=0; y<Columns; y++)
            {
                GameObject column = Instantiate(Coluna, Vector3.zero, Quaternion.identity, row.transform);
                column.name = $"Coluna {y+1}";
                column.transform.GetChild(0).GetComponent<TMP_Text>().text = $"L{x+1}C{y+1}";
                CellController cell = column.GetComponent<CellController>();
                cell.Row = x;
                cell.Column = y;
            }
        }
    } 
    public void ClearMatrix()
    {
        foreach(Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    public void UpdateMatrixCells()
    {
        foreach(Transform row in transform)
            foreach(Transform column in row)
                MatrixCells.Add(column.GetComponent<CellController>());
    }
}
