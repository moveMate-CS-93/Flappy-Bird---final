﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mediapipe.Unity.Sample.HandTracking;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    public float strength = 3f;
    public float gravity = -9.81f;
    public float tilt = 2f;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;

    HandTrackingSolution handTracking;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        handTracking = FindObjectOfType<HandTrackingSolution>();  // Find the HandTrackingSolution component in the scene
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (handTracking != null && handTracking.LeftTouching)
        {
            direction = Vector3.up * strength;
        }
        else
        {
            // Apply gravity even if there's no hand input
            direction.y += gravity * Time.deltaTime;
        }

        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        // Vector3 rotation = transform.eulerAngles;
        // rotation.z = direction.y * tilt;
        // transform.eulerAngles = rotation;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) {
            GameManager.Instance.GameOver();
        } else if (other.gameObject.CompareTag("Scoring")) {
            GameManager.Instance.IncreaseScore();
        }
    }

}