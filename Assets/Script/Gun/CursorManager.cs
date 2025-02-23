using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorNormal; // Texture cho con trỏ chuột bình thường
    [SerializeField] private Texture2D cursorShoot; // Texture cho con trỏ chuột khi bắn
    [SerializeField] private Texture2D cursorReload; // Texture cho con trỏ chuột khi nạp đạn
    private Vector2 hotspot = new Vector2(16, 48); // Điểm nóng của con trỏ chuột. Xác định vị trí "nhấp" chuột.

    void Start()
    {
        Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto); // Thiết lập con trỏ chuột ban đầu là cursorNormal
    }

    private void Update()
    {
        // Kiểm tra nếu người chơi nhấn chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorShoot, hotspot, CursorMode.Auto); // Đổi con trỏ chuột thành cursorShoot
        }
        // Kiểm tra nếu người chơi nhả chuột trái
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto); // Trả con trỏ chuột về cursorNormal
        }

        // Kiểm tra nếu người chơi nhấn chuột phải
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.SetCursor(cursorReload, hotspot, CursorMode.Auto); // Đổi con trỏ chuột thành cursorReload
        }
        // Kiểm tra nếu người chơi nhả chuột phải
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto); // Trả con trỏ chuột về cursorNormal
        }
    }
}