using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DebugClass
{
    public string name;
    public string value;
}

public class DebugManager : SingletonGameObject<DebugManager> {

    public List<DebugClass> list;

    private float deltaTime = 0.0f;

    private void Awake()
    {
        list = new List<DebugClass>();
    }


    // Update is called once per frame
    void Update () {

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    public void listInit(string name, string value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i].name == name)
            {
                list[i].value = value;

                return;
            }
        }

        DebugClass tmp = new DebugClass();

        tmp.name = name;
        tmp.value = value;

        list.Add(tmp);
    }
    
    public void listReset()
    {
        while(list.Count != 0)
        {
            list.Remove(list[0]);
        }
    }

    void OnGUI()
    {
        int w = Screen.width;
        int h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 3 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 3 / 100;
        style.normal.textColor = Color.green;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)    ({2:0} list)", msec, fps, list.Count);
        GUI.Label(rect, text, style);


        for (int i = 0; i < list.Count; i++)
        {
            rect = new Rect(50, 50 + (i * 50), w, h * 5 / 100);
            style.fontSize = h * 5 / 100;
            style.normal.textColor = Color.white;
            text = string.Format("{0:0} :: {1:0}", list[i].name, list[i].value);
            GUI.Label(rect, text, style);
        }

        //listReset();
    }
}
