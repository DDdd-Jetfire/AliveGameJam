using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

[CustomEditor(typeof(NVC))]
public class NVCEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 绘制默认Inspector
        DrawDefaultInspector();

        // 添加分隔线
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("播放控制", EditorStyles.boldLabel);

        // 获取目标脚本
        NVC controller = (NVC)target;

        // 创建按钮布局
        EditorGUILayout.BeginHorizontal();

        // 播放按钮
        if (GUILayout.Button("播放"))
        {
            controller.PlayVideo();
        }

        // 暂停按钮
        if (GUILayout.Button("暂停"))
        {
            controller.PauseVideo();
        }

        // 停止按钮
        if (GUILayout.Button("停止"))
        {
            controller.StopVideo();
        }

        if (GUILayout.Button("更改播放速度"))
        {
            controller.SetVideoSpeed(controller.playSpeed);
        }

        EditorGUILayout.EndHorizontal();

        // 第二行按钮
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.EndHorizontal();

        // 添加状态信息
        if (Application.isPlaying)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("当前状态", EditorStyles.boldLabel);

            if (controller.vp != null)
            {
                EditorGUILayout.LabelField($"播放状态: {(controller.isPlaying ? "播放中" : "已暂停")}");

                if (controller.vp.isPrepared)
                {
                    EditorGUILayout.LabelField($"进度: {controller.vp.time:F1}/{controller.vp.length:F1} 秒");
                }
            }
        }
    }
}