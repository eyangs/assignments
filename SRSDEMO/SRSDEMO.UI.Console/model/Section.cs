// Section.cs - Chapter 14 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections.Generic;

public class Section {

  //----------------
  // Constructor(s).
  //----------------

  public Section(int sNo, string day, string time, Course course,
		 string room, int capacity) {
    SectionNumber = sNo;
    DayOfWeek = day;
    TimeOfDay = time;
    RepresentedCourse = course;
    Room = room;
    SeatingCapacity = capacity;

    // A Professor has not yet been identified.

    Instructor = null;

    // Create empty Dictionary objects.

    EnrolledStudents = new Dictionary<string, Student>();
    AssignedGrades = new Dictionary<Student, TranscriptEntry>();
  }
									
  //-------------------------------
  // Auto-implemented properties.
  //-------------------------------

  public int SectionNumber { get; set; }
  public string DayOfWeek { get; set; }
  public string TimeOfDay { get; set; }
  public Professor Instructor { get; set; }
  public Course RepresentedCourse { get; set; }
  public string Room { get; set; }
  public int SeatingCapacity { get; set; }
  public ScheduleOfClasses OfferedIn { get; set; }

  // The EnrolledStudents Dictionary stores Student object references,
  // using each Student's ID as a String key.

  public Dictionary<string, Student> EnrolledStudents { get; set; }

  // The assignedGrades Dictionary stores TranscriptEntry object
  // references, using a reference to the Student to whom it belongs 
  // as the key.

  public Dictionary<Student, TranscriptEntry> AssignedGrades { get; set; }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // For a Section, we want its String representation to look
  // as follows:
  //
  //	MATH101 - 1 - M - 8:00 AM

  public override string ToString() {
    return RepresentedCourse.CourseNumber+" - "+
           SectionNumber+" - "+DayOfWeek+" - "+
           TimeOfDay;
  }

  //***************************************************
  //
  // The full section number is a concatenation of the
  // course no. and section no., separated by a hyphen;
  // e.g., "ART101 - 1".

  public string GetFullSectionNumber() {
    return RepresentedCourse.CourseNumber+ 
           " - "+SectionNumber;
  }

  //**************************************
  //
  public EnrollFlags Enroll(Student s) {
    // First, make sure that this Student is not already
    // enrolled for this Section, has not already enrolled
    // in another section of this class and that he/she has
    // NEVER taken and passed the course before.  
		
    Transcript transcript = s.Transcript;

    if (s.IsEnrolledIn(this) || 
        s.IsCurrentlyEnrolledInSimilar(this) ||
        transcript.VerifyCompletion(RepresentedCourse)) {
      return EnrollFlags.PREVIOUSLY_ENROLLED;
    }

    // If there are any prerequisites for this course,
    // check to ensure that the Student has completed them.

    Course c = RepresentedCourse;
    if (c.HasPrerequisites()) {

      foreach ( Course pre in c.Prerequisites ) {
	
        // See if the Student's Transcript reflects
        // successful completion of the prerequisite.

        if (!transcript.VerifyCompletion(pre)) {
          return EnrollFlags.PREREQ_NOT_SATISFIED;
        }
      }
    }
		
    // If the total enrollment is already at the
    // the capacity for this Section, we reject this 
    // enrollment request.

    if (!ConfirmSeatAvailability()) {
      return EnrollFlags.SECTION_FULL;
    }
		
    // If we made it to here in the code, we're ready to
    // officially enroll the Student.

    // Note bidirectionality:  this Section holds
    // onto the Student via the Dictionary, and then
    // the Student is given an object reference to this Section.

    EnrolledStudents.Add(s.Id, s);
    s.AddSection(this);
    return EnrollFlags.SUCCESSFULLY_ENROLLED;
  }
	
  //**************************************
  // A private "housekeeping" method.
  //
  private bool ConfirmSeatAvailability() {
    if ( EnrolledStudents.Count < SeatingCapacity ) {
      return true;
    }  
    else {
      return false;
    }
  }

