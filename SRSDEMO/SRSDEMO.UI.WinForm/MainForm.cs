// MainForm.cs - Chapter 16 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// A GUI class.

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

public class MainForm : Form {

  private Button logInButton;
  private Button dropButton;
  private Button saveButton;
  private Button addButton;
  private Button logOutButton;
  private TextBox idTextBox; 
  private TextBox nameTextBox; 
  private TextBox totalTextBox; 
  private ListBox scheduleListBox; 
  private ListBox registeredListBox; 
  private Label classScheduleLabel;
  private Label idLabel;
  private Label nameLabel;
  private Label totalCourseLabel;
  private Label registeredLabel;
  private PasswordForm passwordDialog;

  private ScheduleOfClasses schedule;

  // Maintain a reference to the Student who is logged in.
  // (Whenever this is set to null, nobody is officially logged on.)
  private Student currentUser;

  public MainForm(ScheduleOfClasses schedule) {

    currentUser = null;
    this.schedule = schedule;

    // Create left-hand side labels
    int labelVertSpace = 40;
    int labelLeft = 5;

    nameLabel = new Label();
    nameLabel.Text = "Student Name:";
    nameLabel.Font = new Font(nameLabel.Font, FontStyle.Bold);
    nameLabel.AutoSize = true;
    nameLabel.Top = 5;
    nameLabel.Left = labelLeft;
    nameLabel.TextAlign = ContentAlignment.MiddleCenter;

    idLabel = new Label();
    idLabel.Text = "ID Number:";
    idLabel.Font = new Font(idLabel.Font, FontStyle.Bold);
    idLabel.AutoSize = true;
    idLabel.Top = nameLabel.Top + labelVertSpace;
    idLabel.Left = labelLeft;
    idLabel.TextAlign = ContentAlignment.MiddleCenter;

    totalCourseLabel = new Label();
    totalCourseLabel.Text = "Total Courses:";
    totalCourseLabel.Font = new Font(totalCourseLabel.Font, FontStyle.Bold);
    totalCourseLabel.AutoSize = true;
    totalCourseLabel.Top = idLabel.Top + labelVertSpace;
    totalCourseLabel.Left = labelLeft;
    totalCourseLabel.TextAlign = ContentAlignment.MiddleCenter;

    registeredLabel = new Label();
    registeredLabel.Text = "Registered For:";
    registeredLabel.Font = new Font(registeredLabel.Font, FontStyle.Bold);
    registeredLabel.AutoSize = true;
    registeredLabel.Top = totalCourseLabel.Top + labelVertSpace;
    registeredLabel.Left = labelLeft;

    // Create TextBox components
    nameTextBox = new TextBox();
    nameTextBox.Width = 140;
    nameTextBox.AutoSize = true;
    nameTextBox.Top = nameLabel.Top;
    nameTextBox.Left = nameLabel.Right;
    nameTextBox.ReadOnly = true;
    nameTextBox.BackColor = Color.White;

    idTextBox = new TextBox();
    idTextBox.Width = 140;
    idTextBox.AutoSize = true;
    idTextBox.Top = idLabel.Top;
    idTextBox.Left = idLabel.Right;
    idTextBox.ReadOnly = true;
    idTextBox.BackColor = Color.White;

    totalTextBox = new TextBox();
    totalTextBox.Width = 20;
    totalTextBox.AutoSize = true;  
    totalTextBox.Top = totalCourseLabel.Top;
    totalTextBox.Left = totalCourseLabel.Right;
    totalTextBox.ReadOnly = true;
    totalTextBox.BackColor = Color.White;

    // Create right-hand side labels
    classScheduleLabel = new Label();
    classScheduleLabel.Text = "Schedule of Classes";
    classScheduleLabel.Font = new Font(classScheduleLabel.Font, FontStyle.Bold);
    classScheduleLabel.AutoSize = true;
    classScheduleLabel.Top = 5;
    classScheduleLabel.Left = idTextBox.Right + 30;

    // Create "Schedule of Classes" ListBox Component
    scheduleListBox = new ListBox();
    scheduleListBox.Font = new Font(new FontFamily("Courier New"), 9.0f);
    scheduleListBox.Width = 220;
    scheduleListBox.Height = 225;
    scheduleListBox.Top = classScheduleLabel.Bottom + 5;
    scheduleListBox.Left = idTextBox.Right + 30;

    // Display an alphabetically sorted course catalog list
    // in the scheduleListBox component.
    List<Section> sortedSections = schedule.GetSortedSections();
    foreach( Section section in sortedSections) {
      scheduleListBox.Items.Add(section);
    }

    // Create "Registered For" ListBox Component
    registeredListBox = new ListBox();
    registeredListBox.Font = new Font(new FontFamily("Courier New"), 9.0f);
    registeredListBox.Width = 220;
    registeredListBox.Top = registeredLabel.Bottom + 5;
    registeredListBox.Height = scheduleListBox.Bottom - registeredListBox.Top + 3;
    registeredListBox.Left = labelLeft;

    // Add event handlers to the ListBox components
    scheduleListBox.SelectedIndexChanged += ScheduleSelectionChanged;
    registeredListBox.SelectedIndexChanged += RegisteredSelectionChanged;

    // Create buttons
    int buttonHeight = 40;
    int buttonWidth = 80;
    int buttonTop = 275;

    logInButton = new Button(); 
    logInButton.Text = "Log In";
    logInButton.Height = buttonHeight;
    logInButton.Width = buttonWidth;
    logInButton.Top = buttonTop;
    logInButton.Left = 10;

    dropButton = new Button(); 
    dropButton.Text = "Drop";
    dropButton.Height = buttonHeight;
    dropButton.Width = buttonWidth;
    dropButton.Top = buttonTop;
    dropButton.Left = logInButton.Right + 15;

    saveButton = new Button(); 
    saveButton.Text = "Save My Schedule";
    saveButton.Height = buttonHeight;
    saveButton.Width = buttonWidth;
    saveButton.Top = buttonTop;
    saveButton.Left = dropButton.Right + 15;

    addButton = new Button(); 
    addButton.Text = "Add";
    addButton.Height = buttonHeight;
    addButton.Width = buttonWidth;
    addButton.Top = buttonTop;
    addButton.Left = saveButton.Right + 15;

    logOutButton = new Button(); 
    logOutButton.Text = "Log Out";
    logOutButton.Height = buttonHeight;
    logOutButton.Width = buttonWidth;
    logOutButton.Top = buttonTop;
    logOutButton.Left = addButton.Right + 15;

    // Assign event handlers to the Buttons
    logInButton.Click += LogInButtonClicked;
    addButton.Click += AddButtonClicked;
    dropButton.Click += DropButtonClicked;
    saveButton.Click += SaveButtonClicked;
    logOutButton.Click += LogOutButtonClicked;

    // Initialize the buttons to their proper enabled/disabled
    // state.
    ResetButtons();

    // Add the GUI components to the form
    this.Controls.Add(logInButton);
    this.Controls.Add(dropButton);
    this.Controls.Add(saveButton);
    this.Controls.Add(addButton);
    this.Controls.Add(logOutButton);
    this.Controls.Add(classScheduleLabel);
    this.Controls.Add(idLabel);
    this.Controls.Add(idTextBox);
    this.Controls.Add(nameLabel);
    this.Controls.Add(nameTextBox);
    this.Controls.Add(totalCourseLabel);
    this.Controls.Add(totalTextBox);
    this.Controls.Add(registeredLabel);
    this.Controls.Add(scheduleListBox);
    this.Controls.Add(registeredListBox);

    // Set some appearance properties for the Form
    this.Text = "Student Registration System";
    this.Height = 370;
    this.Width = 550;
    this.MinimumSize = this.Size;
    this.StartPosition = FormStartPosition.CenterScreen;
  }

