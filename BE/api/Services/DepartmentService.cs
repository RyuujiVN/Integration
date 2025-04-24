// using System.Data.Common;
// using api.Dtos;
// using api.Interfaces;
// using api.Mappers;
// using api.Models;
// using Microsoft.EntityFrameworkCore;

// namespace api.Services
// {
//     public class DepartmentService
//     {
//         private readonly IDepartmentRepository<api.Models.SqlServer.Department> _sqlServerRepo;
//         private readonly IDepartmentRepository<api.Models.MySql.Department> _mySqlRepo;
//         private readonly ILogger<DepartmentService> _logger;

//         public DepartmentService(
//             IDepartmentRepository<api.Models.SqlServer.Department> sqlServerRepo,
//             IDepartmentRepository<api.Models.MySql.Department> mySqlRepo,
//             ILogger<DepartmentService> logger)
//         {
//             _sqlServerRepo = sqlServerRepo;
//             _mySqlRepo = mySqlRepo;
//             _logger = logger;
//         }

//         public async Task<ServiceResult<List<DepartmentDto>>> GetAllDepartmentsAsync()
//         {
//             try
//             {
//                 var departments = await _sqlServerRepo.GetAllDepartmentsAsync();
//                 var result = departments.Select(d => d.ToDto()).ToList();
//                 return ServiceResult<List<DepartmentDto>>.CreateSuccess(result);
//             }
//             catch (DbException ex)
//             {
//                 _logger.LogError(ex, "Lỗi kết nối cơ sở dữ liệu khi lấy danh sách phòng ban");
//                 return ServiceResult<List<DepartmentDto>>.CreateError(
//                     ErrorType.ConnectionError,
//                     "Không thể kết nối đến cơ sở dữ liệu SQL Server. Vui lòng kiểm tra kết nối và thử lại."
//                 );
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Lỗi không xác định khi lấy danh sách phòng ban");
//                 return ServiceResult<List<DepartmentDto>>.CreateError(
//                     ErrorType.DatabaseError,
//                     "Đã xảy ra lỗi khi lấy danh sách phòng ban. Vui lòng thử lại sau."
//                 );
//             }
//         }

//         public async Task<ServiceResult<DepartmentDto>> GetDepartmentByIdAsync(int id)
//         {
//             try
//             {
//                 var department = await _sqlServerRepo.GetDepartmentByIdAsync(id);

//                 if (department == null)
//                 {
//                     return ServiceResult<DepartmentDto>.CreateError(
//                         ErrorType.NotFound,
//                         $"Không tìm thấy phòng ban với ID {id}"
//                     );
//                 }

//                 return ServiceResult<DepartmentDto>.CreateSuccess(department.ToDto());
//             }
//             catch (DbException ex)
//             {
//                 _logger.LogError(ex, "Lỗi kết nối cơ sở dữ liệu khi tìm phòng ban {Id}", id);
//                 return ServiceResult<DepartmentDto>.CreateError(
//                     ErrorType.ConnectionError,
//                     "Không thể kết nối đến cơ sở dữ liệu. Vui lòng kiểm tra kết nối và thử lại."
//                 );
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Lỗi không xác định khi tìm phòng ban {Id}", id);
//                 return ServiceResult<DepartmentDto>.CreateError(
//                     ErrorType.DatabaseError,
//                     $"Đã xảy ra lỗi khi tìm phòng ban ID {id}. Chi tiết lỗi: {ex.Message}"
//                 );
//             }
//         }

//         public async Task<ServiceResult<bool>> AddDepartmentAsync(DepartmentDto departmentDto)
//         {
//             bool sqlServerSuccess = false;

//             try
//             {
//                 // Kiểm tra trùng ID
//                 bool existsInSqlServer = await _sqlServerRepo.DepartmentExistsAsync(departmentDto.DepartmentID);
//                 bool existsInMySql = await _mySqlRepo.DepartmentExistsAsync(departmentDto.DepartmentID);

//                 if (existsInSqlServer || existsInMySql)
//                 {
//                     return ServiceResult<bool>.CreateError(
//                         ErrorType.DuplicateId,
//                         $"Phòng ban với ID {departmentDto.DepartmentID} đã tồn tại trong hệ thống. Vui lòng chọn ID khác."
//                     );
//                 }

