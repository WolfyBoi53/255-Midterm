using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerShopScript : MonoBehaviour
{
    public TextMeshProUGUI shopOptionsText;
    public TMP_InputField desktopInputField;
    public TMP_InputField laptopInputField;

    // Arrays for computer models
    private string[] desktopComputers = { "Entry Level Desktop", "Gaming Desktop", "Workstation Desktop" };
    private string[] laptops = { "Basic Laptop", "Ultrabook Laptop", "Gaming Laptop" };

    // Lists for peripherals and computer accessories
    private List<string> peripherals = new List<string> { "Keyboard", "Mouse", "Monitor" };
    private List<string> accessories = new List<string> { "External Hard Drive", "USB-C Hub", "Cooling Pad" };

    // Array to hold prices for desktops
    private float[] desktopPrices = { 499.99f, 1299.99f, 2499.99f };

    // Property for customer status (VIP or regular)
    private bool isVIP;
    public bool IsVIP
    {
        get { return isVIP; }
        set { isVIP = value; }
    }

    // Function to display the shop options menu
    public void ShowShopMenu()
    {
        string menuText = "Welcome to our Computer Shop Menu!\n";
        menuText += "\n--- Desktop Computer Options ---\n";
        menuText += GetOptionsText(desktopComputers);
        menuText += "\n--- Laptop Options ---\n";
        menuText += GetOptionsText(laptops);
        menuText += "\n--- Peripheral Options ---\n";
        menuText += GetOptionsText(peripherals);
        menuText += "\n--- Accessories Options ---\n";
        menuText += GetOptionsText(accessories);

        // VIP status offers additional discount message
        if (isVIP)
        {
            menuText += "\nAs a VIP customer, you receive a 10% discount on all purchases!";
        }
        else
        {
            menuText += "\nBecome a VIP member for discounts!";
        }

        // Update the TMP text with the shop menu
        shopOptionsText.text = menuText;

        // Example of using if/else to categorize the first desktop price
        float firstDesktopPrice = desktopPrices[0];
        string priceCategory;

        // Using if/else to check for price ranges
        if (firstDesktopPrice <= 500)
        {
            priceCategory = "Budget Desktop";
        }
        else if (firstDesktopPrice <= 1000)
        {
            priceCategory = "Mid-Range Desktop";
        }
        else
        {
            priceCategory = "High-End Desktop";
        }

        // Output the category
        Debug.Log("First desktop falls into: " + priceCategory);
    }

    // Function to generate options text for arrays
    private string GetOptionsText(string[] options)
    {
        string result = "";
        for (int i = 0; i < options.Length; i++) // For loop
        {
            result += (i + 1) + ". " + options[i] + "\n";
        }
        return result;
    }

    // Function to generate options text for lists
    private string GetOptionsText(List<string> options)
    {
        string result = "";
        foreach (string option in options) // Foreach loop
        {
            result += (options.IndexOf(option) + 1) + ". " + option + "\n";
        }
        return result;
    }

    // Function to calculate total cost based on selection
    public float CalculateTotalCost(bool vipStatus, int desktopChoice, int laptopChoice)
    {
        float totalCost = 0f;

        // If/else decision based on user selection
        if (desktopChoice >= 0 && desktopChoice < desktopPrices.Length)
        {
            totalCost += desktopPrices[desktopChoice];
        }
        else
        {
            Debug.LogWarning("Invalid desktop selection!");
        }

        // Additional charge based on laptop choice (example)
        if (laptopChoice == 1)
        {
            totalCost += 799.99f; // Ultrabook Laptop
        }
        else if (laptopChoice == 2)
        {
            totalCost += 1499.99f; // Gaming Laptop
        }
        else
        {
            totalCost += 399.99f; // Basic Laptop by default
        }

        // Apply VIP discount if applicable
        if (vipStatus)
        {
            totalCost *= 0.9f; // 10% discount for VIP
        }

        return totalCost;
    }

    public void OnSubmitSelections()
    {
        // Get the selected desktop and laptop from user input
        int selectedDesktop = int.Parse(desktopInputField.text) - 1; // Convert input to index
        int selectedLaptop = int.Parse(laptopInputField.text) - 1;   // Convert input to index

        // Calculate cost based on user selection
        float totalCost = CalculateTotalCost(IsVIP, selectedDesktop, selectedLaptop);
        Debug.Log("Total cost for selection: $" + totalCost.ToString("F2"));
    }

    private void Start()
    {
        // Set VIP status
        IsVIP = true;

        // Display shop menu
        ShowShopMenu();
    }
}
