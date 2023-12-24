using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AnimalLibrary;

public class Lab07
{
    static IEnumerable<Type> GetClasses(string nameSpace)
    {
        var asm = Assembly.Load(nameSpace);
        return asm.GetTypes()
                  .Where(type => type.Namespace == nameSpace);
    }

    public static void Main(string[] args)
    {
        using (StreamWriter fout = new StreamWriter(@"\\Users\\mariaseludakova\\Desktop\\ConsoleApplication1\\MyLibrary.xml"))
        {
            fout.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            fout.WriteLine("<Library>");
            fout.WriteLine("<name>ClassLibrary1</name>");

            foreach (var myType in GetClasses("AnimalLibrary"))
            {
                Console.WriteLine("typename=" + myType.Name + ":");
                Console.WriteLine("abstract=" + myType.IsAbstract + ";");
                Console.WriteLine("public=" + myType.IsPublic + ";");
                Console.WriteLine("basetype=" + myType.BaseType.Name + ";");
                fout.WriteLine("\t<type>");
                fout.WriteLine("\t\t<name>" + myType.Name + "</name>");

                if (myType.IsPublic || myType.IsAbstract)
                {
                    fout.Write("\t\t<modifiers>");
                    if (myType.IsPublic) fout.Write("public ");
                    if (myType.IsAbstract) fout.Write("abstract");

                    fout.WriteLine("</modifiers>");
                }
                fout.WriteLine("\t\t<basetype>" + myType.BaseType.Name + "</basetype>");

                foreach (MemberInfo member in myType.GetMembers(BindingFlags.DeclaredOnly
                    | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static))
                {
                    fout.WriteLine("\t\t<member>");
                    fout.WriteLine("\t\t\t<name>" + member.Name + "</name>");
                    fout.WriteLine("\t<membertype>" + member.MemberType + "</membertype>");
                    Console.WriteLine("\tname=" + member.Name);
                    Console.WriteLine("\tmembertype=" + member.MemberType);

                    if (member.MemberType == MemberTypes.Field)
                    {
                        FieldInfo field = myType.GetField(member.Name);
                        if (field != null)
                        {
                            Type fieldType = field.FieldType;
                            Console.WriteLine("\t\ttype=" + fieldType.Name);
                            fout.WriteLine("\t\t\t<fieldtype>" + fieldType.Name + "</fieldtype>");
                        }
                    }

                    if (member.MemberType == MemberTypes.Property)
                    {
                        PropertyInfo prop = myType.GetProperty(member.Name);
                        if (prop != null)
                        {
                            Type propertyType = prop.PropertyType;
                            Console.WriteLine("\t\ttype=" + propertyType.Name);
                            fout.WriteLine("\t\t\t<propertytype>" + propertyType.Name + "</propertytype>");
                        }
                    }

                    fout.WriteLine("\t\t</member>");
                }
                Console.WriteLine();
                fout.WriteLine("\t</type>");
            }
            fout.WriteLine("</Library>");
        }
    }
}
