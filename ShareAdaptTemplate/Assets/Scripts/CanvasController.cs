using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject CanvasRoot;
    public GameObject MainCanvas;
    public GameObject ShortAlertCanvas;
    public GameObject AlertCanvas;
    public GameObject SettingCanvas;

    public GameObject centerEyeAnchor;

    private Stack<GameObject> menuStack;
    public PreviewObjectController previewObjectController;
    private void Start()
    {
        menuStack = new Stack<GameObject>();
        menuStack.Push(MainCanvas);
    }
    public void ToggleCanvas()
    {
        if(CanvasRoot.activeInHierarchy)
        {
            CanvasRoot.SetActive(false); 
        }else
        {
            Vector3 direction = centerEyeAnchor.transform.forward;
            direction.y = 0; // 确保只在XOZ平面上移动

            CanvasRoot.transform.position = centerEyeAnchor.transform.position + direction.normalized * 1f;
            CanvasRoot.transform.rotation = Quaternion.LookRotation(direction);
            CanvasRoot.SetActive(true);
        }
    }
    public void OpenCanvas(GameObject obj)
    {
        if(menuStack.Count>0) menuStack.Peek().SetActive(false);
        obj.SetActive(true);
        menuStack.Push(obj);
    }
    public void CloseCanvas()
    {
        menuStack.Pop().SetActive(false);
        menuStack.Peek().SetActive(true);
    }
    public void CloseCanvasErasePreview()
    {
        menuStack.Pop().SetActive(false);
        menuStack.Peek().SetActive(true);
        previewObjectController.CloseAll();
    }
    public void ToggleTarget()
    {
        MainCanvas.SetActive(!MainCanvas.activeInHierarchy);
    }
    public void ReplaceCanvas(GameObject obj)
    {
        if (menuStack.Count > 0) menuStack.Pop().SetActive(false);
        obj.SetActive(true);
        menuStack.Push(obj);
    }
    public void ResetCanvas()
    {
        while(menuStack.Count > 0) menuStack.Pop().SetActive(false);
        menuStack.Push(MainCanvas);
        MainCanvas.SetActive(true);
        ShortAlertCanvas.SetActive(false);
        AlertCanvas.SetActive(false);
        SettingCanvas.SetActive(false);
        previewObjectController.CloseAll();
    }
}
