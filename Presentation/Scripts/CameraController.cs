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
    
    [Export] private float _zoomSpeed = 0.1f;
    [Export] private float _minZoom = 0.5f;
    [Export] private float _maxZoom = 1.0f;

    [Export] private Camera2D _camera2D;

    public override void _Ready()
    {
        _camera2D = GetNode<Camera2D>("Camera2D");
        GameLogger.Info($"Camera mode: {_cameraMode}");
    }

    public override void _Input(InputEvent @event)
    {
        if (_cameraMode != CameraMode.Free)
            return;

        // DRAG BUTTON
        if (@event is InputEventMouseButton mouseButtonEvent)
        {
            if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
            {
                if (mouseButtonEvent.Pressed)
                {
                    _isDragging = true;
                    _dragStartMousePosition = mouseButtonEvent.Position;
                    _dragStartCameraPosition = _camera2D.Position;
                }
                else
                {
                    _isDragging = false;
                }
            }

            // ZOOM
            if (mouseButtonEvent.Pressed)
            {
                if (mouseButtonEvent.ButtonIndex == MouseButton.WheelUp)
                    Zoom(_zoomSpeed);

                if (mouseButtonEvent.ButtonIndex == MouseButton.WheelDown)
                    Zoom(-_zoomSpeed);
            }
        }

        // DRAG MOVEMENT
        if (@event is InputEventMouseMotion mouseMotionEvent && _isDragging)
        {
            Vector2 mouseDelta = mouseMotionEvent.Position - _dragStartMousePosition;
            _camera2D.Position = _dragStartCameraPosition - mouseDelta;
        }
    }

    private void Zoom(float amount)
    {
        Vector2 newZoom = _camera2D.Zoom + new Vector2(amount, amount);

        float clamped = Mathf.Clamp(newZoom.X, _minZoom, _maxZoom);
        
        _camera2D.Zoom = new Vector2(clamped, clamped);
    }
}