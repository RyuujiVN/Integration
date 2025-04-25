// using api.Models.MySql;
// using api.Models.SqlServer;
// using api.Repositories;

// namespace api.Services
// {
//     public class IntegrationService
//     {
//         private readonly SqlServerRepository _sqlRepo;
//         private readonly MySqlRepository _mysqlRepo;

//         public IntegrationService(SqlServerRepository sqlRepo, MySqlRepository mysqlRepo)
//         {
//             _sqlRepo = sqlRepo;
//             _mysqlRepo = mysqlRepo;
//         }

//         public async Task<object> GetIntegratedEmployeeDataAsync(int employeeId)
//         {
//             // Lấy thông tin nhân viên từ SQL Server
//             var employee = await _sqlRepo.GetEmployeeByIdAsync(employeeId);
//             if (employee == null)
//                 return null!;

//             // Lấy thông tin chấm công và lương từ MySQL (giả sử có cùng ID)
//             var attendances = await _mysqlRepo.GetAttendancesByEmployeeIdAsync(employeeId);
//             var salaries = await _mysqlRepo.GetSalariesByEmployeeIdAsync(employeeId);

//             // Tạo đối tượng kết hợp
//             return new
//             {
//                 EmployeeInfo = employee,
//                 AttendanceRecords = attendances,
//                 SalaryRecords = salaries
//             };
//         }

//         // Các phương thức tích hợp khác...
//     }
// }