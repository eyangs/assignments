// ScheduleOfClasses.cs - Chapter 14 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections.Generic;

public class ScheduleOfClasses {

  //----------------
  // Constructor(s).
  //----------------

  public ScheduleOfClasses(string semester) {
    Semester = semester;
		
    // Create a new Dictionary.

    SectionsOffered = new Dictionary<string, Section>();
  }

  //-------------------------------
  // Auto-implemented properties.
  //-------------------------------

  public string Semester { get; set; }

  // This Dictionary stores Section object references, using
  // a String concatenation of course no. and section no. as the
  // key, e.g., "MATH101 - 1".

  public Dictionary<string, Section> SectionsOffered { get; set; }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // Used for testing purposes.
	
  public void Display() {
    Console.WriteLine("Schedule of Classes for "+this.Semester);
    Console.WriteLine("");

    // Step through the Dictionary and display all entries.

    foreach ( KeyValuePair<string, Section> kv in SectionsOffered ) {
      Section s = kv.Value;
      s.Display();
      Console.WriteLine("");
    }
  }

  //**************************************
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
}
