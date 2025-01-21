using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
public class MenuController : MonoBehaviour
{
    public GameObject leftPanel; // left Panel of UI
    public GameObject rightPanel; //right Panel of UI
    public Button menuButtonPrefab; //the menu prefab for left panel
    public Button subMenuPrefab;//the menu prefab for right panel
    public MediaEntryManager mediaEntryManager;
    private GameConfig gameConfig;
    public RightToggle rightToggle;
    public GameObject LeftRelationToggle;
    public bool isRelation;         //true mean optimmize with relation
    public AlertCanvasController alertCanvasController;
    public ShortAlertController shortAlertController;   

    void Start()
    {
        gameConfig = GameObject.Find("GameControl").GetComponent<GameConfig>();
        isRelation = false;
        InitMenu();
    }
    //create left menu buttons
    void InitMenu()
    {
        foreach (Transform child in leftPanel.transform)
        {
            Destroy(child.gameObject);
        }
        if (isRelation)
        {
            foreach (var menutype in mediaEntryManager.FilteredMediaTypeListByRelation(gameConfig.getTypeIndex()))
            {
                Button newButton = Instantiate(menuButtonPrefab, leftPanel.transform);
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = menutype.ToString();
                newButton.GetComponent<MenuButton>().Init(rightPanel, menutype, mediaEntryManager, subMenuPrefab, rightToggle, alertCanvasController, shortAlertController);
            }
        }
        else
        {
            foreach (var menutype in mediaEntryManager.mediaTypeList)
            {
                Button newButton = Instantiate(menuButtonPrefab, leftPanel.transform);
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = menutype.ToString();
                newButton.GetComponent<MenuButton>().Init(rightPanel, menutype, mediaEntryManager, subMenuPrefab, rightToggle, alertCanvasController, shortAlertController);
            }
        }
        
    }
    public void toggleRelation()
    {
        if(isRelation)
        {
            LeftRelationToggle.GetComponentInChildren<TextMeshProUGUI>().text = "▲";
            isRelation = false;
            InitMenu();
        }
        else
        {
            LeftRelationToggle.GetComponentInChildren<TextMeshProUGUI>().text = "• • •";
            isRelation = true;
            InitMenu();
        }
    }
}
