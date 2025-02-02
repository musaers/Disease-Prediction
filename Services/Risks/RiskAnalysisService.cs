using System.Threading.Tasks;
using HeartDiseaseAnalysis.Data;
using HeartDiseaseAnalysis.Models;



namespace HeartDiseaseAnalysis.Services
{
    public class RiskAnalysisService
    {
        private readonly ApplicationDbContext _context;

        public RiskAnalysisService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<double> CalculateRiskAsync(RiskAnalysisViewModel model)
        {
            // Individual risk calculations
            double ageRisk = GetAgeRisk(model.age);
            double bmiRisk = CalculateBmiRisk(model.bmi);
            double genderRisk = GetGenderRisk(model.gender);
            double smokingRisk = GetSmokingRisk(model.smokingstatus);
            double diabeticRisk = GetDiabeticRisk(model.diabeticstatus);
            double sleepTimeRisk = GetSleepTimeRisk(model.sleeptime);
            double raceRisk = GetRaceRisk(model.race);

            // Interaction terms
            double genderAndAgeInteraction = genderRisk * ageRisk;
            double bmiAndSmokingInteraction = bmiRisk * smokingRisk;
            double diabeticAndRaceInteraction = diabeticRisk * raceRisk;

            // Weights for factors
            double ageWeight = 0.20;
            double bmiWeight = 0.15;
            double genderWeight = 0.10;
            double smokingWeight = 0.10;
            double diabeticWeight = 0.20;
            double sleepTimeWeight = 0.05;
            double raceWeight = 0.10;

            // Weights for interaction terms
            double genderAndAgeWeight = 0.05;
            double bmiAndSmokingWeight = 0.05;
            double diabeticAndRaceWeight = 0.05;

            // Calculate the overall risk
            double overallRisk = (ageRisk * ageWeight) +
                                 (bmiRisk * bmiWeight) +
                                 (genderRisk * genderWeight) +
                                 (smokingRisk * smokingWeight) +
                                 (diabeticRisk * diabeticWeight) +
                                 (sleepTimeRisk * sleepTimeWeight) +
                                 (raceRisk * raceWeight) +
                                 (genderAndAgeInteraction * genderAndAgeWeight) +
                                 (bmiAndSmokingInteraction * bmiAndSmokingWeight) +
                                 (diabeticAndRaceInteraction * diabeticAndRaceWeight);

            // Normalize the overall risk
            overallRisk = overallRisk / (ageWeight + bmiWeight + genderWeight + smokingWeight + diabeticWeight + sleepTimeWeight + raceWeight + genderAndAgeWeight + bmiAndSmokingWeight + diabeticAndRaceWeight);

            return overallRisk;
        }

        public async Task SaveRiskDataAsync(RiskAnalysisViewModel model)
        {
            var riskAnalysisData = new RiskAnalysisViewModel
            {
                age = model.age,
                height = model.height,
                weight = model.weight,
                bmi = model.bmi,
                diabeticstatus = model.diabeticstatus,
                gender = model.gender,
                race = model.race,
                sleeptime = model.sleeptime,
                smokingstatus = model.smokingstatus,
                bloodsugar = model.bloodsugar,
                marriagestatus = model.marriagestatus,
                hypertension = model.hypertension,
                workplace = model.workplace,
                risk = model.risk
            };

            _context.heartrisk.Add(riskAnalysisData);
            await _context.SaveChangesAsync();
        }

        // Implement the risk factor methods (e.g., GetAgeRisk, CalculateBmiRisk, etc.)
        private double CalculateBmiRisk(double bmi)
        {
            if (bmi < 18.5)
            {
                return 0.08; // Underweight
            }
            else if (bmi >= 18.5 && bmi < 24.9)
            {
                return 0.06; // Normal weight
            }
            else if (bmi >= 25 && bmi < 29.9)
            {
                return 0.09; // Overweight
            }
            else
            {
                return 0.10; // Obese
            }
        }
       
        private double GetAgeRisk(string ageCategory)
        {
            return ageCategory switch
            {
                "18-24" => 0.006,
                "25-29" => 0.008,
                "30-34" => 0.012,
                "35-39" => 0.014,
                "40-44" => 0.023,
                "45-49" => 0.034,
                "50-54" => 0.055,
                "55-59" => 0.074,
                "60-64" => 0.10,
                "65-69" => 0.12,
                "70-74" => 0.15,
                "75-79" => 0.19,
                "80 or older" => 0.22,
                _ => 0.0, // Default risk if the age category is unknown
            };
        }

        private double GetGenderRisk(string gender) { return gender == "Male" ? 0.10 : 0.06; }
        private double GetSmokingRisk(bool smokingStatus) {return smokingStatus ? 0.12 : 0.06; }
        private double GetDiabeticRisk(bool diabeticStatus) { return diabeticStatus ? 0.22 : 0.06; }
        private double GetSleepTimeRisk(int sleepTime)
        {
            if (sleepTime <= 4)
            {
                return 0.15;
            }
            else if (sleepTime > 4 && sleepTime <= 8)
            {
                return 0.08;
            }
            else if (sleepTime > 8 && sleepTime <= 12)
            {
                return 0.13;
            }
            else
            {
                return 0.17;
            }
        }

        private double GetRaceRisk(string race)
        {
            return race switch
            {
                "Asian" => 0.03,
                "Hispanic" => 0.05,
                "Other" => 0.081,
                "Black" => 0.75,
                "White" => 0.092,
                "Indian American/Native Alaskan" => 0.10,
                _ => 0.0, // Default risk if the race is unknown
            };
        }

    }
}

