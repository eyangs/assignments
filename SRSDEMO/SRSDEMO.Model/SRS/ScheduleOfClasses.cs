// ScheduleOfClasses.cs - Chapter 16 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections.Generic;
using System.IO;

public class ScheduleOfClasses {

  //----------------
  // Constructor(s).
  //----------------

  //  Modify the 1-parameter constructor so it calls the new
  //  2-parameter constructor with a dummy ScheduleFile values.
  public ScheduleOfClasses(string semester) : this("", semester) {
  }

  public ScheduleOfClasses(string scheduleFile, string semester) {
    ScheduleFile = scheduleFile;
    Semester = semester;
		
    // Instantiate a new Dictionary.

    SectionsOffered = new Dictionary<string, Section>();
  }

  //-------------------------------
  // Auto-implemented properties.
  //-------------------------------

  public string Semester { get; set; }
  public string ScheduleFile { get; set; }

  // This Dictionary stores Section object references, using
  // a String concatenation of course no. and section no. as the
  // key, e.g., "MATH101 - 1".

  public Dictionary<string, Section> SectionsOffered { get; set; }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  //**************************************
  //	
  public void Display() {
    Console.WriteLine("Schedule of Classes for "+this.Semester);
    Console.WriteLine("");

    // Step through the Dictionary and display all entries.

    foreach ( KeyValuePair<string, Section> kv in SectionsOffered ) {
      Console.WriteLine("Key = \""+kv.Key+"\"");
      Section s = kv.Value;
      s.Display();
      Console.WriteLine("");
    }
  }

  //************************************
  //
  public void AddSection(Section s) {
    // We formulate a key by concatenating the course no.
    // and section no., separated by a hyphen.

    string key = s.RepresentedCourse.CourseNumber+ 
                 " - "+s.SectionNumber;
    SectionsOffered.Add(key, s);

    // Bidirectionally connect the ScheduleOfClasses back to the Section.

    s.OfferedIn = this;
  }

  //*****************************************************
  // The full section number is a concatenation of the
  // course no. and section no., separated by a hyphen;
  // e.g., "ART101 - 1".
  //
  public Section FindSection(string fullSectionNumber) {
    return SectionsOffered[fullSectionNumber];
  }

  //********************************************************
  // Convert the contents of the SectionsOffered Dictionary
  // into a List of Section objects that is sorted in
  // alphabetical order
  //
  public List<Section> GetSortedSections() {
    List<string> sortedKeys = new List<string>();
    List<Section> sortedSections = new List<Section>();

    // Load the sortedKeys List with the keys from the Dictionary
    foreach ( KeyValuePair<string, Section> kv in SectionsOffered ) {
      string key = kv.Key;
      sortedKeys.Add(key);
    }

    // Sort the keys in the List alphabetically
    sortedKeys.Sort();

    // Load the value corresponding to the sorted keys into
    // the sortedSections List
    foreach( string key in sortedKeys ) {
      Section s = SectionsOffered[key];
      sortedSections.Add(s);
    }

    // Return the List containing the sorted Sections
    return sortedSections;
  }

  //**************************************************************
  //
  public void ReadScheduleData(CourseCatalog courseCatalog) {
    // We're going to parse tab-delimited records into
    // six fields - courseNumber, sectionNumber, dayOfWeek, 
    // timeOfDay, room, and capacity.  We'll use courseNumber to 
    // look up the appropriate Course object, and will then
    // call the ScheduleSection() method to fabricate a
    // new Section object.

    StreamReader reader = null;

    try {
      // Open the file. 
      reader = new StreamReader(new FileStream(ScheduleFile, FileMode.Open));

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
        string sectionValue = strings[1];
        string dayOfWeek = strings[2];
        string timeOfDay = strings[3];
        string room = strings[4];
        string capacityValue = strings[5];

        // We need to convert the sectionNumber and capacityValue
        // Strings to ints

        int sectionNumber = Convert.ToInt32(sectionValue);
        int capacity = Convert.ToInt32(capacityValue);

        // Look up the Course object in the Course Catalog.

        Course c =  courseCatalog.FindCourse(courseNumber);

        // Schedule the Section and add it to the Dictionary.

        Section s = c.ScheduleSection(sectionNumber, dayOfWeek, 
                              timeOfDay, room, capacity);
        AddSection(s);

        line = reader.ReadLine();
      }
    }  //  End of try block
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
  static void Main() {

    // We need a CourseCatalog object to test the ScheduleOfClasses
    CourseCatalog catalog = new CourseCatalog("CourseCatalog.dat", "Prerequisites.dat");
    catalog.ReadCourseCatalogData();
    catalog.ReadPrerequisitesData();

    // Create a ScheduleOfClasses object ...
    ScheduleOfClasses schedule = new ScheduleOfClasses("SoC_SP2009.dat", "SP2009");

    //  Read data from the input file ...
    schedule.ReadScheduleData(catalog);

    // ... and use its Display() method to list the ScheduleOfClasses
    // results!

    schedule.Display();
  }
}
