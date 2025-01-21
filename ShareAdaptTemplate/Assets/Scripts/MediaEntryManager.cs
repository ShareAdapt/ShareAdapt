using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 MediaEntryManager include mediaList which procide unitive management
*/
public class MediaEntryManager : MonoBehaviour
{
    
    [SerializeField]
    public List<MediaEntry> mediaList;      //list to store media
    public GameConfig gameConfig;

    public List<MediaEntry> GetListByType(MediaType type)
    {
        return mediaList.FindAll(entry => entry.checkType(type));
    }
    public List<MediaEntry>  FilteredListByRelationAndType(MediaType mediaType, int relationTypeIndex)
    {
        return mediaList.FindAll(entry => (entry.checkType(mediaType) && entry.checkWillingness(relationTypeIndex)));
    }

    //type contain in this list will appear in the left panel
    [SerializeField]
    public List<MediaType> mediaTypeList;
    
    //GetTypeListByRelation( type list without any element filted by relation will not show in the left panel)
    public List<MediaType> FilteredMediaTypeListByRelation(int relationTypeIndex)
    {
        return mediaTypeList.FindAll(item => (FilteredListByRelationAndType(item, relationTypeIndex).Count > 0))
;    }

    void Start()
    {
        int tmp = 0;
        //when mediaList is filtered, id represent the original index in the mediaList
        foreach (var entry in mediaList)
        {
            entry.id = tmp++;
        }
        //use sensor data(baseline)
        if(gameConfig.isBaseLine) 
        { 
            mediaTypeList.Clear();
            mediaTypeList.Add(MediaType.HighResolutionMainCameras);
            mediaTypeList.Add(MediaType.LidearScanner);
            mediaTypeList.Add(MediaType.WorldFacingTrackingCameras);
            mediaTypeList.Add(MediaType.EyeTrackingCameras);
            mediaTypeList.Add(MediaType.MicArray);
            mediaTypeList.Add(MediaType.SpatialImage);
            mediaTypeList.Add(MediaType.AmbientLightSensor);
            mediaTypeList.Add(MediaType.DigitalAvatar);
            mediaTypeList.Add(MediaType.FlickerSensor);
        }
        else
        {

        }
    }
}
