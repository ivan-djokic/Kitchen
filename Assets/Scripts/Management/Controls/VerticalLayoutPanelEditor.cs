// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

//using UnityEditor;
//using UnityEditor.UI;

//namespace Kitchen.Management.Controls
//{
//	[CanEditMultipleObjects]
//	[CustomEditor(typeof(VerticalLayoutPanel))]
//	public class VerticalLayoutPanelEditor : HorizontalOrVerticalLayoutGroupEditor
//	{
//		private SerializedProperty m_defaultIndex;

//		protected override void OnEnable()
//		{
//			base.OnEnable();
//			m_defaultIndex = serializedObject.FindProperty(nameof(m_defaultIndex));
//		}

//		public override void OnInspectorGUI()
//		{
//			base.OnInspectorGUI();

//			serializedObject.Update();
//			EditorGUILayout.PropertyField(m_defaultIndex);
//			serializedObject.ApplyModifiedProperties();
//		}
//	}
//}
