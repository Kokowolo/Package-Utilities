/*
 * File Name: OrbitScript.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: May 3, 2022
 * 
 * Additional Comments:
 *      While this file has been updated to better fit this project, the original version can be found here:
 *		https://youtu.be/hd1QzLf4ZH8
 *
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitScript : MonoBehaviour
{
    /************************************************************/
    #region Fields

    [Tooltip("transform to orbit around")]
    [SerializeField] Transform target = null;

    #endregion
    /************************************************************/
    #region Functions

    private void Update()
    {
        Vector3 relativePos = (target.position + new Vector3(0, 0.5f, 0)) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
        transform.Translate(0, 0, 3 * Time.deltaTime);
    }

    #endregion
    /************************************************************/
}