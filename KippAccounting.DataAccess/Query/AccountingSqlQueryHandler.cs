using KippAcctCodeSearch.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace KippAccounting.DataAccess
{
    public interface IAccountingQueryHandler
    {
        IEnumerable<AccountingCodeViewModel> SearchBy(string purchase, int? organizationCode, int? fundId, int? schoolYear);
        IEnumerable<FundDto> GetAllFunds();
        IEnumerable<OrganizationDto> GetAllOrganizations();
        IEnumerable<SchoolYear> GetAllSchoolYears();
    }

    public class AccountingSqlQueryHandler : IAccountingQueryHandler
    {
        private readonly Database _db;

        public AccountingSqlQueryHandler(IConfigurationSettings configurationSettings)
        {
            _db = new Database(configurationSettings.ConnectionString);
        }

        public IEnumerable<AccountingCodeViewModel> SearchBy(string purchase, int? organizationCode, int? fundId, int? schoolYear)
        {
            var sql= @"SELECT 
                    Purchase
                    ,AccountCode
                    ,Notes 
                FROM dbo.AccountingCode
                WHERE 1 = 1";

            if (!string.IsNullOrEmpty(purchase))
                sql += string.Format("AND Purchase LIKE '%{0}%'", purchase);

            if (organizationCode.HasValue)
                sql += string.Format("AND OrganizationCode = {0}", organizationCode);

            if (fundId.HasValue)
                sql += string.Format("AND FundId = {0}", fundId);

            if (schoolYear.HasValue)
                sql += string.Format("AND FiscalYear = {0}", schoolYear);

            return _db.Fetch<AccountingCodeViewModel>(sql);
        }

        public IEnumerable<FundDto> GetAllFunds()
        {
            return _db.Fetch<FundDto>("SELECT * FROM dbo.Fund;");
        }


        public IEnumerable<OrganizationDto> GetAllOrganizations()
        {
            return _db.Fetch<OrganizationDto>("SELECT * FROM dbo.Organization;");
        }

        public IEnumerable<SchoolYear> GetAllSchoolYears()
        {
            return _db.Fetch<SchoolYear>(
                @"SELECT 
                    FY AS Fy
                    ,SchoolYear AS Year
                FROM dbo.SchoolYear;");
        }
    }
}
