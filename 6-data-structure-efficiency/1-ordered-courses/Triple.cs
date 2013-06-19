using System;
using System.Collections.Generic;

public class Triple : IComparable<Triple>
{

    public Triple(string course,
                string firstName,
                string lastName)
    {
        this.Course = course;
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public string Course { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public int CompareTo(Triple t2) 
    {
        int ret;
        
        if ((ret = this.Course.CompareTo(t2.Course)) != 0)
            return ret;
            
        if ((ret = this.FirstName.CompareTo(t2.FirstName)) != 0)
            return ret;  
            
        ret = this.LastName.CompareTo(t2.LastName);
            
        return ret;
    }

}