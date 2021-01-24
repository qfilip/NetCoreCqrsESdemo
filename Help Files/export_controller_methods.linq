<Query Kind="Program" />

/* 
	Implement in FluentConfigurations,
	and export to constants file with resolved
	api controllers endpoint names.
	Maybe in several files, each for one controller
	(Just generate with File.WriteAllLines())
*/

static void Main(string[] args)
{
    var types = FindSubClassesOf<BaseController>();
    var objMethods = typeof(Object)
        .GetMethods()
        .Select(x => x.Name)
        .ToList();

    objMethods.ForEach(x => Console.WriteLine(x));
    Console.WriteLine();
    foreach(var t in types)
    {
        Console.WriteLine(t.Name);
        var methodNames = t.GetMethods()
            .Select(x => x.Name)
            .Where(x => !objMethods.Contains(x))
            .ToList();

        // concatinate string:
		// $"export const {t}_something = '{methodName}'";
    }
}
    
public static IEnumerable<Type> FindSubClassesOf<TBaseType>() where TBaseType : class
{
    var baseType = typeof(TBaseType);
    var assembly = baseType.Assembly;

    return assembly.GetTypes().Where(t => t.IsSubclassOf(baseType));
}


public class BaseController
{

}

public class CocktailController : BaseController
{
    public void GetAll() { }
    public void GetById() { }
}

public class IngredientController : BaseController
{
    public void GetAll() { }
    public void GetById() { }
}