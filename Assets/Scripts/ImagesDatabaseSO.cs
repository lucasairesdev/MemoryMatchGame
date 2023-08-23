using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageDatabase", menuName = "Scriptable Objects/Image Database", order = 1)]
public class ImagesDatabaseSO : ScriptableObject {

    [SerializeField] public Sprite[] memoryGameImages;

}