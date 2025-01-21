using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class MenuButton : MonoBehaviour
{
    public GameObject rightPanel;
    public List<SubMenuEntry> PrefabList;
    public Button subMenuPrefab;
    public MediaEntryManager mediaEntryManager;
    public MediaType type;
    private GameConfig gameConfig;

    public GameObject RightRelationToggle;
    public bool subIsRelation;
    public RightToggle rightToggle;
    private AlertCanvasController alertCanvasController;
    private ShortAlertController shortAlertController;
    void Start()
    {
        //add onClick method
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            this.onClick();
        });
        gameConfig = GameObject.Find("GameControl").GetComponent<GameConfig>();
        RightRelationToggle = GameObject.Find("RightRelationToggle");

    }
    public void Init(GameObject rightPanel, MediaType type, MediaEntryManager manager, Button subMenuPrefab, RightToggle rightToggle, AlertCanvasController alertCanvasController, ShortAlertController shortAlertController)
    {
        this.rightPanel = rightPanel;
        this.type = type;
        this.alertCanvasController = alertCanvasController;
        this.mediaEntryManager = manager;
        this.subMenuPrefab = subMenuPrefab;
        this.rightToggle  = rightToggle;
        this.shortAlertController = shortAlertController;
        subIsRelation = false;
        
    }
    void onClick()
    {
        //clear right panel
        foreach (Transform child in rightPanel.transform)
        {
            Destroy(child.gameObject);
        }
        //get filtered list
        List<MediaEntry> subMediaList;
        if (subIsRelation) subMediaList = mediaEntryManager.FilteredListByRelationAndType(type, gameConfig.getTypeIndex());
        else subMediaList = mediaEntryManager.GetListByType(type);

        foreach (var subItem in subMediaList)
        {
            Button newButton = Instantiate(subMenuPrefab, rightPanel.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = subItem.name;
            newButton.GetComponent<SubMenuButton>().Init(subItem.name, subItem.id, alertCanvasController, shortAlertController);
        }
        rightToggle.Init(this);
        if (subIsRelation)  RightRelationToggle.GetComponentInChildren<TextMeshProUGUI>().text = "• • •";
        else RightRelationToggle.GetComponentInChildren<TextMeshProUGUI>().text = "▲";
    }
    public void ToggleRelation()
    {
        Debug.Log("toggleRelation");
        if (subIsRelation)
        {
            RightRelationToggle.GetComponentInChildren<TextMeshProUGUI>().text = "▲";
            subIsRelation = false;
            onClick();
        }
        else
        {
            RightRelationToggle.GetComponentInChildren<TextMeshProUGUI>().text = "• • •";
            subIsRelation = true;
            onClick();
        }
    }
}
