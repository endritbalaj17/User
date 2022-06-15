using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Data;
using User.Models;

namespace User.Repository
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly ApplicationDbContext _db;

        public FunctionRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        //public async Task<List<SearchUserResult>> SearchUser(string NumriPersonal, string NumriKarteles, int? GrupiID, string DataFillimit, string DataMbarimit, string Emri, string Mbiemri, string TelMobil, int? KombiID, int? KomunaID, int? VendbanimiID, bool? PerdoruesiAktive, int? GjuhaId) =>
        // await _db.Set<SearchUserResult>().FromSqlInterpolated(sql: $"SELECT * FROM [SearchUsers]({NumriPersonal}, {NumriKarteles}, {GrupiID}, {DataFillimit}, {DataMbarimit}, {Emri}, {Mbiemri}, {TelMobil},{KombiID}, {KomunaID},{VendbanimiID},{PerdoruesiAktive},{GjuhaId})").ToListAsync();


    }
}
