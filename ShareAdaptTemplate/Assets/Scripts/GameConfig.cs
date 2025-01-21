using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public RelationType RelationType;
    public bool isBaseLine;
    public int getTypeIndex()
    {
        switch(this.RelationType)
        {
            case RelationType.CommunalSharing:
                return 0;
            case RelationType.AuthorityRanking:
                return 1;
            case RelationType.EqualityMatching:
                return 2;
            case RelationType.MarketPricing:
                return 3;
            default:
                return -1;
        }
    }
    public RelationType GetRelationType()
    {
        return RelationType;
    }
}
