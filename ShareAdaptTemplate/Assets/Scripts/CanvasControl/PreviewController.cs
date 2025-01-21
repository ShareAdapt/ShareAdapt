using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewController : MonoBehaviour
{
    public GameObject PreviewCanvas;
    public MediaEntryManager mediaEntryManager;
    private SubMenuButton subMenuButton;
    public GameObject GameControl;

    public GameObject previewPos_original;
    public GameObject previewPos_normal;
    public GameObject previewPos_dataAware;

    private GameObject originalPreview;
    private GameObject normalPreview;
    private GameObject dataAwarePreview;

    public void Init(SubMenuButton subMenuButton)
    {
        this.subMenuButton = subMenuButton; 
        PreviewCanvas.SetActive(true);
        originalPreview = GameControl.GetComponent<SharedMediaController>().InstantiateTarget(subMenuButton.index, subMenuButton, mediaEntryManager.mediaList[subMenuButton.index].preview_prefab);
        originalPreview.transform.SetParent(previewPos_original.transform);
        originalPreview.transform.localPosition = Vector3.zero;
        originalPreview.transform.localRotation = Quaternion.Euler(Vector3.zero);

        normalPreview = GameControl.GetComponent<SharedMediaController>().InstantiateTarget(subMenuButton.index, subMenuButton, mediaEntryManager.mediaList[subMenuButton.index].preview_prefabBaseLine);
        normalPreview.transform.SetParent(previewPos_normal.transform);
        normalPreview.transform.localPosition = Vector3.zero;
        normalPreview.transform.localRotation = Quaternion.Euler(Vector3.zero);

        dataAwarePreview = GameControl.GetComponent<SharedMediaController>().InstantiateTarget(subMenuButton.index, subMenuButton, mediaEntryManager.mediaList[subMenuButton.index].prefabDataAware);
        dataAwarePreview.transform.SetParent(previewPos_dataAware.transform);
        dataAwarePreview.transform.localPosition = Vector3.zero;
        dataAwarePreview.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
    public void OnClick_Close()
    {
        GameControl.GetComponent<SharedMediaController>().OnClick_CloseCheckObject();
        originalPreview.DestroySafely();
        normalPreview.DestroySafely();
        dataAwarePreview.DestroySafely();
    }
    public void OnClick_Original()
    {
        GameControl.GetComponent<SharedMediaController>().OnClick_CloseCheckObject();
        GameControl.GetComponent<SharedMediaController>().InstantiateTarget(subMenuButton.index, subMenuButton,mediaEntryManager.mediaList[subMenuButton.index].prefab);
        GameControl.GetComponent<NoticeController>().SendCloseNotice();
    }
    public void OnClick_Adaptive()
    {
        GameControl.GetComponent<SharedMediaController>().OnClick_CloseCheckObject();
        GameControl.GetComponent<SharedMediaController>().InstantiateTarget(subMenuButton.index, subMenuButton, mediaEntryManager.mediaList[subMenuButton.index].prefabDataAware);
        GameControl.GetComponent<NoticeController>().SendCloseNotice();
    }
    public void OnClick_Baseline()
    {
        GameControl.GetComponent<SharedMediaController>().OnClick_CloseCheckObject();
        GameControl.GetComponent<SharedMediaController>().InstantiateTarget(subMenuButton.index, subMenuButton, mediaEntryManager.mediaList[subMenuButton.index].prefabBaseLine);
        GameControl.GetComponent<NoticeController>().SendCloseNotice();
    }
}
