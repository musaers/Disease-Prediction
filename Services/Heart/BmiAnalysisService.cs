using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class BmiAnalysisService
{
    private readonly ApplicationDbContext _context;

    public BmiAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetBmiAnalysisAsync()
    {
        var bmiGroupData = await _context.healthdata
            .Select(d => new
            {
                d.bmi,
                d.heartdisease
            })
            .ToListAsync();

        var groupedData = bmiGroupData.GroupBy(d =>
        {
            if (d.bmi < 18.5)
                return "Underweight (<18.5)";
            else if (d.bmi >= 18.5 && d.bmi < 24.9)
                return "Normal weight (18.5-24.9)";
            else if (d.bmi >= 25 && d.bmi < 29.9)
                return "Overweight (25-29.9)";
            else
                return "Obese (30+)";
        })
        .Select(g => new
        {
            BmiGroup = g.Key,
            HeartDiseaseRate = g.Average(d => Convert.ToInt32(d.heartdisease))
        })
        .OrderBy(g => g.BmiGroup) // Grupları sıralamak için
        .ToList();

        return new
        {
            labels = groupedData.Select(d => d.BmiGroup).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Heart Disease Rate by BMI Group",
                    data = groupedData.Select(d => d.HeartDiseaseRate).ToArray(),
                    backgroundColor = new[]
                    {
                        "rgba(54, 162, 235, 0.2)", // Blue
                        "rgba(255, 99, 132, 0.2)", //red   
                        "rgba(255, 206, 86, 0.2)",   // Yellow
                        "rgba(75, 192, 192, 0.2)",   // Teal   
                    },

                    borderColor = new[]
                    {
                          
                        "rgba(54, 162, 235, 0.2)", // Blue
                        "rgba(255, 99, 132, 0.2)", //red   
                        "rgba(255, 206, 86, 0.2)",   // Yellow
                        "rgba(75, 192, 192, 0.2)",   // Teal
                    },

                    borderWidth = 1
                }
            }
        };
    }
}
