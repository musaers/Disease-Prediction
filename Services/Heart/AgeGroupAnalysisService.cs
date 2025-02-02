using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class AgeGroupAnalysisService
{
    private readonly ApplicationDbContext _context;

    public AgeGroupAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetAgeGroupAnalysisAsync()
    {
        var ageGroupData = await _context.healthdata
            .GroupBy(d => d.agecategory)
            .Select(g => new
            {
                AgeCategory = g.Key,
                HeartDiseaseRate = g.Average(d => Convert.ToInt32(d.heartdisease))
            })
            .ToListAsync();

        return new
        {
            labels = ageGroupData.Select(d => d.AgeCategory).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Heart Disease Rate by Age Group",
                    data = ageGroupData.Select(d => d.HeartDiseaseRate).ToArray(),
                    backgroundColor = new[]
                    {
                        "rgba(255, 99, 132, 0.2)",   // Red
                        "rgba(54, 162, 235, 0.2)",   // Blue
                        "rgba(255, 206, 86, 0.2)",   // Yellow
                        "rgba(75, 192, 192, 0.2)",   // Teal
                        "rgba(153, 102, 255, 0.2)",  // Purple
                        "rgba(255, 159, 64, 0.2)",   // Orange
                        "rgba(20, 203, 207, 0.2)",  // Gray
                        "rgba(105, 180, 132, 0.2)",  // Green
                        "rgba(255, 99, 255, 0.2)",   // Pink
                        "rgba(102, 153, 255, 0.2)",  // Light Blue
                        "rgba(255, 229, 100, 0.2)",  // Light Yellow
                        "rgba(255, 102, 178, 0.2)",  // Light Pink
                        "rgba(64, 159, 255, 0.2)"    // Cyan
                    },

                    borderColor = new[]
                    {
                        "rgba(255, 99, 132, 1)",   // Red
                        "rgba(54, 162, 235, 1)",   // Blue
                        "rgba(255, 206, 86, 1)",   // Yellow
                        "rgba(75, 192, 192, 1)",   // Teal
                        "rgba(153, 102, 255, 1)",  // Purple
                        "rgba(255, 159, 64, 1)",   // Orange
                        "rgba(20, 203, 207, 1)",  // Gray
                        "rgba(105, 180, 132, 1)",  // Green
                        "rgba(255, 99, 255, 1)",   // Pink
                        "rgba(102, 153, 255, 1)",  // Light Blue
                        "rgba(255, 229, 100, 1)",  // Light Yellow
                        "rgba(255, 102, 178, 1)",  // Light Pink
                        "rgba(64, 159, 255, 1)"    // Cyan
                    },

                    borderWidth = 1
                }
            }
        };
    }
}
