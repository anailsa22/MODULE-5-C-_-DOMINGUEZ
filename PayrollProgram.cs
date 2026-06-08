using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class PayrollProgram
{
    static void Main()
    {
        // Employee data
        List<Employee> employees = new List<Employee>
        {
            new Employee("30601234", "Patty", "O.", "Furniture", 55000, 1, true),
            new Employee("340809999", "Jim", "", "Nasium", 22000, 2, false),
            new Employee("350905555", "Earl", "E.", "Bird", 39000, 1, true),
            new Employee("360114321", "Frank", "N.", "Stein", 66000, 2, false)
        };

        Console.WriteLine("PAYROLL REPORT");
        Console.WriteLine("==============================================================");
        Console.WriteLine();

        foreach (Employee emp in employees)
        {
            emp.CalculatePayroll();
            emp.DisplayPayrollInfo();
            Console.WriteLine();
        }
    }
}

class Employee
{
    public string SSN { get; set; }
    public string FirstName { get; set; }
    public string MiddleInitial { get; set; }
    public string LastName { get; set; }
    public decimal AnnualSalary { get; set; }
    public int HealthInsuranceCoverage { get; set; }
    public bool RetirementElected { get; set; }

    public decimal FederalTax { get; set; }
    public decimal StateTax { get; set; }
    public decimal HealthInsuranceCost { get; set; }
    public decimal RetirementDeduction { get; set; }
    public decimal MonthlyGrossPay { get; set; }
    public decimal MonthlyNetPay { get; set; }

    public Employee(string ssn, string firstName, string middleInitial, string lastName, 
                    decimal annualSalary, int healthInsuranceCoverage, bool retirementElected)
    {
        SSN = ssn;
        FirstName = firstName;
        MiddleInitial = middleInitial;
        LastName = lastName;
        AnnualSalary = annualSalary;
        HealthInsuranceCoverage = healthInsuranceCoverage;
        RetirementElected = retirementElected;
    }

    public void CalculatePayroll()
    {
        // Calculate monthly gross pay
        MonthlyGrossPay = AnnualSalary / 12;

        // Calculate federal income tax based on tax brackets
        FederalTax = CalculateFederalTax(AnnualSalary) / 12;

        // Calculate state income tax (6% of annual salary)
        StateTax = (AnnualSalary * 0.06m) / 12;

        // Calculate health insurance cost ($100 per individual per month)
        HealthInsuranceCost = HealthInsuranceCoverage * 100;

        // Calculate retirement deduction (6% of monthly gross if elected)
        RetirementDeduction = RetirementElected ? MonthlyGrossPay * 0.06m : 0;

        // Calculate monthly net pay
        MonthlyNetPay = MonthlyGrossPay - FederalTax - StateTax - HealthInsuranceCost - RetirementDeduction;
    }

    private decimal CalculateFederalTax(decimal annualSalary)
    {
        decimal tax = 0;

        if (annualSalary <= 4999.99m)
        {
            tax = 0;
        }
        else if (annualSalary <= 9999.99m)
        {
            tax = annualSalary * 0.06m;
        }
        else if (annualSalary <= 19999.99m)
        {
            tax = annualSalary * 0.15m;
        }
        else if (annualSalary <= 39999.99m)
        {
            tax = annualSalary * 0.20m;
        }
        else if (annualSalary <= 59999.99m)
        {
            tax = annualSalary * 0.25m;
        }
        else
        {
            tax = annualSalary * 0.30m;
        }

        return tax;
    }

    public void DisplayPayrollInfo()
    {
        // Format name as: Last Name, First Name Middle Initial
        string formattedName = $"{LastName}, {FirstName} {MiddleInitial}".TrimEnd();

        Console.WriteLine($"SSN: {SSN}");
        Console.WriteLine($"Name: {formattedName}");
        Console.WriteLine($"Monthly Gross Pay: ${MonthlyGrossPay:F2}");
        Console.WriteLine($"  Federal Tax:           -${FederalTax:F2}");
        Console.WriteLine($"  State Tax (6%):        -${StateTax:F2}");
        Console.WriteLine($"  Health Insurance:      -${HealthInsuranceCost:F2}");
        Console.WriteLine($"  Retirement (6%):       -${RetirementDeduction:F2}");
        Console.WriteLine($"Monthly Net Pay: ${MonthlyNetPay:F2}");
        Console.WriteLine("--------------------------------------------------------------");
    }
}
