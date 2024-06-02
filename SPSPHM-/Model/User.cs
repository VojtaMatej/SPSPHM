using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Security.Cryptography.X509Certificates;

namespace SPSPHM.Model
{
    public class User : BaseModel
    {
        public long ID { get; set; }
        public string LoginName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long OptoutofEmail { get; set; }
        public string Phone { get; set; }
        public long GroupID { get; set; }
        public long CountryID { get; set; }
        public long CultureID { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastActivity { get; set; }
        public long? LoginAttempts { get; set; }
        public DateTime? LockoutTime { get; set; }
        public DateTime? LockExpiry { get; set; }
        public long IsLocked { get; set; }
        public long IsVerified { get; set; }
        public long IsInternal { get; set; }
        public long DashboardID { get; set; }
        public Double? Lat { get; set; }
        public Double? Lng { get; set; }
        public string AllowNotify { get; set; }

        public static string MakePass(string password, byte[] salt = null, bool needsOnlyHash = false)
        {
            if (salt == null || salt.Length != 16)
            {
                // generate a 128-bit salt using a secure PRNG
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (needsOnlyHash) return hashed;
            Console.WriteLine($"salt: {Convert.ToBase64String(salt)}");
            Console.WriteLine($"hashed: {hashed}");
            // password will be concatenated with salt using ':'
            return $"{hashed}:{Convert.ToBase64String(salt)}";
        }

        public static bool VerifyPassword(string hashedPasswordWithSalt, string passwordToCheck)
        {
            // retrieve both salt and password from 'hashedPasswordWithSalt'
            var passwordAndHash = hashedPasswordWithSalt.Split(':');
            if (passwordAndHash == null || passwordAndHash.Length != 2)
                return false;
            var salt = Convert.FromBase64String(passwordAndHash[1]);
            if (salt == null)
                return false;
            // hash the given password
            var hashOfpasswordToCheck = MakePass(passwordToCheck, salt, true);
            // compare both hashes
            return String.Compare(passwordAndHash[0], hashOfpasswordToCheck) == 0;
        }

        public static User GetByUserID(int userID)
        {
            string sql = "SELECT * FROM users where users.id = @userID";
            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@userID", userID);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        User result = new User();
                        result.PopulateFromReader(dataReader);
                        return result;
                    }
                    return null;
                }
            }
        }

        public static User GetByLoginName(string loginName)
        {
            string sql = "SELECT * FROM users where id = @loginName";
            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@loginName", loginName);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        User result = new User();
                        result.PopulateFromReader(dataReader);
                        return result;
                    }
                    return null;
                }
            }
        }

        public static List<User> GetAll()
        {
            string sql = "SELECT * FROM users";
            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                List<User> list;
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    List<User> ids = new List<User>();
                    while (dataReader.Read())
                    {
                        User result = new User();
                        result.PopulateFromReader(dataReader);
                        ids.Add(result);
                    }
                    list = ids;
                }
                return list;
            }
        }

        protected void PopulateFromReader(IDataReader dataReader)
        {
            ID = (long)dataReader.GetValue("id");
            LoginName = (string)dataReader.GetValue("login_name");
            DisplayName = (string)dataReader.GetValue("display_name");
            Email = (string)dataReader.GetValue("email");
            Password = (string)dataReader.GetValue("password");
            OptoutofEmail = (long)dataReader.GetValue("optoutof_email");
            Phone = (string)dataReader.GetValue("Phone");
            GroupID = (long)dataReader.GetValue("group_id");
            CountryID = (long)dataReader.GetValue("country_id");
            CultureID = (long)dataReader.GetValue("culture_id");
            LastLogin = (DateTime?)dataReader.GetValue("last_login");
            LastActivity = (DateTime?)dataReader.GetValue("last_activity");
            LoginAttempts = (long?)dataReader.GetValue("login_attempts");
            LockoutTime = (DateTime?)dataReader.GetValue("lockout_time");
            LockExpiry = (DateTime?)dataReader.GetValue("lock_expiry");
            IsLocked = (long)dataReader.GetValue("is_locked");
            IsVerified = (long)dataReader.GetValue("is_verified");
            IsInternal = (long)dataReader.GetValue("is_internal");
            DashboardID = (long)dataReader.GetValue("dashboard_id");
            Lat = (double?)dataReader.GetValue("lat");
            Lng = (double?)dataReader.GetValue("lng");
            AllowNotify = (string)dataReader.GetValue("allow_notify");
            this.CreatedAt = (string)dataReader.GetValue("created_at");
            this.CreatedBy = (string)dataReader.GetValue("created_by");
            this.UpdatedAt = (string)dataReader.GetValue("created_at");
            this.UpdatedBy = (string)dataReader.GetValue("updated_by");
        }

        public override void Update()
        {
            string sql = "update users set password = @password, updated_by = @updated_by, updated_at = @updated_at where id = @id";
            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@password", MakePass("guest"));
                cmd.Parameters.AddWithValue("@updated_by", "jirka");
                cmd.Parameters.AddWithValue("@updated_at", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@id", 2);

                Console.WriteLine("updated:{0}", cmd.ExecuteNonQuery());

            }
        }

        public bool ChangePassword(long userID, string oldPassword, string newPassword)
        {
            string sql = "update users set password = @newPassword, updated_by = @updated_by, updated_at = @updated_at where id = @userID and password = @oldPassword";
            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@newPassword", MakePass(newPassword));
                cmd.Parameters.AddWithValue("@oldPassword", oldPassword);
                cmd.Parameters.AddWithValue("@updated_by", "jirka");
                cmd.Parameters.AddWithValue("@updated_at", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@userID", userID);

                return 1 == cmd.ExecuteNonQuery();
            }
        }
    }
}
