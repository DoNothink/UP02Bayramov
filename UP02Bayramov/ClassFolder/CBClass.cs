using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UP02Bayramov.ClassFolder
{
    class CBClass
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
                                Initial Catalog=UP02Bayramov;
                                Integrated Security=True");
        SqlDataAdapter dataAdapter;
        DataSet dataSet;
        
        public void RoleCBLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter("Select RoleId, RoleName " +
                    "From dbo.[Role] Order by RoleId ASC", sqlConnection);
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet,"[Role]");
                comboBox.ItemsSource = dataSet.Tables["[Role]"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.Tables["[Role]"].Columns["RoleName"].ToString();
                comboBox.SelectedValuePath = dataSet.Tables["[Role]"].Columns["RoleId"].ToString();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMessageBox(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
