using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SEP_Web.Database;
using SEP_Web.Helper.Messages;
using SEP_Web.Keys;
using SEP_Web.Models;

namespace SEP_Web.Services;
public class AssessmentServices : IAssessmentServices
{
    private readonly ILogger<IAssessmentServices> _logger;
    private readonly SEP_WebContext _database;

    public AssessmentServices(ILogger<IAssessmentServices> logger, SEP_WebContext database)
    {
        _logger = logger;
        _database = database;
    }
    
    public async Task<Assessment> RegisterAssessments(Assessment assessment)
    {
        try
        {
            if (assessment == null) throw new ArgumentNullException(nameof(assessment), ExceptionMessages.ErrorArgumentNullException);

            await _database.Assessments.AddAsync(assessment);
            await _database.SaveChangesAsync();

            return assessment;

        }
        catch (DbUpdateException dbException) when (dbException.InnerException is MySqlException mySqlException)
        {
            // MYSQL EXEPTIONS :

            _logger.LogError("[ASSESSMENT_SERVICE]: {exceptionMessage} : , {Message}, ErrorCode = {errorCode} - Represents {Error} ", ExceptionMessages.ErrorDatabaseConnection, mySqlException.Message.ToUpper(), mySqlException.Number, mySqlException.ErrorCode);
            _logger.LogError("[ASSESSMENT_SERVICE] : Detalhamento dos erros: {Description} - ", mySqlException.StackTrace.Trim());

            assessment = null;
            return assessment;
        }
        catch (Exception ex) when (ex.InnerException is ArgumentNullException nullException)
        {
            // NULL EXCEPTION :

            _logger.LogWarning("[ASSESSMENT_SERVICE]: {exceptionMessage} : {Message}, Attribute = {ParamName}, value = '{InnerExeption}'", ExceptionMessages.ErrorArgumentNullException, nullException.Message, nullException.ParamName, nullException.InnerException);
            _logger.LogWarning("[ASSESSMENT_SERVICE]: {Description}", nullException.StackTrace.Trim());

            assessment = null;
            return assessment;
        }
    }

