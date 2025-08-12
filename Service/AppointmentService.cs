using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebCalendar.Components;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.DataProtection;
using Radzen;
using WebCalendar.Model;
using Microsoft.Data.SqlClient;



namespace WebCalendar.Service
{
    public class AppointmentService//: IReserveService
    {
        private readonly string _connectionString;

        public AppointmentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region 查詢
        public async Task<List<Appointment>> QueryAppointments()
        {
            
            List<Appointment> Appointments = new List<Appointment>();
            try
            {           

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = @"Select ID,Text,Start,[End] From Appointment";

                    Appointments = conn.Query<Appointment>(sql, new
                    {

                    }).ToList();

                }
            }
            catch (Exception ex) 
            {

            }
            return Appointments;
        }
        #endregion

        #region 編輯
        public async Task EditAppointments(Appointment data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string sql = @"
UPDATE Appointment SET Text=@Text,
Start=@Start,
[End] =@End
WHERE ID=@ID
";

                    await conn.ExecuteAsync(sql, new
                    {
                        data.ID,
                        data.Text,
                        data.Start,
                        data.End,
                    });

                    
                }
            }
            catch (Exception ex) 
            {

            }
        }
        #endregion

        #region 新增
        public async Task AddAppointments(Appointment data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string sql = @"


INSERT INTO Appointment
           (ID,Text,Start,[End])
     VALUES
           ((SELECT COUNT(*)+1 FROM Appointment),@Text,@Start,@End)
";

                    await conn.ExecuteAsync(sql, new
                    {
                        data.Text,
                        data.Start,
                        data.End,
                    });

                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region 刪除
        public async Task DeleteAppointments(Appointment data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string sql = @"
DELETE FROM Appointment
WHERE ID=@ID
";

                    await conn.ExecuteAsync(sql, new
                    {
                        data.ID
                    });

                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
