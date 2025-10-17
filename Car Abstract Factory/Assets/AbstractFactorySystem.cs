using System;
using UnityEditor;
using UnityEngine;

public class VehicleRequirements
{
    public int NumberOfWheels { get; set; }
    public bool Engine { get; set; }
    public int Passengers { get; set; }
    public bool HasCargo { get; set; }
}

public interface IVehicle { }

public class Unicycle : IVehicle { }
public class Bicycle : IVehicle { }
public class Tandem : IVehicle { }
public class Tricycle : IVehicle { }
public class GoKart : IVehicle { }
public class FamilyBike : IVehicle { }

public interface IVehicleFactory
{
    GameObject Create(VehicleRequirements requirements);
}

public class CycleFactory : IVehicleFactory
{
    private GameObject unicyclePrefab;
    private GameObject bicyclePrefab;
    private GameObject tandemPrefab;
    private GameObject tricyclePrefab;
    private GameObject familyBikePrefab;
    private GameObject goKartPrefab;

    public CycleFactory()
    {
        // Assume the prefabs are assigned in the Unity Editor or loaded via Resources.
        unicyclePrefab = Resources.Load<GameObject>("Unicycle");
        bicyclePrefab = Resources.Load<GameObject>("Bicycle");
        tandemPrefab = Resources.Load<GameObject>("Tandem");
        tricyclePrefab = Resources.Load<GameObject>("Tricycle");
        familyBikePrefab = Resources.Load<GameObject>("FamilyBike");
        goKartPrefab = Resources.Load<GameObject>("GoKart");
    }

    public GameObject Create(VehicleRequirements requirements)
    {
        switch (requirements.Passengers)
        {
            case 1:
                if (requirements.NumberOfWheels == 1)
                    return GameObject.Instantiate(unicyclePrefab);
                return GameObject.Instantiate(bicyclePrefab);

            case 2:
                return GameObject.Instantiate(tandemPrefab);

            case 3:
                return GameObject.Instantiate(tricyclePrefab);

            case 4:
                if (requirements.HasCargo)
                    return GameObject.Instantiate(goKartPrefab);
                return GameObject.Instantiate(familyBikePrefab);

            default:
                return GameObject.Instantiate(bicyclePrefab);
        }
    }
}

public class MotorVehicleFactory : IVehicleFactory
{
    private GameObject goKartPrefab;
    private GameObject motorbikePrefab;

    public MotorVehicleFactory()
    {
        // Assume the prefabs are assigned in the Unity Editor or loaded via Resources.
        goKartPrefab = Resources.Load<GameObject>("GoKart");
        motorbikePrefab = Resources.Load<GameObject>("Motorbike");
    }

    public GameObject Create(VehicleRequirements requirements)
    {
        // A simple motor vehicle check, could be extended
        if (requirements.Passengers <= 2)
            return GameObject.Instantiate(motorbikePrefab);

        return GameObject.Instantiate(goKartPrefab);
    }
}

public class VehicleFactory : MonoBehaviour
{
    private IVehicleFactory _factory;
    private VehicleRequirements _requirements;

    public GameObject createdVehicle;

    void Start()
    {
        _requirements = new VehicleRequirements();
        //AskUserForVehicleDetails();
    }

    private void AskUserForVehicleDetails()
    {
        Debug.Log("To Build a Vehicle answer the following questions");

        // Simulating user input (replace with UI interaction)
        _requirements.NumberOfWheels = 4; // Example user input
        _requirements.Engine = true; // Example user input
        _requirements.Passengers = 2; // Example user input
        _requirements.HasCargo = false; // Example user input

        Debug.Log("Creating vehicle based on your input...");

        if (createdVehicle != null)
            Destroy(createdVehicle);

        createdVehicle = GetVehicle(_requirements);
        if (createdVehicle != null)
        {
            Debug.Log($"Created Vehicle: {createdVehicle.name}");
        }
    }

    public GameObject GetVehicle(VehicleRequirements requirements)
    {
        _factory = requirements.Engine ? (IVehicleFactory)new MotorVehicleFactory() : new CycleFactory();
        return _factory.Create(requirements);
    }
}
