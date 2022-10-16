using UnityEditor;
using UnityEngine;

namespace Activ.DPE.Editor{
public static class DPEMenu{

    [MenuItem("Window/Activ/DPE Console")]
    static void OpenConsole() => DPEWindow.DisplayWindow();

    //[MenuItem("Window/Activ/Debug-Chan/Config")]
    //static void EditConfig(){
    //    Debug.Log("Edit config not available yet");
    //}

}}