  //**************************************
  //
  public bool Drop(Student s) {
    // We may only drop a student if he/she is enrolled.

    if (!s.IsEnrolledIn(this)) {
      return false;
    }  
    else {
      // Find the student in our Dictionary, and remove it.

      EnrolledStudents.Remove(s.Id);

      // Note bidirectionality.

      s.DropSection(this);
      return true;
    }
  }

  //**************************************
  //
  public int GetTotalEnrollment() {
    return EnrolledStudents.Count;
  }	

  //**************************************
  //
  public void Display() {
    Console.WriteLine("Section Information:");
    Console.WriteLine("\tSemester:  "+OfferedIn.Semester);
    Console.WriteLine("\tCourse No.:  "+
                       RepresentedCourse.CourseNumber);
    Console.WriteLine("\tSection No:  "+SectionNumber);
    Console.WriteLine("\tOffered:  "+DayOfWeek + 
				   " at "+TimeOfDay);
    Console.WriteLine("\tIn Room:  "+Room);
    if ( Instructor != null ) {
      Console.WriteLine("\tProfessor:  "+Instructor.Name);
    }
    DisplayStudentRoster();
  }
	
  //**************************************
  //
  public void DisplayStudentRoster() {
    Console.Write("\tTotal of "+GetTotalEnrollment()+ 
                  " students enrolled");

    // How we punctuate the preceding message depends on 
    // how many students are enrolled (note that we used
    // a Write() vs. WriteLine() call above).

    if ( GetTotalEnrollment() == 0 ) {
      Console.WriteLine(".");
    }  
    else {
      Console.WriteLine(", as follows:");
    }

    // Use foreach loop to "walk through" the Dictionary.

    foreach ( KeyValuePair<string, Student> kv in EnrolledStudents ) {
      Student s = kv.Value;
      Console.WriteLine("\t\t"+s.Name);
    }
  }

  //**********************************************************
  // This method returns the value null if the Student has not
  // been assigned a grade.
  //
  public string GetGrade(Student s) {

    string grade = null; 

    //  Only continue if the AssignedGrades Dictionary
    //  contains the Student as a key.

    if ( AssignedGrades.ContainsKey(s) == true ) {

       // Retrieve the Student's transcript entry object, if one
       // exists, and in turn retrieve its assigned grade.
       // If we found no TranscriptEntry for this Student, return
       // a null value to signal this.

       TranscriptEntry te = AssignedGrades[s];
       if (te != null) {
         grade = te.Grade; 
       }  
    }

    return grade;
  }

  //************************************************
  //
  public bool PostGrade(Student s, string grade) {

    // Make sure that we haven't previously assigned a
    // grade to this Student by looking in the Dictionary
    // for an entry using this Student as the key.  If
    // we discover that a grade has already been assigned,
    // we return a value of false to indicate that
    // we are at risk of overwriting an existing grade.  
    // (A different method, EraseGrade(), can then be written
    // to allow a Professor to change his/her mind.)

    if ( AssignedGrades.ContainsKey(s) == true ) {
      return false;
    } 

    // First, we create a new TranscriptEntry object.  Note
    // that we are passing in a reference to THIS Section,
    // because we want the TranscriptEntry object,
    // as an association class ..., to maintain object
    // references on the Section as well as on the Student.
    // (We'll let the TranscriptEntry constructor take care of
    // "hooking" this T.E. to the correct Transcript.)

    TranscriptEntry te = new TranscriptEntry(s, grade, this);

    // Then, we add the TranscriptEntry and its associated
    // Student to the AssignedGrades Dictionary.

    AssignedGrades.Add(s, te);

    return true;
  }
	
  //**************************************
  //
  public bool IsSectionOf(Course c) {
    if (c == RepresentedCourse) {
      return true;
    } 
    else {
      return false;
    }
  }
}
