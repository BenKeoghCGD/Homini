using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using NMaterial;

public class GameManager : MonoBehaviour
{

    [Header("Gameplay")]
    [SerializeField] [InspectorName("UI")] GameObject g_UI;
    [SerializeField] [InspectorName("Player")] GameObject g_Player;

    [Header("Cutscenes")]
    [SerializeField] [InspectorName("UI")] GameObject c_UI;
    [SerializeField] [InspectorName("Title")] Text c_Title;
    [SerializeField] [InspectorName("Description")] Text c_Description;
    [SerializeField] [InspectorName("Black Bars (Super Serious)")] GameObject c_BB;

    [Header("Tutorial")]
    [SerializeField] [InspectorName("UI Parent")] GameObject t_UI;
    [SerializeField] [InspectorName("Progress Icon")] Image t_Progress;
    [SerializeField] [InspectorName("Task Title")] Text t_Title;
    [SerializeField] [InspectorName("Task Title")] Text t_Description;

    // Private Variables
    bool inCutscene = false;
    int GameplayStage = 0, currStage = -1, GameplayTask = 0, currTask = -1;
    float taskCur = 0, taskTarget = 0;
    InventorySystem _is;
    InventoryItem _ii;

    private void Awake() => DontDestroyOnLoad(this.gameObject);

    private void Start()
    {
        c_Title.color = new Color(c_Title.color.r, c_Title.color.g, c_Title.color.b, 0);
        c_Description.color = new Color(c_Description.color.r, c_Description.color.g, c_Description.color.b, 0);
        c_BB.transform.localScale = Vector3.one * 1.5f;
        g_UI.SetActive(!inCutscene);
        g_Player.SetActive(!inCutscene);
        c_UI.SetActive(inCutscene);
        c_BB.SetActive(false);
        _is = FindObjectOfType<InventorySystem>();
    }

    private void Update()
    {
        g_UI.SetActive(!inCutscene);
        g_Player.SetActive(!inCutscene);
        c_UI.SetActive(inCutscene);

        if (inCutscene) return;

        string title = "", description = "";
        switch (GameplayStage)
        {
            case 0: // TUTORIAL
                if(GameplayStage != currStage)
                {
                    c_Title.text = "TUTORIAL";
                    c_Description.text = "Introduction to gameplay mechanics.";
                    BeginAnim(true);
                    break;
                }


                switch(GameplayTask)
                {
                    case 0:
                        title = "Walking Around";
                        description = "Walk Forward";
                        taskTarget = 1.5f;
                        if(Input.GetKey(KeyCode.W)) taskCur += Time.deltaTime;
                        if (taskCur >= taskTarget)
                        {
                            taskCur = 0f;
                            GameplayTask++;
                        }
                        break;
                    case 1:
                        title = "Walking Around";
                        description = "Walk Backwards";
                        taskTarget = 1.5f;
                        if(Input.GetKey(KeyCode.S)) taskCur += Time.deltaTime;
                        if (taskCur >= taskTarget)
                        {
                            taskCur = 0f;
                            GameplayTask++;
                        }
                        break;
                    case 2:
                        title = "Walking Around";
                        description = "Walk Left";
                        taskTarget = 1.5f;
                        if(Input.GetKey(KeyCode.A)) taskCur += Time.deltaTime;
                        if (taskCur >= taskTarget)
                        {
                            taskCur = 0f;
                            GameplayTask++;
                        }
                        break;
                    case 3:
                        title = "Walking Around";
                        description = "Walk Right";
                        taskTarget = 1.5f;
                        if(Input.GetKey(KeyCode.D)) taskCur += Time.deltaTime;
                        if (taskCur >= taskTarget)
                        {
                            taskCur = 0f;
                            GameplayTask++;
                        }
                        break;

                    default:
                        GameplayStage++;
                        GameplayTask = 0;
                        break;
                }
                break;

            case 1: // TUTORIAL
                if (GameplayStage != currStage)
                {
                    c_Title.text = "CRAFT AN AXE";
                    c_Description.text = "Introduction to foraging and crafting.";
                    BeginAnim(true);
                    break;
                }


                switch (GameplayTask)
                {
                    case 0:
                        title = "GATHER RESOURCES";
                        description = "Gather 2 Wood";
                        taskTarget = 2f;
                        if (_is.Get(Mat.getIID(Materials.WOOD), out _ii)) taskCur = _ii.StackSize;
                        else taskCur = 0;
                        if (taskCur >= taskTarget)
                        {
                            taskCur = 0f;
                            GameplayTask++;
                        }
                        break;
                    case 1:
                        title = "GATHER RESOURCES";
                        description = "Gather 3 Stone";
                        taskTarget = 3f;
                        if (_is.Get(Mat.getIID(Materials.STONE), out _ii)) taskCur = _ii.StackSize;
                        else taskCur = 0;
                        if (taskCur >= taskTarget)
                        {
                            taskCur = 0f;
                            GameplayTask++;
                        }
                        break;
                    case 2:
                        title = "CRAFT THE AXE";
                        description = "Find the Crafting Table";
                        taskTarget = 100f;
                        float dist = Vector3.Distance(FindObjectOfType<Player>().transform.position, FindObjectOfType<CraftingBench>().transform.position);
                        taskCur = taskTarget - dist;
                        taskCur += taskTarget / 10f;
                        if (taskCur >= taskTarget)
                        {
                            taskCur = 0f;
                            GameplayTask++;
                        }
                        break;

                    default:
                        GameplayStage++;
                        GameplayTask = 0;
                        break;
                }
                break;

            default:
                t_UI.SetActive(false);
                break;
        }

        t_Progress.fillAmount = taskCur / taskTarget;
        t_Title.text = title;
        t_Description.text = description;

    }

