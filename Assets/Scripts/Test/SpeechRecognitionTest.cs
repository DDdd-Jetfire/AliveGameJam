//using UnityEngine;
//using UnityEngine.Windows.Speech;
//using System.Collections.Generic;

//public class SpeechRecognitionTest : MonoBehaviour
//{
//    private DictationRecognizer dictationRecognizer;
//    private KeywordRecognizer keywordRecognizer;

//    // 关键词列表
//    private readonly string[] keywords = { "开始", "stop", "hello", "unity" };

//    // 存储识别历史
//    private List<string> recognitionHistory = new List<string>();
//    private const int maxHistory = 10;

//    void Start()
//    {

//        if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
//        {
//            Debug.LogError("麦克风权限被拒绝！");

//        }
//        Debug.Log("正在初始化语音识别...");

//        // 检查麦克风设备
//        foreach (var device in Microphone.devices)
//        {
//            Debug.Log("可用麦克风: " + device);
//        }



//        Debug.Log("语音识别测试脚本启动");
//        Debug.Log("说 '开始' 开始自由语音识别，'停止' 停止识别");

//        // 初始化关键词识别器
//        InitializeKeywordRecognizer();
//    }

//    void InitializeKeywordRecognizer()
//    {
//        keywordRecognizer = new KeywordRecognizer(keywords);
//        keywordRecognizer.OnPhraseRecognized += OnKeywordRecognized;
//        keywordRecognizer.Start();

//        Debug.Log("关键词识别器已启动，等待指令...");
//    }

//    void InitializeDictationRecognizer()
//    {
//        dictationRecognizer = new DictationRecognizer();

//        dictationRecognizer.DictationResult += (text, confidence) =>
//        {
//            string result = $"识别结果: {text} (置信度: {confidence})";
//            Debug.Log(result);
//            AddToHistory(result);
//        };

//        dictationRecognizer.DictationHypothesis += (text) =>
//        {
//            Debug.Log($"假设结果: {text}");
//        };

//        dictationRecognizer.DictationComplete += (cause) =>
//        {
//            if (cause != DictationCompletionCause.Complete)
//            {
//                Debug.LogError($"识别中断，原因: {cause}");
//            }
//            CleanupDictationRecognizer();
//        };

//        dictationRecognizer.DictationError += (error, hresult) =>
//        {
//            Debug.LogError($"识别错误: {error} (HRESULT: {hresult})");
//            CleanupDictationRecognizer();
//        };

//        dictationRecognizer.Start();
//        Debug.Log("自由语音识别已启动，请开始说话...");
//    }

//    void OnKeywordRecognized(PhraseRecognizedEventArgs args)
//    {
//        string keyword = args.text;
//        Debug.Log($"识别到关键词: {keyword} (置信度: {args.confidence})");
//        AddToHistory($"关键词: {keyword}");

//        switch (keyword.ToLower())
//        {
//            case "开始":
//                if (dictationRecognizer == null)
//                {
//                    InitializeDictationRecognizer();
//                }
//                break;

//            case "stop":
//                CleanupDictationRecognizer();
//                break;

//            case "hello":
//                Debug.Log("你好！这是一个语音识别测试。");
//                AddToHistory("系统: 你好！");
//                break;

//            case "unity":
//                Debug.Log("Unity 是一款强大的游戏引擎！");
//                AddToHistory("系统: Unity 是一款强大的游戏引擎！");
//                break;
//        }
//    }

//    void AddToHistory(string entry)
//    {
//        recognitionHistory.Add(entry);
//        if (recognitionHistory.Count > maxHistory)
//        {
//            recognitionHistory.RemoveAt(0);
//        }
//    }

//    void CleanupDictationRecognizer()
//    {
//        if (dictationRecognizer != null)
//        {
//            dictationRecognizer.Stop();
//            dictationRecognizer.Dispose();
//            dictationRecognizer = null;
//            Debug.Log("自由语音识别已停止");
//        }
//    }

//    void OnDestroy()
//    {
//        CleanupDictationRecognizer();

//        if (keywordRecognizer != null)
//        {
//            keywordRecognizer.Stop();
//            keywordRecognizer.Dispose();
//        }
//    }

//    // 可选：在Update中显示最近识别结果
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.H))
//        {
//            Debug.Log("=== 识别历史 ===");
//            foreach (var entry in recognitionHistory)
//            {
//                Debug.Log(entry);
//            }
//        }
//    }
//}


using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections;
using System.Collections.Generic;