    public async Task<Assessment> Assess(Assessment assess)
    {
        try
        {
            Assessment assessmentEdit = SearchForId(assess.Id) ?? throw new ArgumentNullException(nameof(assess), ExceptionMessages.ErrorArgumentNullException);

            assessmentEdit.Stats = AssessmentStatsEnum.EVALUATED;
            assessmentEdit.StartEvaluationPeriod = DateTime.Now;
            assessmentEdit.EndEvaluationPeriod = DateTime.Now.AddMonths(12);
            assessmentEdit.Crit1_Clau1 = assess.Crit1_Clau1;

            assessmentEdit.Crit2_Clau1 = assess.Crit2_Clau1;
            assessmentEdit.Crit2_Clau2 = assess.Crit2_Clau2;
            assessmentEdit.Crit2_Clau3 = assess.Crit2_Clau3;
            assessmentEdit.Crit2_Clau4 = assess.Crit2_Clau4;
            assessmentEdit.Crit2_Clau5 = assess.Crit2_Clau5;

            assessmentEdit.Crit3_Clau1 = assess.Crit3_Clau1;
            assessmentEdit.Crit3_Clau2 = assess.Crit3_Clau2;
            assessmentEdit.Crit3_Clau3 = assess.Crit3_Clau3;
            assessmentEdit.Crit3_Clau4 = assess.Crit3_Clau4;
            assessmentEdit.Crit3_Clau5 = assess.Crit3_Clau5;

            assessmentEdit.Crit4_Clau1 = assess.Crit4_Clau1;
            assessmentEdit.Crit4_Clau2 = assess.Crit4_Clau2;
            assessmentEdit.Crit4_Clau3 = assess.Crit4_Clau3;
            assessmentEdit.Crit4_Clau4 = assess.Crit4_Clau4;
            assessmentEdit.Crit4_Clau5 = assess.Crit4_Clau5;

            assessmentEdit.Crit5_Clau1 = assess.Crit5_Clau1;
            assessmentEdit.Crit5_Clau2 = assess.Crit5_Clau2;
            assessmentEdit.Crit5_Clau3 = assess.Crit5_Clau3;
            assessmentEdit.Crit5_Clau4 = assess.Crit5_Clau4;
            assessmentEdit.Crit5_Clau5 = assess.Crit5_Clau5;

            assessmentEdit.Crit6_Clau1 = assess.Crit6_Clau1;
            assessmentEdit.Crit6_Clau2 = assess.Crit6_Clau2;
            assessmentEdit.Crit6_Clau3 = assess.Crit6_Clau3;
            assessmentEdit.Crit6_Clau4 = assess.Crit6_Clau4;
            assessmentEdit.Crit6_Clau5 = assess.Crit6_Clau5;

            assessmentEdit.Crit5_Clau1 = assess.Crit5_Clau1;
            assessmentEdit.Crit5_Clau2 = assess.Crit5_Clau2;
            assessmentEdit.Crit5_Clau3 = assess.Crit5_Clau3;
            assessmentEdit.Crit5_Clau4 = assess.Crit5_Clau4;
            assessmentEdit.Crit5_Clau5 = assess.Crit5_Clau5;

            assessmentEdit.Crit7_Clau1 = assess.Crit7_Clau1;
            assessmentEdit.Crit7_Clau2 = assess.Crit7_Clau2;
            assessmentEdit.Crit7_Clau3 = assess.Crit7_Clau3;
            assessmentEdit.Crit7_Clau4 = assess.Crit7_Clau4;
            assessmentEdit.Crit7_Clau5 = assess.Crit7_Clau5;
            
            assessmentEdit.Crit8_Clau1 = assess.Crit8_Clau1;

            assessmentEdit.Crit9_Clau1 = assess.Crit9_Clau1;
            assessmentEdit.Crit9_Clau2 = assess.Crit9_Clau2;
            assessmentEdit.Crit9_Clau3 = assess.Crit9_Clau3;
            assessmentEdit.Crit9_Clau4 = assess.Crit9_Clau4;
            assessmentEdit.Crit9_Clau5 = assess.Crit9_Clau5;

            assessmentEdit.MedicalRestriction = assess.MedicalRestriction;
            assessmentEdit.Crit10_Justification = assess.Crit10_Justification;
            assessmentEdit.ForwardingDate = assess.ForwardingDate;


            assessmentEdit.Tot_Crit1 = assess.Crit1_Clau1;
            assessmentEdit.Average_Crit1 = Convert.ToDouble(assessmentEdit.Tot_Crit1);

            assessmentEdit.Tot_Crit2 = assess.Crit2_Clau1 + assess.Crit2_Clau2 + assess.Crit2_Clau3 + assess.Crit2_Clau4 + assess.Crit2_Clau5;
            assessmentEdit.Average_Crit2 = Convert.ToDouble(assessmentEdit.Tot_Crit2)/5;

            assessmentEdit.Tot_Crit3 = assess.Crit3_Clau1 + assess.Crit3_Clau2 + assess.Crit3_Clau3 + assess.Crit3_Clau4 + assess.Crit3_Clau5;
            assessmentEdit.Average_Crit3 = Convert.ToDouble(assessmentEdit.Tot_Crit3)/5;

            assessmentEdit.Tot_Crit4 = assess.Crit4_Clau1 + assess.Crit4_Clau2 + assess.Crit4_Clau3 + assess.Crit4_Clau4 + assess.Crit4_Clau5;
            assessmentEdit.Average_Crit4 = Convert.ToDouble(assessmentEdit.Tot_Crit4)/5;

            assessmentEdit.Tot_Crit5 = assess.Crit5_Clau1 + assess.Crit5_Clau2 + assess.Crit5_Clau3 + assess.Crit5_Clau4 + assess.Crit5_Clau5;
            assessmentEdit.Average_Crit5 = Convert.ToDouble(assessmentEdit.Tot_Crit5)/5;

            assessmentEdit.Tot_Crit6 = assess.Crit6_Clau1 + assess.Crit6_Clau2 + assess.Crit6_Clau3 + assess.Crit6_Clau4 + assess.Crit6_Clau5;
            assessmentEdit.Average_Crit6 = Convert.ToDouble(assessmentEdit.Tot_Crit6)/5;

            assessmentEdit.Tot_Crit7 = assess.Crit7_Clau1 + assess.Crit7_Clau2 + assess.Crit7_Clau3 + assess.Crit7_Clau4 + assess.Crit7_Clau5;
            assessmentEdit.Average_Crit7 = Convert.ToDouble(assessmentEdit.Tot_Crit7)/5;

            assessmentEdit.Tot_Crit8 = assess.Crit5_Clau1;
            assessmentEdit.Average_Crit8 = Convert.ToDouble(assessmentEdit.Tot_Crit8);

            assessmentEdit.Tot_Crit9 = assess.Crit9_Clau1 + assess.Crit9_Clau2 + assess.Crit9_Clau3 + assess.Crit9_Clau4 + assess.Crit9_Clau5;
            assessmentEdit.Average_Crit9 = Convert.ToDouble(assessmentEdit.Tot_Crit9)/5;

            assessmentEdit.Grand_Tot = assessmentEdit.Tot_Crit1 + assessmentEdit.Tot_Crit2 + assessmentEdit.Tot_Crit3 + assessmentEdit.Tot_Crit4 + assessmentEdit.Tot_Crit5;
            assessmentEdit.Overall_Average = Convert.ToDouble(assessmentEdit.Grand_Tot)/10;


            _database.Assessments.Update(assessmentEdit);
            await _database.SaveChangesAsync();

            return assessmentEdit;
        }
        catch (DbUpdateException dbException) when (dbException.InnerException is MySqlException mySqlException)
        {
            // MYSQL EXEPTIONS :

            _logger.LogError("[ASSESSMENTS_SERVICE]: {exceptionMessage} : , {Message}, ErrorCode = {errorCode} - Represents {Error} ", ExceptionMessages.ErrorDatabaseConnection, mySqlException.Message.ToUpper(), mySqlException.Number, mySqlException.ErrorCode);
            _logger.LogError("[ASSESSMENTS_SERVICE] : Detalhamento dos erros: {Description} - ", mySqlException.StackTrace.Trim());

            assess = null;
            return assess;
        }
        catch (Exception ex) when (ex.InnerException is ArgumentNullException nullException)
        {
            // NULL EXCEPTION :

            _logger.LogWarning("[ASSESSMENTS_SERVICE]: {exceptionMessage} : {Message}, Attribute = {ParamName}, value = '{InnerExeption}'", ExceptionMessages.ErrorArgumentNullException, nullException.Message, nullException.ParamName, nullException.InnerException);
            _logger.LogWarning("[ASSESSMENTS_SERVICE]: {Description}", nullException.StackTrace.Trim());

            assess = null;
            return assess;
        }
    }

