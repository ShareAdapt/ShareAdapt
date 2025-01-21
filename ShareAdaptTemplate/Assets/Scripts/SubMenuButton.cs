using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuButton : MonoBehaviour
{
    public string subName;      //name show in the UI
    public int index;       //The corresponding media's index in the mediaList

    public GameObject GameControl;
    public CanvasController canvasController;
    private MediaEntryManager mediaEntryManager;
    private SharedMediaController sharedMediaController;
    private NoticeController noticeController;
    private GameConfig gameConfig;
    public GameObject tmpObject;

    private AlertCanvasController alertCanvasController;
    public Button CheckButton;
    public Button ShareButton;
    private GameObject ObjectButtonPanel;
    public GameObject settingCanvas;
    public PreviewController previewController;
    public ShortAlertController shortAlertController;
    public PreviewObjectController previewObjectController;

    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            onClick();
        });
    }
    private void SetControl()
    {
        GameControl = GameObject.Find("GameControl");
        mediaEntryManager = GameControl.GetComponent<MediaEntryManager>();
        sharedMediaController = GameControl.GetComponent<SharedMediaController>();
        noticeController = GameControl.GetComponent<NoticeController>();
        gameConfig = GameControl.GetComponent<GameConfig>();
        canvasController = GameControl.GetComponent<CanvasController> ();
        ObjectButtonPanel = gameObject.transform.Find("ButtonPanel").gameObject;
        CheckButton = ObjectButtonPanel.transform.Find("CheckButton").gameObject.GetComponent<Button>();
        ShareButton = ObjectButtonPanel.transform.Find("ShareButton").gameObject.GetComponent<Button>();
        settingCanvas = GameControl.GetComponent<SensitiveSettingCanvasController>().settingCanvas;
        previewController = GameControl.GetComponent<PreviewController>();
        shortAlertController = GameControl.GetComponent<ShortAlertController>();
        previewObjectController = GameControl.GetComponent<PreviewObjectController>();
    }
    // subName used for UI, index used for get the media from mediaList
    public void Init(string subName, int index, AlertCanvasController controller, ShortAlertController shortAlertController)
    {
        SetControl();
        this.index = index;
        this.subName = subName;
        alertCanvasController = controller;
        this.shortAlertController = shortAlertController;
        transform.Find("Image").GetComponent<Image>().color = mediaEntryManager.mediaList[index].instance.GetStateColor();
        transform.Find("RawImage").GetComponent<RawImage>().texture = mediaEntryManager.mediaList[index].texture;
    }

    void onClick_Toggle()
    {
        ObjectButtonPanel.SetActive(true);
    }
    public void OnClick_Share()
    {
        ObjectButtonPanel.SetActive(false);
        onClick();
    }
    public void OnClick_Check()
    {
        ObjectButtonPanel.SetActive(false);

        //InstantiateTarget
        previewController.Init(this);
    }

    public void OnClick_Setting()
    {
        ObjectButtonPanel.SetActive(false);
        GameControl.GetComponent<SensitiveSettingCanvasController>().Init(this);
        settingCanvas.SetActive(true);
    }

    void onClick()
    {
        //设置好alertcanvas的参数，然后activate，
        var media = mediaEntryManager.mediaList[index];
        //spawn and deactivate => activateAll
        if (media.instance.state == MediaState.notSpawn || media.instance.state == MediaState.deactivate)
        {
            //relation allowed
            if (mediaEntryManager.mediaList[index].checkRelation(gameConfig.getTypeIndex()))
            {
                shortAlertController.Init(this);
                canvasController.OpenCanvas(canvasController.ShortAlertCanvas);
                previewObjectController.ShowOriginal(this);
            }
            else
            {
                alertCanvasController.Init(this);
                canvasController.OpenCanvas(canvasController.AlertCanvas);
                previewObjectController.ShowThree(this);
            }
        }
        //spawn and activate => deactivateAll
        else if (media.instance.state == MediaState.activateAll)
        {
            ToggleMedia(mediaEntryManager.mediaList[index].prefab);
        }
    }
    public void SetRelationAllow()
    {
        mediaEntryManager.mediaList[index].relation[gameConfig.getTypeIndex()] = 1;
    }

    public void ToggleMedia(GameObject prefab)
    {
        Debug.Log("ToggleMedia");

        var media = mediaEntryManager.mediaList[index];
        Debug.Log(index);
        //not spawn yet => spawn
       
        if (media.instance.state == MediaState.notSpawn)
        {
            GameControl.GetComponent<SharedMediaController>().SpawnTarget(index, prefab);
            GameControl.GetComponent<NoticeController>().SendNotice(media.name);
            transform.Find("Image").GetComponent<Image>().color = Color.green;
        }
        //spawn and deactivate => activateAll
        else if (media.instance.state == MediaState.deactivate)
        {
            //GameControl.GetComponent<SharedMediaController>().ActivateAllMediaStatus(index);
            //GameControl.GetComponent<NoticeController>().SendNotice(media.name);
            //transform.Find("Image").GetComponent<Image>().color = Color.green;
            GameControl.GetComponent<SharedMediaController>().SpawnTarget(index, prefab);
            GameControl.GetComponent<NoticeController>().SendNotice(media.name);
            transform.Find("Image").GetComponent<Image>().color = Color.green;
        }
        //spawn and activate => deactivateAll
        else if (media.instance.state == MediaState.activateAll)
        {
            GameControl.GetComponent<SharedMediaController>().DeactivatedAllMediaStatus(index);
            transform.Find("Image").GetComponent<Image>().color = Color.red;
        }
    }
}
