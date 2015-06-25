// Faculty.cs - Chapter 16 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// An IMPLEMENTATION class.

using System;
using System.Collections.Generic;
using System.IO;

public class Faculty {

  //------------
  // Fields.
  //------------

  //----------------
  // Constructor(s).
  //----------------

  public Faculty(string facultyFile, string assignmentsFile) {
    
    //  Initialize auto-implemented property values
    FacultyFile = facultyFile;
    AssignmentsFile = assignmentsFile;

    // Instantiate a new Dictionary.
    Professors = new Dictionary<string, Professor>();
  }

  //-------------------------------
  // Auto-implemented properties.
  //-------------------------------

  public string FacultyFile { get; set; }
  public string AssignmentsFile { get; set; }
  public Dictionary<string, Professor> Professors { get; set; }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  //**************************
  public void Display() {
    Console.WriteLine("Faculty:");
    Console.WriteLine("");

    // Use a foreach statement to step through the Dictionary.
    foreach ( KeyValuePair<string, Professor> kv in Professors ) {
      Professor p = kv.Value;
      p.Display();
      Console.WriteLine("");
    }

    return;
  }

  //*****************************************
  //
  public void AddProfessor(Professor p) {
    Professors.Add(p.Id, p);
  }

  //*******************************************
  //
  public Professor FindProfessor(string id) {
    return Professors[id];
  }

  //*************************************
  //
  public void ReadFacultyData() {
    // We're going to parse tab-delimited records into
    // four attributes -- name, id, title, and dept --
    // and then call the Professor constructor to fabricate a new
    // professor.

    StreamReader reader = null;

    try {
      // Open the file. 
      reader = new StreamReader(new FileStream(FacultyFile, FileMode.Open));

      //  Read first line from input file.
      string line = reader.ReadLine();

      //  Keep reading lines until there aren't any more.
      while (line != null) {

        // We'll use the Split() method of the String class to split
        // the line we read from the file into substrings using tabs 
        // as the delimiter.

        string[] strings = line.Split('\t');

        // Now assign the value of the fields to the appropriate
        // substring

        string name = strings[0];
        string id = strings[1];
        string title = strings[2];
        string dept = strings[3];

        // Finally, we call the Professor constructor to create
        // an appropriate Professor object, and store it in our
        // collection.

        Professor p = new Professor(name, id, title, dept);
        AddProfessor(p);

        line = reader.ReadLine();
      }
    }  //  end of try block
    catch (FileNotFoundException f) {
      Console.WriteLine(f);
    }
    catch (IOException i) {
      Console.WriteLine(i);
    }
    finally {
      //  Close the input stream.
      if ( reader != null ) {
        reader.Close();
      }
    }

    return;
  }

  //*********************************************************************
  //
  public void ReadAssignmentData(ScheduleOfClasses scheduleOfClasses) {

  // We have to read a second file containing the teaching
  // assignments.

    StreamReader reader = null;

    try {
      // Open the file. 
      reader = new StreamReader(new FileStream(AssignmentsFile, FileMode.Open));

      string line = reader.ReadLine();
      while (line != null) {

        // Once again we'll make use of the Split() method to split
        // the line into substrings using tabs as the delimiter

        string[] strings = line.Split('\t');

        // Now assign the value of the fields to the appropriate
        // substring

        string id = strings[0];

        // The full section number is a concatenation of the
        // course no. and section no., separated by a hyphen;
        // e.g., "ART101 - 1".

        string fullSectionNumber = strings[1];

        // Look these two objects up in the appropriate collections
        // using the ScheduleOfClasses reference that was passed to 
        // this method.
  
        Professor p = FindProfessor(id); 
        Section s = scheduleOfClasses.FindSection(fullSectionNumber); 
        if (p != null && s != null) {
          p.AgreeToTeach(s);
        }

        line = reader.ReadLine();
      }
    }  //  end of try block
    catch (FileNotFoundException f) {
      Console.WriteLine(f);
    }
    catch (IOException i) {
      Console.WriteLine(i);
    }
    finally {
      //  Close the input stream.
      if ( reader != null ) {
        reader.Close();
      }
    }

    return;
  }

  //********************************************
  // Test scaffold.
  //
  //static void Main() {
  //  // We need a CourseCatalog object to test the Faculty class

  //  CourseCatalog catalog = new CourseCatalog("CourseCatalog.dat", "Prerequisites.dat");
  //  catalog.ReadCourseCatalogData();
  //  catalog.ReadPrerequisitesData();

  //  // We also need a ScheduleOfClasses object.

  //  ScheduleOfClasses schedule = new ScheduleOfClasses("SoC_SP2009.dat", "SP2009");
  //  schedule.ReadScheduleData(catalog);

  //  //  Create a Faculty object.

  //  Faculty faculty = new Faculty("Faculty.dat", "TeachingAssignments.dat");

  //  //  Read Faculty data from input file.

  //  faculty.ReadFacultyData();
  //  faculty.ReadAssignmentData(schedule);

  //  // Display information about the faculty.

  //  faculty.Display();
  //}

}
