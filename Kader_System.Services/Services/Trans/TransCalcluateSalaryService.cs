namespace Kader_System.Services.Services.Trans
{
    public class TransCalcluateSalaryService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> localizer) : ITransCalcluateSalaryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer = localizer;

        public async Task<Response<object>> CalculateSalary(CalcluateSalaryModelRequest model)
        {

            var empolyees = await _unitOfWork.Employees.GetSpecificSelectAsync(x =>
            (!model.EmployeeId.HasValue || x.Id == model.EmployeeId) &&
            (!model.CompanyId.HasValue || x.CompanyId == model.CompanyId) &&
            (!model.ManagerId.HasValue || x.ManagementId == model.ManagerId)
            , x => x);






            if (empolyees is null)
            {
                var msg = _localizer[Localization.NotFoundData];
                return new()
                {
                    Data = null,
                    Msg = msg,
                    Check = false
                };
            }




            var empolyeeWithCaculatedSalary = await _unitOfWork.StoredProcuduresRepo.SpCalculateSalary(model.StartCalculationDate, model.StartActionDay, string.Join('-', empolyees.Select(x => x.Id).ToList()));

            var spcaculatedSalarydetils = await _unitOfWork.StoredProcuduresRepo.SpCalculateSalaryDetails(model.StartCalculationDate, model.StartActionDay, string.Join('-', empolyees.Select(x => x.Id).ToList()));
            var spcaculatedSalarytransDetils = await _unitOfWork.StoredProcuduresRepo.SpCalculatedSalaryDetailedTrans(model.StartCalculationDate, model.StartActionDay, string.Join('-', empolyees.Select(x => x.Id).ToList()));
            var transcation = _unitOfWork.BeginTransaction();
            try
            {

                var TransCalculatorMaster = (await _unitOfWork.TransSalaryCalculator.GetSpecificSelectAsync(x => x.DocumentDate.Month == 11, x => x)).FirstOrDefault();

                if (TransCalculatorMaster == null)
                {
                    TransCalculatorMaster = new TransSalaryCalculator
                    {
                        DocumentDate = model.DocumentDate,
                        CompanyId = model.CompanyId,
                        ManagementId = model.ManagerId,
                        IsMigrated = true
                    };


                    TransCalculatorMaster = await _unitOfWork.TransSalaryCalculator.AddAsync(TransCalculatorMaster);
                    await _unitOfWork.CompleteAsync();

                }


                var listoftransDetails = new List<TransSalaryCalculatorDetail>();
                foreach (var empolyee in empolyeeWithCaculatedSalary)
                {
                    var calculatedBefore = await _unitOfWork.TransSalaryCalculatorDetailsRepo.ExistAsync(
                        x => x.EmployeeId == empolyee.EmployeeId
                        && x.TransSalaryCalculatorsId == TransCalculatorMaster.Id);

                    if (!calculatedBefore)
                    {
                        var transDetails = new TransSalaryCalculatorDetail
                        {
                            EmployeeId = empolyee.EmployeeId,
                            Salary = empolyee.CalculatedSalary + empolyee.TotalSalary,
                            TransSalaryCalculatorsId = TransCalculatorMaster.Id,

                        };
                        listoftransDetails.Add(transDetails);


                    }

                }
                await _unitOfWork.TransSalaryCalculatorDetailsRepo.AddRangeAsync(listoftransDetails);
                await _unitOfWork.CompleteAsync();










                transcation.Commit();

            }
            catch (Exception ex)
            {

                transcation.Rollback();


            }
            return new()
            {
                Data = new
                {
                    ListOfsp = spcaculatedSalarytransDetils,
                    caluculatedEvery = empolyeeWithCaculatedSalary,
                    sumvalue = spcaculatedSalarytransDetils

                },
                Msg = null,
                Error = null
            };

        }
    }
}
