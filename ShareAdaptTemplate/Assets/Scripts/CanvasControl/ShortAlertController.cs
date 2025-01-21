using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortAlertController : MonoBehaviour
{
    public SubMenuButton subMenuButton;
    public CanvasController controller;
    public SensitiveSettingCanvasController sensitiveSettingCanvasController;
    public PreviewObjectController previewObjectController;
    public Logging logging;
    public MediaEntryManager mediaEntryManager;
    void Start()
    {
        
    }
    public void Init(SubMenuButton subMenuButton)
    {
        this.subMenuButton = subMenuButton;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Onlick_ShareNow()
    {
        subMenuButton.ToggleMedia(mediaEntryManager.mediaList[subMenuButton.index].prefab);
        controller.ResetCanvas();
        controller.ToggleCanvas();
        logging.Share_Origin(mediaEntryManager.mediaList[subMenuButton.index].name);
    }
    public void OnClick_MoreOption()
    {
        controller.AlertCanvas.GetComponent<AlertCanvasController>().Init(subMenuButton);
        controller.ReplaceCanvas(controller.AlertCanvas);
        previewObjectController.ShowThree(subMenuButton);
    }
    public void OnClick_Setting()
    {
        sensitiveSettingCanvasController.Init(subMenuButton);
        controller.OpenCanvas(controller.SettingCanvas);
    }
}
