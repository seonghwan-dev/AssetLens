﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RV
{
	[CustomEditor(typeof(ReferenceSetting))]
	internal sealed class ReferenceSettingInspector : Editor
	{
		private SerializedProperty enabled = default;
		private SerializedProperty pauseInPlaymode = default;
		private SerializedProperty traceSceneObject = default;
		private SerializedProperty useEditorUtilityWhenSearchDependencies = default;
		private SerializedProperty localization = default;

		private bool unlockDangerZone = false;
		private bool isInProgress = false;

		private void OnEnable()
		{
			enabled = serializedObject.FindProperty(nameof(enabled));
			pauseInPlaymode = serializedObject.FindProperty(nameof(pauseInPlaymode));
			traceSceneObject = serializedObject.FindProperty(nameof(traceSceneObject));
			useEditorUtilityWhenSearchDependencies = serializedObject.FindProperty(nameof(useEditorUtilityWhenSearchDependencies));
			localization = serializedObject.FindProperty(nameof(localization));
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			EditorGUI.BeginChangeCheck();
			
			EditorGUILayout.PropertyField(enabled, new GUIContent(Localize.Inst.setting_enabled));
			EditorGUILayout.Space(12);

			
			EditorGUI.BeginDisabledGroup(!enabled.boolValue);
			{
				// EditorGUI.indentLevel++;
				{
					EditorGUILayout.LabelField(Localize.Inst.setting_workflow, EditorStyles.boldLabel);
					EditorGUILayout.BeginVertical("HelpBox");

					EditorGUILayout.PropertyField(pauseInPlaymode, new GUIContent(Localize.Inst.setting_pauseInPlaymode));
					EditorGUILayout.PropertyField(traceSceneObject, new GUIContent(Localize.Inst.setting_traceSceneObjects));
					EditorGUILayout.PropertyField(useEditorUtilityWhenSearchDependencies, new GUIContent(Localize.Inst.setting_useEditorUtilityWhenSearchDependencies));
					EditorGUILayout.EndVertical();
					
					EditorGUILayout.Space(10);
					
					EditorGUILayout.LabelField(Localize.Inst.setting_miscellaneous, EditorStyles.boldLabel);
					EditorGUILayout.BeginVertical("HelpBox");
					
					var root = Path.GetFullPath($"Packages/kr.seonghwan.reference/Languages");
					var currentLocale = localization.stringValue;

					var localNames = new List<string>();
					var languageFiles = Directory.GetFiles(root, "*.json");
					foreach (string file in languageFiles)
					{
						var fi = new FileInfo(file);
						localNames.Add(fi.Name.Replace(".json",""));
					}

					int selected = localNames.IndexOf(currentLocale);
					int changed = EditorGUILayout.Popup(Localize.Inst.setting_language, selected, localNames.ToArray());

					if (selected != changed)
					{
						ReferenceSetting.Localization = localNames[changed];
						Localize.Inst = ReferenceSetting.LoadLocalization;
					}
					EditorGUILayout.EndVertical();
				}
				// EditorGUI.indentLevel--;
			}
			EditorGUI.EndDisabledGroup();
			
			EditorGUILayout.Space(20);
			
			using (new EditorGUILayout.HorizontalScope(GUILayout.Height(26)))
			{
				GUILayout.FlexibleSpace();

				if (!unlockDangerZone)
				{

					if (GUILayout.Button(Localize.Inst.setting_unlockDangerzone, new GUILayoutOption[]
					{
						GUILayout.Width(200), GUILayout.Height(24)
					}))
					{
						unlockDangerZone = true;
					}
				}
				else
				{
					GUILayout.Label(Localize.Inst.setting_dangerzone, GUILayout.Height(24));
				}

				GUILayout.FlexibleSpace();
			}
			
			using (new EditorGUILayout.VerticalScope("HelpBox"))
			{
				EditorGUILayout.Space(4);

				using (new EditorGUI.DisabledGroupScope(!unlockDangerZone || isInProgress))
				{
					if (GUILayout.Button(Localize.Inst.setting_cleanUpCache,
						new[] { GUILayout.Height(30) }))
					{
						Debug.Log("Delete...");
						isInProgress = true;
						
						CleanUpCache();
					}
				}	
				EditorGUILayout.Space(4);
			}

			if (EditorGUI.EndChangeCheck())
			{
				ReferenceWindow.isDirty = true;
			}
			
			serializedObject.ApplyModifiedProperties();
		}

		private async void CleanUpCache()
		{
			int processedAssetCount = await ReferenceCache.CleanUpAssets();
			Debug.Log($"{processedAssetCount} asset caches removed!");

			isInProgress = false;
		}
	}
}