    public async Task<ICollection<Assessment>> AssessmentsList()
    {
        try
        {
            ICollection<Assessment> test = await _database.Assessments.ToListAsync();

            if (test?.Count == 0)
                throw new ArgumentNullException(nameof(test), ExceptionMessages.ErrorArgumentNullException);

            return test ?? new List<Assessment>();
        }
        catch (DbUpdateException dbException) when (dbException.InnerException is MySqlException mySqlException)
        {
            // MYSQL EXEPTIONS :

            _logger.LogError("[ASSESSMENT_SERVICE]: {exceptionMessage} : , {Message}, ErrorCode = {errorCode} - Represents {Error} ", ExceptionMessages.ErrorDatabaseConnection, mySqlException.Message.ToUpper(), mySqlException.Number, mySqlException.ErrorCode);
            _logger.LogError("[ASSESSMENT_SERVICE] : Detalhamento dos erros: {Description} - ", mySqlException.StackTrace.Trim());

            return new List<Assessment>();
        }
        catch (Exception ex) when (ex.InnerException is ArgumentNullException nullException)
        {
            // NULL EXCEPTION :

            _logger.LogWarning("[ASSESSMENT_SERVICE]: {exceptionMessage} : {Message}, Attribute = {ParamName}, value = '{InnerExeption}'", ExceptionMessages.ErrorArgumentNullException, nullException.Message, nullException.ParamName, nullException.InnerException);
            _logger.LogWarning("[ASSESSMENT_SERVICE]: {Description}", nullException.StackTrace.Trim());

            return new List<Assessment>();
        }
    }

    public async Task<ICollection<Assessment>> AssessmentsList(int id)
    {
        try
        {
            ICollection<Assessment> test = await _database.Assessments.Where(x => x.CivilServantId == id).ToListAsync();

            if (test?.Count == 0)
                throw new ArgumentNullException(nameof(test), ExceptionMessages.ErrorArgumentNullException);

            return test ?? new List<Assessment>();
        }
        catch (DbUpdateException dbException) when (dbException.InnerException is MySqlException mySqlException)
        {
            // MYSQL EXEPTIONS :

            _logger.LogError("[ASSESSMENT_SERVICE]: {exceptionMessage} : , {Message}, ErrorCode = {errorCode} - Represents {Error} ", ExceptionMessages.ErrorDatabaseConnection, mySqlException.Message.ToUpper(), mySqlException.Number, mySqlException.ErrorCode);
            _logger.LogError("[ASSESSMENT_SERVICE] : Detalhamento dos erros: {Description} - ", mySqlException.StackTrace.Trim());

            return new List<Assessment>();
        }
        catch (Exception ex) when (ex.InnerException is ArgumentNullException nullException)
        {
            // NULL EXCEPTION :

            _logger.LogWarning("[ASSESSMENT_SERVICE]: {exceptionMessage} : {Message}, Attribute = {ParamName}, value = '{InnerExeption}'", ExceptionMessages.ErrorArgumentNullException, nullException.Message, nullException.ParamName, nullException.InnerException);
            _logger.LogWarning("[ASSESSMENT_SERVICE]: {Description}", nullException.StackTrace.Trim());

            return new List<Assessment>();
        }
    }

    public async Task<string> ServantName(int? CivilServantId)
    {
        ICollection<CivilServant> servant =  await _database.Servants.Where(x => x.Id == CivilServantId).ToListAsync();
        return servant.FirstOrDefault().Name;
    }

    public Assessment SearchForId(int id)
    {
        return _database.Assessments.FirstOrDefault(x => x.Id == id);
    }
}