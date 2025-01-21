using System;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

//Media instance's activation state.
[Serializable]
public enum MediaState
{
    deactivate,       // spawned but not activate in everyone
    activateOwn,      // spawned and activate only in own (not use now)
    activateAll,       // spawned and activate in everyone
    notSpawn
}

//Media type for filter
[Serializable]
public enum MediaType
{
    VisualContent,
    PhysicalReconstruction,
    MixedDisplay,
    DigitalAvatar,
    RemotePerspective,
    SpatialImage,
    //Sensor Type
    HighResolutionMainCameras, 
    WorldFacingTrackingCameras,
    EyeTrackingCameras,
    InertialMeasurementUnits,
    FlickerSensor,
    AmbientLightSensor,
    MicArray,
    LidearScanner
}



[Serializable]
public enum RelationType
{
    CommunalSharing,    //0
    AuthorityRanking,       //1
    EqualityMatching,       //2
    MarketPricing,            //3
}
/*
 * represent the instance of media
 * before spawn, state is set to notSpawn
 */
[Serializable]
public struct MediaInstance
{
    public NetworkId id;
    public NetworkObject networkObject;
    public MediaState state;

    public MediaInstance(NetworkId id, NetworkObject networkObject)
    {
        this.id = id;
        this.networkObject = networkObject;
        this.state = MediaState.notSpawn;
    }
    public void SetState(MediaState state)
    {
        this.state = state;
    }

    public Color GetStateColor()
    {
        switch (this.state)
        {
            case MediaState.notSpawn:
                return Color.gray;
            case MediaState.activateAll:
                return Color.green;
            default:
                return Color.red;
        }
    }
}

/*
 * The data type for media, which includes all the data of the media.
 * id represent the index in mediaList
 */
[Serializable]
public class MediaEntry
{
    public string name;
    public GameObject prefab;
    public GameObject prefabBaseLine;
    public GameObject prefabDataAware;
    public Texture texture;
    public Texture textureBaseLine;
    public Texture textureDataAware;
    public GameObject preview_prefab;
    public GameObject preview_prefabBaseLine;
    public GameObject preview_prefabDataAware;
    public MediaInstance instance;
    public int[] relation = new int[4];
    public int[] willingness = new int[4];
    public List<MediaType> typeList;
    public MediaType type;
    public int id;
    public bool isSpawnOriginal;

    public MediaEntry(string name, GameObject prefab,  int[] relation, MediaType type, int[] willingness)
    {
        this.name = name;
        this.prefab = prefab;
        this.relation = relation;
        this.instance = new MediaInstance();
        instance.state = MediaState.notSpawn;    //before spawn, state is set to notSpawn
        this.willingness = willingness;
    }

    public int getRelation(int typeIndex)
    {
        return relation[typeIndex];
    }
    public bool checkRelation(int typeIndex)
    {
        return relation[typeIndex] < 1;
    }
    public int getWillingness(int typeIndex)
    {
        return willingness[typeIndex];
    }
    public bool checkType(MediaType type)
    {
        return typeList.Contains(type) || this.type == type;
    }
    public bool checkWillingness(int typeIndex)
    {
        return willingness[typeIndex] > 0;
    }
    static public RelationType GetRelationTypeFromIndex(int index)
    {
        switch (index)
        {
            case 0:
                return RelationType.CommunalSharing;
            case 1:
                return RelationType.AuthorityRanking;
            case 2:
                return RelationType.EqualityMatching;
            case 3:
                return RelationType.MarketPricing;
            default:
                return RelationType.CommunalSharing;
        }
    }
}