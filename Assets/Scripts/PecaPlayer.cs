using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PecaPlayer", menuName = "Tabuleiro/PecaPlayer")]
public class PecaPlayer : ScriptableObject
{
    [System.Serializable]
    public struct RuleMove
    {
        public int RowMove;
        public int ColumMove;
    }

    public RuleMove ActualPosition = new RuleMove(){ RowMove = 2, ColumMove=2};

    public List<RuleMove> MovesPossible = new List<RuleMove>();
}
