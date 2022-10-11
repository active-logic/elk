using System;
using UnityEngine; using UnityEditor;
using GL  = UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;

namespace Activ.BTL.Editors{
[CustomEditor(typeof(Activ.BTL.BTL))]
public class BTLEditor : Editor{

    public override void OnInspectorGUI(){
        var btl = (Activ.BTL.BTL)target;
        base.OnInspectorGUI();
        if(btl.exceptionMessage != null){
            EGL.HelpBox(btl.exceptionMessage, MessageType.Warning);
        }else if(string.IsNullOrEmpty(btl.path)){
            EGL.HelpBox($"No source file", MessageType.Info);
        }else if(!btl.hasValidPath){
            EGL.HelpBox($"Not found: {btl.path}", MessageType.Info);
        }
    }

}}
