// Transcript.cs - Chapter 14 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections.Generic;

public class Transcript {

  //----------------
  // Constructor(s).
  //----------------

  public Transcript(Student s) {
    StudentOwner = s;

    // Create an empty List.

    TranscriptEntries = new List<TranscriptEntry>();
  }

  //-----------------------------
  // Auto-implemented property.
  //-----------------------------

  public Student StudentOwner { get; set; }
  public List<TranscriptEntry> TranscriptEntries { get; set; }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  public bool VerifyCompletion(Course c) {
    bool outcome = false;

    // Step through all TranscriptEntries, looking for one
    // which reflects a Section of the Course of interest.

    foreach ( TranscriptEntry te in TranscriptEntries ) {
      Section s = te.Section;

      if ( s.IsSectionOf(c) ) {
        // Ensure that the grade was high enough.

        if ( TranscriptEntry.PassingGrade(te.Grade) ) {
          outcome = true;

          // We've found one, so we can afford to
          // terminate the loop now.

          break;
        }
      }
    }

    return outcome;
  }

  //****************************************************
  //
  public void AddTranscriptEntry(TranscriptEntry te) {
    TranscriptEntries.Add(te);
  }

  //**************************************
  //
  public void Display() {
    Console.WriteLine("Transcript for:  " +
                       this.StudentOwner.ToString());

    if ( TranscriptEntries.Count == 0 ) {
      Console.WriteLine("\t(no entries)");
    }  
    else {
      foreach ( TranscriptEntry te in TranscriptEntries ) {
        Section sec = te.Section;
        Course c = sec.RepresentedCourse;
        ScheduleOfClasses soc = sec.OfferedIn;

        Console.WriteLine("\tSemester:        "+soc.Semester);
        Console.WriteLine("\tCourse No.:      "+c.CourseNumber);
        Console.WriteLine("\tCredits:         "+c.Credits);
        Console.WriteLine("\tGrade Received:  "+te.Grade);
        Console.WriteLine("\t-----");
      }
    }
  }
}