    private void BeginAnim(bool isStage = false) => StartCoroutine(beginAnim(isStage));

    private IEnumerator beginAnim(bool isStage = false)
    {
        inCutscene = true;
        GameObject go_Title = c_Title.gameObject;
        GameObject go_Description = c_Description.gameObject;

        Vector3 pos_Title = go_Title.transform.position;
        Vector3 pos_Description = go_Description.transform.position;

        Vector3 offset_Title = pos_Title - new Vector3(0, 20f);
        Vector3 offset_Description = pos_Description - new Vector3(0, 20f);

        yield return blackBars(true);

        for (float f = 0; f < 1f; f += Time.deltaTime)
        {
            go_Title.transform.position = Vector3.Lerp(offset_Title, pos_Title, f);
            go_Description.transform.position = Vector3.Lerp(offset_Description, pos_Description, f);
            c_Title.color = new Color(c_Title.color.r, c_Title.color.g, c_Title.color.b, f);
            c_Description.color = new Color(c_Description.color.r, c_Description.color.g, c_Description.color.b, f);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        for (float f = 1; f > 0f; f -= Time.deltaTime)
        {
            go_Title.transform.position = Vector3.Lerp(offset_Title, pos_Title, f);
            go_Description.transform.position = Vector3.Lerp(offset_Description, pos_Description, f);
            c_Title.color = new Color(c_Title.color.r, c_Title.color.g, c_Title.color.b, f);
            c_Description.color = new Color(c_Description.color.r, c_Description.color.g, c_Description.color.b, f);
            yield return null;
        }
        yield return blackBars(false);

        if (isStage) currStage = GameplayStage;
        else currTask = GameplayTask;

        inCutscene = false;
    }

    bool sh = true;
    [ContextMenu("bb")]
    public void blck()
    {
        GameplayStage++;
    }

    public IEnumerator blackBars(bool show = true)
    {
        if (show)
        {
            c_BB.SetActive(true);
            for (float f = 1.5f; f > 1f; f -= Time.deltaTime)
            {
                c_BB.transform.localScale = Vector3.one * f;
                yield return null;
            }
        }
        else
        {
            for (float f = 1f; f < 1.5f; f += Time.deltaTime)
            {
                c_BB.transform.localScale = Vector3.one * f;
                yield return null;
            }

            c_BB.SetActive(false);
        }
    }
}