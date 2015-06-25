// TranscriptEntry.cs - Chapter 14 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;

public class TranscriptEntry {

  //----------------
  // Constructor(s).
  //----------------

  public TranscriptEntry(Student s, string grade, Section se) {
    Student = s;
    Section = se;
    Grade = grade;

    // Add the TranscriptEntry to the Student's Transcript.

    s.Transcript.AddTranscriptEntry(this);
  }

  //--------------------------------
  // Auto-implemented properties.
  //--------------------------------

  public Student Student { get; set; }
  public Section Section { get; set; }
  public string Grade { get; set; }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // These next two methods are declared to be static, so that they
  // may be used as utility methods.

  public static bool ValidateGrade(string grade) {
    bool outcome = false;

    string[] possibleGrades = {"A+", "A", "A-", "B+", "B", "B-", "C+",
                               "C", "C-", "D+", "D", "D-", "F", "I" };

    foreach ( string g in possibleGrades ) {
      if ( grade.Equals(g) ) {
        outcome = true;
        break;
      }
    }

    return outcome;
  }

  //*************************************************
  //
  public static bool PassingGrade(string grade) {
    // First, make sure it is a valid grade.

    if ( !ValidateGrade(grade) ) {
      return false;
    }

    // Next, make sure that the grade is a D- or better.

    if ( grade.StartsWith("A") || grade.StartsWith("B") ||
         grade.StartsWith("C") || grade.StartsWith("D")) {
      return true;
    } 
    else {
      return false;
    }
  }
}
