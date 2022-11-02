using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peca : MonoBehaviour
{
    public PecaPlayer MoveRules;
    public int Row;
    public int Column;
    private CellController cell;
    public float velocityMove;
    public void Start()
    {
        UpdateActualPosition();
    }

    public void UpdateActualPosition()
    {
        cell = transform.parent.GetComponent<CellController>();
        MoveRules.ActualPosition.RowMove = Row = cell.Row;
        MoveRules.ActualPosition.ColumMove = Column = cell.Column;
    }
}
