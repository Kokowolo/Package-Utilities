/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 22, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kokowolo.Utilities;
using Kokowolo.Utilities.Scheduling;

namespace Scheduling
{
    public class TestController : MonoBehaviourSingleton<TestController>
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        [Header("Settings")]
        [SerializeField, Min(-0.01f)] float time;
        [SerializeField] bool useUserInput;

        [Header("Data")]
        [SerializeField] bool value;

        int setValueCount;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public const string ScenePath = "Packages/com.kokowolo.utilities/Tests/Runtime/Scheduling/Scene.unity";

        UnityEngine.UI.Text Text => transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();

        public static bool UseUserInput => Instance.useUserInput;

        public static float Time => Instance.time;
        public static bool Value 
        {
            get => Instance.value;
            set 
            {
                Instance.value = value;
                if (value) Instance.setValueCount++;
                Instance.OnValidate();
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        void OnValidate()
        {
            Text.text = useUserInput && value ? $"Waiting for Input #{setValueCount}" : "";
            enabled = useUserInput;
        }

        protected override void Singleton_Awake()
        {
            setValueCount = 0;
        }
        
        void Update()
        {
            if (Input.anyKeyDown) Value = false;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}