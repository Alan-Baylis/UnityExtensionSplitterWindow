using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

namespace UnityEditorExtension
{
	public class SpritterWindowTest : EditorWindow
	{
		private Splitter splitter;
		private  Rect rect = new Rect();
		private const float height = 20f;
		private List<string> list = new List<string>();
		private Vector2 pos = Vector2.zero;

		[MenuItem("SplitterWindow/TestWindow", false, int.MaxValue)]
		public static void ShowWindow()
		{
			CreateInstance<SpritterWindowTest>().Show();
		}

		public void OnGUI()
		{
			if (splitter == null)
			{
				splitter = new Splitter(position.height / 2, DrawUpperGUI, DrawDwonGUI, Splitter.SplitMode.Horizonal, 30f);
			}	
			rect.width = position.width;
			rect.height = position.height;
			if (splitter.DoSplitter(rect))
			{
				Repaint();
			}
		}

		private void DrawUpperGUI(Rect rect)
		{
			using (new GUILayout.AreaScope(rect))
			{
				pos = EditorGUILayout.BeginScrollView(pos);
				IEnumerator enumerator = list.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						var str = (string)enumerator.Current;
						GUILayout.Label(str, GUILayout.Height(height));
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				GUILayout.EndScrollView();
			}
		}

		private void DrawDwonGUI(Rect rect)
		{
			using (new GUILayout.AreaScope(rect))
			{
				GUILayout.Space(10f);
				if (GUILayout.Button("add"))
				{
					list.Add(System.Guid.NewGuid().ToString());
				}
				if (GUILayout.Button("clear"))
				{
					list.Clear();
				}
			}
		}
	}
}
	
#endif