//                 // Thêm vào SQL Server
//                 try
//                 {
//                     await _sqlServerRepo.AddDepartmentAsync(departmentDto.ToSqlServerModel());
//                     await _sqlServerRepo.SaveChangesAsync();
//                     sqlServerSuccess = true;
//                 }
//                 catch (Exception ex)
//                 {
//                     _logger.LogError(ex, "Lỗi khi thêm phòng ban vào SQL Server");
//                     return ServiceResult<bool>.CreateError(
//                         ErrorType.DatabaseError,
//                         "Không thể thêm phòng ban vào SQL Server. Chi tiết lỗi: " + ex.Message
//                     );
//                 }

//                 // Thêm vào MySQL
//                 try
//                 {
//                     await _mySqlRepo.AddDepartmentAsync(departmentDto.ToMySqlModel());
//                     await _mySqlRepo.SaveChangesAsync();
//                     return ServiceResult<bool>.CreateSuccess(true);
//                 }
//                 catch (Exception ex)
//                 {
//                     _logger.LogError(ex, "Lỗi khi thêm phòng ban vào MySQL");

//                     // Nếu đã thêm thành công vào SQL Server, thực hiện rollback
//                     if (sqlServerSuccess)
//                     {
//                         try
//                         {
//                             var sqlDept = await _sqlServerRepo.GetDepartmentByIdAsync(departmentDto.DepartmentID);
//                             if (sqlDept != null)
//                             {
//                                 _sqlServerRepo.DeleteDepartment(sqlDept);
//                                 await _sqlServerRepo.SaveChangesAsync();
//                             }
//                         }
//                         catch (Exception rollbackEx)
//                         {
//                             _logger.LogError(rollbackEx, "Lỗi khi rollback thêm phòng ban trên SQL Server");
//                         }
//                     }

//                     return ServiceResult<bool>.CreateError(
//                         ErrorType.SynchronizationError,
//                         "Đã xảy ra lỗi khi đồng bộ phòng ban với hệ thống MySQL. Thao tác đã bị hủy. Chi tiết lỗi: " + ex.Message
//                     );
//                 }
//             }
//             catch (DbException ex)
//             {
//                 _logger.LogError(ex, "Lỗi kết nối cơ sở dữ liệu khi thêm phòng ban");
//                 return ServiceResult<bool>.CreateError(
//                     ErrorType.ConnectionError,
//                     "Không thể kết nối đến cơ sở dữ liệu. Vui lòng kiểm tra kết nối và thử lại."
//                 );
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Lỗi không xác định khi thêm phòng ban");
//                 return ServiceResult<bool>.CreateError(
//                     ErrorType.DatabaseError,
//                     "Đã xảy ra lỗi không xác định khi thêm phòng ban. Chi tiết lỗi: " + ex.Message
//                 );
//             }
//         }

//         // Thêm phương thức này nếu chưa có
//         public async Task<ServiceResult<bool>> UpdateDepartmentAsync(int id, string departmentName)
//         {
//             try
//             {
//                 // Kiểm tra tồn tại
//                 var sqlDept = await _sqlServerRepo.GetDepartmentByIdAsync(id);
//                 var mysqlDept = await _mySqlRepo.GetDepartmentByIdAsync(id);

//                 if (sqlDept == null && mysqlDept == null)
//                 {
//                     return ServiceResult<bool>.CreateError(
//                         ErrorType.NotFound,
//                         $"Không tìm thấy phòng ban với ID {id} trong hệ thống."
//                     );
//                 }

//                 // Lưu trạng thái cũ để rollback nếu cần
//                 string? oldSqlName = sqlDept?.DepartmentName;
//                 bool sqlUpdateSuccess = false;

//                 // Cập nhật SQL Server
//                 if (sqlDept != null)
//                 {
//                     try
//                     {
//                         sqlDept.DepartmentName = departmentName;
//                         sqlDept.UpdatedAt = DateTime.Now;
//                         _sqlServerRepo.UpdateDepartment(sqlDept);
//                         await _sqlServerRepo.SaveChangesAsync();
//                         sqlUpdateSuccess = true;
//                     }
//                     catch (Exception ex)
//                     {
//                         _logger.LogError(ex, "Lỗi khi cập nhật phòng ban trong SQL Server");
//                         return ServiceResult<bool>.CreateError(
//                             ErrorType.DatabaseError,
//                             "Không thể cập nhật phòng ban trong SQL Server. Chi tiết lỗi: " + ex.Message
//                         );
//                     }
//                 }

