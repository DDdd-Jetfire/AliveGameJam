using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

[CustomEditor(typeof(NVC))]
public class NVCEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // ����Ĭ��Inspector
        DrawDefaultInspector();

        // ��ӷָ���
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("���ſ���", EditorStyles.boldLabel);

        // ��ȡĿ��ű�
        NVC controller = (NVC)target;

        // ������ť����
        EditorGUILayout.BeginHorizontal();

        // ���Ű�ť
        if (GUILayout.Button("����"))
        {
            controller.PlayVideo();
        }

        // ��ͣ��ť
        if (GUILayout.Button("��ͣ"))
        {
            controller.PauseVideo();
        }

        // ֹͣ��ť
        if (GUILayout.Button("ֹͣ"))
        {
            controller.StopVideo();
        }

        if (GUILayout.Button("���Ĳ����ٶ�"))
        {
            controller.SetVideoSpeed(controller.playSpeed);
        }

        EditorGUILayout.EndHorizontal();

        // �ڶ��а�ť
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.EndHorizontal();

        // ���״̬��Ϣ
        if (Application.isPlaying)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("��ǰ״̬", EditorStyles.boldLabel);

            if (controller.vp != null)
            {
                EditorGUILayout.LabelField($"����״̬: {(controller.isPlaying ? "������" : "����ͣ")}");

                if (controller.vp.isPrepared)
                {
                    EditorGUILayout.LabelField($"����: {controller.vp.time:F1}/{controller.vp.length:F1} ��");
                }
            }
        }
    }
}