using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRoomFollow : MonoBehaviour
{

    public string groupName;
    public AudioRoomFollowType type=AudioRoomFollowType.Optimal;
    public float height;
    private static Transform listener;
    private AudioRoomPack pack;

    private void Start()
    {
        pack = AudioRoom.GetAudioRoomPack(groupName);
    }

    private void Update()
    {
        //optional
        if (listener == null)
        {
            listener = FindObjectOfType<AudioListener>().transform;
            return;
        }
        if (pack == null)
        {
            Debug.LogError("Can't find any AudioRoom of groupName:" + groupName);
            return;
        }

        if (type == AudioRoomFollowType.Optimal) transform.position = pack.OptimalLocate(listener.position);
        else if (type == AudioRoomFollowType.Single) transform.position = pack.SingleLocate(listener.position);
        else transform.position = pack.PoolLocate(listener.position);

        transform.position += new Vector3(0, height, 0);

    }
}

public enum AudioRoomFollowType
{
    Single,         //Behaves individually and include only first registered possible AudioRoom
    Optimal,        //Behaves individually and includes all possible AudioRooms
    Pool            //Snaps his position to the closest and unused AudioRoom by any other pool GameObject
}