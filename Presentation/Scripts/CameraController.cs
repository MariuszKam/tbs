using Godot;
using TBS.Infrastructure.Logging;

namespace TBS.Presentation.Scripts;

public enum CameraMode
{
    Free,
    Follow
}
public partial class CameraController : Node
{
    private CameraMode _cameraMode = CameraMode.Free;
    
    private bool _isDragging = false;
    private Vector2 _dragStartMousePosition;
    private Vector2 _dragStartCameraPosition;

    [Export] private Camera2D _camera2D;

    public override void _Ready()
    {
        _camera2D = GetNode<Camera2D>("Camera2D");
        GameLogger.Info($"Camera mode: {_cameraMode}");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (_cameraMode != CameraMode.Free)
            return;

        // START / STOP DRAG
        if (@event is InputEventMouseButton mouseButtonEvent)
        {
            if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
            {
                if (mouseButtonEvent.Pressed)
                {
                    _isDragging = true;
                    _dragStartMousePosition = mouseButtonEvent.Position;
                    _dragStartCameraPosition = _camera2D.Position;
                    GameLogger.Info($"Dragging camera {_dragStartCameraPosition} to {_dragStartMousePosition}");
                }
                else
                {
                    _isDragging = false;
                }
            }
        }

        // DRAG MOVEMENT
        if (@event is InputEventMouseMotion mouseMotionEvent)
        {
            if (_isDragging)
            {
                Vector2 mouseDelta = mouseMotionEvent.Position - _dragStartMousePosition;

                _camera2D.Position = _dragStartCameraPosition - mouseDelta;
            }
        }
    }
}