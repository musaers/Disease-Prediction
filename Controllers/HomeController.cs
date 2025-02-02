using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using HeartDiseaseAnalysis.Services;
using HeartDiseaseAnalysis.Models;

namespace HeartDiseaseAnalysis.Controllers
{
    public class HomeController : Controller
    {
        private readonly AgeGroupAnalysisService _ageGroupAnalysisService;
        private readonly BmiAnalysisService _bmiAnalysisService;

        private readonly SleepTimeAnalysisService _sleepTimeAnalysisService;
        private readonly RaceAnalysisService _raceAnalysisService;

        private readonly SmokeAnalysisService _smokeAnalysisService;

        private readonly DiabetesAnalysisService _diabetesAnalysisService;
        private readonly GenderAnalysisService _genderAnalysisService;

        private readonly TensionAnalysisService _tensionAnalysisService;

        private readonly MarriageAnalysisService _marriageAnalysisService;
        private readonly WorkAnalysisService _workAnalysisService;

        private readonly BloodSugarAnalysisService _bloodSugarAnalysisService;

        private readonly AgeAnalysisService _ageAnalysisService;
        private readonly SGenderAnalysisService _sGenderAnalysisService;

        private readonly DeathRateAnalysisServiceByYear _deathRateAnalysisServiceByYear;
        private readonly DeathRateAnalysisServiceByMonth _deathRateAnalysisServiceByMonth;

        private readonly RiskAnalysisService _riskAnalysisService;

        

        public IActionResult Index()
        {
            return View();
        }

        public HomeController(AgeGroupAnalysisService ageGroupAnalysisService, BmiAnalysisService bmiAnalysisService, SleepTimeAnalysisService sleepTimeAnalysisService, 
                            RaceAnalysisService raceAnalysisService, SmokeAnalysisService smokeAnalysisService, DiabetesAnalysisService diabetesAnalysisService,
                            GenderAnalysisService genderAnalysisService, TensionAnalysisService tensionAnalysisService, MarriageAnalysisService marriageAnalysisService,
                            WorkAnalysisService workAnalysisService, BloodSugarAnalysisService bloodSugarAnalysisService, AgeAnalysisService ageAnalysisService,
                            SGenderAnalysisService sGenderAnalysisService, DeathRateAnalysisServiceByYear deathRateAnalysisServiceByYear, DeathRateAnalysisServiceByMonth deathRateAnalysisServiceByMonth
                            ,RiskAnalysisService riskAnalysisService)
        {
            _ageGroupAnalysisService = ageGroupAnalysisService;
            _bmiAnalysisService = bmiAnalysisService;
            _sleepTimeAnalysisService = sleepTimeAnalysisService;
            _raceAnalysisService = raceAnalysisService;
            _smokeAnalysisService = smokeAnalysisService;
            _diabetesAnalysisService = diabetesAnalysisService;
            _genderAnalysisService = genderAnalysisService;
            _tensionAnalysisService = tensionAnalysisService;
            _marriageAnalysisService = marriageAnalysisService;
            _workAnalysisService = workAnalysisService;
            _bloodSugarAnalysisService = bloodSugarAnalysisService;
            _ageAnalysisService = ageAnalysisService;
            _sGenderAnalysisService = sGenderAnalysisService;
            _deathRateAnalysisServiceByYear = deathRateAnalysisServiceByYear;
            _deathRateAnalysisServiceByMonth = deathRateAnalysisServiceByMonth;
            _riskAnalysisService = riskAnalysisService;
            

        }

        public async Task<IActionResult> AgeGroupAnalysis()
        {
            ViewData["ChartData"] = JsonConvert.SerializeObject(await _ageGroupAnalysisService.GetAgeGroupAnalysisAsync());
            return View("Chart");
        }

        public async Task<IActionResult> BmiAnalysis()
        {
            ViewData["ChartData"] = JsonConvert.SerializeObject(await _bmiAnalysisService.GetBmiAnalysisAsync());
            return View("Chart");
        }

        public async Task<IActionResult> SleepTimeAnalysis()
        {
            ViewData["ChartData"] = JsonConvert.SerializeObject(await _sleepTimeAnalysisService.GetSleepTimeAnalysisAsync());
            return View("Chart");
        }

       
        
        public async Task<IActionResult> RaceAnalysis()
        {
            ViewData["ChartData"] = JsonConvert.SerializeObject(await _raceAnalysisService.GetRaceAnalysisAsync());
            return View("Chart");
        }

