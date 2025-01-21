using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Meta.WitAi;


public class SharedMediaController : NetworkBehaviour
{
    public MediaEntryManager mediaEntryManager;
    public GameObject centerEyeAnchor;
    public GameObject tmpObject;
    public SubMenuButton tmpSubButton;
    public NetworkId SpawnTarget(int id, GameObject prefab)
    {
        
        Debug.Log("SpawnTarget");
        NetworkObject networkObject;
        var media = mediaEntryManager.mediaList[id];
        Vector3 spawnPosition;
        Quaternion spawnRotation;
        
        /*switch (media.type)
        {
            //VisualContent spawn at the position of right controller
            case MediaType.VisualContent:
                 spawnPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                 spawnRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
                networkObject = Runner.Spawn(prefab, spawnPosition, spawnRotation, Runner.LocalPlayer);
                media.instance = new MediaInstance(networkObject.Id, networkObject);
                media.instance.SetState(MediaState.activateAll);
                return networkObject.Id;
                
            case MediaType.MixedDisplay:
                Vector3 direction = centerEyeAnchor.transform.forward;
                direction.y = 0; // make sure only in XOZ
                spawnPosition = centerEyeAnchor.transform.position + direction.normalized * 1f;
                spawnRotation = Quaternion.LookRotation(direction);
                networkObject = Runner.Spawn(prefab, spawnPosition, spawnRotation, Runner.LocalPlayer);
                media.instance = new MediaInstance(networkObject.Id, networkObject);
                media.instance.SetState(MediaState.activateAll);
                return networkObject.Id;
            
            case MediaType.SpatialImage:
                direction = centerEyeAnchor.transform.forward;
                direction.y = 0; // make sure only in XOZ
                spawnPosition = centerEyeAnchor.transform.position + direction.normalized * 1f;
                spawnRotation = Quaternion.LookRotation(direction);
                networkObject = Runner.Spawn(prefab, spawnPosition, spawnRotation, Runner.LocalPlayer);
                media.instance = new MediaInstance(networkObject.Id, networkObject);
                media.instance.SetState(MediaState.activateAll);
                return networkObject.Id;
            
                //Other spawn at the setted position of prefab
            default:
                networkObject = Runner.Spawn(prefab, media.prefab.transform.position, media.prefab.transform.rotation ,Runner.LocalPlayer);
                media.instance = new MediaInstance(networkObject.Id, networkObject);
                media.instance.SetState(MediaState.activateAll);
                return networkObject.Id;
        }*/
        if (media.isSpawnOriginal)
        {
            Debug.Log("isSpawnOriginal");
            networkObject = Runner.Spawn(prefab, media.prefab.transform.position, media.prefab.transform.rotation, Runner.LocalPlayer);
            media.instance = new MediaInstance(networkObject.Id, networkObject);
            media.instance.SetState(MediaState.activateAll);
            return networkObject.Id;
        }
        else
        {
            Vector3 direction = centerEyeAnchor.transform.forward;
            direction.y = 0; // make sure only in XOZ
            spawnPosition = centerEyeAnchor.transform.position + direction.normalized * 1f;
            spawnRotation = Quaternion.LookRotation(direction);
            networkObject = Runner.Spawn(prefab, spawnPosition, spawnRotation, Runner.LocalPlayer);
            media.instance = new MediaInstance(networkObject.Id, networkObject);
            media.instance.SetState(MediaState.activateAll);
            return networkObject.Id;
        }
       
    }
    public void OnClick_CloseCheckObject()
    {
        if(tmpObject) tmpObject.DestroySafely();
    }
    public GameObject InstantiateTarget(int id, SubMenuButton subMenu, GameObject prefab)
    {
        var media = mediaEntryManager.mediaList[id];
        Vector3 spawnPosition;
        Quaternion spawnRotation;
        tmpSubButton = subMenu;
        
        switch (media.type)
        {
            //VisualContent spawn at the position of right controller
            case MediaType.VisualContent:
                spawnPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                spawnRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
                tmpObject = Instantiate(prefab, spawnPosition, spawnRotation);
                return tmpObject;

            case MediaType.MixedDisplay:
                Vector3 direction = centerEyeAnchor.transform.forward;
                direction.y = 0; // make sure only in XOZ
                spawnPosition = centerEyeAnchor.transform.position + direction.normalized * 1f;
                spawnRotation = Quaternion.LookRotation(direction);
                tmpObject = Instantiate(prefab, spawnPosition, spawnRotation);
                return tmpObject;

            case MediaType.SpatialImage:
                direction = centerEyeAnchor.transform.forward;
                direction.y = 0; // make sure only in XOZ
                spawnPosition = centerEyeAnchor.transform.position + direction.normalized * 1f;
                spawnRotation = Quaternion.LookRotation(direction);
                tmpObject = Instantiate(prefab, spawnPosition, spawnRotation);
                return tmpObject;

            //Other spawn at the setted position of prefab
            default:
                tmpObject = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
                return tmpObject;
        }
    }
    //Set target media's status to deactivate amoung all players
    public void DeactivatedAllMediaStatus(int target_id)
    {
        mediaEntryManager.mediaList[target_id].instance.SetState(MediaState.deactivate);
        RPC_DeactivatedAllMediaStatus(mediaEntryManager.mediaList[target_id].instance.id);
    }
    //Set target media's status to activate in local player, deactivate in local player, 
    public void ActivateOwnMediaStatus(int target_id)
    {
        ActivateLocalMediaStatus(mediaEntryManager.mediaList[target_id].instance.id);
        RPC_DeactivateOthersMediaStatus(mediaEntryManager.mediaList[target_id].instance.id);
    }

