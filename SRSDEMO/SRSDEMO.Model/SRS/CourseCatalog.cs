// CourseCatalog.cs - Chapter 16 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// An IMPLEMENTATION class.

using System;
using System.Collections.Generic;
using System.IO;

public class CourseCatalog {

  //----------------
  // Constructor(s).
  //----------------

  public CourseCatalog(string courseCatalogFile, string prerequisitesFile) {
    
    //  Initialize auto-implemented property values
    CourseCatalogFile = courseCatalogFile;
    PrerequisitesFile = prerequisitesFile;

    // Create a new Dictionary.
    Courses = new Dictionary<string, Course>();

  }

  //-------------------------------
  // Auto-implemented properties.
  //-------------------------------

  public string CourseCatalogFile { get; set; }
  public string PrerequisitesFile { get; set; }
  public Dictionary<string, Course> Courses { get; set; }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  //**********************
  public void Display() {
    Console.WriteLine("Course Catalog:");
    Console.WriteLine("");

    // Use a foreach statement to step through the Dictionary.
    foreach ( KeyValuePair<string, Course> kv in Courses ) {
      Course c = kv.Value;
      c.Display();
      Console.WriteLine("");
    }
  }

  //*********************************
  //
  public void AddCourse(Course c) {
    Courses.Add(c.CourseNumber, c);
  }

  //********************************************
  //
  public Course FindCourse(string courseNumber) {
    return Courses[courseNumber];
  }

  //*************************************
  //
  public void ReadCourseCatalogData() {
    // We're going to parse tab-delimited records into
    // three elements -- courseNumber, courseName, and credits --
    // and then create a new Course object using these values.

    StreamReader reader = null;

    try {
      // Open the file. 
      reader = new StreamReader(new FileStream(CourseCatalogFile, FileMode.Open));

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

        string courseNumber = strings[0];
        string courseName = strings[1];
        string creditValue = strings[2];

        // We have to convert the last value into a number,
        // using a static method on the Double class to do so.

        double credits = Convert.ToDouble(creditValue);

        // We create a Course object, and store it in our
        // collection.

        Course c = new Course(courseNumber, courseName, credits);
        AddCourse(c);

        //  Read the next line of data (if any)
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

  //************************************************************
  // We must read a second file defining the prerequisites, in
  // order to "hook" Course objects together.
  // This next version is used when reading in prerequisites.
  //
  public void ReadPrerequisitesData() {

    // We're going to parse tab-delimited records into
    // two values, representing the courseNo "A" of 
    // a course that serves as a prerequisite for 
    // courseNo "B".

    StreamReader reader = null;

    try {
      // Open the file. 
      reader = new StreamReader(new FileStream(PrerequisitesFile, FileMode.Open));

      //  Read first line from input file.
      string line = reader.ReadLine();

      //  Keep reading lines until there aren't any more.
      while (line != null) {

        // Once again we'll make use of the Split() method to split
        // the line into substrings using tabs as the delimiter

        string[] strings = line.Split('\t');

        // Now assign the value of the fields to the appropriate
        // substring

        string courseNoA = strings[0];
        string courseNoB = strings[1];

        // Look these two courses up in the CourseCatalog.

        Course a = FindCourse(courseNoA); 
        Course b = FindCourse(courseNoB); 
        if (a != null && b != null) {
          b.AddPrerequisite(a);
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

  //******************************************
  // Test scaffold.
  //
  //static void Main() {

  //  // We create a CourseCatalog object ...

  //  CourseCatalog catalog = new CourseCatalog("CourseCatalog.dat", "Prerequisites.dat");

  //  //  Read data from input files
  //  catalog.ReadCourseCatalogData();
  //  catalog.ReadPrerequisitesData();

  //  // ... and use its Display() method to demonstrate the
  //  // results!

  //  catalog.Display();
  //}
}
