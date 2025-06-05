/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: May 11, 2022
 * 
 * Additional Comments:
 *		File Line Length: ~140
 *
 *      Shortcut -> alt + shift + index
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using System.IO;

namespace Kokowolo.Utilities.Editor
{
    /// <summary>
    /// Manages the current Graphics Settings for the project
    /// </summary>
    public class GraphicsSettingsManager
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        // Graphics Settings Names
        // TODO: [BED-108] Read Quality Settings - read which setting is currently selected... unfortunately this would require creating 
        //      some ScriptableObject names below and changing the Clear() method
        private const string GraphicsLowName = "Built-In";
        private const string GraphicsMediumName = "URP_Performant";
        private const string GraphicsHighName = "URP_Balanced";
        private const string GraphicsHighestName = "URP_HighFidelity";
        
        // Menu Names
        private const string GraphicsLowMenuName = "Kokowolo/Graphics Settings/" + GraphicsLowName + " &#1";
        private const string GraphicsMediumMenuName = "Kokowolo/Graphics Settings/" + GraphicsMediumName + " &#2";
        private const string GraphicsHighMenuName = "Kokowolo/Graphics Settings/" + GraphicsHighName + " &#3";
        private const string GraphicsHighestMenuName = "Kokowolo/Graphics Settings/" + GraphicsHighestName + " &#4";

        // Menu Item params
        private const bool isValidateFunction = false;
        private const int priority = 40;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        private static int _SettingsIndex = -1;
        private static int SettingsIndex
        {
            // get => _SettingsIndex;
            set
            {
                if (0 < _SettingsIndex && _SettingsIndex <= 4)
                {
                    Menu.SetChecked(GetMenuName(_SettingsIndex), false);
                }
                _SettingsIndex = value;
                if (0 < _SettingsIndex && _SettingsIndex <= 4)
                {
                    Menu.SetChecked(GetMenuName(_SettingsIndex), true);
                } 
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        [InitializeOnLoadMethod]
        private static void Clear()
        {
            SettingsIndex = 0;
            Menu.SetChecked(GetMenuName(1), false);
            Menu.SetChecked(GetMenuName(2), false);
            Menu.SetChecked(GetMenuName(3), false);
            Menu.SetChecked(GetMenuName(4), false);
        }

        [MenuItem(itemName: GraphicsLowMenuName, isValidateFunction, priority + 1)]
        public static void SetGraphicsSettingsLow()
        {
            SetGraphicsSettings(1);
        }

        [MenuItem(itemName: GraphicsMediumMenuName, isValidateFunction, priority + 2)]
        public static void SetGraphicsSettingsMedium()
        {
            SetGraphicsSettings(2);
        }

        [MenuItem(itemName: GraphicsHighMenuName, isValidateFunction, priority + 3)]
        public static void SetGraphicsSettingsHigh()
        {
            SetGraphicsSettings(3);
        }

        [MenuItem(itemName: GraphicsHighestMenuName, isValidateFunction, priority + 4)]
        public static void SetGraphicsSettingsHighest()
        {
            SetGraphicsSettings(4);
        }

        private static void SetGraphicsSettings(int settingsIndex)
        {
            // set checked
            SettingsIndex = settingsIndex;

            // assign grapics settings
            QualitySettings.SetQualityLevel(settingsIndex, applyExpensiveChanges: true);
            GraphicsSettings.defaultRenderPipeline = QualitySettings.renderPipeline;
            LogCurrentRenderPipeline();
        }

        private static string GetMenuName(int settingsIndex)
        {
            return settingsIndex switch
            {
                1 => GraphicsLowMenuName,
                2 => GraphicsMediumMenuName,
                3 => GraphicsHighMenuName,
                4 => GraphicsHighestMenuName,
                _ => throw new System.Exception("settings option not found"),
            };
        }

        private static void LogCurrentRenderPipeline()
        {
            // GraphicsSettings.defaultRenderPipeline determines the default render pipeline
            // If it is null, the default is the Built-in Render Pipeline
            if (GraphicsSettings.defaultRenderPipeline != null)
            {
                LogManager.Log("The default render pipeline is defined by " + GraphicsSettings.defaultRenderPipeline.name);
            }
            else
            {
                LogManager.Log("The default render pipeline is the Built-in Render Pipeline");
            }

            // QualitySettings.renderPipeline determines the override render pipeline for the current quality level
            // If it is null, no override exists for the current quality level
            // If an override render pipeline is defined, Unity uses that; otherwise, it falls back to the default value
            if (QualitySettings.renderPipeline != null)
            {
                LogManager.Log("The override render pipeline for the current quality level is defined by " + QualitySettings.renderPipeline.name);
                LogManager.Log("The active render pipeline is the override render pipeline");
            }
            else
            {
                LogManager.Log("No override render pipeline exists for the current quality level");
                LogManager.Log("The active render pipeline is the default render pipeline");
            }

            // To get a reference to the Render Pipeline Asset that defines the active render pipeline,
            // without knowing if it is the default or an override, use GraphicsSettings.currentRenderPipeline
            if (GraphicsSettings.currentRenderPipeline != null)
            {
                LogManager.Log("The active render pipeline is defined by " + GraphicsSettings.currentRenderPipeline.name);
            }
            else
            {
                LogManager.Log("The active render pipeline is the Built-in Render Pipeline");
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}