using System.Runtime;
using AutomationDataReading.Models.Sql;
using AutomationDataReading.Services.Interfaces;

namespace AutomationDataReading.Services;

public class SqlService: ISqlReader<ProfileRecord>, ISqlReader<RoleRecord>, ISqlReader<ProfileRolesRecord>
{
    Task<List<ProfileRecord>> ISqlReader<ProfileRecord>.GetList()
    {
        throw new NotImplementedException();
    }

    Task<List<RoleRecord>> ISqlReader<RoleRecord>.GetList()
    {
        throw new NotImplementedException();
    }

    Task<List<ProfileRolesRecord>> ISqlReader<ProfileRolesRecord>.GetList()
    {
        throw new NotImplementedException();
    }
}