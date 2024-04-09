/*
 * File Name: LayerAttributeDrawer.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 8, 2024
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace Kokowolo.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerAttributeDrawer : PropertyDrawer
    {
        /************************************************************/
        #region Functions

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer && property.propertyType != SerializedPropertyType.LayerMask) 
            {
                EditorGUI.LabelField(position, "property must be an Integer or LayerMask");
                return;
            }

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                property.intValue = EditorGUI.LayerField(position, label, property.intValue);
            }
            else
            {
                if (LayerMaskUtils.ContainsMultipleLayers(property.intValue))
                {
                    property.intValue = 0;
                }
                
                int layer = EditorGUI.LayerField(position, label, LayerMaskUtils.LayerMaskToLayer(property.intValue));
                property.intValue = 1 << layer;
            }
            
        }

        #endregion
        /************************************************************/
    }
}