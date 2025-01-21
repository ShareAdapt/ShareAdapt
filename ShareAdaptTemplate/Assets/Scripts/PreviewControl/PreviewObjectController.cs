using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObjectController : MonoBehaviour
{
    public MediaEntryManager mediaEntryManager;
    private SubMenuButton subMenuButton;
    public GameObject GameControl;

    public GameObject previewPos_original;
    public GameObject previewPos_normal;
    public GameObject previewPos_dataAware;

    private GameObject originalPreview;
    private GameObject normalPreview;
    private GameObject dataAwarePreview;

    public GameObject note_original;
    public GameObject note_normal;
    public GameObject note_dataAware;

    public void Init(SubMenuButton subMenuButton)
    {
        this.subMenuButton = subMenuButton;
    }
    private void Close()
    {
        if (originalPreview) originalPreview.DestroySafely();
        if (normalPreview) normalPreview.DestroySafely();
        if (dataAwarePreview) dataAwarePreview.DestroySafely();
        note_original.SetActive(false);
        note_normal.SetActive(false);
        note_dataAware.SetActive(false);
    }
    private void Original()
    {
        originalPreview = GameControl.GetComponent<SharedMediaController>().InstantiateTarget(subMenuButton.index, subMenuButton, mediaEntryManager.mediaList[subMenuButton.index].preview_prefab);
        originalPreview.transform.SetParent(previewPos_original.transform);
        originalPreview.transform.localPosition = Vector3.zero;
        originalPreview.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Debug.Log("Original");
        note_original.SetActive(true);
    }
    private void Adaptive()
    {
        
        dataAwarePreview = GameControl.GetComponent<SharedMediaController>().InstantiateTarget(subMenuButton.index, subMenuButton, mediaEntryManager.mediaList[subMenuButton.index].preview_prefabDataAware);
        dataAwarePreview.transform.SetParent(previewPos_dataAware.transform);
        dataAwarePreview.transform.localPosition = Vector3.zero;
        dataAwarePreview.transform.localRotation = Quaternion.Euler(Vector3.zero);
        note_dataAware.SetActive(true);
    }
    private void Baseline()
    {
        normalPreview = GameControl.GetComponent<SharedMediaController>().InstantiateTarget(subMenuButton.index, subMenuButton, mediaEntryManager.mediaList[subMenuButton.index].preview_prefabBaseLine);
        normalPreview.transform.SetParent(previewPos_normal.transform);
        normalPreview.transform.localPosition = Vector3.zero;
        normalPreview.transform.localRotation = Quaternion.Euler(Vector3.zero);
        note_normal.SetActive(true);
    }
    public void ShowThree(SubMenuButton subMenuButton)
    {
        Init(subMenuButton);
        Close();
        Original();
        Adaptive();
        Baseline();
    }
    public void CloseAll()
    {
        Close();
    }
    public void ShowOriginal(SubMenuButton subMenuButton)
    {
        Init(subMenuButton);
        Close();
        Original();
    }
}
