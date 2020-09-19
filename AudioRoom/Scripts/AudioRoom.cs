using System.Collections.Generic;
using UnityEngine;


public class AudioRoom : MonoBehaviour
{

    public string groupName;
    public Vector3 pos=Vector3.zero;
    public Vector3 size=new Vector3(1,1,1);
    private static List<AudioRoomPack> arp;

    private void Awake()
    {
        //register
        if (arp == null) arp = new List<AudioRoomPack>();
        foreach(AudioRoomPack a in arp)
            if (a.name == groupName)
            {
                a.targets.Add(this);
                return;
            }
        arp.Add(new AudioRoomPack(groupName));
        arp[arp.Count - 1].targets.Add(this);
    }

    private void OnDestroy()
    {
        //unregister
        for (int i = 0; i < arp.Count; i++)
            if (arp[i].name == groupName)
            {
                arp[i].targets.Remove(this);
                if (arp[i].targets.Count == 0) arp.RemoveAt(i);
            }
            //hmm...
    }
        
    public Vector3 Locate(Vector3 target)
    {
        Vector3 actualPos = transform.position + pos;
        Vector3 delta = actualPos - target;
        delta.x = Mathf.Max(Mathf.Min(delta.x, size.x), -size.x);
        delta.y = Mathf.Max(Mathf.Min(delta.y, size.y), -size.y);
        delta.z = Mathf.Max(Mathf.Min(delta.z, size.z), -size.z);
        return actualPos - delta;
    }

    public static AudioRoomPack GetAudioRoomPack(string name)
    {
        foreach(AudioRoomPack a in arp) if (a.name == name) return a;
        return null;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.43921568f, 0.43921568f, 1f);
        Gizmos.DrawWireCube(transform.position+ pos, size * 2);
    }

}



public class AudioRoomPack
{
    public string name;
    public List<AudioRoom> targets;
    public List<AudioRoom> pool;
    float time=0f; //used to detect new frame

    public AudioRoomPack(string name)
    {
        this.name = name;
        targets = new List<AudioRoom>();
        pool = new List<AudioRoom>();
    }

    public Vector3 SingleLocate(Vector3 target)
    {
        return SpecialLocate(target,0);
    }

    public Vector3 SpecialLocate(Vector3 target,int i)
    {
        return targets[i].Locate(target);
    }

    public Vector3 OptimalLocate(Vector3 target)
    {
        float disWinner, disCurrent;
        disWinner = float.PositiveInfinity;
        Vector3 winner,current;
        winner = Vector3.zero;
        foreach (AudioRoom a in targets)
        {
            current=a.Locate(target);
            disCurrent = FastDistance(target, current);
            if (disCurrent < disWinner)
            {
                disWinner = disCurrent;
                winner = current;
            }
        }
        return winner;
    }

    public Vector3 PoolLocate(Vector3 target)
    {
        if (time != Time.time)
        {
            pool.Clear();
            time = Time.time;
        }
        float disWinner, disCurrent;
        disWinner = float.PositiveInfinity;
        Vector3 winner, current;
        winner = Vector3.zero;
        AudioRoom winnerAudioRoom=null;
        foreach (AudioRoom a in targets)
        {
            if(IsAudioRoomNotPooling(a))
            {
                
                current = a.Locate(target);
                disCurrent = FastDistance(target, current);
                if (disCurrent < disWinner)
                {
                    disWinner = disCurrent;
                    winner = current;
                    winnerAudioRoom = a;
                }
            }
        }
        pool.Add(winnerAudioRoom);
        return winner;
    }

    public bool IsAudioRoomNotPooling(AudioRoom a)
    {
        foreach (AudioRoom s in pool)
            if (s == a) return false;
        return true;
    }

    private float FastDistance(Vector3 a, Vector3 b)
    {
        a = a - b;
        return a.x*a.x+ a.y * a.y+ a.z * a.z;
    }

}
