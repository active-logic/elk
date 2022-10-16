using System.Collections.Generic;
using System.Linq;
using UnityEngine; using UnityEditor;
using static UnityEditor.EditorGUILayout;
using Ed = UnityEditor.EditorApplication;
using GL = UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;

namespace Activ.DPE.Editor{
public partial class DPEWindow : EditorWindow{

    Vector2 p_scroll;
    const int FontSize = 13;
    const float ScrubberButtonsHeight = 24f;
    public static DPEWindow instance;
    //LogWindowModel model = new LogWindowModel();
    string breakpoint;
    //int breakpointDelayFrames = 0;
    //
    Activ.DPE.Solver target;
    GameObject targetObject;
    string targetName;
    static Font normalButtonFont;
    static Font _font;
    static float time;
    float minRating = 0.5f;
    int offBy = -1;

    string currentLog;
    //int frame = -1;  // Last frame while playing (store this?)
    // approx msg count (helps tracking memory overheads)
    public static int cumulatedMessageCount;

    DPEWindow(){
        /*
        Ed.pauseStateChanged +=
            (PauseState s) => { if(s == PauseState.Paused) Repaint(); };
        Activ.Loggr.Logger<LogMessage, object>.onMessage += OnGenericMessage;
        */
    }

    //void OnFocus(){
    //    SceneView.duringSceneGui -= OnSceneGUI;
    //    SceneView.duringSceneGui += OnSceneGUI;
    //}

    void OnGUI(){
        UpdateSelection();
        var output = "Select a DPE agent";
        bool passOnly = !Ed.isPaused && isPlaying;
        if(target != null) output =
            targetName+"\n"+
            Activ.DPE.LogFormat.Format(
                target.GetLog(), passOnly, minRating, offBy
            );
        DrawTextView(output, ref p_scroll);
        //
        GL.BeginHorizontal();
        GL.Label("min rating: ", GL.MaxWidth(60f));
        minRating = FloatField(minRating, GL.MaxWidth(30f));
        GL.Label("off by at most: ", GL.MaxWidth(100f));
        offBy = IntField(offBy, GL.MaxWidth(30f));
        GL.EndHorizontal();
    }

    void OnInspectorUpdate(){
        Repaint();
    }

    void UpdateSelection(){
        if(Selection.activeGameObject){
            var x = Selection.activeGameObject
                             .GetComponent<Activ.DPE.Solver>();
            if(x != null){
                target = x;
                targetName = Selection.activeGameObject.name;
            }
        }
    }

    void DrawTextView(string text, ref Vector2 scroll){
        scroll = BeginScrollView(scroll);
        GUI.backgroundColor = Color.black;
        ConfigTextAreaStyle();
        GL.TextArea(text, GL.ExpandHeight(true));
        EndScrollView();
        GUI.backgroundColor = Color.white;
    }

    void ConfigTextAreaStyle(){
        var f = monofont;
        if(f == null) Debug.LogError("font not available");
        var style = GUI.skin.textArea;
        style.font = f;
        style.fontSize = FontSize;
        style.normal.textColor  = Color.white * 0.9f;
        style.focused.textColor = Color.white;
        style.focused.textColor = Color.white;
    }

/*
    void OnSceneGUI(SceneView sceneView){
        var sel = PrologHistoryGUI.Draw(model.filtered,
                                        model.pgRange);
        if(Ed.isPaused || !isPlaying){
            model.pgRange = sel ?? model.pgRange;
            Repaint();
        }else{
            model.pgRange = null;
        }
    }
*/

    void OnSelectionChange()
    { if(Ed.isPaused || !isPlaying) Repaint(); }

/*
    void Clear(){
        DebugChan.logger = null;
        PrologLogger.Clear();
        model.Clear();
        SceneView.RepaintAll();
        Repaint();
    }

    void SelectPrev(){
        model.Prev();
        SceneView.RepaintAll();
    }

    void SelectNext(){
        model.Next();
        SceneView.RepaintAll();
    }
*/

    public static void DisplayWindow(){
        instance = (DPEWindow)EditorWindow
                   .GetWindow<DPEWindow>(title: "DPE");
        instance.Show();
    }

    static Font monofont{ get{
        if(_font) return _font;
        var avail = new []{
            "Menlo", "Consolas", "Inconsolata",
            "Bitstream Vera Sans Mono", "Oxygen Mono", "Ubuntu Mono",
            "Cousine", "Courier", "Courier New", "Lucida Console",
            "Monaco"
        }.Intersect(Font.GetOSInstalledFontNames()).First();
        return _font = Font.CreateDynamicFontFromOSFont(avail,
                                                        FontSize);
    }}

//    bool browsing
//    => (Ed.isPaused || !isPlaying) && model.pgRange != null;

    static bool isPlaying => Application.isPlaying;

//    bool useHistory => Config.allFrames && canUseHistory;

//    bool canUseHistory => Ed.isPaused || !Ed.isPlaying;

}}
