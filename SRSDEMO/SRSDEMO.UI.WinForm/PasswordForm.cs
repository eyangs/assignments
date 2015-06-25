using System;
using System.Windows.Forms;
using System.Drawing;

public class PasswordForm : Form {

  private Button logInButton;
  private TextBox idTextBox; 
  private TextBox passwordTextBox; 
  private Label idLabel;
  private Label passwordLabel;
  private string id;
  private string password;

  public PasswordForm() {

    int componentTop = 15;

    // create label components
    idLabel = new Label();
    idLabel.Text = "Student ID:";
    idLabel.Top = componentTop;
    idLabel.Left = 15;
    idLabel.Width = 70;
    idLabel.Font = new Font(idLabel.Font, FontStyle.Bold);

    passwordLabel = new Label();
    passwordLabel.Text = "Password:";
    passwordLabel.Top = componentTop+30;
    passwordLabel.Left = 15;
    passwordLabel.Width = 70;
    passwordLabel.Font = new Font(passwordLabel.Font, FontStyle.Bold);

    // Create TextBox components
    idTextBox = new TextBox();
    idTextBox.Height = 40;
    idTextBox.Width = 100;
    idTextBox.Top = componentTop;
    idTextBox.Left = passwordLabel.Right;

    passwordTextBox = new TextBox();
    passwordTextBox.Height = 40;
    passwordTextBox.Width = 100;
    passwordTextBox.Top = componentTop+30;
    passwordTextBox.Left = passwordLabel.Right;
    passwordTextBox.PasswordChar = '*';

    logInButton = new Button(); 
    logInButton.Text = "Log In";
    logInButton.Height = 20;
    logInButton.Width = 50;
    logInButton.Top = componentTop+60;
    logInButton.Left = 95;

    // Assign event handler to the Button
    logInButton.Click += LogInButtonClicked;

    // Add the GUI components to the form
    this.Controls.Add(idLabel);
    this.Controls.Add(idTextBox);
    this.Controls.Add(passwordLabel);
    this.Controls.Add(passwordTextBox);
    this.Controls.Add(logInButton);

    this.Text = "User Login";
    this.Height = 150;
    this.Width = 240;
    this.MinimumSize = this.Size;
    this.StartPosition = FormStartPosition.CenterScreen;
  }

  // Property
  public string Id {
    get {
      return id;
    }
  }

  public string Password {
    get {
      return password;
    }
  }

  //**********************************************
  // event handling method for "Log In" Button
  //
  public void LogInButtonClicked(object source, EventArgs e) {

      id = idTextBox.Text.Trim();
      password = passwordTextBox.Text.Trim();
      this.Visible = false;
  }

  //******************
  // Test scaffold
  //
  //public static void Main(string[] args) {
  //  PasswordForm passwordForm = new PasswordForm();
  //  passwordForm.Visible = true;
  //  Application.Run(passwordForm);
  //}
}
