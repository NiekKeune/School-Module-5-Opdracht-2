using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GradesPrototype.Data;
using GradesPrototype.Services;

namespace GradesPrototype.Controls
{
    /// <summary>
    /// Interaction logic for ChangePasswordDialog.xaml
    /// </summary>
    public partial class ChangePasswordDialog : Window
    {
        public ChangePasswordDialog()
        {
            InitializeComponent();
        }

        // If the user clicks OK to change the password, validate the information that the user has provided
        private void ok_Click(object sender, RoutedEventArgs e)
        {
            User currentUser;

            if (SessionContext.UserRole == Role.Student)
            {
                currentUser = SessionContext.CurrentStudent;
            }

            else
            {
                currentUser = SessionContext.CurrentTeacher;
            }
            
            string oldPwd = oldPassword.Password;
            if (!currentUser.VerifyPassword(oldPwd))
            {
                MessageBox.Show("The old password was incorrect.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string newPwd = newPassword.Password;
            string confirmPwd = confirm.Password;

            if (String.Compare(newPwd, confirmPwd) != 0)        //Converts the new password and confirm password to a series of numbers, which it'll then compare. If both fields are different, it'll give a -1 or 1 and will give this error.
            {
                MessageBox.Show("The new password and confirm password do not contain the same password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!currentUser.SetPassword(newPwd))       //Checks if the password is complex enough.
            {
                MessageBox.Show("Couldn't set the new password because the password was not complex enough.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Indicate that the data is valid
            this.DialogResult = true;
        }
    }
}
