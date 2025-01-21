using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Android.Gradle;

public class AlertCanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    public SubMenuButton subMenuButton;
    public CanvasController controller;
    public SensitiveSettingCanvasController sensitiveSettingCanvasController;
    public GameObject alert;
    public MediaEntryManager mediaEntryManager;
    public GameConfig gameConfig;
    public Logging logging;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(SubMenuButton subMenuButton)
    {
        this.subMenuButton = subMenuButton;
        if (!mediaEntryManager.mediaList[subMenuButton.index].checkRelation(gameConfig.getTypeIndex())) alert.SetActive(true);
        else alert.SetActive(false);
        //this.GetComponentInChildren<TextMeshProUGUI>().text = subMenuButton.subName;
    }

    public void OnClick_AllowOnce()
    {
        subMenuButton.ToggleMedia(mediaEntryManager.mediaList[subMenuButton.index].prefab);
        controller.ResetCanvas();
        controller.ToggleCanvas();
        logging.Share_Origin(mediaEntryManager.mediaList[subMenuButton.index].name);
    }
    public void OnClick_NotAllow()
    {
        controller.CloseCanvas();
        controller.ToggleCanvas();
        controller.CloseCanvasErasePreview();
    }
    public void OnClick_AllowOnceOptimize()
    {
        subMenuButton.ToggleMedia(mediaEntryManager.mediaList[subMenuButton.index].prefabDataAware);
        controller.ResetCanvas();
        controller.ToggleCanvas();
        logging.Share_Our(mediaEntryManager.mediaList[subMenuButton.index].name);
    }
    public void OnClick_AllowOnceBaseline()
    {
        subMenuButton.ToggleMedia(mediaEntryManager.mediaList[subMenuButton.index].prefabBaseLine);
        controller.ResetCanvas();
        controller.ToggleCanvas();
        logging.Share_BaseLine(mediaEntryManager.mediaList[subMenuButton.index].name);
    }

    public void OnClick_AllowAlways()
    {
        subMenuButton.SetRelationAllow();
        subMenuButton.ToggleMedia(mediaEntryManager.mediaList[subMenuButton.index].prefab);
        controller.ResetCanvas();
        controller.ToggleCanvas();
        logging.Share_Origin(mediaEntryManager.mediaList[subMenuButton.index].name);
        logging.Set_Relation(mediaEntryManager.mediaList[subMenuButton.index].name, gameConfig.GetRelationType(), 1);
    }
    public void OnClick_Setting()
    {
        sensitiveSettingCanvasController.Init(subMenuButton);
        controller.OpenCanvas(controller.SettingCanvas);
    }
}
