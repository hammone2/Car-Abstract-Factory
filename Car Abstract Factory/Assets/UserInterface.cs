using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleCreationUI : MonoBehaviour
{
    public TMP_InputField wheelsInput;
    public Toggle engineToggle;
    public TMP_InputField passengersInput;
    public Toggle cargoToggle;
    public Button createButton;
    public TextMeshProUGUI outputText;

    private VehicleFactory vehicleFactory;

    void Start()
    {
        createButton.onClick.AddListener(OnCreateVehicleButtonClicked);
        vehicleFactory = gameObject.AddComponent<VehicleFactory>(); //new VehicleFactory();
    }

    void OnCreateVehicleButtonClicked()
    {
        var requirements = new VehicleRequirements
        {
            NumberOfWheels = int.Parse(wheelsInput.text),
            Engine = engineToggle.isOn,
            Passengers = int.Parse(passengersInput.text),
            HasCargo = cargoToggle.isOn
        };

        GameObject vehicle = vehicleFactory.GetVehicle(requirements);
        if (vehicle != null)
        {
            outputText.text = $"Created Vehicle: {vehicle.name}";
        }
        else
        {
            outputText.text = "No vehicle created.";
        }
    }
}
