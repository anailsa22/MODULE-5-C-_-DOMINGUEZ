#include <iostream>
#include <fstream>
#include <iomanip>
#include <string>
#include <sstream>
#include <cmath>

using namespace std;

// Function to calculate federal tax based on income bracket
double calculateFederalTax(double salary) {
    if (salary <= 4999.99)
        return 0;
    else if (salary <= 9999.99)
        return salary * 0.06;
    else if (salary <= 19999.99)
        return salary * 0.15;
    else if (salary <= 39999.99)
        return salary * 0.20;
    else if (salary <= 59999.99)
        return salary * 0.25;
    else
        return salary * 0.30;
}

// Function to parse name and format as "Last, First Middle Initial"
string formatName(string firstName, string middleName, string lastName) {
    string formatted = lastName + ", " + firstName + " ";
    if (!middleName.empty()) {
        formatted += middleName[0];
    }
    return formatted;
}

int main() {
    ifstream inputFile("employees.txt");
    
    // Check if file opened successfully
    if (!inputFile.is_open()) {
        cerr << "Error: Could not open employees.txt file." << endl;
        return 1;
    }
    
    string ssn, firstName, middleName, lastName;
    int annualSalary, healthCoverageCount;
    char retirementFlag;
    
    cout << fixed << setprecision(2);
    cout << "\n========== EMPLOYEE PAYROLL REPORT ==========" << endl;
    cout << left << setw(15) << "SSN" 
         << left << setw(30) << "Name" 
         << left << setw(15) << "Monthly Pay" << endl;
    cout << "============================================" << endl;
    
    while (inputFile >> ssn >> firstName >> middleName >> lastName 
                     >> annualSalary >> healthCoverageCount >> retirementFlag) {
        
        // Format name as "Last, First Middle Initial"
        string formattedName = formatName(firstName, middleName, lastName);
        
        // Calculate monthly salary
        double monthlySalary = annualSalary / 12.0;
        
        // Calculate deductions
        double federalTax = calculateFederalTax(annualSalary) / 12.0;
        double stateTax = (annualSalary * 0.06) / 12.0;
        double healthInsurance = healthCoverageCount * 100.0;
        double retirementDeduction = 0;
        
        if (retirementFlag == 'Y' || retirementFlag == 'y') {
            retirementDeduction = (annualSalary * 0.06) / 12.0;
        }
        
        // Calculate net pay
        double totalDeductions = federalTax + stateTax + healthInsurance + retirementDeduction;
        double monthlyNetPay = monthlySalary - totalDeductions;
        
        // Display employee information
        cout << left << setw(15) << ssn 
             << left << setw(30) << formattedName 
             << "$ " << setw(12) << monthlyNetPay << endl;
        
        // Optional: Display detailed deduction breakdown
        cout << "  Details:" << endl;
        cout << "    Annual Salary: $" << fixed << setprecision(2) << annualSalary << endl;
        cout << "    Monthly Salary: $" << fixed << setprecision(2) << monthlySalary << endl;
        cout << "    Federal Tax: $" << fixed << setprecision(2) << federalTax << endl;
        cout << "    State Tax (6%): $" << fixed << setprecision(2) << stateTax << endl;
        cout << "    Health Insurance: $" << fixed << setprecision(2) << healthInsurance << endl;
        cout << "    Retirement (6%): $" << fixed << setprecision(2) << retirementDeduction << endl;
        cout << "    Total Deductions: $" << fixed << setprecision(2) << totalDeductions << endl;
        cout << endl;
    }
    
    inputFile.close();
    cout << "============================================" << endl;
    
    return 0;
}