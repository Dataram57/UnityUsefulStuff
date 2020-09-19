using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

[CustomEditor(typeof(AudioRoom))]
public class AudioRoomEditor : Editor
{

    BoxBoundsHandle b = new BoxBoundsHandle();
    AudioRoom a;
    bool edit = false;

    private void Awake()
    {
        a = (target as AudioRoom);
        b.center = a.pos + a.transform.position;
        b.size = a.size * 2;
    }
    public void OnSceneGUI()
    {
        if (edit)
        {
            a = (target as AudioRoom);
            Handles.color = Color.blue;
            a.pos = b.center - a.transform.position;
            a.size = b.size / 2;
            b.DrawHandle();
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        string s = "Enable editing";
        if (edit) s = "Disable editing";
        if (GUILayout.Button(s))
        {
            Awake();
            edit = !edit;
        }

    }
}