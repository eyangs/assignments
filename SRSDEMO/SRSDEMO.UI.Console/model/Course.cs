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
      //�ڶ��⣬�Ľ����޿γ̷�������ֹ���Լ���Ϊ����
     //�жϿγ����Ƿ����
      if (string.Equals(c.CourseName, this.CourseName))
      {
          //�γ�����ͬ ��Ϊͬһ�ſγ� �������
          Console.WriteLine("���ܽ�������Ϊ���޿γ�");          
      }
      else 
      {
          //����������
          Prerequisites.Add(c);
      }

    //Prerequisites.Add(c);
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
  //������ ����ScheduleOfSection�����Ĵ����߼���ʹ֮�������ظ��Ŀγ̱��
  //����һ����̬�ֶα�ʾѡ�α��
  public static int NOOfSection=1;
  public Section ScheduleSection(string day, string time, string room,
				       int capacity) {
    // Create a new Section (note the creative way in
    // which we are assigning a section number) ...

      //�µ�ѡ�����±�ʾ��
       Section s = new Section(NOOfSection++, 
                day, time, this, room, capacity);

    //Section s = new Section(OfferedAsSection.Count + 1, 
    //            day, time, this, room, capacity);
		
    // ... and then add it to the List
    OfferedAsSection.Add(s);
		
    return s;
  }
    //������ΪCourseʵ��һ��CancelSection����
    //����ScheduleOfSection�����Ĵ����߼���ʹ֮�������ظ��Ŀγ̱��
  public void  CancelSection(Section sec)
  {
      Console.WriteLine("ɾ��ǰ�Ľ��Ϊ��");
      Console.WriteLine("------------------------");
      for (int i = 0; i < OfferedAsSection.Count; i++)
      {
          Console.WriteLine(OfferedAsSection[i]);
      }
      Console.WriteLine("-----------------------");
      //�Ƴ�ѡ��s 
      //��������
      int suoyin = 255;
      for (int i = 0; i < OfferedAsSection.Count;i++ )
      {
          if (OfferedAsSection[i]==sec)
          {
              //����ѡ��ȡ������
              suoyin = i;
              break;
          }
      }
      if (suoyin != 255)
      {
          OfferedAsSection.RemoveAt(suoyin);          
          Console.WriteLine(sec +"���ſγ��Ѿ��Ƴ���");
      }
      else 
      {
          Console.WriteLine("�Ѿ�������" + sec + "���ſγ̣�");
      }
      Console.WriteLine("ɾ����Ľ��Ϊ��");
      Console.WriteLine("------------------------");
      for (int i = 0; i < OfferedAsSection.Count; i++)
      {
          Console.WriteLine(OfferedAsSection[i]);
      }
      Console.WriteLine( "-----------------------");

 
      

      //bool successed = OfferedAsSection.Remove(sec);
      //Console.WriteLine(successed );
      //Console.WriteLine(sec +"���ſγ��Ѿ��Ƴ���");
      
  }



}
