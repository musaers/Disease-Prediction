using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class SleepTimeAnalysisService
{
    private readonly ApplicationDbContext _context;

    public SleepTimeAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    

    public async Task<object> GetSleepTimeAnalysisAsync()
    {
        var sleepData = await _context.healthdata
            .Select(d => new
            {
                d.sleeptime,
                d.heartdisease
            })
            .ToListAsync();

        var groupedData = sleepData.GroupBy(d =>
        {
            if (d.sleeptime <= 4)
                return "0-4 hours";
            else if (d.sleeptime > 4 && d.sleeptime <=8)
                return "4-8 hours" ;
            else if (d.sleeptime > 8 && d.sleeptime <= 12)
                return "8-12 hours";
            else
                return "12+";
        })
        .Select(g => new
        {
            sleepData = g.Key,
            HeartDiseaseRate = g.Average(d => Convert.ToInt32(d.heartdisease))
        })
        .OrderBy(g => g.sleepData) // Grupları sıralamak için
        .ToList();

        return new
        {
            labels = groupedData.Select(d => d.sleepData).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Heart Disease Rate by Daily Sleep Time",
                    data = groupedData.Select(d => d.HeartDiseaseRate).ToArray(),
                    backgroundColor = new[]
                    {
                        "rgba(255, 99, 132, 0.2)",   // Red
                        "rgba(54, 162, 235, 0.2)",   // Blue
                        "rgba(255, 206, 86, 0.2)",   // Yellow
                        "rgba(75, 192, 192, 0.2)",   // Teal
                        
                    },

                    borderColor = new[]
                    {
                        "rgba(255, 99, 132, 1)",   // Red
                        "rgba(54, 162, 235, 1)",   // Blue
                        "rgba(255, 206, 86, 1)",   // Yellow
                        "rgba(75, 192, 192, 1)",   // Teal
                        
                    },
                    borderWidth = 1
                }
            }
        };
    }
}


