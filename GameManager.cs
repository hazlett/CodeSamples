using GTMY.Controller;
using GTMY.GestureLibrary;
using GTMY.Graph;
using GTMY.KinectUsage;
using GTMY.Skeletal;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private MultilanePathController pathController;
    private List<IController> controllers;
    public GameObject PlayerGameObject;
    public List<Vector3> WaypointPositions;
   
    void Start()
    { 
        IMultilanePath path = new MultilanePath(waypointPositions, 4);
        pathController = new MultilanePathController(path, 4);

        InitializeControllers();

        ObstaclePainter obstaclePainter = new ObstaclePainter(Resources.Load<GameObject>("ObstacleSample"), 25f, 10f, path);
        obstaclePainter.Paint();

        ObstaclePainter collectablePainter = new ObstaclePainter(Resources.Load<GameObject>("CollectableSample"), 30f, 20f, path);
        collectablePainter.Paint();
    }

    private void InitializeControllers()
    {
        controllers = new List<IController>();

	//our gesture library is event based and our keyboard controller is a command pattern.

        IController keyboard = new Controller();
        keyboard.Add(new KeyDownEvent(KeyCode.UpArrow, new MoveForwardAction(PlayerGameObject.transform, pathController)));
        keyboard.Add(new KeyDownEvent(KeyCode.Space, new DestroyObstacle()));
        keyboard.Add(new KeyDownEvent(KeyCode.LeftArrow, new MoveLeftAction(PlayerGameObject.transform, pathController)));
        keyboard.Add(new KeyDownEvent(KeyCode.RightArrow, new MoveRightAction(PlayerGameObject.transform, pathController)));

        controllers.Add(keyboard);

        IController gestureController = new GestureController(new KinectBodySource(), 50, true, "Assets/xml/GestureMeasurements.xml", "Assets/xml/GestureDefinitions.xml", false);
        gestureController.Add(new GestureBinding("row-gestures", new NoOpAction()));

        controllers.Add(gestureController);
        GestureRecognizerManager.GestureRecognizedEvent += GestureRecognizerManager_GestureRecognizedEvent;
    }

    private void GestureRecognizerManager_GestureRecognizedEvent(string gestureName, int level, object publisher)
    {
        switch (gestureName)
        {
            case "row-gestures":
                MovePlayerForward();
                break;
        }
    }

    private void MovePlayerForward()
    {
        MoveForwardAction forward = new MoveForwardAction(PlayerGameObject.transform, pathController);
        forward.Trigger(null);
    }

    void Update()
    {
        foreach (IController controller in controllers)
        {
            controller.Update();
        }
    }
}