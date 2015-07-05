// Course.cs - Chapter 14 version.

// Copyright 2008 by Jacquie Barker  and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections.Generic;

public class Course {

  //----------------
  // Constructor(s).
  //----------------

  public Course(string cNo, string cName, double credits) {

    //  Initialize property values.

    CourseNumber = cNo;
    CourseName = cName;
    Credits = credits;

    // Create two empty Lists.

    OfferedAsSection = new List<Section>();
    Prerequisites = new List<Course>();
  }

  //-------------------------------
  // Auto-implemented properties.
  //-------------------------------

  public string CourseNumber { get; set; }
  public string CourseName { get; set; }
  public double Credits { get; set; }
  public List<Section> OfferedAsSection { get; set; }
  public List<Course> Prerequisites { get; set; }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // Used for testing purposes.

  public void Display() {
    Console.WriteLine("Course Information:");
    Console.WriteLine("\tCourse No.:  "+CourseNumber);
    Console.WriteLine("\tCourse Name:  "+CourseName);
    Console.WriteLine("\tCredits:  "+Credits);
    Console.WriteLine("\tPrerequisite Courses:");

    // We use a foreach statement to step through the prerequisites.

    foreach ( Course c in Prerequisites ) {
      Console.WriteLine("\t\t" + c.ToString());
    }

    // Note use of Write vs. WriteLine in next line of code.

    Console.Write("\tOffered As Section(s):  ");
    for(int i=0; i<OfferedAsSection.Count; i++) {
      Section s = (Section)OfferedAsSection[i];
      Console.Write(s.SectionNumber + " ");
    }

    // Print a blank line.
    Console.WriteLine("");
  }
	
  //**************************************
  //
  public override string ToString() {
    return CourseNumber + ":  "+CourseName;
  }

  //**************************************
  //
  public void AddPrerequisite(Course c) {
    int  result;//�ڶ���
      result = String.Compare(c.CourseName, this.CourseName);
      if (result != 0)
          Prerequisites.Add(c);
      else { Console.WriteLine(c+"���ܰ����Լ���Ϊ���޿γ�"); }
  }

  //**************************************
  //
  public bool HasPrerequisites() {
    if ( Prerequisites.Count > 0 ) {
      return true;
    }  
    else {
      return false;
    }
  }

  //******************************************************************
  public static int i = 0;
  public Section ScheduleSection(string day, string time, string room,
				       int capacity) {
    // Create a new Section (note the creative way in
    // which we are assigning a section number) ...
    Section s = new Section(i++, 
				day, time, this, room, capacity);
     //������ʹ�γ̵�section���ظ�
    // ... and then add it to the List
    OfferedAsSection.Add(s);
    return s;}
    //������
  public void CancelSection(Section sec)
    {
        int index = -1;
        if (OfferedAsSection.Contains(sec))
        {

            for (int i = 0; i < OfferedAsSection.Count; i++)
            {
                if (OfferedAsSection[i] == sec)
                {
                    //����ѡ��ȡ������
                    index = i;
                    break;
                }
            }
               OfferedAsSection.RemoveAt(index);
                Console.WriteLine(sec + "���ſγ��Ѿ��Ƴ���");}
           
            else
            {
                Console.WriteLine(sec + "���ſγ̲����ڣ�");
            }
     }


  
}