  //**********************************************
  // event handling method for "Log In" Button
  //
  public void LogInButtonClicked(object source, EventArgs e) {

      // First, clear the fields reflecting the
      // previous student's information.
      ClearFields();

      //  Display password dialog
      passwordDialog = new PasswordForm();
      passwordDialog.ShowDialog(this);

      string password = passwordDialog.Password;
      string id = passwordDialog.Id;
      passwordDialog.Dispose();

      // We'll try to construct a Student based on
      // the id we read, and if a file containing
      // Student's information cannot be found,
      // we have a problem.

      currentUser = new Student(id+".dat");
      currentUser.ReadStudentData(schedule);

      // Test to see if the Student fields were properly
      // initialized. If not, reset currentUser to null
      // and display a message box

      if (!currentUser.StudentSuccessfullyInitialized()) {
        // Drat!  The ID was invalid.
        currentUser = null;

        // Let the user know that login failed,
        string message = "Invalid student ID; please try again.";
        MessageBox.Show(message, "Invalid Student ID",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      else {
        // We have a valid Student.  Now, we need
        // to validate the password.

        if (currentUser.ValidatePassword(password)) {

          // Let the user know that the login succeeded.
          string message = 
               "Log in succeeded for " + currentUser.Name + ".";
          MessageBox.Show(message, "Log In Succeeded",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);

          // Load the data for the current user into the TextBox and
          // ListBox components.
          SetFields(currentUser);
        }
        else {
          // The id was okay, but the password validation failed;
          // notify the user of this.
          currentUser = null;
          string message = "Invalid password; please try again.";
          MessageBox.Show(message, "Invalid Password",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }
      // Check states of the various buttons.
      ResetButtons();
  }

  //**********************************************
  // event handling method for the "Drop" Button
  //
  public void DropButtonClicked(object source, EventArgs e) {
    // Determine which section is selected (note that we must
    // cast it, as it is returned as an Object reference).
    Section selected = (Section)registeredListBox.SelectedItem;

    // Drop the Student from the Section.
    selected.Drop(currentUser);

    // Display a confirmation message.
    string message = "Course " + 
       selected.RepresentedCourse.CourseNumber + " dropped.";
    MessageBox.Show(message, "Request Successful",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

    // Update the list of sections that 
    // this student is registered for.
    registeredListBox.Items.Clear();
    List<Section> enrolledSections = currentUser.Attends;
    foreach( Section section in enrolledSections ) {
      registeredListBox.Items.Add(section);
    }

    // Update the field representing student's course total.
    totalTextBox.Text = "" + currentUser.GetCourseTotal();

    // Check states of the various buttons.
    ResetButtons();
  }

  //**********************************************
  // event handling method for the "Save" Button
  //
  public void SaveButtonClicked(object source, EventArgs e) {
    bool success = currentUser.WriteStudentData();
    if (success) {
      // Let the user know that his/her
      // schedule was successfully saved.
      MessageBox.Show("Schedule saved", "Schedule Saved",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    else {
      // Let the user know that there was a problem.
      string message = "Problem saving your " +
                       "schedule; please contact " +
                       "SRS Support Staff for assistance.";
      MessageBox.Show(message, "Problem Saving Schedule",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
  }

  //**********************************************
  // event handling method for the "Add" Button
  //
  public void AddButtonClicked(object source, EventArgs e) {
    // Determine which section is selected (note that we must
    // cast it, as it is returned as an Object reference).
    Section selected = (Section)scheduleListBox.SelectedItem;

    // Attempt to enroll the student in the section, noting
    // the status code that is returned.
    EnrollFlags status = selected.Enroll(currentUser);

    // Report the status to the user.
    if (status == EnrollFlags.SECTION_FULL) {
      MessageBox.Show("Sorry - that section is full.", "Request Denied",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
    else if (status == EnrollFlags.PREREQ_NOT_SATISFIED) {
      string message = "You haven't satisfied all " +
                  "of the prerequisites for this course.";
      MessageBox.Show(message, "Request Denied",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
    else if (status == EnrollFlags.PREVIOUSLY_ENROLLED) {
      string message = "You are enrolled in or have successfully " +
                    "completed a section of this course.";
      MessageBox.Show(message, "Request Denied",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
    else {  // success!
      string message = "Seat confirmed in " +
             selected.RepresentedCourse.CourseNumber + ".";
      MessageBox.Show(message, "Request Successful",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);

      // Update the list of sections 
      // that this student is registered for.
      registeredListBox.Items.Clear();

      List<Section> enrolledSections = currentUser.Attends;
      foreach( Section section in enrolledSections ) {
        registeredListBox.Items.Add(section);
      }

      // Update the field representing student's course total.
      totalTextBox.Text = "" + currentUser.GetCourseTotal();

      // Clear the selection in the schedule of classes list.
      scheduleListBox.SelectedItem = null;
    }

    // Check states of the various buttons.
    ResetButtons();
  }

  //**********************************************
  // event handling method for "Log Out" Button
  //
  public void LogOutButtonClicked(object source, EventArgs e) {
    ClearFields();
    idTextBox.Text = "";
    currentUser = null;

    // Clear the selection in the
    // schedule of classes list.
    scheduleListBox.SelectedItem = null;

    // Check states of the various buttons.
    ResetButtons();

  }

  //***************************************************************
  // event handling method for the "Schedule of Classes" ListBox
  //
  public void ScheduleSelectionChanged(object source, EventArgs e) {
    // When an item is selected in this list,
    // we clear the selection in the other list.
    if (scheduleListBox.SelectedItem != null)  {
      registeredListBox.SelectedItem = null;
    }

    // reset the enabled state of the buttons
    ResetButtons();
  }

  //***************************************************************
  // event handling method for the "Registered For:" ListBox
  //
  public void RegisteredSelectionChanged(object source, EventArgs e) {
    // When an item is selected in this list,
    // we clear the selection in the other list.
    if (registeredListBox.SelectedItem != null)  {
      scheduleListBox.SelectedItem = null;
    }

    // reset the enabled state of the buttons
    ResetButtons();
  }

  //*******************************************
  // event handling method for the idTextBox
  //
  public void IdTextBoxKeyUp(object source, KeyEventArgs e) {
    // We only want to act if the Enter key is pressed
    if ( e.KeyCode == Keys.Enter ) {
  
      // First, clear the fields reflecting the
      // previous student's information.
      ClearFields();

      // We'll try to construct a Student based on
      // the id we read, and if a file containing
      // Student's information cannot be found,
      // we have a problem.

      currentUser = new Student(idTextBox.Text+".dat");
      currentUser.ReadStudentData(schedule);

      // Test to see if the Student fields were properly
      // initialized. If not, reset currentUser to null
      // and display a message box

      if (!currentUser.StudentSuccessfullyInitialized()) {
        // Drat!  The ID was invalid.
        currentUser = null;

        // Let the user know that login failed,
        string message = "Invalid student ID; please try again.";
        MessageBox.Show(message, "Invalid Student ID",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      else {
        // Hooray!  We found one!  Now, we need
        // to request and validate the password.
        passwordDialog = new PasswordForm();
        passwordDialog.ShowDialog(this);

        string password = passwordDialog.Password;
        passwordDialog.Dispose();

        if (currentUser.ValidatePassword(password)) {
          // Let the user know that the
          // login succeeded.
          string message = 
               "Log in succeeded for " + currentUser.Name + ".";
          MessageBox.Show(message, "Log In Succeeded",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);

          // Load the data for the current user into the TextBox and
          // ListBox components.
          SetFields(currentUser);
        }
        else {
          // The id was okay, but the password validation failed;
          // notify the user of this.
          string message = "Invalid password; please try again.";
          MessageBox.Show(message, "Invalid Password",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }
      // Check states of the various buttons.
      ResetButtons();
    }
  }

  // These are private housekeeping methods

  //******************************************************************
  // Because there are so many different situations in which one or
  // more buttons need to be (de)activated, and because the logic is
  // so complex, we centralize it here and then just call this method
  // whenever we need to check the state of one or more of the buttons.  
  // It is a tradeoff of code elegance for execution efficiency:  
  // we are doing a bit more work each time (because we don't need to 
  // reset all four buttons every time), but since the execution time
  // is minimal, this seems like a reasonable tradeoff.
  //
  private void ResetButtons() {
    // There are four conditions which collectively govern the
    // state of each button:
    //	
    // 1:  Whether a user is logged on or not.
    bool isLoggedOn;
    if (currentUser != null) {
      isLoggedOn = true;
    }
    else {
      isLoggedOn = false;
    }
		
    // 2:   Whether the user is registered for at least one course.
    bool atLeastOne;
    if (currentUser != null && currentUser.GetCourseTotal() > 0) {
      atLeastOne = true;
    }
    else {
      atLeastOne = false;
    }


    // 3:   Whether a registered course has been selected.
    bool courseSelected;
    if (registeredListBox.SelectedItem == null) {
      courseSelected = false;
    }
    else {
      courseSelected = true;
    }
		
    // 4:   Whether an item is selected in the Schedule of Classes.
    bool catalogSelected;
    if (scheduleListBox.SelectedItem == null)  {
      catalogSelected = false;
    }
    else {
      catalogSelected = true;
    }

    // Now, verify the conditions on a button-by-button basis.

    // Log In button:
    if (isLoggedOn) {
      logInButton.Enabled = false;  
    }
    else {
      logInButton.Enabled = true;  
    }

    // Drop button:
    if (isLoggedOn && atLeastOne && courseSelected) {
      dropButton.Enabled = true;
    }
    else {
      dropButton.Enabled = false;
    }

    // Add button:
    if (isLoggedOn && catalogSelected) {
      addButton.Enabled = true;
    }
    else {
      addButton.Enabled = false;
    }

    // Save My Schedule button:
    if (isLoggedOn) {
      saveButton.Enabled = true;
    }
    else {
      saveButton.Enabled = false;
    }

    // Log Out button:
    if (isLoggedOn) {
      logOutButton.Enabled = true;  
    }
    else {
      logOutButton.Enabled = false;  
    }
  }

  //**************************************
  // Called whenever a user is logged off.
  //
  private void ClearFields() {
    nameTextBox.Text = "";
    idTextBox.Text = "";
    totalTextBox.Text = "";
    registeredListBox.Items.Clear();
  }

  //*****************************************************************
  // Set the various fields, lists, etc. to reflect the information
  // associated with a particular student.  (Used when logging in.)
  //
  private void SetFields(Student theStudent) {
    nameTextBox.Text = theStudent.Name;
    idTextBox.Text = theStudent.Id;
    int total = theStudent.GetCourseTotal();
    totalTextBox.Text = ""+total;

    // If the student is registered for any courses, list these, too.
    if (total > 0) {
      // Use the GetEnrolledSections() method to obtain a list
      // of the sections that the student is registered for and
      // add the sections to the registered ListBox

      List<Section> enrolledSections = currentUser.Attends;
      foreach( Section section in enrolledSections ) {
        registeredListBox.Items.Add(section);
      }
    }
  }  //  end of SetFields method
}
