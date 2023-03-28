namespace Ef_LINQ_Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var collection = new List<Student>()
            {
                new Student {Name = "Bobi", Marks = new List<double> {5.0, 5.5, 6.0, 5.25, 6.0} },
                new Student {Name = "Pe6i", Marks = new List<double> {4.0, 4.5, 5.0, 4.0, 4.75} },



            };


            // collection2 is IEnumerable => it can be foreached / not IQueryable
             // methods of LINQ WHERE
             // return new collection

                                                // predicate -> function return bool
            var collection2 = collection.Where(s => s.Name == "Bobi");

            foreach (var student in collection2)
            {
                Console.WriteLine($"{student.Name} with marks: {string.Join(", ", student.Marks)}");
            }

            // predicate
            var collectionMarksAvgLessThan5 = collection
            /*Filtering -> reduce rows, collection*/.Where(x => x.Marks.Average() < 5.0)
            /*Projection -> change type*/.Select( s => new StudentProjection 
                                         {
                                             Name = s.Name,
                                             NameInitial = s.Name.Substring(0, 1),
                                             AverageMarks = s.Marks.Average(),
                                         })
                                         .OrderBy(sp =>  sp.AverageMarks);

            foreach (var student in collectionMarksAvgLessThan5)
            {
                Console.WriteLine($"{student.Name} with marks: {string.Join(", ", student.AverageMarks)}");

            }

            // WITH .SELECT =>
            // 1. (+) Navigational Properies Access in the lambda expressons
            // 2. (+) Get Only the columns we need
            // 3. (-) no update and delete


            //NO .SELECT =>
            // 1. (-) No access to navigational properties
            // 2. (-) Get all columns for entity
            // 3. (+) update / delete entity / SaveChanges

            // ALWAYS USE .SELECT 
            // EXCEPT WHEN WANT TO GET ENTITY AND UPDATE OR DELETE IT




            static bool Predicate (Student student)
            {
                return student.Marks.Average() < 5.0;

                //if (student.Marks.Average() < 5)
                //{
                //    return true;
                //}
                //else              
                //{
                //    return false;
                //}

            }
                                                                   // x => x.Marks.Average() < 5.0;
            static bool PredicateWithAnonymousFunction(Student student) => student.Marks.Average() < 5.0; 

        }


        public class StudentProjection
        {
            public string Name { get; set; }
            public string NameInitial { get; set; }
            public double AverageMarks { get; set; }
        }


        public class Student
        {

           

            public string Name  { get; set; }



            public List<double> Marks { get; set; }


        }
    }
}