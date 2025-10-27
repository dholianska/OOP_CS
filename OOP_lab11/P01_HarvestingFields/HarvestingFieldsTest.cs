 namespace P01_HarvestingFields
{
    using System;
    using System.Reflection;

    public class HarvestingFieldsTest
    {
        public static void Main()
        {
            Type type = typeof(HarvestingFields);

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public
                                        | BindingFlags.NonPublic);

            string input;
            while ((input = Console.ReadLine()) != "HARVEST")
            {
                FieldInfo[] filtered = input switch
                {
                    "private" => Array.FindAll(fields, f => f.IsPrivate),
                    "public" => Array.FindAll(fields, f => f.IsPublic),
                    "protected" => Array.FindAll(fields, f => f.IsFamily),
                    "all" => fields,
                    _ => Array.Empty<FieldInfo>()
                };

                foreach (var field in filtered)
                {
                    string accessModifier;
                    if (field.IsPublic) accessModifier = "public";
                    else if (field.IsFamily) accessModifier = "protected";
                    else accessModifier = "private";

                    Console.WriteLine($"{accessModifier} {field.FieldType.Name} {field.Name}");
                }
                
            }
        }
    }
}
