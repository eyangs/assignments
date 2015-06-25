// Student.cs - Chapter 16 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections.Generic;
using System.IO;

public class Student : Person {

  //------------
  // Field.
  //------------

  private List<Section> attends; // of Sections
	
  //----------------
  // Constructor(s).
  //----------------

  public Student(string studentFile, string name, string id, string major, 
                 string degree) : base(name, id) {

    //  Assign auto-implemented property values
    Major = major;
    Degree = degree;
    StudentFile = studentFile;
    Transcript = new Transcript(this);

    // Create an empty List.

    attends = new List<Section>();

    // Initialize the password to be the first three digits
    // of the name of the student's data file.
    this.Password = this.StudentFile.Substring(0,3);  // added for GUI purposes

  }
	
  // A second form of constructor, used when a Student has not yet
  // declared a major or degree.
  // Reuse the code of the other Student constructor.

  public Student(string studentFile, string name, string id) : this(studentFile, name, id, "TBD", "TBD"){
  }

  // A third form of constructor, used when the only information 
  // about a Student is his/her Student data file name.

  public Student(string studentFile) : this(studentFile, "TBD", "TBD", "TBD", "TBD"){
  }

  //-------------------------------
  // Auto-implemented properties.
  //-------------------------------

  public string Major { get; protected set; }
  public string Degree { get; protected set; }
  public Transcript Transcript { get; protected set; }
  public string StudentFile { get; protected set; }
  public string Password { get; protected set; }

  //-----------------------------
  // Standard property for List.
  //-----------------------------

  public List<Section> Attends {
    get {
      return attends;
    }
    set {
      attends = value;
    }
  }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  //***************************************************************
  // Used after the constructor is called to verify whether or not
  // there were any file access errors.
  //
  public bool StudentSuccessfullyInitialized() {
    if (Name.Equals("TBD")) {
      return false;
    }
    else {
      return true;
    }
  }

  //**************************************
  //	
  public override void Display() {
    // First, let's display Person class data elements.

    base.Display();
		
    // Then, display Student-specific info.

    Console.WriteLine("Student-Specific Information:");
    Console.WriteLine("\tMajor:  "+Major);
    Console.WriteLine("\tDegree:  "+Degree);
    Console.WriteLine("\tStudent File:  "+StudentFile);
    DisplayCourseSchedule();
    Transcript.Display();
  }	
	
  //******************************************************************
  // We are forced to program this method because it is specified
  // as an abstract method in our parent class (Person); failing to
  // do so would render the Student class abstract, as well.
  //
  // For a Student, we wish to return a String as follows:
  //
  // 	Jackson Palmer (123-45-6789) [Master of Science - Math]
  //
  public override string ToString() {
    return Name + " (" + Id + ") [" + Degree +" - " + Major + "]";
  }

  //**************************************
  //	
  public void DisplayCourseSchedule() {
    // Display a title first.

    Console.WriteLine("Course Schedule for " + Name);
		
    // Step through the List of Section objects, 
    // processing these one by one.

    foreach ( Section s in attends ) {

      // Since the attends List contains Sections that the
      // Student took in the past as well as those for which
      // the Student is currently enrolled, we only want to
      // report on those for which a grade has not yet been
      // assigned.
            
      if ( s.GetGrade(this) == null ) {
        Console.WriteLine("\tCourse No.:  "+ 
                           s.RepresentedCourse.CourseNumber);
        Console.WriteLine("\tSection No.:  "+s.SectionNumber);
        Console.WriteLine("\tCourse Name:  "+ 
                           s.RepresentedCourse.CourseName);
        Console.WriteLine("\tMeeting Day and Time Held:  "+
                          s.DayOfWeek + " - "+
                          s.TimeOfDay);
        Console.WriteLine("\tRoom Location:  "+s.Room);
        Console.WriteLine("\tProfessor's Name:  "+
                           s.Instructor.Name);
        Console.WriteLine("\t-----");
      }
    }
  }
	
  //**************************************
  //	
  public void AddSection(Section s) {
    attends.Add(s);
  }
	
  //**************************************
  //	
  public void DropSection(Section s) {
    attends.Remove(s);
  }