    //Set target media's status to activate amoung all players
    public void ActivateAllMediaStatus(int target_id)
    {
        RPC_ActivateAllMediaStatus(mediaEntryManager.mediaList[target_id].instance.id, mediaEntryManager.mediaList[target_id].type, mediaEntryManager.mediaList[target_id].isSpawnOriginal);
        mediaEntryManager.mediaList[target_id].instance.SetState(MediaState.activateAll);
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    private void RPC_ActivateAllMediaStatus(NetworkId target_id, MediaType type, bool ori)
    {
        Runner.TryFindObject(target_id, out NetworkObject obj);
        if (obj != null) {
            Vector3 spawnPosition;
            Quaternion spawnRotation;
            /*switch (type)
            {
                case MediaType.RemotePerspective:
                    break;
                
                case MediaType.VisualContent:
                    spawnPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                    spawnRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
                    obj.gameObject.transform.position = spawnPosition;
                    obj.gameObject.transform.rotation = spawnRotation;
                    break;

                case MediaType.MixedDisplay:
                    Vector3 direction = centerEyeAnchor.transform.forward;
                    direction.y = 0; // make sure only in XOZ
                    spawnPosition = centerEyeAnchor.transform.position + direction.normalized * 1f;
                    spawnRotation = Quaternion.LookRotation(direction);
                    obj.gameObject.transform.position = spawnPosition;
                    obj.gameObject.transform.rotation = spawnRotation;
                    break;
                
                case MediaType.DigitalAvatar:
                    break;
                
                case MediaType.SpatialImage:
                    direction = centerEyeAnchor.transform.forward;
                    direction.y = 0; // make sure only in XOZ
                    spawnPosition = centerEyeAnchor.transform.position + direction.normalized * 1f;
                    spawnRotation = Quaternion.LookRotation(direction);
                    obj.gameObject.transform.position = spawnPosition;
                    obj.gameObject.transform.rotation = spawnRotation;
                    break;
                
                case MediaType.PhysicalReconstruction:
                    break;
                
                default:
                    break;
            }
            */
            if (!ori)
            {
                Vector3 direction = centerEyeAnchor.transform.forward;
                direction.y = 0; // make sure only in XOZ
                spawnPosition = centerEyeAnchor.transform.position + direction.normalized * 1f;
                spawnRotation = Quaternion.LookRotation(direction);
                obj.gameObject.transform.position = spawnPosition;
                obj.gameObject.transform.rotation = spawnRotation;
            }

            obj.gameObject.SetActive(true); 
        }
    }

    private void ActivateLocalMediaStatus(NetworkId target_id)
    {
        Runner.TryFindObject(target_id, out NetworkObject obj);
        if (obj != null) obj.gameObject.SetActive(true);
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    private void RPC_DeactivatedAllMediaStatus(NetworkId target_id)
    {
        Runner.TryFindObject(target_id, out NetworkObject obj);
        if(obj!=null) obj.gameObject.SetActive(false);
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    private void RPC_DeactivateOthersMediaStatus(NetworkId target_id)
    {
        Runner.TryFindObject(target_id, out NetworkObject obj);
        if (obj != null) obj.gameObject.SetActive(false);
    }
}
