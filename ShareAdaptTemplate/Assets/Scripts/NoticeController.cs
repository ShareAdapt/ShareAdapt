using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NoticeController : NetworkBehaviour
{
    public GameObject noticePrefab;
    public Transform noticePos;

    //send the Notice
    public void SendNotice(string name)
    {
        RPC_SpawnNotice_Receive("A", name);
        SpawnNotice_Send("B", name);

    }
    public void SendCloseNotice()
    {
        GameObject noticeInstance = Instantiate(noticePrefab, new Vector3(-0.03f, 0.06f, 0.1f), Quaternion.Euler(50, 0, 0));

        noticeInstance.GetComponent<Notice>().noticeText = "Press 'X' to close";
        noticeInstance.transform.SetParent(noticePos, false);
    }
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    private void RPC_SpawnNotice_Receive(string senderName, string mediaName)
    {
        // instantiate the Notice locally
        GameObject noticeInstance = Instantiate(noticePrefab, new Vector3(-0.03f, 0.06f, 0.1f), Quaternion.Euler(50, 0, 0));

        noticeInstance.GetComponent<Notice>().noticeText = senderName + " share with you a " + mediaName;
        noticeInstance.transform.SetParent(noticePos, false);
    }
    private void SpawnNotice_Send(string receiverName, string mediaName)
    {
        //  instantiate the Notice locally
        GameObject noticeInstance = Instantiate(noticePrefab, new Vector3(-0.03f, 0.06f, 0.1f), Quaternion.Euler(50, 0, 0));

        noticeInstance.GetComponent<Notice>().noticeText = mediaName + " is shared with  " + receiverName;
        noticeInstance.transform.SetParent(noticePos, false);
    }
}
