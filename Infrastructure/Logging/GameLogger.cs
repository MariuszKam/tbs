using System;
using Godot;

namespace TBS.Utils;

public static class GameLogger
{
    public static void Info(string message)
    {
        GD.Print($"[INFO] {DateTime.Now:HH:mm:ss} | {message}");
    }

    public static void Warn(string message)
    {
        GD.PushWarning($"[WARN] {DateTime.Now:HH:mm:ss} | {message}");
    }

    public static void Error(string message)
    {
        GD.PushError($"[ERROR] {DateTime.Now:HH:mm:ss} | {message}");
    }
}