        public async Task<IActionResult> SmokeAnalysis()
        {
            var result = await _smokeAnalysisService.GetSmokeAnalysisWithSummaryAsync();
            ViewData["ChartData"] = JsonConvert.SerializeObject(result.ChartData);
            ViewData["AnalysisSummary"] = result.AnalysisSummary;
            return View("Chart");
        }

        public async Task<IActionResult> DiabetesAnalysis()
        {
            var result = await _diabetesAnalysisService.GetDiabetesAnalysisWithSummaryAsync();
            ViewData["ChartData"] = JsonConvert.SerializeObject(result.ChartData);
            ViewData["AnalysisSummary"] = result.AnalysisSummary;
            return View("Chart");
        }

        public async Task<IActionResult> GenderAnalysis()
        {
            var result = await _genderAnalysisService.GetGenderAnalysisWithSummaryAsync();
            ViewData["ChartData"] = JsonConvert.SerializeObject(result.ChartData);
            ViewData["AnalysisSummary"] = result.AnalysisSummary;
            return View("Chart");
        }

        public async Task<IActionResult> TensionAnalysis()
        {
            var result = await _tensionAnalysisService.GetTensionAnalysisWithSummaryAsync();
            ViewData["ChartData"] = JsonConvert.SerializeObject(result.ChartData);
            ViewData["AnalysisSummary"] = result.AnalysisSummary;
            return View("Chart");
        }

        public async Task<IActionResult> MarriageAnalysis()
        {
            var result = await _marriageAnalysisService.GetMarriageAnalysisWithSummaryAsync();
            ViewData["ChartData"] = JsonConvert.SerializeObject(result.ChartData);
            ViewData["AnalysisSummary"] = result.AnalysisSummary;
            return View("Chart");
        }
        public async Task<IActionResult> WorkAnalysis()
        {
            ViewData["ChartData"] = JsonConvert.SerializeObject(await _workAnalysisService.GetWorkAnalysisAsync());
            return View("Chart");
        }

        public async Task<IActionResult> BloodSugarAnalysis()
        {
            ViewData["ChartData"] = JsonConvert.SerializeObject(await _bloodSugarAnalysisService.GetBloodSugarAnalysisAsync());
            return View("Chart");
        }

        public async Task<IActionResult> AgeAnalysis()
        {
            ViewData["ChartData"] = JsonConvert.SerializeObject(await _ageAnalysisService.GetAgeAnalysisAsync());
            return View("Chart");
        }

        public async Task<IActionResult> SGenderAnalysis()
        {
            var result = await _sGenderAnalysisService.GetSGenderAnalysisWithSummaryAsync();
            ViewData["ChartData"] = JsonConvert.SerializeObject(result.ChartData);
            ViewData["AnalysisSummary"] = result.AnalysisSummary;
            return View("Chart");
        }

        public async Task<IActionResult> DeathRateAnalysisByYear()
        {
            
            ViewData["ChartData"] = JsonConvert.SerializeObject(await _deathRateAnalysisServiceByYear.GetDeathRateAnalysisByYearAsync());
            return View("DeathRateAnalysis");
        }

        public async Task<IActionResult> DeathRateAnalysisByMonth()
        {
            ViewData["ChartData"] = JsonConvert.SerializeObject(await _deathRateAnalysisServiceByMonth.GetDeathRateAnalysisByMonthAsync());
            
            return View("DeathRateAnalysis");
        }



        [HttpPost]
        public IActionResult SubmitHeartDiseasePrediction(RiskAnalysisViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Assuming you calculate the risk and save it to the database
                var riskResult = _riskAnalysisService.CalculateRiskAsync(model);

                // Save the risk result in your database if needed, then redirect
                // Assuming that the model has an Id or the ID of the saved entry can be retrieved
                int resultId = model.id; // Or retrieve the Id after saving

                // Redirect to the prediction result page
                return RedirectToAction("RiskResult", new { id = resultId });
            }
            
            // If validation fails, stay on the same page and show validation messages
            return View("HeartDiseasePrediction", model);
        }

     


        public IActionResult HeartDiseasePrediction()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HeartDiseasePrediction(RiskAnalysisViewModel model)
        {
            if (ModelState.IsValid)
            {
                double risk = await _riskAnalysisService.CalculateRiskAsync(model);
                model.risk = risk;
                await _riskAnalysisService.SaveRiskDataAsync(model);
                return View("HeartDiseaseResult", model);
            }

            return View(model);
        }

        


        

        
    }

}