  //************************************************************	
  // Determine whether the Student is already enrolled in THIS
  // EXACT Section.
  //
  public bool IsEnrolledIn(Section s) {
    return attends.Contains(s);
  }
	
  //****************************************************************
  // Determine whether the Student is already enrolled in ANOTHER
  // Section of this SAME Course.
  //
  public bool IsCurrentlyEnrolledInSimilar(Section s1) {
    bool foundMatch = false;
    Course c1 = s1.RepresentedCourse;

    foreach( Section s2 in attends ) {
      Course c2 = s2.RepresentedCourse;
      if (c1 == c2) {
        // There is indeed a Section in the attends
        // List representing the same Course.
        // Check to see if the Student is CURRENTLY
        // ENROLLED (i.e., whether or not he has
        // yet received a grade).  If there is no
        // grade, he/she is currently enrolled; if
        // there is a grade, then he/she completed
        // the course some time in the past.
        if ( s2.GetGrade(this) == null ) {
          // No grade was assigned!  This means
          // that the Student is currently
          // enrolled in a Section of this
          // same Course.
          foundMatch = true;
          break;
        }
      }
    }

    return foundMatch;
  }
		
  //******************************************************
  // The next two methods were added for use with the GUI
  //
  public bool ValidatePassword(string pw) {

    if (pw == null) {
      return false;
    }
    if (pw.Equals(Password)) {
      return true;
    }
    else {
      return false;
    }
  }

  public int GetCourseTotal() {
    return attends.Count;
  }

  //**********************************************************
  //  Read Student data from an input file.
  //
  public void ReadStudentData(ScheduleOfClasses schedule) {
    // We're going to parse tab-delimited records into
    // four attributes -- id, name, major, and degree. 

    StreamReader reader = null;

    try {
      // Open the file. 
      reader = new StreamReader(new FileStream(StudentFile, FileMode.Open));

      //  Read first line from input file.

      string line = reader.ReadLine();

      // We'll use the Split() method of the String class to split
      // the line we read from the file into substrings using tabs 
      // as the delimiter.

      string[] strings = line.Split('\t');

      // Now assign the value of the auto-implemented properties to the appropriate
      // substring

      Id = strings[0];
      Name = strings[1];
      Major = strings[2];
      Degree = strings[3];

      //  Read subsequent lines (if any) from input file.
      line = reader.ReadLine();

      //  Keep reading lines to get section info.
      while (line != null) {

        // The full section number is a concatenation of the
        // course no. and section no., separated by a hyphen;
        // e.g., "ART101 - 1".

        string fullSectionNumber = line.Trim();
        Section s = schedule.FindSection(fullSectionNumber);

        // Note that we are using the Section class's enroll()
        // method to ensure that bidirectionality is established
        // between the Student and the Section.
        s.Enroll(this);

        line = reader.ReadLine();
      }
    }  //  end of try block
    catch (FileNotFoundException f) {
      string message = "WARNING: "+f;
      //Console.WriteLine(f);
    }
    catch (IOException i) {
      string message = "WARNING: "+i;
      //Console.WriteLine(i);
    }
    finally {
      //  Close the input stream.
      if ( reader != null ) {
        reader.Close();
      }
    }

    return;
  }

  //*************************************************************
  // This method writes out all of the student's information to
  // his/her Student data file when he/she logs off.
  //
  public bool WriteStudentData() {
    StreamWriter writer = null;
    bool success = false;

    try {
      // Attempt to create the Student data file.  Note that
      // it will overwrite one if it already exists, which
      // is what we want to have happen.

      writer = new StreamWriter(new FileStream(StudentFile, FileMode.OpenOrCreate));

      // First, we output the header record as a tab-delimited
      // record.

      writer.WriteLine(Id + "\t" + Name + "\t" +
                       Major + "\t" + Degree);

      // Then, we output one record for every Section that
      // the Student is enrolled in.

      foreach( Section s in attends ) {
        writer.WriteLine(s.GetFullSectionNumber());
      }

      success = true;

    }  //  end of try block
    catch (IOException e) {
      // Signal that an error has occurred.
      string message = "WARNING: "+e;
      // Console.WriteLine(e);
    }
    finally {
      //  Close the output stream.
      if ( writer != null ) {
        writer.Close();
      }
    }
		
    return success;
  }
}