//                 // Cập nhật MySQL
//                 if (mysqlDept != null)
//                 {
//                     try
//                     {
//                         mysqlDept.DepartmentName = departmentName;
//                         _mySqlRepo.UpdateDepartment(mysqlDept);
//                         await _mySqlRepo.SaveChangesAsync();
//                     }
//                     catch (Exception ex)
//                     {
//                         _logger.LogError(ex, "Lỗi khi cập nhật phòng ban trong MySQL");

//                         // Rollback SQL Server nếu cần
//                         if (sqlUpdateSuccess && sqlDept != null && oldSqlName != null)
//                         {
//                             try
//                             {
//                                 sqlDept.DepartmentName = oldSqlName;
//                                 sqlDept.UpdatedAt = DateTime.Now;
//                                 _sqlServerRepo.UpdateDepartment(sqlDept);
//                                 await _sqlServerRepo.SaveChangesAsync();
//                             }
//                             catch (Exception rollbackEx)
//                             {
//                                 _logger.LogError(rollbackEx, "Lỗi khi rollback cập nhật phòng ban trên SQL Server");
//                             }
//                         }

//                         return ServiceResult<bool>.CreateError(
//                             ErrorType.SynchronizationError,
//                             "Đã xảy ra lỗi khi đồng bộ cập nhật phòng ban với MySQL. Chi tiết lỗi: " + ex.Message
//                         );
//                     }
//                 }

//                 return ServiceResult<bool>.CreateSuccess(true);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Lỗi không xác định khi cập nhật phòng ban {Id}", id);
//                 return ServiceResult<bool>.CreateError(
//                     ErrorType.DatabaseError,
//                     $"Đã xảy ra lỗi không xác định khi cập nhật phòng ban. Chi tiết lỗi: {ex.Message}"
//                 );
//             }
//         }
//         public async Task<ServiceResult<bool>> DeleteDepartmentAsync(int id)
//         {
//             bool deleted = false;
//             bool sqlServerDeleted = false;
//             api.Models.SqlServer.Department? sqlServerDepartment = null;

//             try
//             {
//                 // Xóa từ SQL Server
//                 try
//                 {
//                     sqlServerDepartment = await _sqlServerRepo.GetDepartmentByIdAsync(id);
//                     if (sqlServerDepartment != null)
//                     {
//                         _sqlServerRepo.DeleteDepartment(sqlServerDepartment);
//                         await _sqlServerRepo.SaveChangesAsync();
//                         sqlServerDeleted = true;
//                         deleted = true;
//                     }
//                 }
//                 catch (Exception ex)
//                 {
//                     _logger.LogError(ex, "Lỗi khi xóa phòng ban trong SQL Server");
//                     return ServiceResult<bool>.CreateError(
//                         ErrorType.DatabaseError,
//                         "Không thể xóa phòng ban trong SQL Server. Chi tiết lỗi: " + ex.Message
//                     );
//                 }

//                 // Xóa từ MySQL
//                 try
//                 {
//                     var mySqlDepartment = await _mySqlRepo.GetDepartmentByIdAsync(id);
//                     if (mySqlDepartment != null)
//                     {
//                         _mySqlRepo.DeleteDepartment(mySqlDepartment);
//                         await _mySqlRepo.SaveChangesAsync();
//                         deleted = true;
//                     }
//                 }
//                 catch (Exception ex)
//                 {
//                     _logger.LogError(ex, "Lỗi khi xóa phòng ban trong MySQL");

//                     // Rollback SQL Server nếu đã xóa
//                     if (sqlServerDeleted && sqlServerDepartment != null)
//                     {
//                         try
//                         {
//                             await _sqlServerRepo.AddDepartmentAsync(sqlServerDepartment);
//                             await _sqlServerRepo.SaveChangesAsync();
//                         }
//                         catch (Exception rollbackEx)
//                         {
//                             _logger.LogError(rollbackEx, "Lỗi khi rollback xóa phòng ban trong SQL Server");
//                         }
//                     }

