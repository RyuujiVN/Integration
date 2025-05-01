using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Dividend;
using api.Models.SqlServer;
namespace api.Mappers
{
    public static class DividendMapper
    {
        public static DividendDto toDividendDto(this Dividend dividend)
        {
            return new DividendDto
            {
                DividendID = dividend.DividendID,
                EmployeeID = dividend.EmployeeID,
                DividendAmount = dividend.DividendAmount,
                DividendDate = dividend.DividendDate,
                CreatedAt = dividend.CreatedAt
            };
        } 
        public static Dividend toCreateDividend(this CreateDividendDto dividendDto)
        {
            return new Dividend
            {
                DividendID = dividendDto.DividendID,
                EmployeeID = dividendDto.EmployeeID,
                DividendAmount = dividendDto.DividendAmount,
                DividendDate = dividendDto.DividendDate,
                CreatedAt = dividendDto.CreatedAt
            };
        }
        public static Dividend toUpdateDividend(this UpdateDividendDto dividendDto)
        {
            return new Dividend
            {
                EmployeeID = dividendDto.EmployeeID,
                DividendAmount = dividendDto.DividendAmount,
                DividendDate = dividendDto.DividendDate,
            };
        }  
    }
}