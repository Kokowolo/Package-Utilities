/**
 * File Name: GenUtils.cs
 * Description: Script that contains general utility functions
 * 
 * Authors: Will Lacey
 * Date Created: October 14, 2020
 * 
 * Additional Comments: 
 *      File Line Length: 120
 *
 *      This script relates to MathUtils.cs
 **/

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

using Diagnostics = System.Diagnostics;

namespace Kokowolo.Utilities
{
    public static class General
    {
        #region Mouse Raytracing Functions

        public static bool IsMouseOverGUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        public static Ray MouseScreenPointToRay()
        {
            return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        }

        #endregion
    }
}