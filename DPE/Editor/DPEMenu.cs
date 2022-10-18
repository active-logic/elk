using UnityEditor;
using UnityEngine;

namespace Activ.DPE.Editor{
public static class DPEMenu{

    [MenuItem("Window/Activ/DPE Console")]
    static void OpenConsole() => DPEWindow.DisplayWindow();

}}
