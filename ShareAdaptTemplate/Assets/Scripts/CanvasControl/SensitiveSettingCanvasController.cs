using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitiveSettingCanvasController : MonoBehaviour
{
    public SubMenuButton subMenuButton;
    public List<GameObject> relationButtonList;
    public Material sensitiveMeterial;
    public Material notSensMeterial;
    public MediaEntryManager mediaEntryManager;
    public GameObject settingCanvas;
    public Logging logging;
    public void Init(SubMenuButton subMenuButton)
    {
        this.subMenuButton = subMenuButton;
        for(int i = 0;i<4;i++)
        {
            relationButtonList[i].GetComponent<Image>().color = mediaEntryManager.mediaList[subMenuButton.index].relation[i] <1 ? notSensMeterial.color : sensitiveMeterial.color;
        }
    }
    private void setColor(int res)
    {
        relationButtonList[res].GetComponent<Image>().color = mediaEntryManager.mediaList[subMenuButton.index].relation[res] < 1 ? notSensMeterial.color : sensitiveMeterial.color;
    }
    public void OnClick_ToggleRelation1()
    {
        mediaEntryManager.mediaList[subMenuButton.index].relation[0] = 1 - mediaEntryManager.mediaList[subMenuButton.index].relation[0];
        setColor(0);
        logging.Set_Relation(mediaEntryManager.mediaList[subMenuButton.index].name, RelationType.CommunalSharing, mediaEntryManager.mediaList[subMenuButton.index].relation[0]);
    }
    public void OnClick_ToggleRelation2()
    {
        mediaEntryManager.mediaList[subMenuButton.index].relation[1] = 1 - mediaEntryManager.mediaList[subMenuButton.index].relation[1];
        setColor(1);
        logging.Set_Relation(mediaEntryManager.mediaList[subMenuButton.index].name, RelationType.AuthorityRanking, mediaEntryManager.mediaList[subMenuButton.index].relation[1]);
    }
    public void OnClick_ToggleRelation3()
    {
        mediaEntryManager.mediaList[subMenuButton.index].relation[2] = 1 - mediaEntryManager.mediaList[subMenuButton.index].relation[2];
        logging.Set_Relation(mediaEntryManager.mediaList[subMenuButton.index].name, RelationType.EqualityMatching, mediaEntryManager.mediaList[subMenuButton.index].relation[2]);
        setColor(2);
    }
    public void OnClick_ToggleRelation4()
    {
        mediaEntryManager.mediaList[subMenuButton.index].relation[3] = 1 - mediaEntryManager.mediaList[subMenuButton.index].relation[3];
        setColor(3);
        logging.Set_Relation(mediaEntryManager.mediaList[subMenuButton.index].name, RelationType.MarketPricing, mediaEntryManager.mediaList[subMenuButton.index].relation[3]);
    }
}