public class SpeechRecognitionTest : MonoBehaviour
    {
    private DictationRecognizer dictationRecognizer;
    private KeywordRecognizer keywordRecognizer;

    // 修改为更明确的中文关键词
    private readonly string[] keywords = { "开始录音", "停止录音", "你好", "Unity" };

    private List<string> recognitionHistory = new List<string>();
    private const int maxHistory = 10;

    // 新增麦克风状态检测
    private bool isMicrophoneActive = false;
    private AudioClip microphoneClip;
    private const int sampleWindow = 128;

    IEnumerator Start()
        {
        Debug.Log("=== 语音识别系统初始化 ===");

        // 1. 强制请求麦克风权限
        yield return RequestMicrophonePermission();

        // 2. 检查麦克风设备
        CheckMicrophones();

        // 3. 启动麦克风音量监测
        StartMicrophoneMonitoring();

        // 4. 初始化关键词识别器
        InitializeKeywordRecognizer();

        Debug.Log("指令列表:\n" +
                 "'开始录音' - 启动自由录音\n" +
                 "'停止录音' - 停止录音\n" +
                 "'你好' - 测试交互\n" +
                 "'Unity' - 系统信息");
        }

    IEnumerator RequestMicrophonePermission()
        {
        Debug.Log("正在请求麦克风权限...");
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);

        if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
            {
            Debug.LogError("麦克风权限被拒绝！请检查系统设置");
            yield break;
            }

        Debug.Log("麦克风权限已授予");
        }

    void CheckMicrophones()
        {
        if (Microphone.devices.Length == 0)
            {
            Debug.LogError("未检测到麦克风设备！");
            return;
            }

        Debug.Log("可用麦克风设备:");
        foreach (var device in Microphone.devices)
            {
            Debug.Log($"- {device}");
            }
        }

    void StartMicrophoneMonitoring()
        {
        microphoneClip = Microphone.Start(null, true, 1, 44100);
        isMicrophoneActive = true;
        Debug.Log("麦克风输入监测已启动");
        }

    void InitializeKeywordRecognizer()
        {
        // 清理现有识别器
        if (keywordRecognizer != null)
            {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
            }

        // 使用低置信度提高识别率
        keywordRecognizer = new KeywordRecognizer(keywords, ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += OnKeywordRecognized;

        try
            {
            keywordRecognizer.Start();
            Debug.Log("关键词识别器启动成功（灵敏度: Low）");
            }
        catch (System.Exception e)
            {
            Debug.LogError($"识别器启动失败: {e.Message}");
            }
        }

    void InitializeDictationRecognizer()
        {
        CleanupDictationRecognizer();

        dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            string result = $"识别结果: {text} (准确率: {confidence:P0})";
            Debug.Log(result);
            AddToHistory(result);
        };

        dictationRecognizer.DictationHypothesis += (text) =>
        {
            Debug.Log($"正在识别: {text}");
        };

        dictationRecognizer.DictationComplete += (cause) =>
        {
            string message = cause == DictationCompletionCause.Complete ?
                "正常结束" : $"异常中断: {cause}";
            Debug.Log($"自由录音结束 - {message}");
            CleanupDictationRecognizer();
        };

        dictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogError($"识别错误: {error} (代码: {hresult})");
            CleanupDictationRecognizer();
        };

        dictationRecognizer.Start();
        Debug.Log("自由录音模式已激活，请开始说话...");
        }

    void OnKeywordRecognized(PhraseRecognizedEventArgs args)
        {
        string keyword = args.text;


        Debug.Log($"识别到指令: [{keyword}] ");
        AddToHistory($"指令: {keyword}");

        switch (keyword)
            {
            case "开始录音":
                if (dictationRecognizer == null)
                    {
                    InitializeDictationRecognizer();
                    }
                break;

            case "停止录音":
                CleanupDictationRecognizer();
                break;

            case "你好":
                string response = "你好！语音系统运行中，当前音量: " + GetMicrophoneLevel();
                Debug.Log(response);
                AddToHistory($"系统: {response}");
                break;

            case "Unity":
                Debug.Log("Unity语音系统已就绪");
                AddToHistory("系统: Unity引擎运行正常");
                break;
            }
        }

    // 新增：实时获取麦克风音量（0-1）
    float GetMicrophoneLevel()
        {
        if (!isMicrophoneActive) return 0f;

        float levelMax = 0;
        float[] waveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(null) - sampleWindow;

        if (micPosition < 0) return 0;

        microphoneClip.GetData(waveData, micPosition);

        for (int i = 0; i < sampleWindow; i++)
            {
            float wavePeak = Mathf.Abs(waveData[i]);
            levelMax = Mathf.Max(levelMax, wavePeak);
            }

        return levelMax;
        }

    void AddToHistory(string entry)
        {
        recognitionHistory.Add($"[{Time.time:F1}s] {entry}");
        if (recognitionHistory.Count > maxHistory)
            {
            recognitionHistory.RemoveAt(0);
            }
        }

    void CleanupDictationRecognizer()
        {
        if (dictationRecognizer != null)
            {
            dictationRecognizer.Stop();
            dictationRecognizer.Dispose();
            dictationRecognizer = null;
            Debug.Log("自由录音已停止");
            }
        }

    void Update()
        {
        // 按H显示历史记录
        if (Input.GetKeyDown(KeyCode.H))
            {
            Debug.Log("=== 最近识别记录 ===");
            foreach (var entry in recognitionHistory)
                {
                Debug.Log(entry);
                }
            }

        // 按V显示实时音量（调试用）

        Debug.Log($"当前麦克风音量: {GetMicrophoneLevel():P0}");

        }

    void OnDestroy()
        {
        CleanupDictationRecognizer();

        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
            {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
            }

        if (isMicrophoneActive)
            {
            Microphone.End(null);
            }
        }
    }
