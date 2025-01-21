using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Meta.XR.MultiplayerBlocks.Fusion;

public class NetworkController : NetworkBehaviour
{
    [Networked]
    private NetworkObject localObject { get; set; }

    [SerializeField]
    private GameObject localObjectPrefab;

    [SerializeField]
    private GameObject localAvatar;

    [SerializeField]
    private AvatarSpawnerFusion avatarSpawnerFusion;

    [Networked]
    private NetworkId tmpTargetAvatarId { get; set; }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    public void RPC_ToggleObject_Other()
    {
        if (localObject != null)
        {
            localObject.gameObject.SetActive(!localObject.gameObject.activeSelf);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    public void RPC_ToggleObject_All()
    {
        if (localObject != null)
        {
            localObject.gameObject.SetActive(!localObject.gameObject.activeSelf);
        }
    }

    [SerializeField]
    public void SpawnCapsule()
    {
        // Spawn the capsule network object
        Vector3 spawnPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Quaternion spawnRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        NetworkObject networkObject = Runner.Spawn(localObjectPrefab, spawnPosition, spawnRotation, Runner.LocalPlayer);

        //Set the Gameobject to localObject;
        localObject = networkObject;
    }
    [SerializeField]
    public void toggleAvatar()
    {
        //Debug.Log("toggleAvatar1");
        //tmpTargetAvatar = avatarSpawnerFusion._spawnedAvatarNetworkObject;
        if (localAvatar == null) localAvatar = GameObject.Find("LocalAvatar");
        Runner.TryFindObject(localAvatar.GetComponent<NetworkObject>().Id, out var obj);
        tmpTargetAvatarId = localAvatar.GetComponent<NetworkObject>().Id;
       
        if (tmpTargetAvatarId != null)
        {
            Debug.Log("toggleAvatar");
            RPC_toggleAvatar(tmpTargetAvatarId);
        }
    }
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal=false)]
    private void RPC_toggleAvatar(NetworkId networkId)
    {
        if(networkId != null)
        {
            Runner.TryFindObject(networkId, out var obj);
            if(obj != null) obj.gameObject.SetActive(!obj.gameObject.activeSelf);
        }
    }
}
