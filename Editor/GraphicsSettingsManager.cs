/*
 * File Name: GraphicsSettingsManager.cs
 * Description: This script is for managing the current Graphics Settings for the project
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: May 11, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
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
    public class GraphicsSettingsManager
    {
        /************************************************************/
        #region Functions

        [MenuItem(itemName: "Kokowolo/Graphics Settings/Built-In &#1", isValidateFunction: false, priority: 40)]
        public static void SetGraphicsSettingsLow()
        {
            SetGraphicsSettings(0);
        }

        [MenuItem(itemName: "Kokowolo/Graphics Settings/URP-Performant &#2", isValidateFunction: false, priority: 40)]
        public static void SetGraphicsSettingsMedium()
        {
            SetGraphicsSettings(1);
        }

        [MenuItem(itemName: "Kokowolo/Graphics Settings/URP-Balanced &#3", isValidateFunction: false, priority: 40)]
        public static void SetGraphicsSettingsHigh()
        {
            SetGraphicsSettings(2);
        }

        [MenuItem(itemName: "Kokowolo/Graphics Settings/URP-HighFidelity &#4", isValidateFunction: false, priority: 40)]
        public static void SetGraphicsSettingsHighest()
        {
            SetGraphicsSettings(3);
        }

        private static void SetGraphicsSettings(int index)
        {
            QualitySettings.SetQualityLevel(index, applyExpensiveChanges: true);
            GraphicsSettings.defaultRenderPipeline = QualitySettings.renderPipeline;
            LogCurrentRenderPipeline();
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
        /************************************************************/
    }
}