//                     return ServiceResult<bool>.CreateError(
//                         ErrorType.SynchronizationError,
//                         "Đã xảy ra lỗi khi đồng bộ xóa phòng ban với hệ thống MySQL. Thao tác đã bị hủy. Chi tiết lỗi: " + ex.Message
//                     );
//                 }

//                 if (!deleted)
//                 {
//                     return ServiceResult<bool>.CreateError(
//                         ErrorType.NotFound,
//                         $"Không tìm thấy phòng ban với ID {id} trong hệ thống."
//                     );
//                 }

//                 return ServiceResult<bool>.CreateSuccess(true);
//             }
//             catch (DbException ex)
//             {
//                 _logger.LogError(ex, "Lỗi kết nối cơ sở dữ liệu khi xóa phòng ban {Id}", id);
//                 return ServiceResult<bool>.CreateError(
//                     ErrorType.ConnectionError,
//                     "Không thể kết nối đến cơ sở dữ liệu. Vui lòng kiểm tra kết nối và thử lại."
//                 );
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Lỗi không xác định khi xóa phòng ban {Id}", id);
//                 return ServiceResult<bool>.CreateError(
//                     ErrorType.DatabaseError,
//                     $"Đã xảy ra lỗi không xác định khi xóa phòng ban ID {id}. Chi tiết lỗi: {ex.Message}"
//                 );
//             }
//         }

//         // Kiểm tra tính đồng bộ giữa hai hệ thống
//         public async Task<ServiceResult<bool>> CheckSynchronizationAsync()
//         {
//             try
//             {
//                 var sqlDepts = await _sqlServerRepo.GetAllDepartmentsAsync();
//                 var mysqlDepts = await _mySqlRepo.GetAllDepartmentsAsync();

//                 var sqlIds = sqlDepts.Select(d => d.DepartmentID).ToHashSet();
//                 var mysqlIds = mysqlDepts.Select(d => d.DepartmentID).ToHashSet();

//                 if (!sqlIds.SetEquals(mysqlIds))
//                 {
//                     var onlyInSql = sqlIds.Except(mysqlIds).ToList();
//                     var onlyInMysql = mysqlIds.Except(sqlIds).ToList();

//                     string message = "Phát hiện dữ liệu không đồng bộ giữa hai hệ thống. ";
//                     if (onlyInSql.Any())
//                         message += $"IDs chỉ có trong SQL Server: {string.Join(", ", onlyInSql)}. ";
//                     if (onlyInMysql.Any())
//                         message += $"IDs chỉ có trong MySQL: {string.Join(", ", onlyInMysql)}.";

//                     return ServiceResult<bool>.CreateError(
//                         ErrorType.SynchronizationError,
//                         message
//                     );
//                 }

//                 // Kiểm tra tên có khớp không
//                 var nameDiscrepancies = new List<int>();
//                 foreach (var sqlDept in sqlDepts)
//                 {
//                     var mysqlDept = mysqlDepts.FirstOrDefault(d => d.DepartmentID == sqlDept.DepartmentID);
//                     if (mysqlDept != null && sqlDept.DepartmentName != mysqlDept.DepartmentName)
//                     {
//                         nameDiscrepancies.Add(sqlDept.DepartmentID);
//                     }
//                 }

//                 if (nameDiscrepancies.Any())
//                 {
//                     return ServiceResult<bool>.CreateError(
//                         ErrorType.SynchronizationError,
//                         $"Phát hiện tên phòng ban không khớp giữa hai hệ thống cho IDs: {string.Join(", ", nameDiscrepancies)}."
//                     );
//                 }

//                 return ServiceResult<bool>.CreateSuccess(true);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Lỗi khi kiểm tra đồng bộ dữ liệu");
//                 return ServiceResult<bool>.CreateError(
//                     ErrorType.DatabaseError,
//                     "Đã xảy ra lỗi khi kiểm tra tính đồng bộ dữ liệu: " + ex.Message
//                 );
//             }
//         }
//     }
// }