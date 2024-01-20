using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using UnityEditor;


public class NewRegionalWeather: EditorWindow
{
    [MenuItem("Assets/Create/Weather")]
    public static void CreateMyAsset()
    {
        Weather asset = ScriptableObject.CreateInstance<Weather>();
        RegionalWeather.LoadWeather();
        int objectName = RegionalWeather.CheckNameUseage(0, RegionalWeather.allWeather);
        string newAssetName = "Weather(" + objectName + ")";
        AssetDatabase.CreateAsset(asset, "Assets/Resources/ScriptableObjects/" + newAssetName + ".asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

}


public class RegionalWeather : EditorWindow
{
    public static Weather[] allWeather;
    public Object mail;
    public static bool[] activeDisplays;
    public static bool[] activeDisplaysSituational;
    public static Weather[] situationalWeather;
    private Vector2 scrollPos = new Vector2();

    [MenuItem("Mail Settings/Manage Mail")]
    static void Init()
    {
        RegionalWeather window =
            (RegionalWeather)EditorWindow.GetWindow(typeof(RegionalWeather));
    }

    public static int CheckNameUseage(int name, Weather[] weatherList)
    {
        var finalName = "Mail(" + name + ")";
        for (int a = 0; a < weatherList.Length; a++)
        {
            if (weatherList[a].name == finalName)
            {
                name++;
                return (CheckNameUseage(name, weatherList));
            }
        }
        return name;
    }

    public void DestroyMail(string mailName, bool situational)
    {
        if (!situational)
        {
            AssetDatabase.DeleteAsset("Assets/Alasdair/Resources/ScriptableObjects/" + mailName + ".asset");
        }
        else
        {
            AssetDatabase.DeleteAsset("Assets/Alasdair/Resources/SituationalScriptableObjects/" + mailName + ".asset");
        }
        LoadWeather();
    }


    private static bool[] SetAll(bool[] openWidows, bool value)
    {
        for (int b = 0; b < openWidows.Length; b++)
        {
            openWidows[b] = value;
        }
        return openWidows;
    }


    public static void LoadWeather()
    {
        allWeather = Resources.LoadAll<Weather>("ScriptableObjects");
        activeDisplays = new bool[allWeather.Length];
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        scrollPos = GUILayout.BeginScrollView(scrollPos, false, true); GUILayout.BeginVertical();
        scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
        //mail = EditorGUILayout.ObjectField(mail, typeof(Mail), true);
        //if (!EditorApplication.isPlaying)

        if (GUILayout.Button("Load Mail"))
        {
            LoadWeather();
        }
        if (GUILayout.Button("Create new Mail"))
        {
            NewRegionalWeather.CreateMyAsset();
            LoadWeather();
            Repaint();
        }

        if (activeDisplays != null && activeDisplays.Length > 0)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Expand All"))
            {
                SetAll(activeDisplays, true);
                SetAll(activeDisplaysSituational, true);
            }

            if (GUILayout.Button("Collapse All"))
            {
                SetAll(activeDisplays, false);
                SetAll(activeDisplaysSituational, false);
            }
            EditorGUILayout.EndHorizontal();
        }

        // + mailName + ".asset"
        CreateList(allWeather, activeDisplays);

        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }


    private void CreateList(Weather[] mailList, bool[] displayInfo)
    {
        if (mailList != null)
        {
            for (int i = 0; i < mailList.Length; i++)
            {
                EditorUtility.SetDirty(mailList[i]);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(mailList[i].name + "'s Message: ");
                GUILayout.Space(-100);

                /*
                mailList[i].message = EditorGUILayout.TextField(mailList[i].message);
                displayInfo[i] = EditorGUILayout.Foldout(displayInfo[i], "");
                if (GUILayout.Button("Delete"))
                {
                    if (situationalMail.Contains(mailList[i]))
                    {
                        DestroyMail(mailList[i].name, true);
                        break;
                    }
                    else
                    {
                        DestroyMail(mailList[i].name, false);
                        break;
                    }
                }
                EditorGUILayout.EndHorizontal();


                if (displayInfo[i])
                {

                    EditorGUILayout.BeginHorizontal();
                    ApplySpacing("Email Request type: ");

                    var rType = (Weather.requestType)EditorGUILayout.EnumPopup(mailList[i].emailRequest);
                    mailList[i].emailRequest = Mail.requestType.approval;

                    //EnumFlagsField(mailList[i].emailRequest);

                    if (mailList[i].emailRequest == Mail.requestType.approval)
                    {
                        ApplySpacing("ExpectedResult: ");
                        mailList[i].expectedResult = (Mail.result)EditorGUILayout.EnumPopup(mailList[i].expectedResult);

                    }
                    else
                    {
                        // Should be a new variable for the multiple choice expected result but not implementing that yet

                        ApplySpacing("ExpectedResult: ");
                        mailList[i].expectedResult = (Mail.result)EditorGUILayout.EnumPopup(mailList[i].expectedResult);

                    }

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    ApplySpacing("Sanity bonus: ");
                    mailList[i].sanityBonus = EditorGUILayout.FloatField(mailList[i].sanityBonus);

                    ApplySpacing("Sanity negation: ");
                    mailList[i].sanityNegation = EditorGUILayout.FloatField(mailList[i].sanityNegation);

                    ApplySpacing("Energy negation: ");
                    mailList[i].energyNegation = EditorGUILayout.FloatField(mailList[i].energyNegation);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    ApplySpacing("Include Responce");
                    mailList[i].hasResponce = EditorGUILayout.Toggle(mailList[i].hasResponce);
                    EditorGUILayout.EndHorizontal();

                    if (mailList[i].hasResponce)
                    {
                        EditorGUILayout.BeginHorizontal();
                        ApplySpacing("Attatch Responce");
                        mailList[i].responceMail = (Mail)EditorGUILayout.ObjectField(mailList[i].responceMail, typeof(Mail), true);

                        ApplySpacing("Respond if: ");
                        mailList[i].respondIf = (Mail.result)EditorGUILayout.EnumPopup(mailList[i].respondIf);

                        ApplySpacing("Responce Delay: ");
                        mailList[i].followUpDelay = EditorGUILayout.FloatField(mailList[i].followUpDelay);
                        EditorGUILayout.EndHorizontal();

                    }

                    EditorGUILayout.BeginHorizontal();
                    ApplySpacing("AttachImageFile: ");
                    mailList[i].hasImageAttached = EditorGUILayout.Toggle(mailList[i].hasImageAttached);
                    if (mailList[i].hasImageAttached)
                    {
                        ApplySpacing("Image file");
                        mailList[i].attachedImage = (Sprite)EditorGUILayout.ObjectField(mailList[i].attachedImage, typeof(Sprite), false);
                    }
                    EditorGUILayout.EndHorizontal();


                    if (allMail[i].emailRequest == Mail.requestType.multipleChoice)
                    {

                        if (allMail[i].choices.Length != 0) {
                            for (int x = 0; x < allMail[i].choices.Length; x++)
                            {
                                var scheduleTime = EditorGUILayout.TextField(allMail[i].choices[x]);
                            }
                        }
                    }
                
               }*/
                
            }
        }
    }

    void ApplySpacing(string title)
    {
        GUILayout.Space(10);
        EditorGUILayout.LabelField(title);
        GUILayout.Space(-80);
    }

    /*
    void IterateMail(Weather mail)
    {
        if (mail.followUpMail != null)
        {
            foreach (Weather section in mail.followUpMail)
            {
                if (section != null)
                {

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(10 * dialogHierarchy);
                    section.Entry = EditorGUILayout.TextField("Entry", section.Entry);
                    if (GUILayout.Button("Add Dialogs"))
                    {
                        section.Responses = new Dialog[3];
                        for (int i = 0; i < section.Responses.Length; i++)
                        {
                            section.Responses[i] = CreateInstance<Weather>();
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    IterateDialog(section);
                    dialogHierarchy += 1;
                }
            }
        }
    
    }*/
    


    public void OnInspectorUpdate()
    {
        this.Repaint();
    }
}


