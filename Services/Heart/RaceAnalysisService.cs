using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;



public class RaceAnalysisService
{
    private readonly ApplicationDbContext _context;

    public RaceAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetRaceAnalysisAsync()
    {
        var RaceData = await _context.healthdata
            .GroupBy(d => d.race)
            .Select(g => new
            {
                RaceData = g.Key,
                HeartDiseaseRate = g.Average(d => Convert.ToInt32(d.heartdisease))
            })
            .ToListAsync();

        return new
        {
            labels = RaceData.Select(d => d.RaceData).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Heart Disease Rate by Ethnicity",
                    data = RaceData.Select(d => d.HeartDiseaseRate).ToArray(),
                    backgroundColor = new[]
                    {
                        "rgba(255, 99, 132, 0.2)",  //red
                        "rgba(255, 206, 86, 0.2)",   //yellow
                        "rgba(10, 10, 10, 0.6)",    // white
                        
                        "rgba(75, 192, 192, 0.2)",   // Teal
                        "rgba(153, 102, 255, 0.2)",
                        "rgba(0, 125, 12, 0.2)",  // white
                          // Orange
                        
                    },

                    borderColor = new[]
                    {
                        "rgba(255, 99, 132, 0.2)",  //red
                        "rgba(255, 206, 86, 0.2)",   //yellow
                        "rgba(10, 10, 10, 0.2)",    // white
                        
                        "rgba(75, 192, 192, 0.2)",   // Teal
                        "rgba(153, 102, 255, 0.2)",
                        "rgba(255, 255, 255, 0.5)",  // black
                        
                    },
                    borderWidth = 1
                }
            }
        };
    }
}
