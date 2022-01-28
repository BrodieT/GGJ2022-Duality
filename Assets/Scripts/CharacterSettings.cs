using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Settings", menuName = "Character Settings", order = 0)]
public class CharacterSettings : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField, Tooltip("How fast the character will move left/right"), Min(0)]
    public float characterMoveSpeed = 5.0f;
    [SerializeField, Tooltip("How fast the character will fall due to gravity")]
    public float gravityAmount = -9.8f;
    [SerializeField, Tooltip("The position of the character's feet relative to their transform")]
    public Vector3 feetPosition = new Vector3(0, -0.25f);
    [SerializeField, Tooltip("The radius around the character's feet that we will check for ground")]
    public float groundCheckRadius = 0.5f;
    [SerializeField, Tooltip("What is considered ground")]
    public LayerMask groundLayer = default;

    [Header("Jump Settings")]
    [SerializeField, Tooltip("How much force is applied when the character jumps. (Set to 0 to disable jump)")]
    public float jumpAmount = 10.0f;
    [SerializeField, Tooltip("How long after exiting the ground can the character still jump")]
    public float groundCheckRememberTime = 0.5f;
    [SerializeField, Tooltip("How long after pressing the jump button can the jump be triggered")]
    public float jumpPressRememberTime = 0.25f;


    [Header("Art & Animation Settings")]
    [SerializeField, Tooltip("Determines whether the sprites for this character must be flipped to face left")]
    public bool flipSprites = true;
    [SerializeField, Tooltip("The animation parameter for triggering the walk animation (Leave blank to disable animation)")]
    public string walkAnim = "Walk";
    [SerializeField, Tooltip("The animation parameter for triggering the jump animation (Leave blank to disable animation)")]
    public string jumpAnim = "Jump";
    [SerializeField, Tooltip("The animation parameter for triggering the land animation (Leave blank to disable animation)")]
    public string landAnim = "Land";
    [SerializeField, Tooltip("The animation parameter for triggering the fall animation (Leave blank to disable animation)")]
    public string fallAnim = "Fall";

    [Header("Audio Settings")]
    [SerializeField, Tooltip("The sfx played when the character lands")]
    public AudioClip landSFX = default;
}
