#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Michsky.MUIP
{
    public class ContextMenu : Editor
    {
        static string s_ObjectPath;

        static void GetObjectPath()
        {
            s_ObjectPath = AssetDatabase.GetAssetPath(Resources.Load("MUIP Manager"));
            s_ObjectPath = s_ObjectPath.Replace("Resources/MUIP Manager.asset", "").Trim();
            s_ObjectPath = s_ObjectPath + "Prefabs/";
        }

        static void MakeSceneDirty(GameObject source, string sourceName)
        {
            if (Application.isPlaying == false)
            {
                Undo.RegisterCreatedObjectUndo(source, sourceName);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }

        static void ShowErrorDialog()
        {
            EditorUtility.DisplayDialog("Modern UI Pack", "Cannot create the object due to missing manager file. " +
                    "Make sure you have 'MUIP Manager' file in Modern UI Pack > Resources folder.", "Okay");
        }

        static void UpdateCustomEditorPath()
        {
            string mainPath = AssetDatabase.GetAssetPath(Resources.Load("MUIP Manager"));
            mainPath = mainPath.Replace("Resources/MUIP Manager.asset", "").Trim();
            string darkPath = mainPath + "Skins/MUI Skin Dark.guiskin";
            string lightPath = mainPath + "Skins/MUI Skin Light.guiskin";

            EditorPrefs.SetString("MUIP.CustomEditorDark", darkPath);
            EditorPrefs.SetString("MUIP.CustomEditorLight", lightPath);
        }

        static void CreateObject(string resourcePath)
        {
            try
            {
                GetObjectPath();
                UpdateCustomEditorPath();
                GameObject clone = Instantiate(AssetDatabase.LoadAssetAtPath(s_ObjectPath + resourcePath + ".prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;

                try
                {
                    if (Selection.activeGameObject == null)
                    {
                        var canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[0];
                        clone.transform.SetParent(canvas.transform, false);
                    }

                    else { clone.transform.SetParent(Selection.activeGameObject.transform, false); }

                    clone.name = clone.name.Replace("(Clone)", "").Trim();
                    MakeSceneDirty(clone, clone.name);
                }

                catch
                {
                    CreateCanvas();
                    var canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[0];
                    clone.transform.SetParent(canvas.transform, false);
                    clone.name = clone.name.Replace("(Clone)", "").Trim();
                    MakeSceneDirty(clone, clone.name);
                }

                Selection.activeObject = clone;
            }

            catch { ShowErrorDialog(); }
        }

        static void CreateButton(string resourcePath)
        {
            try
            {
                GetObjectPath();
                UpdateCustomEditorPath();
                GameObject clone = Instantiate(AssetDatabase.LoadAssetAtPath(s_ObjectPath + resourcePath + ".prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;

                try
                {
                    if (Selection.activeGameObject == null)
                    {
                        var canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[0];
                        clone.transform.SetParent(canvas.transform, false);
                    }

                    else { clone.transform.SetParent(Selection.activeGameObject.transform, false); }

                    clone.name = "Button";
                    LayoutRebuilder.ForceRebuildLayoutImmediate(clone.GetComponent<RectTransform>());
                    MakeSceneDirty(clone, clone.name);
                }

                catch
                {
                    CreateCanvas();
                    var canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[0];
                    clone.transform.SetParent(canvas.transform, false);
                    clone.name = "Button";
                    MakeSceneDirty(clone, clone.name);
                }

                Selection.activeObject = clone;
            }

            catch { ShowErrorDialog(); }
        }

        [MenuItem("GameObject/Modern UI Pack/Canvas", false, -24)]
        static void CreateCanvas()
        {
            try
            {
                GetObjectPath();
                UpdateCustomEditorPath();
                GameObject clone = Instantiate(AssetDatabase.LoadAssetAtPath(s_ObjectPath + "Other/Canvas" + ".prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                clone.name = clone.name.Replace("(Clone)", "").Trim();
                Selection.activeObject = clone;
                MakeSceneDirty(clone, clone.name);
            }

            catch { ShowErrorDialog(); }
        }

        [MenuItem("Tools/Modern UI Pack/Show UI Manager %#M")]
        static void ShowManager()
        {
            Selection.activeObject = Resources.Load("MUIP Manager");

            if (Selection.activeObject == null)
                Debug.Log("<b>[Modern UI Pack]</b>Can't find a file named 'MUIP Manager'. Make sure you have 'MUIP Manager' file in Resources folder.");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Standard", false, -12)]
        static void Bdef()
        {
            CreateButton("Button/Basic - Outline/Standard");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic/Standard", false, 0)]
        static void Bbst()
        {
            CreateButton("Button/Basic/Standard");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic/White", false, 0)]
        static void Bbwhi()
        {
            CreateButton("Button/Basic/White");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic/Gray", false, 0)]
        static void Bbgr()
        {
            CreateButton("Button/Basic/Gray");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic/Blue", false, 0)]
        static void Bbbl()
        {
            CreateButton("Button/Basic/Blue");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic/Brown", false, 0)]
        static void Bbbrw()
        {
            CreateButton("Button/Basic/Brown");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic/Green", false, 0)]
        static void Bbgre()
        {
            CreateButton("Button/Basic/Green");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic/Night", false, 0)]
        static void Bbni()
        {
            CreateButton("Button/Basic/Night");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic/Orange", false, 0)]
        static void Bbor()
        {
            CreateButton("Button/Basic/Orange");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic/Pink", false, 0)]
        static void Bbpin()
        {
            CreateButton("Button/Basic/Pink");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic/Purple", false, 0)]
        static void Bbpurp()
        {
            CreateButton("Button/Basic/Purple");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic/Red", false, 0)]
        static void Bbred()
        {
            CreateButton("Button/Basic/Red");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Gradient/White", false, 0)]
        static void Bgwhi()
        {
            CreateButton("Button/Basic - Gradient/White");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Gradient/Gray", false, 0)]
        static void Bggr()
        {
            CreateButton("Button/Basic - Gradient/Gray");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Gradient/Blue", false, 0)]
        static void Bgbl()
        {
            CreateButton("Button/Basic - Gradient/Blue");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Gradient/Brown", false, 0)]
        static void Bgbrw()
        {
            CreateButton("Button/Basic - Gradient/Brown");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Gradient/Green", false, 0)]
        static void Bggre()
        {
            CreateButton("Button/Basic - Gradient/Green");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Gradient/Night", false, 0)]
        static void Bgni()
        {
            CreateButton("Button/Basic - Gradient/Night");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Gradient/Orange", false, 0)]
        static void Bgor()
        {
            CreateButton("Button/Basic - Gradient/Orange");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Gradient/Pink", false, 0)]
        static void Bgpin()
        {
            CreateButton("Button/Basic - Gradient/Pink");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Gradient/Purple", false, 0)]
        static void Bgpurp()
        {
            CreateButton("Button/Basic - Gradient/Purple");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Gradient/Red", false, 0)]
        static void Bgred()
        {
            CreateButton("Button/Basic - Gradient/Red");
        }  

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline/Standard", false, 0)]
        static void Bowhs()
        {
            CreateButton("Button/Basic - Outline/Standard");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline/White", false, 0)]
        static void Bowhi()
        {
            CreateButton("Button/Basic - Outline/White");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline/Gray", false, 0)]
        static void Bogr()
        {
            CreateButton("Button/Basic - Outline/Gray");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline/Blue", false, 0)]
        static void Bobl()
        {
            CreateButton("Button/Basic - Outline/Blue");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline/Brown", false, 0)]
        static void Bobrw()
        {
            CreateButton("Button/Basic - Outline/Brown");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline/Green", false, 0)]
        static void Bogre()
        {
            CreateButton("Button/Basic - Outline/Green");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline/Night", false, 0)]
        static void Boni()
        {
            CreateButton("Button/Basic - Outline/Night");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline/Orange", false, 0)]
        static void Boor()
        {
            CreateButton("Button/Basic - Outline/Orange");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline/Pink", false, 0)]
        static void Bopin()
        {
            CreateButton("Button/Basic - Outline/Pink");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline/Purple", false, 0)]
        static void Bopurp()
        {
            CreateButton("Button/Basic - Outline/Purple");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline/Red", false, 0)]
        static void Bored()
        {
            CreateButton("Button/Basic - Outline/Red");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline Gradient/White", false, 0)]
        static void Bogwhi()
        {
            CreateButton("Button/Basic - Outline Gradient/White");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline Gradient/Gray", false, 0)]
        static void Bogbgr()
        {
            CreateButton("Button/Basic - Outline Gradient/Gray");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline Gradient/Blue", false, 0)]
        static void Bogbl()
        {
            CreateButton("Button/Basic - Outline Gradient/Blue");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline Gradient/Brown", false, 0)]
        static void Bogbrw()
        {
            CreateButton("Button/Basic - Outline Gradient/Brown");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline Gradient/Green", false, 0)]
        static void Boggre()
        {
            CreateButton("Button/Basic - Outline Gradient/Green");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline Gradient/Night", false, 0)]
        static void Bogni()
        {
            CreateButton("Button/Basic - Outline Gradient/Night");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline Gradient/Orange", false, 0)]
        static void Bogor()
        {
            CreateButton("Button/Basic - Outline Gradient/Orange");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline Gradient/Pink", false, 0)]
        static void Bogpin()
        {
            CreateButton("Button/Basic - Outline Gradient/Pink");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline Gradient/Purple", false, 0)]
        static void Bogpurp()
        {
            CreateButton("Button/Basic - Outline Gradient/Purple");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Basic - Outline Gradient/Red", false, 0)]
        static void Bogred()
        {
            CreateButton("Button/Basic - Outline Gradient/Red");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded/Standard", false, 0)]
        static void Brs()
        {
            CreateButton("Button/Rounded/Standard");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded/White", false, 0)]
        static void Brw()
        {
            CreateButton("Button/Rounded/White");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded/Gray", false, 0)]
        static void Brg()
        {
            CreateButton("Button/Rounded/Gray");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded/Blue", false, 0)]
        static void Brb()
        {
            CreateButton("Button/Rounded/Blue");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded/Brown", false, 0)]
        static void Brbr()
        {
            CreateButton("Button/Rounded/Brown");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded/Green", false, 0)]
        static void Brgr()
        {
            CreateButton("Button/Rounded/Green");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded/Night", false, 0)]
        static void Brn()
        {
            CreateButton("Button/Rounded/Night");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded/Orange", false, 0)]
        static void Bro()
        {
            CreateButton("Button/Rounded/Orange");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded/Pink", false, 0)]
        static void Brp()
        {
            CreateButton("Button/Rounded/Pink");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded/Purple", false, 0)]
        static void Brpu()
        {
            CreateButton("Button/Rounded/Purple");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded/Red", false, 0)]
        static void Brr()
        {
            CreateButton("Button/Rounded/Red");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Gradient/White", false, 0)]
        static void Brgw()
        {
            CreateButton("Button/Rounded - Gradient/White");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Gradient/Gray", false, 0)]
        static void Brgg()
        {
            CreateButton("Button/Rounded - Gradient/Gray");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Gradient/Blue", false, 0)]
        static void Brgb()
        {
            CreateButton("Button/Rounded - Gradient/Blue");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Gradient/Brown", false, 0)]
        static void Brgbr()
        {
            CreateButton("Button/Rounded - Gradient/Brown");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Gradient/Green", false, 0)]
        static void Brggr()
        {
            CreateButton("Button/Rounded - Gradient/Green");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Gradient/Night", false, 0)]
        static void Brgn()
        {
            CreateButton("Button/Rounded - Gradient/Night");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Gradient/Orange", false, 0)]
        static void Brgo()
        {
            CreateButton("Button/Rounded - Gradient/Orange");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Gradient/Pink", false, 0)]
        static void Brgp()
        {
            CreateButton("Button/Rounded - Gradient/Pink");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Gradient/Purple", false, 0)]
        static void Brgpu()
        {
            CreateButton("Button/Rounded - Gradient/Purple");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Gradient/Red", false, 0)]
        static void Brgre()
        {
            CreateButton("Button/Rounded - Gradient/Red");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline/Standard", false, 0)]
        static void Bros()
        {
            CreateButton("Button/Rounded - Outline/Standard");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline/White", false, 0)]
        static void Brow()
        {
            CreateButton("Button/Rounded - Outline/White");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline/Gray", false, 0)]
        static void Brog()
        {
            CreateButton("Button/Rounded - Outline/Gray");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline/Blue", false, 0)]
        static void Brob()
        {
            CreateButton("Button/Rounded - Outline/Blue");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline/Brown", false, 0)]
        static void Brobr()
        {
            CreateButton("Button/Rounded - Outline/Brown");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline/Green", false, 0)]
        static void Brogr()
        {
            CreateButton("Button/Rounded - Outline/Green");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline/Night", false, 0)]
        static void Bron()
        {
            CreateButton("Button/Rounded - Outline/Night");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline/Orange", false, 0)]
        static void Broo()
        {
            CreateButton("Button/Rounded - Outline/Orange");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline/Pink", false, 0)]
        static void Brop()
        {
            CreateButton("Button/Rounded - Outline/Pink");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline/Purple", false, 0)]
        static void Bropu()
        {
            CreateButton("Button/Rounded - Outline/Purple");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline/Red", false, 0)]
        static void Brore()
        {
            CreateButton("Button/Rounded - Outline/Red");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline Gradient/White", false, 0)]
        static void Brogw()
        {
            CreateButton("Button/Rounded - Outline Gradient/White");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline Gradient/Gray", false, 0)]
        static void Brogg()
        {
            CreateButton("Button/Rounded - Outline Gradient/Gray");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline Gradient/Blue", false, 0)]
        static void Brogb()
        {
            CreateButton("Button/Rounded - Outline Gradient/Blue");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline Gradient/Brown", false, 0)]
        static void Brogbr()
        {
            CreateButton("Button/Rounded - Outline Gradient/Brown");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline Gradient/Green", false, 0)]
        static void Broggr()
        {
            CreateButton("Button/Rounded - Outline Gradient/Green");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline Gradient/Night", false, 0)]
        static void Brogn()
        {
            CreateButton("Button/Rounded - Outline Gradient/Night");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline Gradient/Orange", false, 0)]
        static void Brogo()
        {
            CreateButton("Button/Rounded - Outline Gradient/Orange");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline Gradient/Pink", false, 0)]
        static void Brogp()
        {
            CreateButton("Button/Rounded - Outline Gradient/Pink");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline Gradient/Purple", false, 0)]
        static void Brogpu()
        {
            CreateButton("Button/Rounded - Outline Gradient/Purple");
        }

        [MenuItem("GameObject/Modern UI Pack/Button/Rounded - Outline Gradient/Red", false, 0)]
        static void Brogre()
        {
            CreateButton("Button/Rounded - Outline Gradient/Red");
        }

        [MenuItem("GameObject/Modern UI Pack/Charts/Pie Chart", false, 0)]
        static void Cpc()
        {
            CreateObject("Charts/Pie Chart");
        }

        [MenuItem("GameObject/Modern UI Pack/Context Menu/Standard", false, 0)]
        static void Ctxm()
        {
            CreateObject("Context Menu/Context Menu");
        }

        [MenuItem("GameObject/Modern UI Pack/Dropdown/Standard", false, 0)]
        static void Dsd()
        {
            CreateObject("Dropdown/Dropdown");
        }

        [MenuItem("GameObject/Modern UI Pack/Dropdown/Multi Select", false, 0)]
        static void Dmsd()
        {
            CreateObject("Dropdown/Dropdown - Multi Select");
        }

        [MenuItem("GameObject/Modern UI Pack/Horizontal Selector/Standard", false, 0)]
        static void Hss()
        {
            CreateObject("Horizontal Selector/Horizontal Selector");
        }

        [MenuItem("GameObject/Modern UI Pack/Input Field/Multi-Line", false, 0)]
        static void Iffml()
        {
            CreateObject("Input Field/Input Field - Multi-Line");
        }

        [MenuItem("GameObject/Modern UI Pack/Input Field/Fading (Left Aligned)", false, 0)]
        static void Iffla()
        {
            CreateObject("Input Field/Input Field - Fading (Left)");
        }

        [MenuItem("GameObject/Modern UI Pack/Input Field/Fading (Middle Aligned)", false, 0)]
        static void Iffma()
        {
            CreateObject("Input Field/Input Field - Fading (Middle)");
        }

        [MenuItem("GameObject/Modern UI Pack/Input Field/Fading (Right Aligned)", false, 0)]
        static void Iffra()
        {
            CreateObject("Input Field/Input Field - Fading (Right)");
        }

        [MenuItem("GameObject/Modern UI Pack/Input Field/Standard (Left Aligned)", false, 0)]
        static void Ifsla()
        {
            CreateObject("Input Field/Input Field - Standard (Left)");
        }

        [MenuItem("GameObject/Modern UI Pack/Input Field/Standard (Middle Aligned)", false, 0)]
        static void Ifsma()
        {
            CreateObject("Input Field/Input Field - Standard (Middle)");
        }

        [MenuItem("GameObject/Modern UI Pack/Input Field/Standard (Right Aligned)", false, 0)]
        static void Ifsra()
        {
            CreateObject("Input Field/Input Field - Standard (Right)");
        }

        [MenuItem("GameObject/Modern UI Pack/List View/Custom", false, 0)]
        static void Lvc()
        {
            CreateObject("List View/List View Custom");
        }

        [MenuItem("GameObject/Modern UI Pack/List View/Dynamic (Experimental)", false, 0)]
        static void Lvd()
        {
            CreateObject("List View/List View");
        }

        [MenuItem("GameObject/Modern UI Pack/Modal Window/Style 1", false, 0)]
        static void Mwss()
        {
            CreateObject("Modal Window/Style 1");
        }

        [MenuItem("GameObject/Modern UI Pack/Modal Window/Style 2", false, 0)]
        static void Mwsss()
        {
            CreateObject("Modal Window/Style 2");
        }

        [MenuItem("GameObject/Modern UI Pack/Movable Window/Standard", false, 0)]
        static void Mvwsswt()
        {
            CreateObject("Movable Window/Movable Window");
        }

        [MenuItem("GameObject/Modern UI Pack/Notification/Fade Animation", false, 0)]
        static void Nsn()
        {
            CreateObject("Notification/Fading Notification");
        }

        [MenuItem("GameObject/Modern UI Pack/Notification/Popup Animation", false, 0)]
        static void Nsp()
        {
            CreateObject("Notification/Popup Notification");
        }

        [MenuItem("GameObject/Modern UI Pack/Notification/Slide Animation", false, 0)]
        static void Nss()
        {
            CreateObject("Notification/Sliding Notification");
        }

        [MenuItem("GameObject/Modern UI Pack/Progress Bar/Standard", false, 0)]
        static void Pbs()
        {
            CreateObject("Progress Bar/PB - Standard");
        }

        [MenuItem("GameObject/Modern UI Pack/Progress Bar/Radial Thin", false, 0)]
        static void Pbrt()
        {
            CreateObject("Progress Bar/PB - Radial (Thin)");
        }

        [MenuItem("GameObject/Modern UI Pack/Progress Bar/Radial Light", false, 0)]
        static void Pbrl()
        {
            CreateObject("Progress Bar/PB - Radial (Light)");
        }

        [MenuItem("GameObject/Modern UI Pack/Progress Bar/Radial Regular", false, 0)]
        static void Pbrr()
        {
            CreateObject("Progress Bar/PB - Radial (Regular)");
        }

        [MenuItem("GameObject/Modern UI Pack/Progress Bar/Radial Bold", false, 0)]
        static void Pbrb()
        {
            CreateObject("Progress Bar/PB - Radial (Bold)");
        }

        [MenuItem("GameObject/Modern UI Pack/Progress Bar/Radial Filled Horizontal", false, 0)]
        static void Pbrfh()
        {
            CreateObject("Progress Bar/PB - Radial Filled Horizontal");
        }

        [MenuItem("GameObject/Modern UI Pack/Progress Bar/Radial Filled Vertical", false, 0)]
        static void Pbrfv()
        {
            CreateObject("Progress Bar/PB - Radial Filled Vertical");
        }

        [MenuItem("GameObject/Modern UI Pack/Spinner/Standard Fill", false, 0)]
        static void Pblsf()
        {
            CreateObject("Spinner/Spinner - Standard Fill");
        }

        [MenuItem("GameObject/Modern UI Pack/Spinner/Standard Run", false, 0)]
        static void Pblsr()
        {
            CreateObject("Spinner/Spinner - Standard Run");
        }

        [MenuItem("GameObject/Modern UI Pack/Spinner/Radial Material", false, 0)]
        static void Pblrm()
        {
            CreateObject("Spinner/Spinner - Radial Material");
        }

        [MenuItem("GameObject/Modern UI Pack/Spinner/Radial Pie", false, 0)]
        static void Pblrp()
        {
            CreateObject("Spinner/Spinner - Radial Pie");
        }

        [MenuItem("GameObject/Modern UI Pack/Spinner/Radial Run", false, 0)]
        static void Pblrr()
        {
            CreateObject("Spinner/Spinner - Radial Run");
        }

        [MenuItem("GameObject/Modern UI Pack/Spinner/Radial Trapez", false, 0)]
        static void Pblrt()
        {
            CreateObject("Spinner/Spinner - Radial Trapez");
        }

        [MenuItem("GameObject/Modern UI Pack/Scrollbar/Standard", false, 0)]
        static void Scs()
        {
            CreateObject("Scrollbar/Scrollbar");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Standard/Standard", false, 0)]
        static void Sls()
        {
            CreateObject("Slider/Standard/Slider - Standard");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Standard/Standard (Input)", false, 0)]
        static void Sli()
        {
            CreateObject("Slider/Standard/Slider - Standard (Input)");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Standard/Standard (Popup)", false, 0)]
        static void Slsp()
        {
            CreateObject("Slider/Standard/Slider - Standard (Popup)");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Standard/Standard (Value)", false, 0)]
        static void Slsv()
        {
            CreateObject("Slider/Standard/Slider - Standard (Value)");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Gradient/Gradient", false, 0)]
        static void Slg()
        {
            CreateObject("Slider/Gradient/Slider - Gradient");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Gradient/Gradient (Input)", false, 0)]
        static void Slgi()
        {
            CreateObject("Slider/Gradient/Slider - Gradient (Input)");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Gradient/Gradient (Popup)", false, 0)]
        static void Slgp()
        {
            CreateObject("Slider/Gradient/Slider - Gradient (Popup)");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Gradient/Gradient (Value)", false, 0)]
        static void Slgv()
        {
            CreateObject("Slider/Gradient/Slider - Gradient (Value)");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Outline/Outline", false, 0)]
        static void Slo()
        {
            CreateObject("Slider/Outline/Slider - Outline");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Outline/Outline (Input)", false, 0)]
        static void Sloi()
        {
            CreateObject("Slider/Outline/Slider - Outline (Input)");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Outline/Outline (Popup)", false, 0)]
        static void Slop()
        {
            CreateObject("Slider/Outline/Slider - Outline (Popup)");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Outline/Outline (Value)", false, 0)]
        static void Slov()
        {
            CreateObject("Slider/Outline/Slider - Outline (Value)");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Radial/Radial", false, 0)]
        static void Slr()
        {
            CreateObject("Slider/Radial/Slider - Radial");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Radial/Radial (Gradient)", false, 0)]
        static void Slrg()
        {
            CreateObject("Slider/Radial/Slider - Radial (Gradient)");
        }

        [MenuItem("GameObject/Modern UI Pack/Slider/Range/Range", false, 0)]
        static void Slra()
        {
            CreateObject("Slider/Range/Slider - Range");
        }

        [MenuItem("GameObject/Modern UI Pack/Switch/Standard", false, 0)]
        static void Sws()
        {
            CreateObject("Switch/Switch - Standard");
        }

        [MenuItem("GameObject/Modern UI Pack/Toggle/Standard", false, 0)]
        static void Tstst()
        {
            CreateObject("Toggle/Toggle - Standard");
        }

        [MenuItem("GameObject/Modern UI Pack/Toggle/Standard (Light)", false, 0)]
        static void Tstl()
        {
            CreateObject("Toggle/Toggle - Standard (Light)");
        }

        [MenuItem("GameObject/Modern UI Pack/Toggle/Standard (Regular)", false, 0)]
        static void Tstr()
        {
            CreateObject("Toggle/Toggle - Standard (Regular)");
        }

        [MenuItem("GameObject/Modern UI Pack/Toggle/Standard (Bold)", false, 0)]
        static void Tstb()
        {
            CreateObject("Toggle/Toggle - Standard (Bold)");
        }

        [MenuItem("GameObject/Modern UI Pack/Toggle/Toggle Group Panel", false, 0)]
        static void Ttgp()
        {
            CreateObject("Toggle/Toggle Group Panel");
        }

        [MenuItem("GameObject/Modern UI Pack/Tooltip/Tooltip Manager", false, 0)]
        static void Tts()
        {
            CreateObject("Tooltip/Tooltip");
        }

        [MenuItem("GameObject/Modern UI Pack/Window Manager/Standard", false, 0)]
        static void Mwm()
        {
            CreateObject("Window Manager/Window Manager");
        }
    }
}
#endif