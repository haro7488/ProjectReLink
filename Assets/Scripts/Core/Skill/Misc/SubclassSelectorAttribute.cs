using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ProjectReLink.Utils
{
    public class SubclassSelectorAttribute : PropertyAttribute
    {
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
    public class SubclassSelectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference)
            {
                EditorGUI.LabelField(position, label.text, "Use [SubclassSelector] with [SerializeReference] only.");
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            // 1. 헤더 영역 (Label + Dropdown) 계산
            Rect headerRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            // 2. 폴드아웃 레이블 그리기
            property.isExpanded =
                EditorGUI.Foldout(new Rect(headerRect.x, headerRect.y, EditorGUIUtility.labelWidth, headerRect.height),
                    property.isExpanded, label, true);

            // 3. 드롭다운 버튼 그리기 (레이블 영역 제외한 우측)
            Rect buttonRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y,
                position.width - EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);

            string currentTypeName = GetCurrentTypeName(property);

            if (EditorGUI.DropdownButton(buttonRect, new GUIContent(currentTypeName), FocusType.Keyboard))
            {
                ShowTypeMenu(property);
            }

            // 4. 자식 필드 그리기 (폴드아웃이 열려있고 값이 있을 때)
            if (property.isExpanded && !string.IsNullOrEmpty(property.managedReferenceFullTypename))
            {
                EditorGUI.indentLevel++;

                // managedReferenceValue의 필드들을 순회하며 그리기
                SerializedProperty childProp = property.Copy();
                int depth = childProp.depth;
                bool enterChildren = true;

                float currentY = position.y + EditorGUIUtility.singleLineHeight +
                                 EditorGUIUtility.standardVerticalSpacing;

                while (childProp.NextVisible(enterChildren) && childProp.depth > depth)
                {
                    enterChildren = false;
                    float childHeight = EditorGUI.GetPropertyHeight(childProp, true);
                    Rect childRect = new Rect(position.x, currentY, position.width, childHeight);

                    EditorGUI.PropertyField(childRect, childProp, true);

                    currentY += childHeight + EditorGUIUtility.standardVerticalSpacing;
                }

                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;

            if (property.isExpanded && !string.IsNullOrEmpty(property.managedReferenceFullTypename))
            {
                SerializedProperty childProp = property.Copy();
                int depth = childProp.depth;
                bool enterChildren = true;

                while (childProp.NextVisible(enterChildren) && childProp.depth > depth)
                {
                    enterChildren = false;
                    height += EditorGUI.GetPropertyHeight(childProp, true) + EditorGUIUtility.standardVerticalSpacing;
                }
            }

            return height;
        }

        private string GetCurrentTypeName(SerializedProperty property)
        {
            if (string.IsNullOrEmpty(property.managedReferenceFullTypename)) return "None (Null)";
            var split = property.managedReferenceFullTypename.Split(' ');
            return split.Length > 1 ? split[1].Split('.').Last() : split[0];
        }

        private void ShowTypeMenu(SerializedProperty property)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("None"), string.IsNullOrEmpty(property.managedReferenceFullTypename), () =>
            {
                property.managedReferenceValue = null;
                property.serializedObject.ApplyModifiedProperties();
            });

            Type baseType = GetFieldType(property);
            if (baseType != null)
            {
                var derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => baseType.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

                foreach (var type in derivedTypes)
                {
                    bool isSelected = property.managedReferenceFullTypename.Contains(type.FullName);
                    menu.AddItem(new GUIContent(type.Name), isSelected, () =>
                    {
                        property.managedReferenceValue = Activator.CreateInstance(type);
                        property.serializedObject.ApplyModifiedProperties();
                    });
                }
            }

            menu.ShowAsContext();
        }

        private Type GetFieldType(SerializedProperty property)
        {
            var typeName = property.managedReferenceFieldTypename.Split(' ');
            if (typeName.Length < 2) return null;
            return Type.GetType($"{typeName[1]}, {typeName[0]}");
        }
    }
#endif
}