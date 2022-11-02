using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour 
    
{
    public bool AvailableToMove;
    public int Row, Column;

    public static Transform peca;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnSelectPeca);
    }

    public void SetAvaiableToMove()
    {
        StartCoroutine(SetAvaiableToMoveRoutine());
    }
    public IEnumerator SetAvaiableToMoveRoutine()
    {
        if(transform.childCount > 1)
        {
            Transform inimigo =  transform.GetChild(1);
            if(inimigo.gameObject.tag == "Inimigo")
            {
                Destroy(inimigo.gameObject);
            }
            
        }

        Debug.Log($"Possibilidade: {transform.parent.name} {name}");
        peca.SetParent(transform);
        yield return new WaitForEndOfFrame();
        peca.GetComponent<Peca>().UpdateActualPosition();
        foreach(CellController c in MatrizController.MatrixCells)
        {
            var cor = Random.Range(0,5);
            if(cor == 1.0f)
            {
                var color = c.GetComponent<Button>().colors;
                color.normalColor = Color.red;
                c.GetComponent<Button>().colors = color;
                c.GetComponent<Button>().onClick.RemoveAllListeners();
                c.GetComponent<Button>().onClick.AddListener(c.OnSelectPeca);
            }
            else if(cor == 2.0f)
            {
                var color = c.GetComponent<Button>().colors;
                color.normalColor = Color.blue;
                c.GetComponent<Button>().colors = color;
                c.GetComponent<Button>().onClick.RemoveAllListeners();
                c.GetComponent<Button>().onClick.AddListener(c.OnSelectPeca);
            }
            else if(cor == 3.0f)
            {
                var color = c.GetComponent<Button>().colors;
                color.normalColor = Color.yellow;
                c.GetComponent<Button>().colors = color;
                c.GetComponent<Button>().onClick.RemoveAllListeners();
                c.GetComponent<Button>().onClick.AddListener(c.OnSelectPeca);
            }
            else
            {
                var color = c.GetComponent<Button>().colors;
                color.normalColor = Color.white;
                c.GetComponent<Button>().colors = color;
                c.GetComponent<Button>().onClick.RemoveAllListeners();
                c.GetComponent<Button>().onClick.AddListener(c.OnSelectPeca);
            }

           
            
        }
        while(peca.localPosition != Vector3.zero)
        {
            Vector3 vel = new Vector3();
            peca.localPosition = Vector3.SmoothDamp(peca.localPosition, Vector3.zero, ref vel, Time.deltaTime, peca.GetComponent<Peca>().velocityMove);
            yield return new WaitForEndOfFrame();
        }
        peca.localPosition = Vector3.zero;
    }
    
    public void OnSelectPeca()
    {
        Debug.Log($"Está no: {transform.parent.name} {name} | Possui peça? {(transform.childCount > 1 ? "Sim": "Não")}");
        if(transform.childCount > 1)
        {
            
            peca = transform.GetChild(1);
            if(peca.gameObject.tag == "Inimigo")
            {
                peca = null;
                return;
            }
            Peca p = peca.GetComponent<Peca>();
            foreach(CellController c in MatrizController.MatrixCells)
            {
                foreach(PecaPlayer.RuleMove movePossible in p.MoveRules.MovesPossible)
                {
                    int rowCellPossibility = p.MoveRules.ActualPosition.RowMove - movePossible.RowMove;
                    int columnCellPossibility = p.MoveRules.ActualPosition.ColumMove - movePossible.ColumMove;
                    Debug.Log($"Cell: ({c.Row}, {c.Column}) | Move: ({rowCellPossibility}, {columnCellPossibility})");
                    if(rowCellPossibility == c.Row && columnCellPossibility == c.Column)
                    {
                        var color = c.GetComponent<Button>().colors;
                        color.normalColor = Color.green;
                        c.GetComponent<Button>().colors = color;
                        c.GetComponent<Button>().onClick.RemoveAllListeners();
                        c.GetComponent<Button>().onClick.AddListener(c.SetAvaiableToMove);
                    }
                }
            }
        }
    }
}
